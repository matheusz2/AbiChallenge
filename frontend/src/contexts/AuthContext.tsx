import React, { createContext, useContext, useState, useEffect, type ReactNode } from 'react';
import { authService } from '../services/api';
import { decodeUserFromToken, isTokenValid } from '../utils/jwt';
import type { User, AuthenticateRequest } from '../types/api';

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (credentials: AuthenticateRequest) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth deve ser usado dentro de um AuthProvider');
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      // Verifica se o token é válido e decodifica as informações do usuário
      if (isTokenValid(token)) {
        const decodedUser = decodeUserFromToken(token);
        if (decodedUser) {
          setUser(decodedUser);
        } else {
          // Token inválido, remove do localStorage
          localStorage.removeItem('token');
        }
      } else {
        // Token expirado, remove do localStorage
        localStorage.removeItem('token');
      }
    }
    setIsLoading(false);
  }, []);

  const login = async (credentials: AuthenticateRequest): Promise<void> => {
    try {
      const response = await authService.login(credentials);

      const responseData = response as any;
      let token: string;
      token = responseData.data.data.token;

      localStorage.setItem('token', token);

      const decodedUser = decodeUserFromToken(token);

      if (decodedUser) {
        setUser(decodedUser);
      } else {
        throw new Error('Token inválido recebido do servidor');
      }
    } catch (error) {
      throw error;
    }
  };

  const logout = () => {
    authService.logout();
    setUser(null);
  };

  const value: AuthContextType = {
    user,
    isAuthenticated: !!user,
    isLoading,
    login,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}; 