using Microsoft.AspNetCore.SignalR;

namespace SmartInventoryBE.Hubs
{
    public class InventoryHub : Hub
    {
        public async Task UpdateInventory(string productId, int quantity)
        {
            await Clients.All.SendAsync("ReceiveInventoryUpdate", productId, quantity);
        }

        public async Task NotifyLowStock(string productId, int currentStock)
        {
            await Clients.All.SendAsync("ReceiveLowStockAlert", productId, currentStock);
        }

        public async Task NotifyOrderStatus(string orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", orderId, status);
        }

        public async Task NotifyCategoryUpdate(string categoryId, string newName)
        {
            await Clients.All.SendAsync("ReceiveCategoryUpdate", categoryId, newName);
        }
    }
}
