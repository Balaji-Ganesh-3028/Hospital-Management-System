using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Interfaces
{
    public interface IDoctorInfo
    {
        int DoctorId { get; set; }
        DateOnly DateOfAssociation { get; set; }
        string LicenseNumber { get; set; }
        int Qualification { get; set; }
        string QualificationName { get; set; }
        int Specialization { get; set; }
        string SpecializationName { get; set; }
        int Designation { get; set; }
        string DesignationName { get; set; }
        int Experience { get; set; }
        string PhoneNumber { get; set; }
    }
}
