import React, { useState, useEffect } from 'react';
import { Package, Plus, Edit, Trash2, Eye, Search, Filter, Star } from 'lucide-react';
import { productService } from '../../services/api';
import type { Product } from '../../types/api';
import Pagination from '../../components/Pagination/Pagination';
import Modal from '../../components/Modal/Modal';
import ProductForm from './ProductForm';

const Products: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [categoryFilter, setCategoryFilter] = useState('');
  const [categories, setCategories] = useState<string[]>([]);

  // Estados dos modais
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const pageSize = 12;

  const fetchProducts = async (page: number = 1) => {
    try {
      setLoading(true);
      setError(null);
      
      const response = await productService.getAll({
        page,
        pageSize,
        sortBy: 'title',
        sortDescending: false
      });

      console.log('Products response:', response);
      
      if (response.data && Array.isArray(response.data)) {
        setProducts(response.data);
        setCurrentPage(1);
        setTotalPages(1);
        setTotalCount(response.data.length);
      } else if (response.data && typeof response.data === 'object') {
        setProducts(response.data.data || []);
        setCurrentPage(response.data.currentPage || 1);
        setTotalPages(response.data.totalPages || 1);
        setTotalCount(response.data.totalCount || 0);
      }
    } catch (err) {
      console.error('Erro ao buscar produtos:', err);
      setError('Erro ao carregar produtos');
    } finally {
      setLoading(false);
    }
  };

  const fetchCategories = async () => {
    try {
      const response = await productService.getCategories();
      console.log('Categories response:', response);
      
      if (response.data && Array.isArray(response.data)) {
        setCategories(response.data);
      } else if (response.data && response.data.data) {
        setCategories(response.data.data);
      }
    } catch (err) {
      console.error('Erro ao buscar categorias:', err);
    }
  };

  useEffect(() => {
    fetchProducts(currentPage);
    fetchCategories();
  }, [currentPage]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const handleCreateProduct = async (productData: any) => {
    try {
      setIsSubmitting(true);
      await productService.create(productData);
      setShowCreateModal(false);
      fetchProducts(currentPage);
    } catch (err) {
      console.error('Erro ao criar produto:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleUpdateProduct = async (productData: any) => {
    try {
      setIsSubmitting(true);
      await productService.update(productData.id, productData);
      setShowEditModal(false);
      setSelectedProduct(null);
      fetchProducts(currentPage);
    } catch (err) {
      console.error('Erro ao atualizar produto:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDeleteProduct = async (productId: string) => {
    if (!window.confirm('Tem certeza que deseja excluir este produto?')) {
      return;
    }

    try {
      await productService.delete(productId);
      fetchProducts(currentPage);
    } catch (err) {
      console.error('Erro ao excluir produto:', err);
      alert('Erro ao excluir produto');
    }
  };

  const handleViewProduct = (product: Product) => {
    setSelectedProduct(product);
    setShowViewModal(true);
  };

  const handleEditProduct = (product: Product) => {
    setSelectedProduct(product);
    setShowEditModal(true);
  };

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(price);
  };

  const filteredProducts = products.filter(product => {
    const matchesSearch = product.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         product.description.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesCategory = categoryFilter === '' || product.category === categoryFilter;
    
    return matchesSearch && matchesCategory;
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
          <Package className="h-8 w-8 text-indigo-600 mr-3" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Produtos</h1>
            <p className="text-sm text-gray-600">Gerenciar produtos do catálogo</p>
          </div>
        </div>
        <button
          onClick={() => setShowCreateModal(true)}
          className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-md flex items-center"
        >
          <Plus className="h-4 w-4 mr-2" />
          Novo Produto
        </button>
      </div>

      {/* Filtros */}
      <div className="bg-white p-4 rounded-lg shadow mb-6">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              placeholder="Buscar produtos..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>
          
          <select
            value={categoryFilter}
            onChange={(e) => setCategoryFilter(e.target.value)}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Todas as Categorias</option>
            {categories.map(category => (
              <option key={category} value={category}>{category}</option>
            ))}
          </select>

          <div className="flex items-center text-sm text-gray-600">
            <Filter className="h-4 w-4 mr-2" />
            {filteredProducts.length} de {totalCount} produtos
          </div>
        </div>
      </div>

      {/* Grid de Produtos */}
      {error ? (
        <div className="bg-red-50 border border-red-200 rounded-md p-4">
          <p className="text-red-600">{error}</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {filteredProducts.map((product) => (
            <div key={product.id} className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow">
              {/* Imagem */}
              <div className="aspect-w-1 aspect-h-1 w-full overflow-hidden">
                <img
                  src={product.image || 'https://via.placeholder.com/300x300?text=Produto'}
                  alt={product.title}
                  className="w-full h-48 object-cover"
                  onError={(e) => {
                    e.currentTarget.src = 'https://via.placeholder.com/300x300?text=Produto';
                  }}
                />
              </div>

              {/* Conteúdo */}
              <div className="p-4">
                <div className="flex justify-between items-start mb-2">
                  <h3 className="text-lg font-semibold text-gray-900 truncate">{product.title}</h3>
                  <span className="text-lg font-bold text-indigo-600">{formatPrice(product.price)}</span>
                </div>
                
                <p className="text-sm text-gray-600 mb-2 line-clamp-2">{product.description}</p>
                
                <div className="flex items-center justify-between mb-4">
                  <span className="inline-flex px-2 py-1 text-xs font-semibold rounded-full bg-gray-100 text-gray-800">
                    {product.category}
                  </span>
                  
                  {product.rating && (
                    <div className="flex items-center">
                      <Star className="h-4 w-4 text-yellow-400 fill-current" />
                      <span className="text-sm text-gray-600 ml-1">
                        {product.rating.rate} ({product.rating.count})
                      </span>
                    </div>
                  )}
                </div>

                {/* Ações */}
                <div className="flex space-x-2">
                  <button
                    onClick={() => handleViewProduct(product)}
                    className="flex-1 bg-indigo-50 text-indigo-600 hover:bg-indigo-100 px-3 py-2 rounded-md text-sm font-medium flex items-center justify-center"
                    title="Visualizar"
                  >
                    <Eye className="h-4 w-4 mr-1" />
                    Ver
                  </button>
                  <button
                    onClick={() => handleEditProduct(product)}
                    className="flex-1 bg-green-50 text-green-600 hover:bg-green-100 px-3 py-2 rounded-md text-sm font-medium flex items-center justify-center"
                    title="Editar"
                  >
                    <Edit className="h-4 w-4 mr-1" />
                    Editar
                  </button>
                  <button
                    onClick={() => handleDeleteProduct(product.id)}
                    className="bg-red-50 text-red-600 hover:bg-red-100 px-3 py-2 rounded-md text-sm font-medium flex items-center justify-center"
                    title="Excluir"
                  >
                    <Trash2 className="h-4 w-4" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Mensagem quando não há produtos */}
      {filteredProducts.length === 0 && !loading && (
        <div className="text-center py-12">
          <Package className="mx-auto h-12 w-12 text-gray-400" />
          <h3 className="mt-2 text-sm font-medium text-gray-900">Nenhum produto encontrado</h3>
          <p className="mt-1 text-sm text-gray-500">
            {searchTerm || categoryFilter
              ? 'Tente ajustar os filtros de busca.'
              : 'Comece criando um novo produto.'}
          </p>
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

      {/* Modal Criar Produto */}
      <Modal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        title="Criar Novo Produto"
        size="lg"
      >
        <ProductForm
          onSubmit={handleCreateProduct}
          onCancel={() => setShowCreateModal(false)}
          isLoading={isSubmitting}
        />
      </Modal>

      {/* Modal Editar Produto */}
      <Modal
        isOpen={showEditModal}
        onClose={() => {
          setShowEditModal(false);
          setSelectedProduct(null);
        }}
        title="Editar Produto"
        size="lg"
      >
        {selectedProduct && (
          <ProductForm
            product={selectedProduct}
            onSubmit={handleUpdateProduct}
            onCancel={() => {
              setShowEditModal(false);
              setSelectedProduct(null);
            }}
            isLoading={isSubmitting}
          />
        )}
      </Modal>

      {/* Modal Visualizar Produto */}
      <Modal
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedProduct(null);
        }}
        title="Detalhes do Produto"
        size="lg"
      >
        {selectedProduct && (
          <div className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {/* Imagem */}
              <div>
                <img
                  src={selectedProduct.image || 'https://via.placeholder.com/300x300?text=Produto'}
                  alt={selectedProduct.title}
                  className="w-full h-64 object-cover rounded-lg"
                  onError={(e) => {
                    e.currentTarget.src = 'https://via.placeholder.com/300x300?text=Produto';
                  }}
                />
              </div>

              {/* Informações */}
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">Título</label>
                  <p className="mt-1 text-lg font-semibold text-gray-900">{selectedProduct.title}</p>
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">Preço</label>
                  <p className="mt-1 text-2xl font-bold text-indigo-600">{formatPrice(selectedProduct.price)}</p>
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">Categoria</label>
                  <span className="mt-1 inline-flex px-2 py-1 text-sm font-semibold rounded-full bg-gray-100 text-gray-800">
                    {selectedProduct.category}
                  </span>
                </div>
                
                {selectedProduct.rating && (
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Avaliação</label>
                    <div className="mt-1 flex items-center">
                      <Star className="h-5 w-5 text-yellow-400 fill-current" />
                      <span className="ml-2 text-sm text-gray-600">
                        {selectedProduct.rating.rate} ({selectedProduct.rating.count} avaliações)
                      </span>
                    </div>
                  </div>
                )}
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">ID</label>
                  <p className="mt-1 text-sm text-gray-900 font-mono">{selectedProduct.id}</p>
                </div>
              </div>
            </div>
            
            <div>
              <label className="block text-sm font-medium text-gray-700">Descrição</label>
              <p className="mt-1 text-sm text-gray-900">{selectedProduct.description}</p>
            </div>
            
            <div className="flex justify-end space-x-3 pt-4">
              <button
                onClick={() => {
                  setShowViewModal(false);
                  setSelectedProduct(null);
                }}
                className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
              >
                Fechar
              </button>
              <button
                onClick={() => {
                  setShowViewModal(false);
                  handleEditProduct(selectedProduct);
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

export default Products; 