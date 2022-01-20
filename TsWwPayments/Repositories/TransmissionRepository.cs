using TsWwPayments.Databases;
using TsWwPaymentsModelApi.Models;
using TsWwPaymentsModelApi.Models.Enums;
using ZarinSharp.Types.Enums;

namespace TsWwPayments.Repositories
{
    public class TransmissionRepository : PaymentsRepository<Transmission>
    {
        public TransmissionRepository(PaymentsContext context) : base(context)
        {
        }

        public async Task<Transmission> InitTransmission(
            int accountId, string authority, string actionId, long amount, Currency currency)
        {
            var transmission = new Transmission
            {
                PaymentsAccountId = accountId,
                Authority = authority,
                CreatedAt = DateTime.UtcNow,
                ActionId = actionId,
                Status = TransmissionStatus.Pending,
                Currency = currency,
                TransferredAmount = amount
            };

            Insert(transmission);
            await SaveAsync();
            return transmission;
        }

        public async Task<Transmission?> GetTransmission(string authority)
        {
            return await FindOneAsync(x=> x.Authority == authority);
        }

        public async Task TransmissionDone(Transmission transmission)
        {
            transmission.DoneAt = DateTime.UtcNow;

            await SaveAsync();
        }
    }
}
