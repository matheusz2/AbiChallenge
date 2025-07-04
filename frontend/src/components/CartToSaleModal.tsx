import React, { useState, useEffect } from 'react';
import { X, ShoppingCart, DollarSign, User } from 'lucide-react';
import type { Cart, Product } from '../types/api';
import DiscountCalculator from './DiscountCalculator';

// Tipos temporários até serem adicionados ao backend
interface Customer {
  id: string;
  name: string;
  email: string;
}

interface CartToSaleModalProps {
  isOpen: boolean;
  onClose: () => void;
  cart: Cart;
  products: Product[];
  customers: Customer[];
  onConvertToSale: (saleData: any) => void;
  isLoading?: boolean;
}

const CartToSaleModal: React.FC<CartToSaleModalProps> = ({
  isOpen,
  onClose,
  cart,
  products,
  customers,
  onConvertToSale,
  isLoading = false
}) => {
  const [selectedCustomerId, setSelectedCustomerId] = useState('');
  const [errors, setErrors] = useState<{ [key: string]: string }>({});

  // Preparar dados dos itens do carrinho para o DiscountCalculator
  const cartItems = cart.products.map(item => {
    // Buscar produto por índice (productId é numérico)
    const productIndex = item.productId - 1; // Ajustar índice
    const product = products[productIndex] || products.find(p => p.id === item.productId.toString());
    const productPrice = product?.price || 0;
    
    return {
      productId: item.productId,
      quantity: item.quantity,
      unitPrice: productPrice
    };
  });

  // Calcular subtotal do carrinho corretamente
  const subtotal = cartItems.reduce((total: number, item) => {
    return total + (item.quantity * item.unitPrice);
  }, 0);

  const itemsCount = cart.products.length;

  // Validar regras de negócio por produto
  const validateBusinessRules = () => {
    const newErrors: { [key: string]: string } = {};
    
    // Verificar se há produtos com mais de 20 unidades
    const productsWithExcessQuantity = cartItems.filter(item => item.quantity > 20);
    if (productsWithExcessQuantity.length > 0) {
      const productIds = productsWithExcessQuantity.map(item => item.productId).join(', ');
      newErrors.quantity = `Produtos com excesso de quantidade (máximo 20): ${productIds}`;
    }
    
    // Verificar se há produtos com quantidade zero ou negativa
    const productsWithInvalidQuantity = cartItems.filter(item => item.quantity <= 0);
    if (productsWithInvalidQuantity.length > 0) {
      const productIds = productsWithInvalidQuantity.map(item => item.productId).join(', ');
      newErrors.quantity = `Produtos com quantidade inválida: ${productIds}`;
    }
    
    return newErrors;
  };

  useEffect(() => {
    if (isOpen) {
      setSelectedCustomerId('');
      setErrors({});
    }
  }, [isOpen]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const newErrors: { [key: string]: string } = {};
    
    if (!selectedCustomerId) {
      newErrors.customerId = 'Cliente é obrigatório';
    }
    
    if (itemsCount === 0) {
      newErrors.items = 'Carrinho deve ter pelo menos um item';
    }
    
    if (itemsCount > 20) {
      newErrors.items = 'Máximo de 20 itens permitido';
    }
    
    // Validar regras de negócio por produto
    const businessRuleErrors = validateBusinessRules();
    Object.assign(newErrors, businessRuleErrors);
    
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }
    
    const saleData = {
      customerId: selectedCustomerId,
      branchId: '1', // Filial padrão
      items: cartItems.map(item => ({
        productId: item.productId.toString(),
        quantity: item.quantity,
        unitPrice: item.unitPrice
      }))
    };
    
    console.log('Dados da venda:', saleData);
    onConvertToSale(saleData);
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-xl max-w-4xl w-full mx-4 max-h-[90vh] overflow-y-auto">
        <div className="flex items-center justify-between p-6 border-b">
          <div className="flex items-center gap-3">
            <ShoppingCart className="w-6 h-6 text-blue-600" />
            <h2 className="text-2xl font-bold text-gray-800">
              Transformar Carrinho em Venda
            </h2>
          </div>
          <button
            onClick={onClose}
            className="text-gray-400 hover:text-gray-600 transition-colors"
          >
            <X className="w-6 h-6" />
          </button>
        </div>

        <div className="p-6">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            {/* Formulário */}
            <div>
              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Cliente */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    <User className="w-4 h-4 inline mr-2" />
                    Cliente *
                  </label>
                  <select
                    value={selectedCustomerId}
                    onChange={(e) => {
                      console.log('Cliente selecionado:', e.target.value);
                      setSelectedCustomerId(e.target.value);
                    }}
                    className={`w-full p-3 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                      errors.customerId ? 'border-red-500' : 'border-gray-300'
                    }`}
                  >
                    <option value="">Selecione um cliente</option>
                    {customers.map((customer) => (
                      <option key={customer.id} value={customer.id}>
                        {customer.name} - {customer.email}
                      </option>
                    ))}
                  </select>
                  {errors.customerId && (
                    <p className="text-red-500 text-sm mt-1">{errors.customerId}</p>
                  )}
                </div>

                {/* Debug Info */}
                <div className="bg-blue-50 p-3 rounded-lg">
                  <p className="text-sm text-blue-800">
                    <strong>Debug:</strong> Cliente selecionado: {selectedCustomerId}
                  </p>
                  <p className="text-sm text-blue-800">
                    <strong>Debug:</strong> Produtos carregados: {products.length}
                  </p>
                  <p className="text-sm text-blue-800">
                    <strong>Debug:</strong> Itens no carrinho: {cart.products.length}
                  </p>
                </div>

                {/* Itens do Carrinho */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    <ShoppingCart className="w-4 h-4 inline mr-2" />
                    Itens do Carrinho ({itemsCount})
                  </label>
                  <div className="border rounded-lg p-4 bg-gray-50 max-h-60 overflow-y-auto">
                    {cart.products.length === 0 ? (
                      <p className="text-gray-500 text-center py-4">
                        Nenhum item no carrinho
                      </p>
                    ) : (
                      <div className="space-y-3">
                        {cartItems.map((item, index) => {
                          const product = products.find(p => p.id === item.productId.toString()) || 
                                        products[item.productId - 1];
                          
                          const productInfo = product || {
                            title: `Produto #${item.productId}`,
                            price: 0
                          };

                          // Calcular desconto por produto
                          const calculateProductDiscount = (quantity: number) => {
                            if (quantity >= 10 && quantity <= 20) return 20;
                            if (quantity >= 4) return 10;
                            return 0;
                          };

                          const discountPercentage = calculateProductDiscount(item.quantity);
                          const itemTotal = item.quantity * item.unitPrice;
                          const itemDiscount = (itemTotal * discountPercentage) / 100;
                          const itemTotalWithDiscount = itemTotal - itemDiscount;

                          return (
                            <div key={`${item.productId}-${index}`} className="flex justify-between items-center p-3 bg-white rounded border">
                              <div className="flex-1">
                                <p className="font-medium text-gray-900">{productInfo.title}</p>
                                <p className="text-sm text-gray-600">
                                  Qtd: {item.quantity} x R$ {item.unitPrice.toFixed(2)}
                                </p>
                                {discountPercentage > 0 && (
                                  <p className="text-xs text-green-600">
                                    Desconto: {discountPercentage}% (-R$ {itemDiscount.toFixed(2)})
                                  </p>
                                )}
                                <p className="text-xs text-gray-500">
                                  ID: {item.productId}
                                </p>
                              </div>
                              <div className="text-right">
                                <p className="font-semibold text-gray-900">
                                  R$ {itemTotalWithDiscount.toFixed(2)}
                                </p>
                                {discountPercentage > 0 && (
                                  <p className="text-xs text-gray-500 line-through">
                                    R$ {itemTotal.toFixed(2)}
                                  </p>
                                )}
                              </div>
                            </div>
                          );
                        })}
                      </div>
                    )}
                  </div>
                  {errors.items && (
                    <p className="text-red-500 text-sm mt-1">{errors.items}</p>
                  )}
                  {errors.quantity && (
                    <p className="text-red-500 text-sm mt-1">{errors.quantity}</p>
                  )}
                </div>

                {/* Botões */}
                <div className="flex gap-3 pt-4">
                  <button
                    type="button"
                    onClick={onClose}
                    className="flex-1 px-4 py-2 border border-gray-300 rounded-lg text-gray-700 hover:bg-gray-50 transition-colors"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    disabled={isLoading || itemsCount === 0 || itemsCount > 20 || Object.keys(validateBusinessRules()).length > 0}
                    className="flex-1 px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 disabled:bg-gray-400 disabled:cursor-not-allowed transition-colors"
                  >
                    {isLoading ? 'Processando...' : 'Criar Venda'}
                  </button>
                </div>
              </form>
            </div>

            {/* Cálculo do Desconto */}
            <div>
              <DiscountCalculator
                itemsCount={itemsCount}
                subtotal={subtotal}
                cartItems={cartItems}
                className="sticky top-0"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CartToSaleModal; 