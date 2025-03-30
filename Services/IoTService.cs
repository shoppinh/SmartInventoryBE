using Microsoft.Azure.Devices.Client;
using System.Text;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace SmartInventoryBE.Services
{
    public class IoTService : IIoTService
    {
        private readonly DeviceClient _deviceClient;
        private readonly ILogger<IoTService> _logger;
        private readonly IHubContext<InventoryHub> _hubContext;

        public IoTService(IConfiguration configuration, ILogger<IoTService> logger, 
            IHubContext<InventoryHub> hubContext)
        {
            var connectionString = configuration["IoTHub:ConnectionString"];
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task SendTelemetryAsync(string deviceId, object telemetryData)
        {
            var messageString = JsonSerializer.Serialize(telemetryData);
            var message = new Message(Encoding.UTF8.GetBytes(messageString));
            await _deviceClient.SendEventAsync(message);
            
            await _hubContext.Clients.All.SendAsync("ReceiveIoTData", deviceId, telemetryData);
        }

        public async Task ProcessDeviceToCloudMessagesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await _deviceClient.ReceiveAsync();
                if (message == null) continue;

                var messageData = Encoding.UTF8.GetString(message.GetBytes());
                _logger.LogInformation($"Received message: {messageData}");
                
                await _deviceClient.CompleteAsync(message);
            }
        }
    }
}
