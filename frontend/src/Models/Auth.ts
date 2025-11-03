export interface RegisterPayload {
  username: string;
  email: string;
  password: string;
  roleId: number;
}

export interface LoginPayload {
  email: string;
  password: string;
}

export interface LoginResponse {
  data: Details;
  txt: string;
  message: string;
  token: string;
}

export interface Details {
  email: string;
  userName: string;
  roleId: string;
  roleName: string;
}
