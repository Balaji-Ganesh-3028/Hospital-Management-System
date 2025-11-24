import "./AuthLayout.css";
import Login from "../Login/Login";
import Register from "../Register/Register";
import { useSearchParams } from "react-router-dom";

const AuthPage = () => {
  // const [activeTab, setActiveTab] = useState("login");
  const [searchParams, setSearchParams] = useSearchParams();

  const activeTab = searchParams.get('page') || 'login';

  const switchTab = (tab: string) => {
    setSearchParams({ page: tab });
  };

  return (
    <div className="auth-layout">
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
            <Register onRegisterSuccess={() => setSearchParams("login")} />
          )}
        </div>
      </div>
      </div>
      </div>
  );
};

export default AuthPage;
