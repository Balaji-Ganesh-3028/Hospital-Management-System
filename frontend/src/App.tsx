import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'
import AuthPage from './pages/Auth/AuthLayout/AuthLayout'

function App() {

  return (
     <>
      <Router>
        <Routes>
          <Route path="/auth" element={<AuthPage />} />
          <Route path="/auth?page=login" element={<AuthPage />} />
          <Route path="/auth?page=signup" element={<AuthPage />} />
        </Routes>
      </Router>
    </>
  )
}

export default App
