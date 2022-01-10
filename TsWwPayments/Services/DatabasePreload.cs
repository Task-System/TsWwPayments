using ImRepositoryPattern;
using TsWwPayments.Databases;
using TsWwPaymentsModelApi.Models;

namespace TsWwPayments.Services
{
    public class DatabasePreload : IHostedService
    {
        private readonly IServiceProvider _services;

        public DatabasePreload(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetService<IUnitOfWork<PaymentsContext>>();

            var basicRepo = db!.GetBaseRepository<Transmission>();

            var transmissions = await basicRepo.FindAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
