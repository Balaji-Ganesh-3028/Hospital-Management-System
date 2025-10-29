namespace DataAccessLayer.models
{
    public class RegisterRequest
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
    }
}
