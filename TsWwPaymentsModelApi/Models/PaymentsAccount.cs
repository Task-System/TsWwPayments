namespace TsWwPaymentsModelApi.Models
{
    /// <summary>
    /// Represents the identity of a user in Payments.
    /// </summary>
    public class PaymentsAccount
    {
        public int PaymentsAccountId { get; set; }

        public long TelegramId { get; set; }

        public string? Name { get; set; }

        /// <summary>
        /// <see cref="Transmission"/>s made by this account.
        /// </summary>
        public ICollection<Transmission>? Transmissions { get; set; }
    }
}
