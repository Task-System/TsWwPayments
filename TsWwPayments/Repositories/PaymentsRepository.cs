using ImRepositoryPattern;
using ImRepositoryPattern.Repository;
using TsWwPayments.Databases;

namespace TsWwPayments.Repositories
{
    public class PaymentsRepository<T> : BaseRepository<PaymentsContext, T>
        where T : class
    {
        public PaymentsRepository(
            PaymentsContext context,
            IUnitOfWork<PaymentsContext> unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
