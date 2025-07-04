import { jwtDecode } from 'jwt-decode';
import type { User } from '../types/api';

// Interface para o payload do JWT token
// As claims do .NET seguem um padrão específico
interface JwtPayload {
  // Claims padrão do .NET
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string; // User ID
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string; // Username
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string; // Email
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string; // Role
  
  // Claims do token real (baseado no log)
  nameid?: string; // User ID
  unique_name?: string; // Username
  email?: string; // Email
  role?: string; // Role
  
  // Claims padrão do JWT
  sub?: string; // Subject (pode ser mapeado)
  name?: string; // Name (pode ser mapeado)
  exp: number; // Expiration time
  iat: number; // Issued at
  nbf?: number; // Not before
  [key: string]: any; // Outras propriedades que podem existir
}

/**
 * Decodifica um JWT token e extrai as informações do usuário
 * @param token - O JWT token
 * @returns Objeto User ou null se o token for inválido
 */
export const decodeUserFromToken = (token: string): User | null => {
  try {
    console.log('🔍 Decodificando token:', typeof token, token?.substring(0, 50) + '...');
    
    if (!token || typeof token !== 'string') {
      console.error('❌ Token inválido - não é string ou está vazio');
      return null;
    }
    
    const decoded = jwtDecode<JwtPayload>(token);
    console.log('📋 Token decodificado:', decoded);
    
    // Verifica se o token ainda é válido
    const currentTime = Date.now() / 1000;
    if (decoded.exp < currentTime) {
      console.warn('⏰ Token expirado');
      return null;
    }

    // Extrai as informações usando as claims do .NET ou fallback para claims padrão
    const userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || decoded.sub || decoded.nameid || '';
    const username = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || decoded.name || decoded.unique_name || '';
    const email = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || decoded.email || '';
    const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decoded.role || '';

    console.log('🔑 Claims extraídas:', { userId, username, email, role });

    // Extrai as informações do usuário do payload
    const user: User = {
      id: userId,
      username: username,
      email: email,
      phone: '', // Não está no token
      status: 'Active', // Assumimos que se o token é válido, o usuário está ativo
      role: role as 'Admin' | 'Customer' | 'Manager',
      createdAt: new Date(decoded.iat * 1000).toISOString(),
      updatedAt: new Date().toISOString()
    };

    console.log('👤 Usuário final:', user);
    return user;
  } catch (error) {
    console.error('❌ Erro ao decodificar token:', error);
    return null;
  }
};

/**
 * Verifica se um token JWT é válido (não expirado)
 * @param token - O JWT token
 * @returns true se o token for válido, false caso contrário
 */
export const isTokenValid = (token: string): boolean => {
  try {
    const decoded = jwtDecode<JwtPayload>(token);
    const currentTime = Date.now() / 1000;
    return decoded.exp > currentTime;
  } catch (error) {
    return false;
  }
};

/**
 * Extrai o tempo de expiração do token
 * @param token - O JWT token
 * @returns Data de expiração ou null se inválido
 */
export const getTokenExpiration = (token: string): Date | null => {
  try {
    const decoded = jwtDecode<JwtPayload>(token);
    return new Date(decoded.exp * 1000);
  } catch (error) {
    return null;
  }
}; 