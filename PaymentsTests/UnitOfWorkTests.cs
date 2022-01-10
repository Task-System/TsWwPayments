using ImRepositoryPattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TsWwPayments.Databases;
using TsWwPaymentsModelApi.Models;
using TsWwPaymentsModelApi.Repositories.CustomRepositories;

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

            unitOfWork.AddRepository("transmissions", typeof(TransmissionRepository<PaymentsContext>));

            var repo = unitOfWork.GetRepository<TransmissionRepository<PaymentsContext>>("transmissions");

            var repo1 = unitOfWork.GetBaseRepository<PaymentsContext, PaymentsAccount>();

            // var list = await repo.EntitySet.ToListAsync();

            System.Console.WriteLine();
        }
    }
}