// Tipos base para as respostas da API
export interface ValidationErrorDetail {
  field: string;
  message: string;
}

export interface ApiResponse {
  success: boolean;
  message: string;
  errors?: ValidationErrorDetail[];
}

export interface ApiResponseWithData<T> extends ApiResponse {
  data: T;
}

export interface PaginatedResponse<T> {
  data: T[];
  currentPage: number;
  totalPages: number;
  totalCount: number;
}

// Tipos para User
export interface User {
  id: string;
  username: string;
  email: string;
  phone: string;
  status: 'Active' | 'Inactive' | 'Suspended';
  role: 'Admin' | 'Customer' | 'Manager';
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserRequest {
  username: string;
  email: string;
  phone: string;
  password: string;
  role: 'Admin' | 'Customer' | 'Manager';
}

export interface UpdateUserRequest {
  id: string;
  username: string;
  email: string;
  phone: string;
  status: 'Active' | 'Inactive' | 'Suspended';
  role: 'Admin' | 'Customer' | 'Manager';
}

// Tipos para Product
export interface Product {
  id: string;
  title: string;
  price: number;
  description: string;
  category: string;
  image: string;
  rating: {
    rate: number;
    count: number;
  };
}

export interface CreateProductRequest {
  title: string;
  price: number;
  description: string;
  category: string;
  image: string;
}

export interface UpdateProductRequest {
  id: string;
  title: string;
  price: number;
  description: string;
  category: string;
  image: string;
}

// Tipos para Sale
export interface SaleItem {
  id: string;
  productId: string;
  quantity: number;
  unitPrice: number;
  discount: number;
  totalAmount: number;
}

export interface Sale {
  id: string;
  saleNumber: string;
  customerId: string;
  customerName: string;
  saleDate: string;
  totalAmount: number;
  branch: string;
  items: SaleItem[];
  cancelled: boolean;
}

export interface CreateSaleRequest {
  customerId: string;
  branch: string;
  items: {
    productId: string;
    quantity: number;
    unitPrice: number;
  }[];
}

export interface UpdateSaleRequest {
  id: string;
  customerId: string;
  branch: string;
  items: {
    productId: string;
    quantity: number;
    unitPrice: number;
  }[];
}

// Tipos para Cart
export interface CartItem {
  id: string;
  productId: string;
  quantity: number;
  unitPrice: number;
}

export interface Cart {
  id: string;
  userId: string;
  date: string;
  items: CartItem[];
}

export interface CreateCartRequest {
  userId: string;
  items: {
    productId: string;
    quantity: number;
  }[];
}

export interface UpdateCartRequest {
  id: string;
  userId: string;
  items: {
    productId: string;
    quantity: number;
  }[];
}

// Tipos para Auth
export interface AuthenticateRequest {
  email: string;
  password: string;
}

export interface AuthenticateResponse {
  token: string;
  user: User;
}

// Tipos para paginação
export interface PaginationParams {
  page?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
}

// Dashboard Types
export interface DashboardStats {
  totalUsers: number;
  totalProducts: number;
  totalSales: number;
  activeCarts: number;
}

export interface DashboardResponse {
  stats: DashboardStats;
} 