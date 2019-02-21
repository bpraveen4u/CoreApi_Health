using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi
{
    public class ReadinessPublisher : IHealthCheckPublisher
    {
        private readonly ILogger _logger;

        public ReadinessPublisher(ILogger<ReadinessPublisher> logger)
        {
            _logger = logger;
        }

        public List<(HealthReport report, CancellationToken cancellationToken)>
            Entries
        { get; } =
                new List<(HealthReport report,
                    CancellationToken cancellationToken)>();

        public Exception Exception { get; set; }

        public Task PublishAsync(HealthReport report,
            CancellationToken cancellationToken)
        {
            Entries.Add((report, cancellationToken));

            _logger.LogInformation("{TIMESTAMP} Readiness Probe Status: {RESULT}",
                DateTime.UtcNow, report.Status);

            if (Exception != null)
            {
                throw Exception;
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
    }
}
