namespace AppModels.Interfaces
{
    public interface IUserDetailsView
    {
        string Email { get; set; }
        string UserType { get; set; }
        int Total { get; set; }
        string PhoneNumber { get; set; }
    }
}
