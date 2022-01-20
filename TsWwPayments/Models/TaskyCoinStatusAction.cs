using TsWwPayments.Services;
using TsWwPaymentsModelApi.Models.Enums;

namespace TsWwPayments.Models
{
    public class TaskyCoinStatusAction : OnTransmissionStatusAction
    {
        public TaskyCoinStatusAction(PaymentItem paymentItem) : base(paymentItem)
        {
        }

        public override Task StatusChanged(TransmissionStatus newStatus, TransmissionFull data)
        {
            throw new NotImplementedException();
        }
    }
}
