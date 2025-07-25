namespace WebProject.Services;
using System.Collections.Generic;

public interface IUserService
{
    Task<int> GetUserId();
}
