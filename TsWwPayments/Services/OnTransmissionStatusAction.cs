using TsWwPayments.Models;
using TsWwPaymentsModelApi.Models.Enums;
using TsWwPayments.Services.OnStatusActions;

namespace TsWwPayments.Services
{
    public abstract class OnTransmissionStatusAction
        : OnStatusAction<TransmissionStatus, TransmissionFull>
    {
        protected PaymentItem PaymentItem { get; }

        protected OnTransmissionStatusAction(PaymentItem paymentItem)
            => PaymentItem = paymentItem;
    }
}
