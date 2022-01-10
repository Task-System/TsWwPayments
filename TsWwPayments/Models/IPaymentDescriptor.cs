namespace TsWwPayments.Models;

public interface IPaymentDescriptor
{
    public string Id { get; }

    public string Name { get; }

    public string? Description { get; }
}
