import React, { useState, useEffect } from 'react';
import { Users, Package, Receipt, ShoppingCart, TrendingUp, TrendingDown, RefreshCw, UserPlus, DollarSign, ShoppingBag } from 'lucide-react';
import { dashboardService } from '../../services/api';
import type { DashboardStats } from '../../types/api';
import { useNavigate } from 'react-router-dom';
import DiscountRules from '../../components/DiscountRules';

const Dashboard: React.FC = () => {
  const [stats, setStats] = useState<DashboardStats>({
    totalUsers: 0,
    totalProducts: 0,
    totalSales: 0,
    activeCarts: 0
  });
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const fetchDashboardStats = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const dashboardStats = await dashboardService.getStats();
      setStats(dashboardStats);
    } catch (err) {
      console.error('Erro ao carregar estatísticas:', err);
      setError('Erro ao carregar estatísticas do dashboard');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchDashboardStats();
  }, []);

  const handleCreateUser = () => {
    navigate('/users');
  };

  const handleAddProduct = () => {
    navigate('/products');
  };

  const handleNewSale = () => {
    navigate('/sales');
  };

  const handleManageCarts = () => {
    navigate('/carts');
  };

  // Dados dos cards com valores reais
  const statsCards = [
    {
      name: 'Total de Usuários',
      value: stats.totalUsers.toLocaleString(),
      change: '+12%',
      changeType: 'positive' as const,
      icon: Users,
    },
    {
      name: 'Total de Produtos',
      value: stats.totalProducts.toLocaleString(),
      change: '+8%',
      changeType: 'positive' as const,
      icon: Package,
    },
    {
      name: 'Total de Vendas',
      value: stats.totalSales.toLocaleString(),
      change: '+15%',
      changeType: 'positive' as const,
      icon: Receipt,
    },
    {
      name: 'Carrinhos Ativos',
      value: stats.activeCarts.toLocaleString(),
      change: '-3%',
      changeType: 'negative' as const,
      icon: ShoppingCart,
    },
  ];

  if (isLoading) {
    return (
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Dashboard</h1>
            <p className="text-gray-600">Carregando estatísticas...</p>
          </div>
        </div>
        <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4">
          {[1, 2, 3, 4].map((i) => (
            <div key={i} className="bg-white overflow-hidden shadow rounded-lg animate-pulse">
              <div className="p-5">
                <div className="flex items-center">
                  <div className="flex-shrink-0">
                    <div className="h-6 w-6 bg-gray-300 rounded"></div>
                  </div>
                  <div className="ml-5 w-0 flex-1">
                    <div className="h-4 bg-gray-300 rounded w-3/4 mb-2"></div>
                    <div className="h-8 bg-gray-300 rounded w-1/2"></div>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Dashboard</h1>
          <p className="text-gray-600">Bem-vindo ao ABI Challenge</p>
        </div>
        <button
          onClick={fetchDashboardStats}
          disabled={isLoading}
          className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 disabled:opacity-50"
        >
          <RefreshCw className={`w-4 h-4 mr-2 ${isLoading ? 'animate-spin' : ''}`} />
          Atualizar
        </button>
      </div>

      {error && (
        <div className="bg-red-50 border border-red-200 rounded-md p-4">
          <div className="flex">
            <div className="ml-3">
              <h3 className="text-sm font-medium text-red-800">
                Erro ao carregar dados
              </h3>
              <div className="mt-2 text-sm text-red-700">
                <p>{error}</p>
              </div>
              <div className="mt-4">
                <button
                  onClick={fetchDashboardStats}
                  className="bg-red-100 px-3 py-2 rounded-md text-sm font-medium text-red-800 hover:bg-red-200"
                >
                  Tentar novamente
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Cards de estatísticas */}
      <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4">
        {statsCards.map((stat) => (
          <div key={stat.name} className="bg-white overflow-hidden shadow rounded-lg">
            <div className="p-5">
              <div className="flex items-center">
                <div className="flex-shrink-0">
                  <stat.icon className="h-6 w-6 text-gray-400" aria-hidden="true" />
                </div>
                <div className="ml-5 w-0 flex-1">
                  <dl>
                    <dt className="text-sm font-medium text-gray-500 truncate">
                      {stat.name}
                    </dt>
                    <dd className="flex items-baseline">
                      <div className="text-2xl font-semibold text-gray-900">
                        {stat.value}
                      </div>
                    </dd>
                  </dl>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Seção de resumo */}
      <div className="grid grid-cols-1 gap-5 lg:grid-cols-3">
        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <h3 className="text-lg leading-6 font-medium text-gray-900">
              Atividade Recente
            </h3>
            <div className="mt-5">
              <div className="space-y-3">
                <div className="flex items-center space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-8 w-8 rounded-full bg-green-100 flex items-center justify-center">
                      <Users className="h-4 w-4 text-green-600" />
                    </div>
                  </div>
                  <div className="flex-1">
                    <p className="text-sm text-gray-900">
                      Novo usuário registrado
                    </p>
                    <p className="text-xs text-gray-500">Há 2 minutos</p>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-8 w-8 rounded-full bg-blue-100 flex items-center justify-center">
                      <Receipt className="h-4 w-4 text-blue-600" />
                    </div>
                  </div>
                  <div className="flex-1">
                    <p className="text-sm text-gray-900">
                      Nova venda realizada
                    </p>
                    <p className="text-xs text-gray-500">Há 5 minutos</p>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-8 w-8 rounded-full bg-yellow-100 flex items-center justify-center">
                      <Package className="h-4 w-4 text-yellow-600" />
                    </div>
                  </div>
                  <div className="flex-1">
                    <p className="text-sm text-gray-900">
                      Produto atualizado
                    </p>
                    <p className="text-xs text-gray-500">Há 10 minutos</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <h3 className="text-lg leading-6 font-medium text-gray-900">
              Ações Rápidas
            </h3>
            <div className="mt-5">
              <div className="space-y-3">
                <button 
                  onClick={handleCreateUser}
                  className="w-full bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-md flex items-center justify-center"
                >
                  <Users className="w-4 h-4 mr-2" />
                  Gerenciar Usuários
                </button>
                <button 
                  onClick={handleAddProduct}
                  className="w-full bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-md flex items-center justify-center"
                >
                  <Package className="w-4 h-4 mr-2" />
                  Gerenciar Produtos
                </button>
                <button 
                  onClick={handleNewSale}
                  className="w-full bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md flex items-center justify-center"
                >
                  <Receipt className="w-4 h-4 mr-2" />
                  Gerenciar Vendas
                </button>
                <button 
                  onClick={handleManageCarts}
                  className="w-full bg-purple-600 hover:bg-purple-700 text-white px-4 py-2 rounded-md flex items-center justify-center"
                >
                  <ShoppingCart className="w-4 h-4 mr-2" />
                  Gerenciar Carrinhos
                </button>
              </div>
            </div>
          </div>
        </div>

        <div className="lg:col-span-1">
          <DiscountRules />
        </div>
      </div>
    </div>
  );
};

export default Dashboard; 