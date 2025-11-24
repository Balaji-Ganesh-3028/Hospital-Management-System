
import React, { createContext, useState, type ReactNode } from 'react';
import type { Details, LoginResponse } from '../Models/Auth';

interface AuthContextType {
  isAuthenticated: boolean;
  user: Details | null;
  login: (userData: LoginResponse) => void;
  logout: () => void;
}

// eslint-disable-next-line react-refresh/only-export-components
export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(!!localStorage.getItem('token'));
  const [user, setUser] = useState<Details | null>(() => {
    const storedUser = localStorage.getItem('user') || null;
    return storedUser !== null && storedUser !== undefined && storedUser !== 'undefined' && storedUser ? JSON.parse(storedUser) : null;
  });

  const login = (userData: LoginResponse) => {
    localStorage.setItem('user', JSON.stringify(userData?.data));
    localStorage.setItem('token', userData?.token);
    setUser(userData?.data);
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
