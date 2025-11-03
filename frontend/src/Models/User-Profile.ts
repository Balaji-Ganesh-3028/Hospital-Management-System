export interface UserProfile {
  userDetails: UserDetails;
  contactDetails: ContactDetails;
}

export interface ContactDetails {
  userId?: number;
  phoneNumber: string;
  doorFloorBuilding: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  country: string;
  pincode: number | string;
  createdBy?: string;
  updatedBy?: string;
}

export interface UserDetails {
  userId?: number;
  fristName: string;
  lastName: string;
  dob: string;
  gender: number | string;
  age: number | string;
  createdBy?: string;
  updatedBy?: string;
}

export interface UserProfileFormData {
  userId?: number;
  firstName: string;
  lastName: string;
  dob: string;
  gender: number | string;
  age: number | string;
  phoneNumber: string;
  doorFloorBuilding: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  country: string;
  pincode: number | string;
}

export interface AllUserDetails {
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
  gender: number;
  phoneNumber: string;
  userType: string;
}
