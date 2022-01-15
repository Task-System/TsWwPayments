using TsWwPayments.Databases;
using TsWwPaymentsModelApi.Models;

namespace TsWwPayments.Repositories
{
    public class PaymentsAccountRepository : PaymentsRepository<PaymentsAccount>
    {
        public PaymentsAccountRepository(PaymentsContext context) : base(context)
        {
        }

        public async Task<PaymentsAccount> CreateAccount(long telegramId, string? firstName)
        {
            var account = new PaymentsAccount
            {
                TelegramId = telegramId,
                Name = firstName,
            };

            Insert(account);

            await SaveAsync();
            return account;
        }
    }
}
