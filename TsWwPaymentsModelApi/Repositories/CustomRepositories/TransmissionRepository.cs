using Microsoft.EntityFrameworkCore;
using TsWwPaymentsModelApi.Models;

namespace TsWwPaymentsModelApi.Repositories.CustomRepositories
{
    public class TransmissionRepository<T> : BaseRepository<T, Transmission>
        where T: DbContext, new()
    {
        public TransmissionRepository(T context, UnitOfWork<T> unitOfWork) 
            : base(context, unitOfWork)
        {
        }
    }
}
