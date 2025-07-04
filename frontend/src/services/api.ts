import axios, { type AxiosResponse } from 'axios';
import { API_CONFIG } from '../config/api';
import { isTokenValid } from '../utils/jwt';
import type {
  ApiResponse,
  ApiResponseWithData,
  PaginatedResponse,
  User,
  CreateUserRequest,
  UpdateUserRequest,
  Product,
  CreateProductRequest,
  UpdateProductRequest,
  Sale,
  CreateSaleRequest,
  UpdateSaleRequest,
  Cart,
  CreateCartRequest,
  UpdateCartRequest,
  AuthenticateRequest,
  AuthenticateResponse,
  PaginationParams,
  DashboardStats,
  GetCategoriesResponse
} from '../types/api';

// Configuração base do axios
const api = axios.create({
  baseURL: API_CONFIG.BASE_URL,
  timeout: API_CONFIG.TIMEOUT,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para adicionar token de autenticação
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    // Verifica se o token ainda é válido antes de enviá-lo
    if (isTokenValid(token)) {
      config.headers.Authorization = `Bearer ${token}`;
    } else {
      // Token expirado, remove do localStorage e redireciona para login
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
  }
  return config;
});

// Interceptor para tratar respostas
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Serviços de Auth
export const authService = {
  login: async (credentials: AuthenticateRequest): Promise<ApiResponseWithData<AuthenticateResponse>> => {
    const response: AxiosResponse<ApiResponseWithData<AuthenticateResponse>> = await api.post('/auth', credentials);
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('token');
  },

  isAuthenticated: (): boolean => {
    return !!localStorage.getItem('token');
  },
};

// Serviços de Users
export const userService = {
  getAll: async (params?: PaginationParams): Promise<PaginatedResponse<User>> => {
    const response = await api.get('/users', { params });
    return response.data.data;
  },

  getById: async (id: string): Promise<User> => {
    const response: AxiosResponse<ApiResponseWithData<User>> = await api.get(`/users/${id}`);
    return response.data.data;
  },

  create: async (user: CreateUserRequest): Promise<User> => {
    const response: AxiosResponse<ApiResponseWithData<User>> = await api.post('/users', user);
    return response.data.data;
  },

  update: async (id: string, user: UpdateUserRequest): Promise<User> => {
    const response: AxiosResponse<ApiResponseWithData<User>> = await api.put(`/users/${id}`, user);
    return response.data.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/users/${id}`);
  },
};

// Serviços de Products
export const productService = {
  getAll: async (params?: PaginationParams): Promise<PaginatedResponse<Product>> => {
    const response = await api.get('/products', { params });
    return response.data.data;
  },

  getById: async (id: string): Promise<Product> => {
    const response: AxiosResponse<ApiResponseWithData<Product>> = await api.get(`/products/${id}`);
    return response.data.data;
  },

  create: async (product: CreateProductRequest): Promise<Product> => {
    const response: AxiosResponse<ApiResponseWithData<Product>> = await api.post('/products', product);
    return response.data.data;
  },

  update: async (id: string, product: UpdateProductRequest): Promise<Product> => {
    const response: AxiosResponse<ApiResponseWithData<Product>> = await api.put(`/products/${id}`, product);
    return response.data.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/products/${id}`);
  },

  getCategories: async (): Promise<string[]> => {
    const response: AxiosResponse<ApiResponseWithData<GetCategoriesResponse>> = await api.get('/products/categories');
    return response.data.data.categories;
  },

  getByCategory: async (category: string, params?: PaginationParams): Promise<PaginatedResponse<Product>> => {
    const response = await api.get(`/products/category/${category}`, { params });
    return response.data.data;
  },
};

// Serviços de Sales
export const saleService = {
  getAll: async (params?: PaginationParams): Promise<PaginatedResponse<Sale>> => {
    const response = await api.get('/sales', { params });
    return response.data.data;
  },

  getById: async (id: string): Promise<Sale> => {
    const response: AxiosResponse<ApiResponseWithData<Sale>> = await api.get(`/sales/${id}`);
    return response.data.data;
  },

  create: async (sale: CreateSaleRequest): Promise<Sale> => {
    const response: AxiosResponse<ApiResponseWithData<Sale>> = await api.post('/sales', sale);
    return response.data.data;
  },

  update: async (sale: UpdateSaleRequest): Promise<Sale> => {
    const response: AxiosResponse<ApiResponseWithData<Sale>> = await api.put('/sales', sale);
    return response.data.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/sales/${id}`);
  },
};

// Serviços de Carts
export const cartService = {
  getAll: async (params?: PaginationParams): Promise<PaginatedResponse<Cart>> => {
    const response = await api.get('/carts', { params });
    return response.data.data;
  },

  getById: async (id: string): Promise<Cart> => {
    const response: AxiosResponse<ApiResponseWithData<Cart>> = await api.get(`/carts/${id}`);
    return response.data.data;
  },

  create: async (cart: CreateCartRequest): Promise<Cart> => {
    const response: AxiosResponse<ApiResponseWithData<Cart>> = await api.post('/carts', cart);
    return response.data.data;
  },

  update: async (cart: UpdateCartRequest): Promise<Cart> => {
    const response: AxiosResponse<ApiResponseWithData<Cart>> = await api.put('/carts', cart);
    return response.data.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/carts/${id}`);
  },
};

// Serviços de Dashboard
export const dashboardService = {
  getStats: async (): Promise<DashboardStats> => {
    try {
      console.log('🔍 Iniciando busca de estatísticas do dashboard...');
      
      // Buscar dados de cada controlador em paralelo
      const [usersResponse, productsResponse, salesResponse, cartsResponse] = await Promise.all([
        // Buscar primeira página de usuários para obter total
        api.get('/users?page=1&pageSize=1'),
        // Buscar primeira página de produtos para obter total  
        api.get('/products?page=1&pageSize=1'),
        // Buscar primeira página de vendas para obter total
        api.get('/sales?page=1&pageSize=1'),
        // Buscar primeira página de carrinhos para obter total
        api.get('/carts?page=1&pageSize=1')
      ]);

      console.log('📊 Respostas recebidas:');
      console.log('Users response:', usersResponse.data);
      console.log('Products response:', productsResponse.data);
      console.log('Sales response:', salesResponse.data);
      console.log('Carts response:', cartsResponse.data);

      // A resposta tem estrutura: { data: { data: { currentPage, totalPages, totalCount, data: [...] } } }
      // Seguindo o padrão response.data.data.totalCount
      const stats = {
        totalUsers: usersResponse.data.data.totalCount || 0,
        totalProducts: productsResponse.data.data.totalCount || 0,
        totalSales: salesResponse.data.data.totalCount || 0,
        activeCarts: cartsResponse.data.data.totalCount || 0
      };

      console.log('📈 Estatísticas calculadas:', stats);

      return stats;
    } catch (error) {
      console.error('❌ Erro ao buscar estatísticas do dashboard:', error);
      
      // Retornar valores padrão em caso de erro
      return {
        totalUsers: 0,
        totalProducts: 0,
        totalSales: 0,
        activeCarts: 0
      };
    }
  }
};

export default api; 