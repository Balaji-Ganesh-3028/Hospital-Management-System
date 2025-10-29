import { useState, type ChangeEvent, type FormEvent } from "react";

function Register() {
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    password: '',
    role: '',
  });

  function handleSubmit(event: FormEvent<HTMLFormElement>): void {
      event.preventDefault();
  
      // Use current component state instead of constructing a FormData from the event
      const { fullName, email, password, role } = formData;
      console.log('Register attempt:', { fullName, email, password, role });
  
      // ...submit to API / reset form / etc...
    }
  
    function handleChange(event: ChangeEvent<HTMLInputElement> | ChangeEvent<HTMLSelectElement>): void {
      const { name, value } = event.target;
      setFormData(prev => ({ ...prev, [name]: value }));
    }
  


  return (
    <>
      <form className="auth-form" onSubmit={handleSubmit}>
        <h2>Create Account</h2>
        <input type="text" id="fullName" name="fullName" value={formData.fullName} onChange={handleChange} placeholder="Full Name" required />
        <input type="email" id="email" name="email" value={formData.email} onChange={handleChange} placeholder="Email" required />
        <input type="password" id="password" name="password" value={formData.password} onChange={handleChange} placeholder="Password" required />
        <select id="role" name="role" value={formData.role} onChange={handleChange} required>
          <option value="" disabled selected>
            Select Role
          </option>
          <option value="user">User</option>
          <option value="admin">Admin</option>
        </select>
        <button type="submit" className="submit-btn">
          Signup
        </button>
      </form>
    </>

  );
}

export default Register;