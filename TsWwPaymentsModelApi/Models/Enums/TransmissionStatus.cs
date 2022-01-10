namespace TsWwPaymentsModelApi.Models.Enums
{
    /// <summary>
    /// A <see cref="Transmission"/> Status.
    /// </summary>
    public enum TransmissionStatus
    {
        /// <summary>
        /// <see cref="Transmission"/> still in progress
        /// </summary>
        Pending,

        /// <summary>
        /// <see cref="Transmission"/> completed with success.
        /// </summary>
        Succeeded,

        /// <summary>
        /// <see cref="Transmission"/> cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// <see cref="Transmission"/> failed with time out.
        /// </summary>
        Timedout,
    }
}
