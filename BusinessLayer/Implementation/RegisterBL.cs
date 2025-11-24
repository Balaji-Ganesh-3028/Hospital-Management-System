using AppModels.RequestModels;
using BusinessLayer.Interface;
using DataAccessLayer.Interface;

namespace BusinessLayer.Implementation
{
    public class RegisterBL : IRegisterService
    {
        private readonly IRegisterDAL _registerDal;

        public RegisterBL(IRegisterDAL registerDal)
        {
            _registerDal = registerDal;
        }

        public async Task<string> RegisterUser(RegisterRequest register)
        {
            // CALL DATA ACCESS LAYER TO REGISTER USER
            return await _registerDal.RegisterUser(register);
        }
    }
}
