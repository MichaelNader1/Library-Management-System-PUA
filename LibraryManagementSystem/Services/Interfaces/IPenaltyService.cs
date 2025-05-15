namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IPenaltyService
    {
        Task AddPenaltyAsync(int borrowingId, int lateDays);
        Task<int> GetPenaltyCountAsync(string userId);
    }

}
