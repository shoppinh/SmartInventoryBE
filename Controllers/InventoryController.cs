using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ApiControllerBase
    {
        private readonly SmartInventoryContext _context;
        private readonly IHubContext<InventoryHub> _hubContext;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<InventoryController> _logger;
        private readonly IMapper _mapper;


        public InventoryController(
            SmartInventoryContext context,
            IHubContext<InventoryHub> hubContext,
            IProductRepository productRepository,
            ILogger<InventoryController> logger,
            IInventoryRepository inventoryRepository,
            IWorkContextService workContextService,
            IMapper mapper)
            : base(workContextService)
        {
            _context = context;
            _hubContext = hubContext;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<InventoryResponse>>> GetInventories()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return CreateSuccessResponse(_mapper.Map<IEnumerable<InventoryResponse>>(inventories), nameof(ApiResponseMessageConstant.Inventory_GetSuccess), ApiResponseMessageConstant.Inventory_GetSuccess);
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<InventoryResponse>> GetInventory(int id)
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Inventory_GetNotFound), ApiResponseMessageConstant.Inventory_GetNotFound);
            }

            return CreateSuccessResponse(_mapper.Map<InventoryResponse>(inventory), nameof(ApiResponseMessageConstant.Inventory_GetSuccess), ApiResponseMessageConstant.Inventory_GetSuccess);
        }

        // PUT: api/Inventory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ApiResponse<InventoryResponse>> PutInventory(int id, UpdateInventoryRequest inventoryRequest)
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Inventory_GetNotFound), ApiResponseMessageConstant.Inventory_GetNotFound);
            }

            _mapper.Map(inventoryRequest, inventory);

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", inventory.Id, inventory.Quantity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Inventory_GetNotFound), ApiResponseMessageConstant.Inventory_GetNotFound);
                }

                throw;
            }

            return CreateSuccessResponse(_mapper.Map<InventoryResponse>(inventory), nameof(ApiResponseMessageConstant.Inventory_UpdateSuccess), ApiResponseMessageConstant.Inventory_UpdateSuccess);
        }

        // POST: api/Inventory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<InventoryResponse>> PostInventory(CreateInventoryRequest inventoryRequest)
        {

            var existedInventory = await _inventoryRepository.FirstOrDefaultAsync((item) => item.ProductId == inventoryRequest.ProductId);
            if (existedInventory != null)
            {
                return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Inventory_GetNotFound), ApiResponseMessageConstant.Inventory_GetNotFound);
            }

            var product = await _productRepository.GetByIdAsync(inventoryRequest.ProductId, ["Category"]);
            if (product == null)
            {
                return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Product_GetNotFound), ApiResponseMessageConstant.Product_GetNotFound);
            }

            var inventory = _mapper.Map<Inventory>(inventoryRequest);
            inventory.Product = product;
            inventory.ProductId = product.Id;

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreateSuccessResponse(_mapper.Map<InventoryResponse>(inventory), nameof(ApiResponseMessageConstant.Inventory_AddSuccess), ApiResponseMessageConstant.Inventory_AddSuccess);
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return CreateResponse<InventoryResponse>(false, null, nameof(ApiResponseMessageConstant.Inventory_GetNotFound), ApiResponseMessageConstant.Inventory_GetNotFound);
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return CreateSuccessResponse(nameof(ApiResponseMessageConstant.Inventory_DeleteSuccess), ApiResponseMessageConstant.Inventory_DeleteSuccess);
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
