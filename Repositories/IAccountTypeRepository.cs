using WebProject.Models;

namespace WebProject.Repositories
{
    public interface IAccountTypeRepository
    {
        Task AddAsync(AccountType accountType);
        Task<bool> IsExistsAsync(AccountType accountType);

        Task<IEnumerable<AccountType>> GetAllByUserIdAsync(int userId);

        Task ModifyAsync(AccountType accountType);

        Task<AccountType> GetbyIdAsync(int id, int userId);
        Task DeleteAsync(int id);

        Task UpdateOrder(IEnumerable<AccountType> sortedAccountTypes);

        Task<bool> AccountTypeBelongToUser(int[] ids, int userId);
    }
}