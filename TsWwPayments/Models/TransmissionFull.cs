using TsWwPaymentsModelApi.Models;

namespace TsWwPayments.Models
{
    /// <summary>
    /// Represents full data about a transmission.
    /// </summary>
    public readonly struct TransmissionFull
    {
        public TransmissionFull(Transmission transmission, PaymentsAccount account)
        {
            Transmission = transmission;
            Account = account;
        }

        public Transmission Transmission { get; }

        public PaymentsAccount Account { get; }
    }
}
