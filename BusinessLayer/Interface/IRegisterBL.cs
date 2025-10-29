using DataAccessLayer.models;

namespace BusinessLayer.Interface
{
    public interface IRegisterService
    {
        Task<string> RegisterUser(RegisterRequest register);
    }
}
