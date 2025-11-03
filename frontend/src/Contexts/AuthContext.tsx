
import React, { createContext, useState, useContext, type ReactNode } from 'react';
import type { LoginResponse } from '../Models/Auth';

interface AuthContextType {
  isAuthenticated: boolean;
  user: LoginResponse | null;
  login: (userData: LoginResponse) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(!!localStorage.getItem('token'));
  const [user, setUser] = useState<LoginResponse | null>(() => {
    const storedUser = localStorage.getItem('user') || null;
    console.log(storedUser);
    return storedUser !== null && storedUser !== undefined && storedUser !== 'undefined' && storedUser ? JSON.parse(storedUser) : null;
  });

  const login = (userData: LoginResponse) => {
    localStorage.setItem('user', JSON.stringify(userData?.data));
    localStorage.setItem('token', userData?.token); // Replace with actual token
    setUser(userData);
    setIsAuthenticated(true);
  };

  const logout = () => {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    setUser(null);
    setIsAuthenticated(false);
    window.location.href = '/';
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
