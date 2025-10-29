import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'
import AuthPage from './pages/Auth/AuthLayout/AuthLayout'
import Dashboard from './pages/Dashboard/Dashboard'
import Layout from './components/Layout/Layout'
import Users from './pages/Users/Users'

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
        </Routes>
      </Router>
    </>
  )
}

export default App
