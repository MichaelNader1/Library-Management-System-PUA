namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IBannedUserService
    {
        Task BanUserAsync(string userId);
        Task<bool> IsUserBannedAsync(string userId);
    }
    }
