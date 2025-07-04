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
  success?: boolean;
  message?: string;
}

// Resposta de categorias do backend
export interface GetCategoriesResponse {
  categories: string[];
}

// Tipos para User
export interface User {
  id: string;
  username: string;
  email: string;
  phone: string;
  status: string; // Backend retorna como string (ex: "Active", "Inactive", "Suspended")
  role: string;   // Backend retorna como string (ex: "Customer", "Manager", "Admin")
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
  status: number; // 0=Unknown, 1=Active, 2=Inactive, 3=Suspended
  role: number;   // 0=None, 1=Customer, 2=Manager, 3=Admin
}

export interface UpdateUserRequest {
  id: string;
  email: string;
  username: string;
  password?: string; // Opcional na atualização
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
  status: number; // 0=Unknown, 1=Active, 2=Inactive, 3=Suspended
  role: number;   // 0=None, 1=Customer, 2=Manager, 3=Admin
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
  rating: {
    rate: number;
    count: number;
  };
}

export interface UpdateProductRequest {
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
export interface CartProduct {
  productId: number; // Backend usa int, apesar de Product.Id ser Guid
  quantity: number;
}

export interface Cart {
  id: string;
  userId: number;
  date: string;
  products: CartProduct[];
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateCartRequest {
  userId: number;
  date: string;
  products: {
    productId: number; // Backend usa int, apesar de Product.Id ser Guid
    quantity: number;
  }[];
}

export interface UpdateCartRequest {
  id: string;
  userId: number;
  date: string;
  products: {
    productId: number; // Backend usa int, apesar de Product.Id ser Guid
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
  _page?: number;
  _size?: number;
  _order?: string;
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