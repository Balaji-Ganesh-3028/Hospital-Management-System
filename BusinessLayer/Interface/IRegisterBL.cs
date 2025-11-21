
using AppModels.RequestModels;

namespace BusinessLayer.Interface
{
    public interface IRegisterService
    {
        Task<string> RegisterUser(RegisterRequest register);
    }
}
