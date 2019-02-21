using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi
{
    public class StartupHostedServiceHealthCheck : IHealthCheck
    {
        public string Name => "slow_dependency_check";

        public bool StartupTaskCompleted { get; set; } = false;

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (StartupTaskCompleted)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("The startup task is finished."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("The startup task is still running."));
        }
    }
}
