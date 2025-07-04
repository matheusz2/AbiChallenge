import React, { useState, useEffect } from 'react';
import { ShoppingCart, User, Plus, Trash2, Package, Users, AlertCircle } from 'lucide-react';
import type { Cart, CreateCartRequest, UpdateCartRequest, CartProduct, Product, User as UserType } from '../../types/api';
import { productService, userService } from '../../services/api';

interface CartFormProps {
  cart?: Cart;
  onSubmit: (data: any) => Promise<void>;
  onCancel: () => void;
  isLoading: boolean;
}

const CartForm: React.FC<CartFormProps> = ({ cart, onSubmit, onCancel, isLoading }) => {
  const [formData, setFormData] = useState({
    userId: 0,
    date: new Date().toISOString(),
    products: [{ productId: 0, quantity: 1 }]
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [availableProducts, setAvailableProducts] = useState<Product[]>([]);
  const [availableUsers, setAvailableUsers] = useState<UserType[]>([]);
  const [loadingData, setLoadingData] = useState(true);

  // Mapeamento Product.Id (Guid) -> número sequencial para usar no backend
  const [productIdMapping, setProductIdMapping] = useState<{ [guid: string]: number }>({});
  const [reverseProductMapping, setReverseProductMapping] = useState<{ [num: number]: string }>({});
  const [duplicateProductError, setDuplicateProductError] = useState<string>('');

  useEffect(() => {
    loadInitialData();
  }, []);

  useEffect(() => {
    if (cart) {
      setFormData({
        userId: cart.userId || 0,
        date: cart.date || new Date().toISOString(),
        products: cart.products?.map(product => ({
          productId: product.productId || 0,
          quantity: product.quantity || 1
        })) || [{ productId: 0, quantity: 1 }]
      });
    }
  }, [cart]);

  const loadInitialData = async () => {
    try {
      setLoadingData(true);
      
      console.log('Carregando dados iniciais...');
      
      // Buscar produtos e usuários em paralelo
      const [productsResponse, usersResponse] = await Promise.all([
        productService.getAll({ _page: 1, _size: 100 }),
        userService.getAll({ _page: 1, _size: 100 })
      ]);

      console.log('Products response:', productsResponse);
      console.log('Users response:', usersResponse);

      const products = productsResponse.data?.data || [];
      const users = usersResponse.data?.data || [];

      console.log('Products array:', products);
      console.log('Users array:', users);

      // Criar mapeamento entre Product.Id (Guid) e números sequenciais
      const guidToNumberMapping: { [guid: string]: number } = {};
      const numberToGuidMapping: { [num: number]: string } = {};
      
      products.forEach((product, index) => {
        const numericId = index + 1; // Começar do 1
        guidToNumberMapping[product.id] = numericId;
        numberToGuidMapping[numericId] = product.id;
      });

      console.log('Product mappings:', { guidToNumberMapping, numberToGuidMapping });

      setAvailableProducts(products);
      setAvailableUsers(users);
      setProductIdMapping(guidToNumberMapping);
      setReverseProductMapping(numberToGuidMapping);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoadingData(false);
    }
  };

  const validateForm = () => {
    const newErrors: Record<string, string> = {};

    if (!formData.userId || formData.userId <= 0) {
      newErrors.userId = 'Usuário é obrigatório';
    }
    
    if (formData.products.length === 0) {
      newErrors.products = 'Pelo menos um produto é obrigatório';
    } else {
      // Verificar produtos duplicados
      const productIds = formData.products.map(p => p.productId).filter(id => id > 0);
      const uniqueProductIds = new Set(productIds);
      
      if (productIds.length !== uniqueProductIds.size) {
        newErrors.products = 'Não é permitido adicionar o mesmo produto mais de uma vez';
      }
      
      formData.products.forEach((product, index) => {
        if (!product.productId || product.productId <= 0) {
          newErrors[`product_${index}_productId`] = 'Produto é obrigatório';
        }
        if (product.quantity <= 0) {
          newErrors[`product_${index}_quantity`] = 'Quantidade deve ser maior que 0';
        }
      });
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) return;

    try {
      const cartData = {
        userId: formData.userId,
        date: formData.date,
        products: formData.products.map(product => ({
          productId: product.productId,
          quantity: product.quantity
        }))
      };

      if (cart) {
        // Edição
        await onSubmit({
          id: cart.id,
          ...cartData
        });
      } else {
        // Criação
        await onSubmit(cartData);
      }
    } catch (error) {
      console.error('Erro ao salvar carrinho:', error);
    }
  };

  const addProduct = () => {
    setFormData(prev => ({
      ...prev,
      products: [...prev.products, { productId: 0, quantity: 1 }]
    }));
  };

  const removeProduct = (index: number) => {
    if (formData.products.length > 1) {
      setFormData(prev => ({
        ...prev,
        products: prev.products.filter((_, i) => i !== index)
      }));
    }
  };

  const updateProduct = (index: number, field: string, value: any) => {
    setFormData(prev => {
      const newProducts = prev.products.map((product, i) => 
        i === index ? { ...product, [field]: value } : product
      );

      // Verificar se há produtos duplicados
      const productIds = newProducts.map(p => p.productId).filter(id => id > 0);
      const uniqueProductIds = new Set(productIds);
      
      if (productIds.length !== uniqueProductIds.size) {
        // Há produtos duplicados, não permitir a atualização
        const duplicateProduct = getProductInfo(value);
        const errorMessage = `Produto "${duplicateProduct?.title || 'Desconhecido'}" já foi adicionado ao carrinho. Não é permitido adicionar o mesmo produto mais de uma vez.`;
        setDuplicateProductError(errorMessage);
        
        // Limpar o erro após 3 segundos
        setTimeout(() => setDuplicateProductError(''), 3000);
        
        return prev;
      }

      // Limpar erro se não há duplicados
      setDuplicateProductError('');
      
      return {
        ...prev,
        products: newProducts
      };
    });
  };

  const getProductInfo = (productId: number) => {
    const productGuid = reverseProductMapping[productId];
    if (!productGuid) return null;
    return availableProducts.find(p => p.id === productGuid);
  };

  const getProductName = (productId: number) => {
    const product = getProductInfo(productId);
    return product ? `${product.title} - $${product.price}` : 'Produto não encontrado';
  };

  const getAvailableProductsForIndex = (currentIndex: number) => {
    // Retorna apenas produtos que não estão selecionados em outras linhas
    const selectedProductIds = formData.products
      .map((p, index) => ({ productId: p.productId, index }))
      .filter(p => p.productId > 0 && p.index !== currentIndex)
      .map(p => p.productId);

    return availableProducts.filter(product => {
      const numericId = productIdMapping[product.id];
      return !selectedProductIds.includes(numericId);
    });
  };

  const getUserName = (userId: number) => {
    console.log('🔍 Buscando usuário com ID:', userId, 'tipo:', typeof userId);
    console.log('📋 Usuários disponíveis:', availableUsers.map(u => ({ 
      id: u.id, 
      idType: typeof u.id, 
      username: u.username 
    })));
    
    // Tentar diferentes abordagens de matching
    let user = availableUsers.find(u => parseInt(u.id) === userId);
    
    if (!user) {
      // Tentar comparação direta string
      user = availableUsers.find(u => u.id === userId.toString());
    }
    
    if (!user) {
      // Tentar comparação direta number
      user = availableUsers.find(u => Number(u.id) === userId);
    }
    
    console.log('✅ Usuário encontrado:', user);
    
    return user ? `${user.username} (${user.email})` : `Usuário não encontrado (ID: ${userId})`;
  };

  if (loadingData) {
    return (
      <div className="flex items-center justify-center py-12">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-600"></div>
        <span className="ml-2 text-gray-600">Carregando produtos e usuários...</span>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <form onSubmit={handleSubmit} className="space-y-6">
        {/* Usuário */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            <Users className="inline h-4 w-4 mr-1" />
            Usuário *
          </label>
          <select
            value={formData.userId || ''}
            onChange={(e) => setFormData(prev => ({ ...prev, userId: parseInt(e.target.value) || 0 }))}
            className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
              errors.userId ? 'border-red-300' : 'border-gray-300'
            }`}
          >
            <option value="">Selecione um usuário</option>
            {availableUsers.map((user) => (
              <option key={user.id} value={parseInt(user.id)}>
                {user.username} - {user.email}
              </option>
            ))}
          </select>
          {errors.userId && <p className="text-red-500 text-xs mt-1">{errors.userId}</p>}
        </div>

        {/* Data */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Data do Carrinho *
          </label>
          <input
            type="datetime-local"
            value={formData.date.substring(0, 16)}
            onChange={(e) => setFormData(prev => ({ ...prev, date: new Date(e.target.value).toISOString() }))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        {/* Produtos do Carrinho */}
        <div>
          <div className="flex items-center justify-between mb-4">
            <h3 className="text-lg font-medium text-gray-900">
              <Package className="inline h-5 w-5 mr-2" />
              Produtos do Carrinho
            </h3>
            <button
              type="button"
              onClick={addProduct}
              className="bg-indigo-600 hover:bg-indigo-700 text-white px-3 py-1 rounded-md text-sm flex items-center"
            >
              <Plus className="h-4 w-4 mr-1" />
              Adicionar Produto
            </button>
          </div>

          {/* Erro de produto duplicado */}
          {duplicateProductError && (
            <div className="mb-4 p-3 bg-red-50 border border-red-200 rounded-md">
              <p className="text-red-600 text-sm">{duplicateProductError}</p>
            </div>
          )}

          <div className="space-y-4">
            {formData.products.map((product, index) => (
              <div key={index} className="border border-gray-200 rounded-lg p-4 bg-gray-50">
                <div className="flex items-center justify-between mb-3">
                  <h4 className="text-sm font-medium text-gray-700">Produto {index + 1}</h4>
                  {formData.products.length > 1 && (
                    <button
                      type="button"
                      onClick={() => removeProduct(index)}
                      className="text-red-600 hover:text-red-800"
                    >
                      <Trash2 className="h-4 w-4" />
                    </button>
                  )}
                </div>

                <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                  <div className="md:col-span-2">
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Produto *
                    </label>
                    <select
                      value={product.productId || ''}
                      onChange={(e) => updateProduct(index, 'productId', parseInt(e.target.value) || 0)}
                      className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                        errors[`product_${index}_productId`] ? 'border-red-300' : 'border-gray-300'
                      }`}
                    >
                      <option value="">Selecione um produto</option>
                      {getAvailableProductsForIndex(index).map((prod) => {
                        const mappedId = productIdMapping[prod.id];
                        return (
                          <option key={prod.id} value={mappedId}>
                            {prod.title} - ${prod.price}
                          </option>
                        );
                      })}
                      {product.productId > 0 && (
                        <option value={product.productId} disabled>
                          {getProductName(product.productId)} (selecionado)
                        </option>
                      )}
                    </select>
                    {errors[`product_${index}_productId`] && (
                      <p className="text-red-500 text-xs mt-1">{errors[`product_${index}_productId`]}</p>
                    )}
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Quantidade *
                    </label>
                    <input
                      type="number"
                      min="1"
                      value={product.quantity}
                      onChange={(e) => updateProduct(index, 'quantity', parseInt(e.target.value) || 1)}
                      className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                        errors[`product_${index}_quantity`] ? 'border-red-300' : 'border-gray-300'
                      }`}
                    />
                    {errors[`product_${index}_quantity`] && (
                      <p className="text-red-500 text-xs mt-1">{errors[`product_${index}_quantity`]}</p>
                    )}
                  </div>
                </div>

                {/* Prévia do produto selecionado */}
                {product.productId > 0 && (
                  <div className="mt-3 p-3 bg-white rounded border border-gray-200">
                    <p className="text-sm text-gray-700">
                      <strong>Produto:</strong> {getProductName(product.productId)}
                    </p>
                    <p className="text-xs text-gray-500 font-mono mt-1">
                      <strong>ID Mapeado:</strong> {product.productId} → <strong>GUID:</strong> {reverseProductMapping[product.productId]}
                    </p>
                  </div>
                )}
              </div>
            ))}
          </div>

          {errors.products && <p className="text-red-500 text-sm mt-2">{errors.products}</p>}
        </div>

        {/* Resumo */}
        <div className="bg-indigo-50 p-4 rounded-lg border border-indigo-200">
          <div className="space-y-2">
            <div className="flex justify-between items-center">
              <span className="text-sm font-medium text-gray-900">Total de Produtos:</span>
              <span className="text-lg font-bold text-indigo-600">
                {formData.products.reduce((total, product) => total + product.quantity, 0)}
              </span>
            </div>
            {formData.userId > 0 && (
              <div className="text-xs text-gray-600">
                <strong>Usuário:</strong> {getUserName(formData.userId)}
              </div>
            )}
            <div className="text-xs text-gray-600">
              <strong>Data:</strong> {new Date(formData.date).toLocaleString('pt-BR')}
            </div>
          </div>
        </div>

        {/* Botões */}
        <div className="flex justify-end space-x-3 pt-4">
          <button
            type="button"
            onClick={onCancel}
            className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            Cancelar
          </button>
          <button
            type="submit"
            disabled={isLoading || loadingData}
            className="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:opacity-50"
          >
            {isLoading ? 'Salvando...' : cart ? 'Atualizar' : 'Criar'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default CartForm; 