namespace TsWwPayments.Services.OnStatusActions
{
    public interface IOnStatusAction<TEnum, TData> where TEnum : Enum
    {
        /// <summary>
        /// Action to execute when <paramref name="newStatus"/> changed.
        /// </summary>
        /// <param name="newStatus">Changed status.</param>
        Task StatusChanged(TEnum newStatus, TData data);
    }
}
