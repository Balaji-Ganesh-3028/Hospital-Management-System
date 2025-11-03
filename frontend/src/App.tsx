import './App.css'
import './styles/common.css'
import { ToastContainer } from 'react-toastify'
// @ts-expect-error: module has no declaration file
import GlobalSpinner from './components/Spinner/GlobalSpinner'
// @ts-expect-error: module has no declaration file
import { SpinnerProvider } from './Contexts/SpinnerContext'
import AppRoutes from './AppRoutes'

function App() {

  return (
    <>
      <SpinnerProvider>
        <GlobalSpinner />
        <AppRoutes />
        <ToastContainer/>
      </SpinnerProvider>
    </>
  )
}

export default App
