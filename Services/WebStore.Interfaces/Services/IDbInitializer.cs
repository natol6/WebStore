namespace WebStore.Interfaces.Services
{
    public interface IDbInitializer
    {
        Task<bool> RemoveAsinc(CancellationToken Cancel = default);

        Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default);
    }
}
