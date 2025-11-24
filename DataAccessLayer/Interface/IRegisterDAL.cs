using AppModels.RequestModels;

namespace DataAccessLayer.Interface
{
    public interface IRegisterDAL
    {
        public Task<string> RegisterUser(RegisterRequest request);
    }
}
