using TsWwPayments.Services.OnStatusActions;
using TsWwPaymentsModelApi.Models.Enums;

namespace TsWwPayments.Models;

public interface IPaymentDescriptor
{
    public string Id { get; }

    public string Name { get; }

    public string? Description { get; }

    /// <summary>
    /// Action to be executed on Transmission status changed.
    /// </summary>
    public IOnStatusAction<TransmissionStatus, TransmissionFull>? OnStatusAction { get; }
}
