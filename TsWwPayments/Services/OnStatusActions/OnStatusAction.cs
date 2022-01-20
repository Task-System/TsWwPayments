namespace TsWwPayments.Services.OnStatusActions
{
    public abstract class OnStatusAction<TEnum, TData>
        : IOnStatusAction<TEnum, TData> where TEnum : Enum
    {
        public abstract Task StatusChanged(TEnum newStatus, TData data);
    }
}
