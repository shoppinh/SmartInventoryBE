using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Inventory_BE.Controllers.Base;
using SmartInventoryBE.Extensions;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;
using WeSpace.Core.ProjectAggregate.Constants;
using WeSpace.Core.ProjectAggregate.ViewModels.Response;

namespace SmartInventoryBE.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        private readonly SmartInventoryContext _context;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(SmartInventoryContext context, ICategoryRepository categoryRepo, IMapper mapper, IWorkContextService workContextService)
            : base(workContextService)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ProductResponse>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return CreateSuccessResponse(_mapper.Map<IEnumerable<ProductResponse>>(products), nameof(ApiResponseMessageConstant.Product_GetSuccess), ApiResponseMessageConstant.Product_GetSuccess);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductResponse>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return CreateResponse<ProductResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
            }

            return CreateSuccessResponse(_mapper.Map<ProductResponse>(product), nameof(ApiResponseMessageConstant.Product_GetSuccess), ApiResponseMessageConstant.Product_GetSuccess);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ApiResponse<ProductResponse>> PutProduct([FromRoute] int id, [FromBody] UpdateProductRequest productRequest)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return CreateResponse<ProductResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
            }

            try
            {
                _mapper.Map(productRequest, product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return CreateResponse<ProductResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
                }
                else
                {
                    throw;
                }
            }

            return CreateSuccessResponse(_mapper.Map<ProductResponse>(product), nameof(ApiResponseMessageConstant.Product_UpdateSuccess), ApiResponseMessageConstant.Product_UpdateSuccess);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<ProductResponse>> PostProduct(CreateProductRequest productRequest)
        {
            var category = await _categoryRepo.GetByIdAsync(productRequest.CategoryId);
            if (category == null)
            {
                return CreateResponse<ProductResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
            }
            
            var product = _mapper.Map<Product>(productRequest);
            product.Category = category;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreateSuccessResponse(_mapper.Map<ProductResponse>(product), nameof(ApiResponseMessageConstant.Product_AddSuccess), ApiResponseMessageConstant.Product_AddSuccess);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return CreateResponse<ProductResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return CreateSuccessResponse(nameof(ApiResponseMessageConstant.Product_DeleteSuccess), ApiResponseMessageConstant.Product_DeleteSuccess);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
