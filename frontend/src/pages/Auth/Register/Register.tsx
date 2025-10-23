import { useState, type ChangeEvent, type FormEvent } from "react";

function Register() {
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    password: ''
  });

  function handleSubmit(event: FormEvent<HTMLFormElement>): void {
      event.preventDefault();
  
      // Use current component state instead of constructing a FormData from the event
      const { fullName, email, password } = formData;
      console.log('Register attempt:', { fullName, email, password });
  
      // ...submit to API / reset form / etc...
    }
  
    function handleChange(event: ChangeEvent<HTMLInputElement>): void {
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
        <button type="submit" className="submit-btn">
          Signup
        </button>
      </form>
    </>

  );
}

export default Register;