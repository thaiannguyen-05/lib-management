using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WinFormsApp1.Services
{
    public class ReservationFulfillmentService : BackgroundService
    {
        private readonly ILogger<ReservationFulfillmentService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public ReservationFulfillmentService(
            ILogger<ReservationFulfillmentService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReservationFulfillmentService starting. Interval: {Interval}", _interval);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var reservationService = scope.ServiceProvider.GetRequiredService<ReservationService>();

                    var expired = await reservationService.CheckAndExpireAsync();
                    var fulfilled = await reservationService.AutoFulfillAllAsync();

                    if (expired > 0 || fulfilled > 0)
                        _logger.LogInformation("Tick complete: {Expired} expired, {Fulfilled} fulfilled", expired, fulfilled);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during reservation fulfillment tick");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("ReservationFulfillmentService stopping");
        }
    }
}
