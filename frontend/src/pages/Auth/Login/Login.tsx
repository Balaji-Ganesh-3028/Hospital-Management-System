import { useState, type ChangeEvent, type FormEvent } from "react";
import type { LoginPayload } from "../../../Models/Auth";
import { login as loginService } from "../../../Service/Auth/Auth";
import { notify } from "../../../Service/Toast-Message/Toast-Message";
import { ToastMessageTypes } from "../../../Enums/Toast-Message";
import { useNavigate } from "react-router-dom";
// @ts-expect-error: module has no declaration file
import { useSpinner } from "../../../Contexts/SpinnerContext";
import { useAuth } from "../../../Contexts/AuthContext";

function Login() {
  const [formData, setFormData] = useState({
      email: '',
      password: ''
    });
  const navigate = useNavigate();
  const { showSpinner, hideSpinner } = useSpinner();
  const { login } = useAuth();

  const handleSubmit = async (event: FormEvent<HTMLFormElement>): Promise<void> => {
    event.preventDefault();

    // Use current component state instead of constructing a FormData from the event
    const { email, password } = formData;
    console.log('Login attempt:', { email, password });

    const payload: LoginPayload = {
      email: email,
      password: password
    }

    showSpinner();
    const response = await loginService(payload);
    console.log('Login response:', response);
    if (response.txt == "Success") {
      notify(response.message, ToastMessageTypes.SUCCESS)
      login(response);
      setTimeout(() => {
        hideSpinner();
        navigate("/dashboard")
      }, 1000)
    }
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