using AppModels.Models;
using AppModels.Interfaces;

namespace AppModels.Views
{
    public class DoctorInfoView: UserDetails, IDoctorInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int DoctorId { get; set; }
        public DateOnly DateOfAssociation { get; set; }
        public string LicenseNumber { get; set; }
        public int Qualification { get; set; }
        public string QualificationName { get; set; }
        public int Specialization { get; set; }
        public string SpecializationName { get; set; }
        public int Designation { get; set; }
        public string DesignationName { get; set; }
        public int Experience { get; set; }
        public string PhoneNumber { get; set; }

    }
}
