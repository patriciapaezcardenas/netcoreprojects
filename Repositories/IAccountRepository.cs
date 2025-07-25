using System;
using WebProject.Models;

namespace WebProject.Repositories;

public interface IAccountRepository
{
    Task CreateAccountAsync(Account account);
}
