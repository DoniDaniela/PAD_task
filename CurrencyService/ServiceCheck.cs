using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CurrencyService
{
    public class ServiceCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // All is well!
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
