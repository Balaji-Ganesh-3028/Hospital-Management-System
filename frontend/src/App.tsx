import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'
import './styles/common.css'
import AuthPage from './pages/Auth/AuthLayout/AuthLayout'
import Dashboard from './pages/Dashboard/Dashboard'
import Layout from './components/Layout/Layout'
import Users from './pages/Users/Users'
import Patients from './pages/Patients/Patients'
import Doctors from './pages/Doctors/Doctors'
import Appointments from './pages/Appointments/Appointments'

function App() {

  return (
     <>
      <Router>
        <Routes>
          <Route path="/auth" element={<AuthPage />} />
          <Route path="/auth?page=login" element={<AuthPage />} />
          <Route path="/auth?page=signup" element={<AuthPage />} />
          <Route path="/dashboard" element={<Layout><Dashboard /></Layout>} />
          <Route path="/users" element={<Layout><Users /></Layout>} />
          <Route path="/patients" element={<Layout><Patients /></Layout>} />
          <Route path="/doctors" element={<Layout><Doctors /></Layout>} />
          <Route path="/appointments" element={<Layout><Appointments /></Layout>} />
        </Routes>
      </Router>
    </>
  )
}

export default App
