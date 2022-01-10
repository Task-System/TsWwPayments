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
            Currency? currency = default)
        {
            Id = id;
            Name = name;
            Description = description;
            Amount = amount;
            Currency = currency?? Currency.IRR;
        }

        public string Id { get; }

        public string Name { get; }

        public string? Description { get; set; }

        public long Amount { get; set; }

        public Currency Currency { get; set; }
    }
}
