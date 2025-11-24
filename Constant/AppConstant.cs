namespace Constant.Constants
{
    public static class AppConstants
    {
        public static class ResponseMessages
        {
            public const string SomethingWentWrong = "Something went worng!!!";
            public const string DatabaseError = "Database error: ";
            public const string InternalServerError = "Internal server error: ";
            public const string UnExpectedError = "An unexpected error occurred.";

            // DOCTOR RELATED MESSAGES
            public const string DoctorDetailsRequired = "Doctor details required";
            public const string DoctorIdRequired = "Doctor Id required";
            public const string DoctorDetailsAddedSuccessfully = "Doctor details added successfully";
            public const string DoctorDetailsUpdatedSuccessfully = "Doctor details updated successfully";
            public const string DoctorDetailNotFound = "Doctor detail not found";
            public const string DoctorDetailsNotFound = "Doctor details not found";

            // PATIENT RELATED MESSAGES
            public const string PatientDetailsRequired = "Patient details required";
            public const string PatientDetailsAddedSuccessfully = "Patient details added successfully";
            public const string PatientDetailsUpdatedSuccessfully = "Patient details updated successfully";
            public static string PatientDetailsNotFound = "Patient details not found";
            public static string PatientDetailNotFound = "Patient detail not found";
            public static string PatientIdRequired = "Patient Id required";

            // LOGIN RELATED MESSAGES
            public const string LoggedInSuccessfully = " logged in successfully.";
            public const string LoginFailed = "Login failed, Something went wrong!!";

            // REGISTERATION & USER RELATED MESSAGES
            public const string UserDetailsReuqired = "User details are required.";
            public const string UserRegisteredSuccessfully = "User registered successfully";
            public const string InsertUserErrorMessage = "An error occurred while inserting the user profile.";
            public const string UpdateUserErrorMessage = "An error occurred while updating the user profile.";
            public const string UserDetailsAddedSuccessfully = "User details added successfully";
            public const string UserDetailsUpdatedSuccessfully = "User details updated successfully";
            public const string UserDeletedSuccessfully = "User deleted successfully";
            public const string UserDeletionFfailed = "User record not deleted";
            public const string UserIdRequired = "UserId is required";
            public const string UserNotFound = "User not found";
            public const string InvalidUserCredentials = "Invalid user credentials";

            //APPOINTMENT RELATED MESSAGE
            public const string DoctorAppointmentFixed = "Doctor appointment fixed";
            public const string DoctorAppointmentUpdates = "Doctor appointment updated";
            public const string AppointmentDetailsRequired = "Appointment details required";
            public const string AppointmentIdRequired = "Appointment Id required";
            public const string AppointmentNotFound = "Appointment not found";
            public const string AppointmentsNotFound = "Appointment's not found";
        }

        public static class DBResponse
        {
            public const string Success = "Success";
            public const string Failed = "Failed";
            public const string Error = "Error";
            public const string NotFound = "Not Found";
            public const string LoginSuccessful = "Login successful";
            public const string InvalidCredentials = "Invalid credentials";
            public const string UserNotFound = "User not found";
        }
    }
}
