using ImRepositoryPattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TsWwPayments.Databases;
using TsWwPayments.Repositories;
using TsWwPaymentsModelApi.Models;

namespace Payments.Tests
{
    [TestClass()]
    public class UnitOfWorkTests
    {
        [TestMethod()]
        public async Task UnitOfWorkTestAsync()
        {
            var unitOfWork = new UnitOfWork<PaymentsContext>();

            //unitOfWork.AddRepository<
            //    TransmissionRepository<PaymentsContext>,
            //    PaymentsContext,
            //    Transmission>("transmissions");

            unitOfWork.AddRepository(typeof(TransmissionRepository));

            var repo = unitOfWork.GetRepository<TransmissionRepository>();

            var repo1 = unitOfWork.GetBaseRepository<PaymentsAccount>();

            // var list = await repo.EntitySet.ToListAsync();

            System.Console.WriteLine();
        }
    }
}