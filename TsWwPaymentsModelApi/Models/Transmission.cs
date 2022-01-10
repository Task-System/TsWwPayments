using System.ComponentModel.DataAnnotations.Schema;
using TsWwPaymentsModelApi.Models.Enums;

namespace TsWwPaymentsModelApi.Models
{
    /// <summary>
    /// Represents a transmission pending or completed
    /// </summary>
    public class Transmission
    {
        public Transmission(
            int transmissionId,
            DateTime createdAt,
            TransmissionStatus status,
            string actionId,
            long transferredAmount,
            int paymentsAccountId,
            DateTime? doneAt = default)
        {
            TransmissionId = transmissionId;
            CreatedAt = createdAt;
            DoneAt = doneAt;
            Status = status;
            ActionId = actionId;
            TransferredAmount = transferredAmount;
            PaymentsAccountId = paymentsAccountId;
        }

        /// <summary>
        /// Unique identifier of this transmission
        /// </summary>
        public int TransmissionId { get; set; }

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

        /// <summary>
        /// Executer account id.
        /// </summary>
        [ForeignKey("PaymentsAccountId")]
        public int PaymentsAccountId { get; set; }

        /// <summary>
        /// Accounts details for Executer.
        /// </summary>
        public PaymentsAccount? PaymentsAccount { get; set; }

    }
}
