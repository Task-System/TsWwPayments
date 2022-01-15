using System.ComponentModel.DataAnnotations.Schema;
using TsWwPaymentsModelApi.Models.Enums;
using ZarinSharp.Types.Enums;

namespace TsWwPaymentsModelApi.Models
{
    /// <summary>
    /// Represents a transmission pending or completed
    /// </summary>
    public class Transmission
    {
        /// <summary>
        /// Unique identifier of this transmission
        /// </summary>
        public int TransmissionId { get; set; }

        public string Authority { get; set; }

        /// <summary>
        /// When this <see cref="Transmission"/> created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When this <see cref="Transmission"/> is done.
        /// </summary>
        public DateTime? DoneAt { get; set; }

        /// <summary>
        /// The status of this <see cref="Transmission"/>
        /// </summary>
        public TransmissionStatus Status { get; set; }

        /// <summary>
        /// Action id that cause this transmission to made.
        /// </summary>
        public string ActionId { get; set; }

        public long TransferredAmount { get; set; }

        public Currency Currency { get; set; }

        /// <summary>
        /// Executer account id.
        /// </summary>
        [ForeignKey("PaymentsAccountId")]
        public int PaymentsAccountId { get; set; }

        /// <summary>
        /// Accounts details for Executer.
        /// </summary>
        public PaymentsAccount? PaymentsAccount { get; set; }

        public long EnsureIRT()
        {
            if (Currency == Currency.IRR)
                return TransferredAmount / 10;
            return TransferredAmount;
        }

    }
}
