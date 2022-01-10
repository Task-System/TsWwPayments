using Microsoft.EntityFrameworkCore;
using TsWwPaymentsModelApi.Models;

namespace TsWwPayments.Databases
{
    public class PaymentsContext : DbContext
    {
        public PaymentsContext(DbContextOptions options) : base(options)
        {
        }

        public PaymentsContext()
        {
        }

        public DbSet<Transmission> Transmissions => Set<Transmission>();

        public DbSet<PaymentsAccount> PaymentsAccounts => Set<PaymentsAccount>();
    }
}
