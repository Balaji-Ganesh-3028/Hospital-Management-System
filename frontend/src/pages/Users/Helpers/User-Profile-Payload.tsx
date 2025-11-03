import type { UserProfile, UserProfileFormData } from "../../../Models/User-Profile"
const toGetUserId = () => {
  const data = localStorage.getItem("user");
  console.log(data);
  if (data) return JSON.parse(data).userId;
  else return 0
}
export const UserProfilePayload = (formData: UserProfileFormData): UserProfile => {
  return {
    userDetails: {
      userId: toGetUserId(),
      fristName: formData.firstName,
      lastName: formData.lastName,
      gender: formData.gender,
      age: formData.age,
      dob: formData.dob
    },
    contactDetails: {
      phoneNumber: formData.phoneNumber,
      doorFloorBuilding: formData.doorFloorBuilding,
      addressLine1: formData.addressLine1,
      addressLine2: formData.addressLine2,
      city: formData.city,
      state: formData.state,
      country: formData.country,
      pincode: formData.pincode
    }
  }
}