import { useState, type ChangeEvent, type FormEvent } from "react";

function Login() {
const [formData, setFormData] = useState({
    email: '',
    password: ''
  });

  function handleSubmit(event: FormEvent<HTMLFormElement>): void {
    event.preventDefault();

    // Use current component state instead of constructing a FormData from the event
    const { email, password } = formData;
    console.log('Login attempt:', { email, password });

    // ...submit to API / reset form / etc...
  }

  function handleChange(event: ChangeEvent<HTMLInputElement>): void {
    const { name, value } = event.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  }

  return (
    <>
      <form className="auth-form" onSubmit={handleSubmit}>
        <h2>Welcome Back</h2>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="Email"
          required
        />
        <input
          type="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          placeholder="Password"
          required
        />
        <button type="submit" className="submit-btn">
          Login
        </button>
      </form>
    </>
  );
}

export default Login; 