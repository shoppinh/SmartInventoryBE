using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Smart_Inventory_BE.Controllers.Base;
using SmartInventoryBE.Extensions;
using SmartInventoryBE.Hubs;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;
using WeSpace.Core.ProjectAggregate.Constants;
using WeSpace.Core.ProjectAggregate.ViewModels.Response;

namespace SmartInventoryBE.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ApiControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHubContext<InventoryHub> _hubContext;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IHubContext<InventoryHub> hubContext, IMapper mapper, IWorkContextService workContextService)
            : base(workContextService)
        {
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return CreateSuccessResponse(_mapper.Map<IEnumerable<CategoryResponse>>(categories), nameof(ApiResponseMessageConstant.Category_GetSuccess), ApiResponseMessageConstant.Category_GetSuccess);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<CategoryResponse>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return CreateResponse<CategoryResponse>(false, null, nameof(ApiResponseMessageConstant.Category_GetNotFound), ApiResponseMessageConstant.Category_GetNotFound);
            }

            return CreateSuccessResponse(_mapper.Map<CategoryResponse>(category), nameof(ApiResponseMessageConstant.Category_GetSuccess), ApiResponseMessageConstant.Category_GetSuccess);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ApiResponse<CategoryResponse>> PutCategory(int id, UpdateCategoryRequest categoryRequest)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return CreateResponse<CategoryResponse>(false, null, nameof(ApiResponseMessageConstant.Category_GetNotFound), ApiResponseMessageConstant.Category_GetNotFound);
            }

            _mapper.Map(categoryRequest, category);

            await _categoryRepository.UpdateAsync(category);
            await _hubContext.Clients.All.SendAsync("ReceiveCategoryUpdate", category.Id, category.Name);
            return CreateSuccessResponse(_mapper.Map<CategoryResponse>(category), nameof(ApiResponseMessageConstant.Category_UpdateSuccess), ApiResponseMessageConstant.Category_UpdateSuccess);
        }

        // POST: api/Categories
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<CategoryResponse>> PostCategory(CreateCategoryRequest createCategoryRequest)
        {
            var category = _mapper.Map<Category>(createCategoryRequest);

            await _categoryRepository.InsertAsync(category);

            return CreateSuccessResponse(_mapper.Map<CategoryResponse>(category), nameof(ApiResponseMessageConstant.Category_AddSuccess), ApiResponseMessageConstant.Category_AddSuccess);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return CreateResponse<CategoryResponse>(false, null, nameof(ApiResponseMessageConstant.Category_GetNotFound), ApiResponseMessageConstant.Category_GetNotFound);
            }

            await _categoryRepository.DeleteAsync(category);

            return CreateSuccessResponse(nameof(ApiResponseMessageConstant.Category_DeleteSuccess), ApiResponseMessageConstant.Category_DeleteSuccess);
        }
    }
}
