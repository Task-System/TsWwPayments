using TsWwPayments.Services.OnStatusActions;
using TsWwPaymentsModelApi.Models.Enums;
using ZarinSharp.Types.Enums;

namespace TsWwPayments.Models
{
    public class PaymentItem: IPaymentDescriptor
    {
        /// <summary>
        /// Describes a payment item
        /// </summary>
        /// <param name="id">A custom and unique id per <see cref="PaymentCase"/></param>
        /// <param name="name">Item name</param>
        /// <param name="amount">Price of item</param>
        /// <param name="description">Optinal description for this item</param>
        /// <param name="currency">Currency! default to IRT.</param>
        public PaymentItem(
            string id,
            string name,
            long amount,
            string? description = default,
            Currency? currency = default,
            Type? typeOfStatusAction = default)
        {
            Id = id;
            Name = name;
            Description = description;
            Amount = amount;
            Currency = currency?? Currency.IRR;
            if (typeOfStatusAction != null)
                OnStatusAction = (IOnStatusAction<TransmissionStatus, TransmissionFull>?)Activator.CreateInstance(
                    typeOfStatusAction, new object[] { this });
        }

        public string Id { get; }

        public string Name { get; }

        public string? Description { get; }

        public long Amount { get; }

        public Currency Currency { get; }

        public IOnStatusAction<TransmissionStatus, TransmissionFull>? OnStatusAction { get; }
    }
}
