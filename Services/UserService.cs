namespace WebProject.Services
{
    public class UserService : IUserService
    {
       public Task<int> GetUserId()
        {
            return Task.FromResult(1); // Simulación de obtención de ID de usuario   
        }
    }
}
