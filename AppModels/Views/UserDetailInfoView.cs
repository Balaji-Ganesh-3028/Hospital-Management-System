using AppModels.Interfaces;
using AppModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Views
{
    public class UserDetailInfoView: UserDetails, IUserDetailsView
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public int Total { get; set; }
        public string? GenderValue { get; set; }
        public int? Age { get; set; }
        public DateTime DOB { get; set; }
        public string? DoorFloorBuilding { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? UserName { get; set; }
    }
}
