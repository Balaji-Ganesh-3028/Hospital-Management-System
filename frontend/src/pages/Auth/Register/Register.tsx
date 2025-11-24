import { useEffect, useState, type ChangeEvent, type FormEvent } from "react";
import { register } from "../../../Service/Auth/Auth";
import type { RegisterPayload } from "../../../Models/Auth";
import { GetRoles } from "../../../Service/Lookups/Lookups";
import type { Roles } from "../../../Models/Lookups";
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext"
import { useNavigate } from "react-router-dom";
import { notify } from "../../../Service/Toast-Message/Toast-Message";
import { ToastMessageTypes } from "../../../Enums/Toast-Message";

interface RegisterProps {
  onRegisterSuccess: () => void;
}

function Register({ onRegisterSuccess }: RegisterProps) {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    password: '',
    role: 0,
  });
  const [roles, setRoles] = useState<Roles[]>([]);
  const { showSpinner, hideSpinner } = useSpinner();

  const handleSubmit = async (event: FormEvent<HTMLFormElement>): Promise<void> => {
    event.preventDefault();
  
    // Use current component state instead of constructing a FormData from the event
    const { fullName, email, password, role } = formData;
    console.log('Register attempt:', { fullName, email, password, role });
    const payload: RegisterPayload = {
      username: fullName,
      email: email,
      password: password,
      roleId: role
    }
    
    showSpinner();
    const response = await register(payload)
    setTimeout(() => {
      notify(response, ToastMessageTypes.SUCCESS);
      hideSpinner();
      onRegisterSuccess();
      navigate("/auth?page=login");
    }, 1000)
    
    console.log('Register response:', response);
    // ...submit to API / reset form / etc...
  };
  
    function handleChange(event: ChangeEvent<HTMLInputElement> | ChangeEvent<HTMLSelectElement>): void {
      const { name, value } = event.target;
      setFormData(prev => ({ ...prev, [name]: value }));
    }
  
  useEffect(() => {
    loadLookups();
  }, []);

  const loadLookups = async() => {
    // Load any necessary lookup data here
    const roles = await GetRoles();
    setRoles(roles);
    console.log('Roles:', roles);
    
  }


  return (
    <>
      <form className="auth-form" onSubmit={handleSubmit}>
        <h2>Create Account</h2>
        <input type="text" id="fullName" name="fullName" value={formData.fullName} onChange={handleChange} placeholder="Full Name" required />
        <input type="email" id="email" name="email" value={formData.email} onChange={handleChange} placeholder="Email" required />
        <input type="password" id="password" name="password" value={formData.password} onChange={handleChange} placeholder="Password" required />
        <select id="role" name="role" value={formData.role} onChange={handleChange} required>
          <option value="" selected>
            Select Role
          </option>
          {roles.map((roles) => (
            <option key={roles.id} value={roles.id} >
            {roles.roleName}
          </option>
          )) }
        </select>
        <button type="submit" className="submit-btn">
          Signup
        </button>
      </form>
    </>

  );
}

export default Register;