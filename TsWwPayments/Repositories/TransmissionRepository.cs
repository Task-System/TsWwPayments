using ImRepositoryPattern;
using TsWwPayments.Databases;
using TsWwPaymentsModelApi.Models;

namespace TsWwPayments.Repositories
{
    public class TransmissionRepository : PaymentsRepository<Transmission>
    {
        public TransmissionRepository(PaymentsContext context, IUnitOfWork<PaymentsContext> unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
