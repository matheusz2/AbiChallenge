import React, { useState, useEffect } from 'react';
import { ShoppingCart, Plus, Edit, Trash2, Eye, Search, Filter, User, Package } from 'lucide-react';
import { cartService, productService } from '../../services/api';
import type { Cart, CartProduct, Product } from '../../types/api';
import Pagination from '../../components/Pagination/Pagination';
import Modal from '../../components/Modal/Modal';
import CartForm from './CartForm';

// Componente para exibir uma linha de produto com informações detalhadas
const ProductRow: React.FC<{ product: CartProduct }> = ({ product }) => {
  const [productInfo, setProductInfo] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadProductInfo = async () => {
      try {
        const response = await productService.getAll({ page: 1, pageSize: 100 });
        const products = response.data || [];
        
        const productFound = products.find((p, index) => (index + 1) === product.productId);
        setProductInfo(productFound || null);
      } catch (error) {
        console.error('Erro ao carregar informações do produto:', error);
        setProductInfo(null);
      } finally {
        setLoading(false);
      }
    };

    loadProductInfo();
  }, [product.productId]);

  if (loading) {
    return (
      <tr>
        <td className="px-4 py-2 text-sm text-gray-900">
          <div className="animate-pulse flex items-center">
            <Package className="h-4 w-4 text-gray-400 mr-2" />
            <span>Carregando...</span>
          </div>
        </td>
        <td className="px-4 py-2 text-sm text-gray-900">
          <div className="animate-pulse h-4 bg-gray-200 rounded w-24"></div>
        </td>
        <td className="px-4 py-2 text-sm text-gray-900">{product.quantity}</td>
      </tr>
    );
  }

  return (
    <tr>
      <td className="px-4 py-2 text-sm text-gray-900">
        <div className="flex items-start">
          <Package className="h-4 w-4 text-gray-400 mr-2 mt-0.5" />
          <div>
            <div className="font-medium text-gray-900">
              {productInfo ? productInfo.title : `Produto #${product.productId}`}
            </div>
            {productInfo && (
              <div className="text-xs text-gray-500">
                {productInfo.category} • ${productInfo.price}
              </div>
            )}
            {!productInfo && (
              <div className="text-xs text-red-500">
                Produto não encontrado
              </div>
            )}
          </div>
        </div>
      </td>
      <td className="px-4 py-2 text-sm text-gray-900">
        <div className="space-y-1">
          <div className="font-mono text-xs text-gray-600">
            Mapeado: #{product.productId}
          </div>
          {productInfo && (
            <div className="font-mono text-xs text-gray-500 break-all">
              GUID: {productInfo.id}
            </div>
          )}
        </div>
      </td>
      <td className="px-4 py-2 text-sm text-gray-900">
        <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-indigo-100 text-indigo-800">
          {product.quantity}
        </span>
      </td>
    </tr>
  );
};

const Carts: React.FC = () => {
  const [carts, setCarts] = useState<Cart[]>([]);
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
  const [selectedCart, setSelectedCart] = useState<Cart | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const pageSize = 10;

  const fetchCarts = async (page: number = 1) => {
    try {
      setLoading(true);
      setError(null);
      
      const response = await cartService.getAll({
        page,
        pageSize,
        sortBy: 'date',
        sortDescending: true
      });

      console.log('Carts response:', response);
      
      // response já é do tipo PaginatedResponse<Cart>
      setCarts(response.data || []);
      setCurrentPage(response.currentPage || 1);
      setTotalPages(response.totalPages || 1);
      setTotalCount(response.totalCount || 0);
    } catch (err) {
      console.error('Erro ao buscar carrinhos:', err);
      setError('Erro ao carregar carrinhos');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCarts(currentPage);
  }, [currentPage]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const handleCreateCart = async (cartData: any) => {
    try {
      setIsSubmitting(true);
      await cartService.create(cartData);
      setShowCreateModal(false);
      fetchCarts(currentPage);
    } catch (err) {
      console.error('Erro ao criar carrinho:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleUpdateCart = async (cartData: any) => {
    try {
      setIsSubmitting(true);
      await cartService.update(cartData);
      setShowEditModal(false);
      setSelectedCart(null);
      fetchCarts(currentPage);
    } catch (err) {
      console.error('Erro ao atualizar carrinho:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDeleteCart = async (cartId: string) => {
    if (!window.confirm('Tem certeza que deseja excluir este carrinho?')) {
      return;
    }

    try {
      await cartService.delete(cartId);
      fetchCarts(currentPage);
    } catch (err) {
      console.error('Erro ao excluir carrinho:', err);
      alert('Erro ao excluir carrinho');
    }
  };

  const handleViewCart = (cart: Cart) => {
    setSelectedCart(cart);
    setShowViewModal(true);
  };

  const handleEditCart = (cart: Cart) => {
    setSelectedCart(cart);
    setShowEditModal(true);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR');
  };

  const getCartProducts = (cart: Cart) => {
    const products = cart.products || [];
    if (!Array.isArray(products)) return [];
    return products;
  };

  const getTotalItems = (cart: Cart) => {
    const products = getCartProducts(cart);
    return products.reduce((total, product) => total + (product.quantity || 0), 0);
  };

  const filteredCarts = carts.filter(cart => {
    const matchesSearch = cart.userId.toString().includes(searchTerm) ||
                         cart.id.toLowerCase().includes(searchTerm.toLowerCase());
    
    return matchesSearch;
  });

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
            <h1 className="text-2xl font-bold text-gray-900">Carrinhos</h1>
            <p className="text-sm text-gray-600">Gerenciar carrinhos de compras</p>
          </div>
        </div>
        <button
          onClick={() => setShowCreateModal(true)}
          className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-md flex items-center"
        >
          <Plus className="h-4 w-4 mr-2" />
          Novo Carrinho
        </button>
      </div>

      {/* Filtros */}
      <div className="bg-white p-4 rounded-lg shadow mb-6">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              placeholder="Buscar carrinhos..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>

          <div className="flex items-center text-sm text-gray-600">
            <Filter className="h-4 w-4 mr-2" />
            {filteredCarts.length} de {totalCount} carrinhos
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
                  Carrinho
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Usuário
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Itens
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
              {filteredCarts.map((cart) => (
                <tr key={cart.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">#{cart.id.substring(0, 8)}...</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="flex items-center">
                      <User className="h-4 w-4 text-gray-400 mr-2" />
                      <span className="text-sm text-gray-900">Usuário #{cart.userId}</span>
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{getTotalItems(cart)} itens</div>
                    <div className="text-xs text-gray-500">{getCartProducts(cart).length} produtos únicos</div>
                    <div className="text-xs text-gray-400 mt-1">
                      IDs: {getCartProducts(cart).map(p => `#${p.productId}`).join(', ')}
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{formatDate(cart.date)}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div className="flex space-x-2">
                      <button
                        onClick={() => handleViewCart(cart)}
                        className="text-indigo-600 hover:text-indigo-900"
                        title="Visualizar"
                      >
                        <Eye className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleEditCart(cart)}
                        className="text-green-600 hover:text-green-900"
                        title="Editar"
                      >
                        <Edit className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleDeleteCart(cart.id)}
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

          {filteredCarts.length === 0 && (
            <div className="text-center py-12">
              <ShoppingCart className="mx-auto h-12 w-12 text-gray-400" />
              <h3 className="mt-2 text-sm font-medium text-gray-900">Nenhum carrinho encontrado</h3>
              <p className="mt-1 text-sm text-gray-500">
                {searchTerm
                  ? 'Tente ajustar os filtros de busca.'
                  : 'Comece criando um novo carrinho.'}
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
            onPageChange={handlePageChange}
          />
        </div>
      )}

      {/* Modal Criar Carrinho */}
      <Modal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        title="Criar Novo Carrinho"
        size="lg"
      >
        <CartForm
          onSubmit={handleCreateCart}
          onCancel={() => setShowCreateModal(false)}
          isLoading={isSubmitting}
        />
      </Modal>

      {/* Modal Editar Carrinho */}
      <Modal
        isOpen={showEditModal}
        onClose={() => {
          setShowEditModal(false);
          setSelectedCart(null);
        }}
        title="Editar Carrinho"
        size="lg"
      >
        {selectedCart && (
          <CartForm
            cart={selectedCart}
            onSubmit={handleUpdateCart}
            onCancel={() => {
              setShowEditModal(false);
              setSelectedCart(null);
            }}
            isLoading={isSubmitting}
          />
        )}
      </Modal>

      {/* Modal Visualizar Carrinho */}
      <Modal
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedCart(null);
        }}
        title="Detalhes do Carrinho"
        size="lg"
      >
        {selectedCart && (
          <div className="space-y-6">
            {/* Informações Gerais */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">ID do Carrinho</label>
                <p className="mt-1 text-sm text-gray-900 font-mono">{selectedCart.id}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Usuário</label>
                <p className="mt-1 text-sm text-gray-900">Usuário #{selectedCart.userId}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Data</label>
                <p className="mt-1 text-sm text-gray-900">{formatDate(selectedCart.date)}</p>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700">Total de Itens</label>
                <p className="mt-1 text-sm text-gray-900">{getTotalItems(selectedCart)} itens</p>
              </div>
            </div>

            {/* Produtos do Carrinho */}
            <div>
              <h3 className="text-lg font-medium text-gray-900 mb-4">Produtos do Carrinho</h3>
              {getCartProducts(selectedCart).length > 0 ? (
                <div className="overflow-x-auto">
                  <table className="min-w-full divide-y divide-gray-200">
                    <thead className="bg-gray-50">
                      <tr>
                        <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                          Produto
                        </th>
                        <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                          ID (GUID)
                        </th>
                        <th className="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase">
                          Quantidade
                        </th>
                      </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                      {getCartProducts(selectedCart).map((product, index) => (
                        <ProductRow key={index} product={product} />
                      ))}
                    </tbody>
                  </table>
                </div>
              ) : (
                <p className="text-sm text-gray-500 text-center py-4">Nenhum produto no carrinho</p>
              )}
            </div>
            
            <div className="flex justify-end space-x-3 pt-4">
              <button
                onClick={() => {
                  setShowViewModal(false);
                  setSelectedCart(null);
                }}
                className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
              >
                Fechar
              </button>
              <button
                onClick={() => {
                  setShowViewModal(false);
                  handleEditCart(selectedCart);
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

export default Carts; 