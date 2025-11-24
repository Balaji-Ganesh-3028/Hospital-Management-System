namespace AppModels.Interfaces
{
    public interface IUserDetailsView
    {
        string Email { get; set; }
        string UserType { get; set; }
        int Total { get; set; }
        string PhoneNumber { get; set; }
        string? GenderValue { get; set; }
        int? Age { get; set; }
        DateTime DOB { get; set; }
        string? DoorFloorBuilding { get; set; }
        string? AddressLine1 { get; set; }
        string? AddressLine2 { get; set; }
        string? City { get; set; }
        string? State { get; set; }
        string? Country { get; set; }
        string? Pincode { get; set; }
        int? RoleId { get; set; }
        string? RoleName { get; set; }
        string? UserName { get; set; }
    }
}
