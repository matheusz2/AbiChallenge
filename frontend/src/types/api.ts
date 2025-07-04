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
  email: string;
  username: string;
  password: string;
  name: {
    firstname: string;
    lastname: string;
  };
  address: {
    city: string;
    street: string;
    number: number;
    zipcode: string;
    geolocation: {
      lat: string;
      long: string;
    };
  };
  phone: string;
  status: number;
  role: number;
}

export interface UpdateUserRequest {
  id: string;
  username: string;
  email: string;
  phone: string;
  status: 1;
  role: 1;
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
  totalPrice: number;
}

export interface Sale {
  id: string;
  customerId: string;
  branchId: string;
  items: SaleItem[];
  subtotal: number;
  discountAmount: number;
  discountPercentage: number;
  total: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateSaleRequest {
  customerId: string;
  branchId: string;
  items: {
    productId: string;
    quantity: number;
    unitPrice: number;
  }[];
}

export interface UpdateSaleRequest {
  id: string;
  customerId: string;
  branchId: string;
  items: {
    productId: string;
    quantity: number;
    unitPrice: number;
  }[];
}

// Tipos para Cart
export interface CartItem {
  id: string;
  productId: number;
  quantity: number;
  unitPrice?: number;
}

export interface Cart {
  id: string;
  userId: number;
  date: string;
  products: CartItem[];
  items?: CartItem[];
}

export interface CreateCartRequest {
  userId: number;
  items: {
    productId: number;
    quantity: number;
  }[];
}

export interface UpdateCartRequest {
  id: string;
  userId: number;
  items: {
    productId: number;
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
  email: string;
  name: string;
  role: string;
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