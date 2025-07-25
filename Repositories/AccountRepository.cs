using System;
using Dapper;
using Microsoft.Data.SqlClient;
using WebProject.Models;

namespace WebProject.Repositories;

public class AccountRepository : IAccountRepository
{
    public readonly IConfiguration configuration;
    private readonly string connectionString;

    public AccountRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task CreateAccountAsync(Account account)
    {
        using var connection = new SqlConnection(connectionString);
        var query = @"INSERT INTO [dbo].[Account] ([Name],[AccountTypeId],[Balance],[Description]) 
        VALUES (@Name,@AccountTypeId,@Balance,@Description)
        SELECT SCOPE_IDENTITY();";

        var id = await connection.QuerySingleAsync<int>(query, account);
        account.Id = id;
    }
}
