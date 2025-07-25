namespace WebProject.Repositories
{
    using Microsoft.Data.SqlClient;
    using WebProject.Models;
    using Dapper;
    using System.Collections.Generic;

    /// <summary>
    /// Repository for managing account types in the database.
    /// This repository provides methods to add, retrieve, and modify account types associated with a user.
    /// </summary>
    public class AccountTypeRepository : IAccountTypeRepository
    {
        /// <summary>
        /// It allows to retrieve settings values.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Constructor for AccountTypeRepository.
        /// Initializes the repository with a configuration object to retrieve the connection string.
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountTypeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ??
             throw new ArgumentNullException("DefaultConnection", "Connection string not found in configuration.");
        }

        /// <summary>
        /// Retrieves all account types for a specific user.
        /// This method fetches all account types associated with a given user ID from the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountType>> GetAllByUserIdAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<AccountType>(
                    "SELECT * FROM AccountType WHERE UserId = @UserId ORDER BY [Order]", new { UserId = userId });
            }
        }

        /// <summary>
    /// Checks if all provided account type IDs belong to a specific user.
    /// This method verifies that all account type IDs in the input array are associated with the given user ID.
    /// </summary>
    /// <param name="ids">Array of account type IDs to check</param>
    /// <param name="userId">ID of the user to verify against</param>
    /// <returns>True if all account types belong to the user, false otherwise</returns>
    public async Task<bool> AccountTypeBelongToUser(int[] ids, int userId)
        {
            var accountTypeList = await GetAllByUserIdAsync(userId);
            var extraAccountType = ids.Except(accountTypeList.Select(x => x.Id)).AsEnumerable();
            
            return !extraAccountType.Any();      
        }

        /// <summary>
        /// Adds a new account type to the database.
        /// This method inserts a new account type into the AccountType table and sets its ID.  
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public async Task AddAsync(AccountType accountType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var id = await connection.QuerySingleAsync<int>(
                    "SP_Account_type_insert",
                    new { Name = accountType.Name, UserId = accountType.UserId },
                    commandType: System.Data.CommandType.StoredProcedure);
                accountType.Id = id;
            }
        }

        /// <summary>
        /// Checks if an account type already exists for a given user.
        /// This method checks if an account type with the same name and user ID already exists in the database.
        /// If it does, it returns true; otherwise, it returns false.   
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsAsync(AccountType accountType)
        {
            bool isItemExists = false;
            using var connection = new SqlConnection(_connectionString);

            var exists = await connection.QueryFirstOrDefaultAsync<int>(
                "SELECT 1 FROM AccountType WHERE Name = @Name AND UserId = @UserId", accountType);

            isItemExists = exists > 0;

            return isItemExists;
        }

        /// <summary>
        /// Retrieves an account type by its ID and user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AccountType> GetbyIdAsync(int id, int userId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<AccountType>(
                "SELECT Id, [Name], [Order], UserId " +
                "FROM AccountType " +
                "WHERE Id = @Id AND UserId=@UserId",
                new { Id = id, UserId = userId });
        }

        /// <summary>
        /// Modifies an existing account type.      
        /// /// Throws an exception if the account type does not exist or if a duplicate name is found for the same user.
        /// </summary>  
        public async Task ModifyAsync(AccountType accountType)
        {
            if (accountType == null)
            {
                throw new ArgumentNullException(nameof(accountType), "AccountType cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UPDATE AccountType SET Name = @Name, [Order] = @Order WHERE Id = @Id",
                accountType);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("DELETE FROM AccountType WHERE Id = @Id", new { Id = id });
        }

        public async Task UpdateOrder(IEnumerable<AccountType> sortedAccountTypes)
        {
            if (sortedAccountTypes is null || sortedAccountTypes.Count() == 0)
            {
                return;
            }

            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE dbo.AccountType SET [Order] = @Order WHERE [Id] = @Id";
            await connection.ExecuteAsync(query, sortedAccountTypes);
        }
    }
}