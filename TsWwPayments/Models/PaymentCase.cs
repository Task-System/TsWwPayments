using TsWwPayments.Services.OnStatusActions;
using TsWwPaymentsModelApi.Models.Enums;
using ZarinSharp.Types.Enums;

namespace TsWwPayments.Models
{
    public class PaymentCase: IPaymentDescriptor
    {
        /// <summary>
        /// Describes a payment case
        /// </summary>
        /// <param name="caseId">Unique case custom id</param>
        /// <param name="caseName">Name of payment case</param>
        /// <param name="description">Description of the case</param>
        /// <param name="price">
        /// Optional price!
        /// <para><paramref name="items"/> will be ignored if the case has a price!</para>
        /// </param>
        /// <param name="items">
        /// Optional. for base case which has items to choose, instead of itself
        /// <para>This will be ignored if <paramref name="price"/> is not null!</para>
        /// </param>
        /// <param name="priceRange">Price range for free price case, if <paramref name="price"/> and <paramref name="items"/> are null.</param>
        /// <remarks>If both <paramref name="price"/> and <paramref name="items"/> are null,
        /// Then the case will be considered as a price free case based on <paramref name="priceRange"/>!
        /// <para>If <paramref name="items"/>, <paramref name="price"/>, <paramref name="priceRange"/>
        /// All are null then it's an error.</para>
        /// </remarks>
        public PaymentCase (string caseId,
                            string caseName,
                            string? description = default,
                            long? price = default,
                            Currency currency = Currency.IRT,
                            IEnumerable<PaymentItem>? items = default,
                            (long Minimum, long Maximum)? priceRange = default,
                            IOnStatusAction<TransmissionStatus, TransmissionFull>? onStatusAction = default)
        {
            if (price != null)
                CaseType = PaymentCaseType.SealedCase;
            else if (items != null)
                CaseType = PaymentCaseType.AbstractCase;
            else if (priceRange != null)
                CaseType = PaymentCaseType.FreePriceCase;
            else
                throw new Exception(
                    "You should specify one of following: price, items, priceRange");

            Id = caseId;
            Name = caseName;
            Description = description;
            Items = items;
            Price = price;
            PriceRange = priceRange;
            OnStatusAction = onStatusAction;
            Currency = currency;
        }

        public string Id { get; }

        public string Name { get; }

        /// <summary>
        /// Payment type for this case.
        /// </summary>
        public PaymentCaseType CaseType { get; }

        /// <summary>
        /// Description of the case
        /// </summary>
        public string? Description { get; }

        public Currency Currency { get; } = Currency.IRT;

        /// <summary>
        /// Ignored if <see cref="CaseType"/> is not <see cref="PaymentCaseType.AbstractCase"/>
        /// </summary>
        public IEnumerable<PaymentItem>? Items { get; }

        /// <summary>
        /// Ignored if <see cref="CaseType"/> is not <see cref="PaymentCaseType.SealedCase"/>
        /// </summary>
        public long? Price { get; }

        /// <summary>
        /// Ignored if <see cref="CaseType"/> is not <see cref="PaymentCaseType.FreePriceCase"/>
        /// </summary>
        public (long Minimum, long Maximum)? PriceRange { get; }

        /// <summary>
        /// Action to be executed on Transmission status changed.
        /// </summary>
        public IOnStatusAction<TransmissionStatus, TransmissionFull>? OnStatusAction { get; }
    }
}
