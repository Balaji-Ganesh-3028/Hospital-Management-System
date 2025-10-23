import { useState } from "react";
import "./AuthLayout.css";
import Login from "../Login/Login";
import Register from "../Register/Register";

const AuthPage = () => {
  const [activeTab, setActiveTab] = useState("login");

  const switchTab = (tab: string) => {
    setActiveTab(tab);
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-container">
        {/* Tabs */}
        <div className="auth-tabs">
          <button
            className={activeTab === "login" ? "active" : ""}
            onClick={() => switchTab("login")}
          >
            Login
          </button>
          <button
            className={activeTab === "signup" ? "active" : ""}
            onClick={() => switchTab("signup")}
          >
            Signup
          </button>
        </div>

        {/* Forms */}
        <div className="auth-content">
          {activeTab === "login" && (
            <Login />
          )}

          {activeTab === "signup" && (
            <Register />
          )}
        </div>
      </div>
    </div>
  );
};

export default AuthPage;
