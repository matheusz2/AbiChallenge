import React from 'react';
import { Outlet, Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { 
  Users, 
  ShoppingCart, 
  Package, 
  Receipt, 
  LogOut,
  Home
} from 'lucide-react';

const Layout: React.FC = () => {
  const { logout, user } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const isActiveRoute = (path: string) => {
    return location.pathname === path;
  };

  const getLinkClasses = (path: string) => {
    const baseClasses = "whitespace-nowrap py-2 px-1 border-b-2 font-medium text-sm flex items-center";
    const activeClasses = "border-indigo-500 text-indigo-600";
    const inactiveClasses = "border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300";
    
    return `${baseClasses} ${isActiveRoute(path) ? activeClasses : inactiveClasses}`;
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <nav className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between h-16">
            <div className="flex">
              <div className="flex-shrink-0 flex items-center">
                <h1 className="text-xl font-bold text-gray-900">ABI Challenge</h1>
              </div>
              <div className="hidden sm:ml-6 sm:flex sm:space-x-8">
                <Link
                  to="/dashboard"
                  className={getLinkClasses('/dashboard')}
                >
                  <Home className="w-4 h-4 mr-2" />
                  Dashboard
                </Link>
                <Link
                  to="/users"
                  className={getLinkClasses('/users')}
                >
                  <Users className="w-4 h-4 mr-2" />
                  Usuários
                </Link>
                <Link
                  to="/products"
                  className={getLinkClasses('/products')}
                >
                  <Package className="w-4 h-4 mr-2" />
                  Produtos
                </Link>
                <Link
                  to="/sales"
                  className={getLinkClasses('/sales')}
                >
                  <Receipt className="w-4 h-4 mr-2" />
                  Vendas
                </Link>
                <Link
                  to="/carts"
                  className={getLinkClasses('/carts')}
                >
                  <ShoppingCart className="w-4 h-4 mr-2" />
                  Carrinhos
                </Link>
              </div>
            </div>
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <span className="text-gray-700 text-sm mr-4">
                  Olá, {user?.username || 'Usuário'}
                </span>
                <button
                  onClick={handleLogout}
                  className="bg-red-600 hover:bg-red-700 text-white px-3 py-2 rounded-md text-sm font-medium flex items-center"
                >
                  <LogOut className="w-4 h-4 mr-2" />
                  Sair
                </button>
              </div>
            </div>
          </div>
        </div>
      </nav>

      <main className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <div className="px-4 py-6 sm:px-0">
          <Outlet />
        </div>
      </main>
    </div>
  );
};

export default Layout; 