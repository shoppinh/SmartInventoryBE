using System;

namespace SmartInventoryBE.Interfaces.Services;

public interface IIoTService
{
    public Task SendTelemetryAsync(string deviceId, object telemetryData);
    public Task ProcessDeviceToCloudMessagesAsync(CancellationToken cancellationToken);

}
