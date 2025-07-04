import React, { useState, useEffect } from 'react';
import { ShoppingCart, Plus, Edit, Trash2, Eye, Search, Filter, DollarSign, Building, Percent } from 'lucide-react';
import { saleService, productService, userService } from '../../services/api';
import type { Sale, Product, User } from '../../types/api';
import Pagination from '../../components/Pagination/Pagination';
import Modal from '../../components/Modal/Modal';
import SaleForm from './SaleForm';
import CreateSaleModal from '../../components/CreateSaleModal';

// Dados mockados para filiais
interface Branch {
  id: string;
  name: string;
  city: string;
}

const BRANCHES: Branch[] = [
  {
    id: '9b2f2719-4325-46cb-a7d5-f9a05058dc93',
    name: 'Filial Belo Horizonte',
    city: 'Belo Horizonte'
  },
  {
    id: '9b2f2719-4325-46cb-a7d5-f9a05058dc92',
    name: 'Filial Rio de Janeiro',
    city: 'Rio de Janeiro'
  }
];

const Sales: React.FC = () => {
  const [sales, setSales] = useState<Sale[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');

  // Estados dos modais
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedSale, setSelectedSale] = useState<Sale | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  // Estados para criação de venda
  const [products, setProducts] = useState<Product[]>([]);
  const [users, setUsers] = useState<User[]>([]);

  const pageSize = 10;

  const fetchSales = async (page: number = 1) => {
    try {
      setLoading(true);
      setError(null);
      const response = await saleService.getAll({
        _page: page,
        _size: pageSize,
        _order: 'createdAt'
      });

      if (response?.data?.data && Array.isArray(response.data.data)) {
        setSales(response.data.data);
        setTotalPages(response.data.totalPages);
        setTotalCount(response.data.totalCount);
      } else {
        console.error('Resposta inválida da API:', response);
        setError('Erro ao carregar vendas: formato de resposta inválido');
      }
    } catch (err) {
      console.error('Erro ao buscar vendas:', err);
      setError('Erro ao carregar vendas');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSales(currentPage);
  }, [currentPage]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const handleCreateSale = async (saleData: any) => {
    try {
      setIsSubmitting(true);
      console.log('Criando venda com dados:', saleData);
      
      // Validar payload antes do envio
      if (!saleData.customerId || !saleData.branchId || !saleData.items || saleData.items.length === 0) {
        throw new Error('Dados da venda incompletos');
      }

      // Validar cada item
      saleData.items.forEach((item: any, index: number) => {
        if (!item.productId || item.quantity <= 0 || item.unitPrice <= 0) {
          throw new Error(`Item ${index + 1} inválido`);
        }
      });

      await saleService.create(saleData);
      setShowCreateModal(false);
      fetchSales(currentPage);
    } catch (err) {
      console.error('Erro ao criar venda:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleUpdateSale = async (saleData: any) => {
    try {
      setIsSubmitting(true);
      await saleService.update(saleData);
      setShowEditModal(false);
      setSelectedSale(null);
      fetchSales(currentPage);
    } catch (err) {
      console.error('Erro ao atualizar venda:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDeleteSale = async (saleId: string) => {
    if (!window.confirm('Tem certeza que deseja excluir esta venda?')) {
      return;
    }

    try {
      await saleService.delete(saleId);
      fetchSales(currentPage);
    } catch (err) {
      console.error('Erro ao excluir venda:', err);
      alert('Erro ao excluir venda');
    }
  };

  const handleViewSale = (sale: Sale) => {
    setSelectedSale(sale);
    setShowViewModal(true);
  };

  const handleEditSale = (sale: Sale) => {
    setSelectedSale(sale);
    setShowEditModal(true);
  };

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(price);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleString('pt-BR');
  };

  // Função para obter o nome do usuário pelo customerId
  const getUserName = (customerId: string) => {
    const user = users.find(u => u.id === customerId);
    if (user) {
      return `${user.name.firstname} ${user.name.lastname}`;
    }
    return customerId; // Fallback para o ID se não encontrar o usuário
  };

  // Função para obter o nome da filial pelo branchId
  const getBranchName = (branchId: string) => {
    const branch = BRANCHES.find(b => b.id === branchId);
    if (branch) {
      return branch.name;
    }
    return branchId; // Fallback para o ID se não encontrar a filial
  };

  // Função para calcular desconto por item
  const calculateItemDiscount = (quantity: number, unitPrice: number) => {
    let discountPercentage = 0;
    if (quantity >= 10 && quantity <= 20) {
      discountPercentage = 20;
    } else if (quantity >= 4) {
      discountPercentage = 10;
    }
    
    const itemTotal = quantity * unitPrice;
    const discountAmount = (itemTotal * discountPercentage) / 100;
    const totalWithDiscount = itemTotal - discountAmount;
    
    return {
      discountPercentage,
      discountAmount,
      totalWithDiscount,
      itemTotal
    };
  };

  const filteredSales = sales.filter(sale => {
    const customerName = getUserName(sale.customerId).toLowerCase();
    const branchName = getBranchName(sale.branchId).toLowerCase();
    const matchesSearch = customerName.includes(searchTerm.toLowerCase()) ||
                         branchName.includes(searchTerm.toLowerCase()) ||
                         sale.id.toLowerCase().includes(searchTerm.toLowerCase());
    
    return matchesSearch;
  });

  // Carregar dados necessários para criação de venda
  useEffect(() => {
    const loadSaleData = async () => {
      try {
        // Carregar produtos
        const productsResponse = await productService.getAll({ _page: 1, _size: 100 });
        setProducts(productsResponse.data?.data || []);

        // Carregar usuários (clientes)
        const usersResponse = await userService.getAll({ _page: 1, _size: 100 });
        setUsers(usersResponse.data?.data || []);
        
        console.log('Usuários carregados:', usersResponse.data?.data);
      } catch (error) {
        console.error('Erro ao carregar dados para venda:', error);
      }
    };

    if (showCreateModal) {
      loadSaleData();
    }
  }, [showCreateModal]);

  if (loading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-indigo-600"></div>
      </div>
    );
  }

  return (
    <div className="p-6">
      {/* Header */}
      <div className="flex justify-between items-center mb-6">
        <div className="flex items-center">
          <ShoppingCart className="h-8 w-8 text-indigo-600 mr-3" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Vendas</h1>
            <p className="text-sm text-gray-600">Gerenciar vendas do sistema</p>
          </div>
        </div>
        <button
          onClick={() => setShowCreateModal(true)}
          className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-md flex items-center"
        >
          <Plus className="h-4 w-4 mr-2" />
          Nova Venda
        </button>
      </div>

      {/* Filtros */}
      <div className="bg-white p-4 rounded-lg shadow mb-6">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              placeholder="Buscar por cliente, filial ou ID..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>

          <div className="flex items-center text-sm text-gray-600">
            <Filter className="h-4 w-4 mr-2" />
            {filteredSales.length} de {totalCount} vendas
          </div>
        </div>
      </div>

      {/* Tabela */}
      {error ? (
        <div className="bg-red-50 border border-red-200 rounded-md p-4">
          <p className="text-red-600">{error}</p>
        </div>
      ) : (
        <div className="bg-white shadow rounded-lg overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Venda
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Cliente
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Filial
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Itens
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Total
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Data
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Ações
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {filteredSales.map((sale) => (
                <tr key={sale.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">#{sale.id.substring(0, 8)}...</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{getUserName(sale.customerId)}</div>
                    <div className="text-xs text-gray-500">{sale.customerId}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{getBranchName(sale.branchId)}</div>
                    <div className="text-xs text-gray-500">{sale.branchId}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{sale.items?.length || 0} itens</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">{formatPrice(sale.total || 0)}</div>
                    {sale.discountAmount && sale.discountAmount > 0 && (
                      <div className="text-xs text-green-600">
                        Desconto: {formatPrice(sale.discountAmount)}
                      </div>
                    )}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{formatDate(sale.createdAt)}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div className="flex space-x-2">
                      <button
                        onClick={() => handleViewSale(sale)}
                        className="text-indigo-600 hover:text-indigo-900"
                        title="Visualizar"
                      >
                        <Eye className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleEditSale(sale)}
                        className="text-green-600 hover:text-green-900"
                        title="Editar"
                      >
                        <Edit className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleDeleteSale(sale.id)}
                        className="text-red-600 hover:text-red-900"
                        title="Excluir"
                      >
                        <Trash2 className="h-4 w-4" />
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {filteredSales.length === 0 && (
            <div className="text-center py-12">
              <ShoppingCart className="mx-auto h-12 w-12 text-gray-400" />
              <h3 className="mt-2 text-sm font-medium text-gray-900">Nenhuma venda encontrada</h3>
              <p className="mt-1 text-sm text-gray-500">
                {searchTerm
                  ? 'Tente ajustar os filtros de busca.'
                  : 'Comece criando uma nova venda.'}
              </p>
            </div>
          )}
        </div>
      )}

      {/* Paginação */}
      {totalPages > 1 && (
        <div className="mt-6">
          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            totalCount={totalCount}
            pageSize={pageSize}
            onPageChange={handlePageChange}
          />
        </div>
      )}

      {/* Modal Criar Venda */}
      <CreateSaleModal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        products={products}
        customers={users.map(user => ({
          id: user.id,
          name: `${user.name.firstname} ${user.name.lastname}`,
          email: user.email
        }))}
        branches={BRANCHES}
        onCreateSale={handleCreateSale}
        isLoading={isSubmitting}
      />

      {/* Modal Editar Venda */}
      <Modal
        isOpen={showEditModal}
        onClose={() => {
          setShowEditModal(false);
          setSelectedSale(null);
        }}
        title="Editar Venda"
        size="xl"
      >
        {selectedSale && (
          <SaleForm
            sale={selectedSale}
            onSubmit={handleUpdateSale}
            onCancel={() => {
              setShowEditModal(false);
              setSelectedSale(null);
            }}
            isLoading={isSubmitting}
          />
        )}
      </Modal>

      {/* Modal Visualizar Venda */}
      <Modal
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedSale(null);
        }}
        title="Detalhes da Venda"
        size="lg"
      >
        {selectedSale && (
          <div className="space-y-6">
            {/* Informações Gerais */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">ID da Venda</label>
                <p className="mt-1 text-sm text-gray-900 font-mono">{selectedSale.id}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Cliente</label>
                <p className="mt-1 text-sm text-gray-900">{getUserName(selectedSale.customerId)}</p>
                <p className="text-xs text-gray-500">{selectedSale.customerId}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Filial</label>
                <p className="mt-1 text-sm text-gray-900">{getBranchName(selectedSale.branchId)}</p>
                <p className="text-xs text-gray-500">{selectedSale.branchId}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Data</label>
                <p className="mt-1 text-sm text-gray-900">{formatDate(selectedSale.createdAt)}</p>
              </div>
            </div>

            {/* Itens da Venda */}
            <div>
              <h3 className="text-lg font-medium text-gray-900 mb-4">Itens da Venda</h3>
              <div className="overflow-x-auto">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead className="bg-gray-50">
                    <tr>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Produto
                      </th>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Quantidade
                      </th>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Preço Unit.
                      </th>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Subtotal
                      </th>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Desconto
                      </th>
                      <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                        Total
                      </th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    {selectedSale.items?.map((item, index) => {
                      const discountInfo = calculateItemDiscount(item.quantity, item.unitPrice);
                      
                      return (
                        <tr key={index}>
                          <td className="px-4 py-2 text-sm text-gray-900">{item.productId}</td>
                          <td className="px-4 py-2 text-sm text-gray-900">{item.quantity}</td>
                          <td className="px-4 py-2 text-sm text-gray-900">{formatPrice(item.unitPrice)}</td>
                          <td className="px-4 py-2 text-sm text-gray-900">
                            {formatPrice(discountInfo.itemTotal)}
                          </td>
                          <td className="px-4 py-2 text-sm text-gray-900">
                            {discountInfo.discountPercentage > 0 ? (
                              <div className="flex items-center gap-1">
                                <Percent className="w-3 h-3 text-green-600" />
                                <span className="text-green-600 font-medium">
                                  {discountInfo.discountPercentage}% (-{formatPrice(discountInfo.discountAmount)})
                                </span>
                              </div>
                            ) : (
                              <span className="text-gray-400">-</span>
                            )}
                          </td>
                          <td className="px-4 py-2 text-sm font-medium text-gray-900">
                            {formatPrice(discountInfo.totalWithDiscount)}
                          </td>
                        </tr>
                      );
                    })}
                  </tbody>
                </table>
              </div>
            </div>

            {/* Resumo dos Descontos */}
            {selectedSale.items && selectedSale.items.some(item => {
              const discountInfo = calculateItemDiscount(item.quantity, item.unitPrice);
              return discountInfo.discountPercentage > 0;
            }) && (
              <div className="bg-blue-50 p-4 rounded-lg">
                <h4 className="text-sm font-semibold text-blue-800 mb-2 flex items-center gap-2">
                  <Percent className="w-4 h-4" />
                  Resumo dos Descontos Aplicados
                </h4>
                <div className="space-y-2">
                  {selectedSale.items.map((item, index) => {
                    const discountInfo = calculateItemDiscount(item.quantity, item.unitPrice);
                    if (discountInfo.discountPercentage > 0) {
                      return (
                        <div key={index} className="flex justify-between text-sm">
                          <span className="text-blue-700">
                            Item {index + 1} ({item.quantity} unidades):
                          </span>
                          <span className="text-blue-800 font-medium">
                            {discountInfo.discountPercentage}% de desconto (-{formatPrice(discountInfo.discountAmount)})
                          </span>
                        </div>
                      );
                    }
                    return null;
                  })}
                </div>
              </div>
            )}

            {/* Totais */}
            <div className="bg-gray-50 p-4 rounded-lg">
              <div className="space-y-2">
                {selectedSale.subtotal && (
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Subtotal:</span>
                    <span className="text-sm font-medium">{formatPrice(selectedSale.subtotal)}</span>
                  </div>
                )}
                {selectedSale.discountAmount && selectedSale.discountAmount > 0 && (
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">
                      Desconto Total ({selectedSale.discountPercentage}%):
                    </span>
                    <span className="text-sm font-medium text-green-600">
                      -{formatPrice(selectedSale.discountAmount)}
                    </span>
                  </div>
                )}
                <div className="flex justify-between border-t pt-2">
                  <span className="text-lg font-medium text-gray-900">Total:</span>
                  <span className="text-lg font-bold text-indigo-600">
                    {formatPrice(selectedSale.total || 0)}
                  </span>
                </div>
              </div>
            </div>
            
            <div className="flex justify-end space-x-3 pt-4">
              <button
                onClick={() => {
                  setShowViewModal(false);
                  setSelectedSale(null);
                }}
                className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
              >
                Fechar
              </button>
              <button
                onClick={() => {
                  setShowViewModal(false);
                  handleEditSale(selectedSale);
                }}
                className="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700"
              >
                Editar
              </button>
            </div>
          </div>
        )}
      </Modal>
    </div>
  );
};

export default Sales; 