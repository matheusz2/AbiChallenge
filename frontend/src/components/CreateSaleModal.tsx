import React, { useState, useEffect } from 'react';
import { X, ShoppingCart, DollarSign, User, Plus, Trash2, AlertTriangle, Building } from 'lucide-react';
import type { Product } from '../types/api';
import DiscountCalculator from './DiscountCalculator';

// Tipos temporários até serem adicionados ao backend
interface Customer {
  id: string;
  name: string;
  email: string;
}

interface Branch {
  id: string;
  name: string;
  city: string;
}

interface SaleItem {
  productId: string;
  quantity: number;
  unitPrice: number;
}

interface CreateSaleModalProps {
  isOpen: boolean;
  onClose: () => void;
  products: Product[];
  customers: Customer[];
  branches: Branch[];
  onCreateSale: (saleData: any) => void;
  isLoading?: boolean;
}

const CreateSaleModal: React.FC<CreateSaleModalProps> = ({
  isOpen,
  onClose,
  products,
  customers,
  branches,
  onCreateSale,
  isLoading = false
}) => {
  const [selectedCustomerId, setSelectedCustomerId] = useState('');
  const [selectedBranchId, setSelectedBranchId] = useState('');
  const [saleItems, setSaleItems] = useState<SaleItem[]>([]);
  const [errors, setErrors] = useState<{ [key: string]: string }>({});

  // Preparar dados dos itens para o DiscountCalculator
  const cartItems = saleItems.map(item => ({
    productId: parseInt(item.productId) || 0,
    quantity: item.quantity,
    unitPrice: item.unitPrice
  }));

  // Calcular subtotal da venda
  const subtotal = saleItems.reduce((total, item) => {
    return total + (item.quantity * item.unitPrice);
  }, 0);

  const itemsCount = saleItems.length;

  // Validar regras de negócio por produto
  const validateBusinessRules = () => {
    const newErrors: { [key: string]: string } = {};
    
    // Verificar se há produtos com mais de 20 unidades
    const productsWithExcessQuantity = saleItems.filter(item => item.quantity > 20);
    if (productsWithExcessQuantity.length > 0) {
      const productIds = productsWithExcessQuantity.map(item => item.productId).join(', ');
      newErrors.quantity = `Produtos com excesso de quantidade (máximo 20): ${productIds}`;
    }
    
    // Verificar se há produtos com quantidade zero ou negativa
    const productsWithInvalidQuantity = saleItems.filter(item => item.quantity <= 0);
    if (productsWithInvalidQuantity.length > 0) {
      const productIds = productsWithInvalidQuantity.map(item => item.productId).join(', ');
      newErrors.quantity = `Produtos com quantidade inválida: ${productIds}`;
    }

    // Verificar se há produtos duplicados
    const productIds = saleItems.map(item => item.productId);
    const duplicateProductIds = productIds.filter((id, index) => productIds.indexOf(id) !== index);
    if (duplicateProductIds.length > 0) {
      newErrors.duplicates = `Produtos duplicados: ${duplicateProductIds.join(', ')}`;
    }
    
    return newErrors;
  };

  useEffect(() => {
    if (isOpen) {
      setSelectedCustomerId('');
      setSelectedBranchId('');
      setSaleItems([]);
      setErrors({});
    }
  }, [isOpen]);

  const handleAddItem = () => {
    const newItem: SaleItem = {
      productId: '',
      quantity: 1,
      unitPrice: 0
    };
    setSaleItems([...saleItems, newItem]);
  };

  const handleRemoveItem = (index: number) => {
    setSaleItems(saleItems.filter((_, i) => i !== index));
  };

  const handleItemChange = (index: number, field: keyof SaleItem, value: string | number) => {
    const updatedItems = [...saleItems];
    updatedItems[index] = {
      ...updatedItems[index],
      [field]: value
    };

    // Se o produto foi alterado, atualizar o preço unitário
    if (field === 'productId') {
      const product = products.find(p => p.id === value);
      updatedItems[index].unitPrice = product?.price || 0;
    }

    setSaleItems(updatedItems);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const newErrors: { [key: string]: string } = {};
    
    if (!selectedCustomerId) {
      newErrors.customerId = 'Cliente é obrigatório';
    }

    if (!selectedBranchId) {
      newErrors.branchId = 'Filial é obrigatória';
    }
    
    if (itemsCount === 0) {
      newErrors.items = 'Venda deve ter pelo menos um item';
    }
    
    if (itemsCount > 20) {
      newErrors.items = 'Máximo de 20 itens permitido';
    }

    // Validar itens
    saleItems.forEach((item, index) => {
      if (!item.productId) {
        newErrors[`item${index}Product`] = 'Produto é obrigatório';
      }
      if (item.quantity <= 0) {
        newErrors[`item${index}Quantity`] = 'Quantidade deve ser maior que zero';
      }
      if (item.unitPrice <= 0) {
        newErrors[`item${index}Price`] = 'Preço deve ser maior que zero';
      }
    });

    // Validar regras de negócio por produto
    const businessRuleErrors = validateBusinessRules();
    Object.assign(newErrors, businessRuleErrors);
    
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }
    
    const saleData = {
      customerId: selectedCustomerId,
      branchId: selectedBranchId,
      items: saleItems
    };
    
    console.log('Dados da venda:', saleData);
    onCreateSale(saleData);
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-xl max-w-6xl w-full mx-4 max-h-[90vh] overflow-y-auto">
        <div className="flex items-center justify-between p-6 border-b">
          <div className="flex items-center gap-3">
            <DollarSign className="w-6 h-6 text-green-600" />
            <h2 className="text-2xl font-bold text-gray-800">
              Criar Nova Venda
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
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
            {/* Formulário */}
            <div className="lg:col-span-2">
              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Cliente e Filial */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  {/* Cliente */}
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      <User className="w-4 h-4 inline mr-2" />
                      Cliente *
                    </label>
                    <select
                      value={selectedCustomerId}
                      onChange={(e) => setSelectedCustomerId(e.target.value)}
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

                  {/* Filial */}
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      <Building className="w-4 h-4 inline mr-2" />
                      Filial *
                    </label>
                    <select
                      value={selectedBranchId}
                      onChange={(e) => setSelectedBranchId(e.target.value)}
                      className={`w-full p-3 border rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                        errors.branchId ? 'border-red-500' : 'border-gray-300'
                      }`}
                    >
                      <option value="">Selecione uma filial</option>
                      {branches.map((branch) => (
                        <option key={branch.id} value={branch.id}>
                          {branch.name} - {branch.city}
                        </option>
                      ))}
                    </select>
                    {errors.branchId && (
                      <p className="text-red-500 text-sm mt-1">{errors.branchId}</p>
                    )}
                  </div>
                </div>

                {/* Itens da Venda */}
                <div>
                  <div className="flex items-center justify-between mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      <ShoppingCart className="w-4 h-4 inline mr-2" />
                      Itens da Venda ({itemsCount})
                    </label>
                    <button
                      type="button"
                      onClick={handleAddItem}
                      className="flex items-center gap-2 px-3 py-1 text-sm bg-blue-600 text-white rounded-md hover:bg-blue-700"
                    >
                      <Plus className="w-4 h-4" />
                      Adicionar Item
                    </button>
                  </div>

                  {saleItems.length === 0 ? (
                    <div className="text-center py-8 border-2 border-dashed border-gray-300 rounded-lg">
                      <ShoppingCart className="mx-auto h-12 w-12 text-gray-400" />
                      <p className="mt-2 text-sm text-gray-500">Nenhum item adicionado</p>
                      <p className="text-xs text-gray-400">Clique em "Adicionar Item" para começar</p>
                    </div>
                  ) : (
                    <div className="space-y-4">
                      {saleItems.map((item, index) => {
                        const product = products.find(p => p.id === item.productId);
                        
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
                          <div key={index} className="border rounded-lg p-4 bg-gray-50">
                            <div className="flex items-center justify-between mb-3">
                              <h4 className="font-medium text-gray-700">Item #{index + 1}</h4>
                              <button
                                type="button"
                                onClick={() => handleRemoveItem(index)}
                                className="text-red-600 hover:text-red-800"
                              >
                                <Trash2 className="w-4 h-4" />
                              </button>
                            </div>
                            
                            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                              <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                  Produto *
                                </label>
                                <select
                                  value={item.productId}
                                  onChange={(e) => handleItemChange(index, 'productId', e.target.value)}
                                  className={`w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                    errors[`item${index}Product`] ? 'border-red-500' : 'border-gray-300'
                                  }`}
                                >
                                  <option value="">Selecione um produto</option>
                                  {products.map((product) => (
                                    <option key={product.id} value={product.id}>
                                      {product.title} - R$ {product.price.toFixed(2)}
                                    </option>
                                  ))}
                                </select>
                                {errors[`item${index}Product`] && (
                                  <p className="text-red-500 text-xs mt-1">{errors[`item${index}Product`]}</p>
                                )}
                              </div>

                              <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                  Quantidade *
                                </label>
                                <input
                                  type="number"
                                  min="1"
                                  max="20"
                                  value={item.quantity}
                                  onChange={(e) => handleItemChange(index, 'quantity', parseInt(e.target.value) || 1)}
                                  className={`w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                    errors[`item${index}Quantity`] ? 'border-red-500' : 'border-gray-300'
                                  }`}
                                />
                                {errors[`item${index}Quantity`] && (
                                  <p className="text-red-500 text-xs mt-1">{errors[`item${index}Quantity`]}</p>
                                )}
                                {item.quantity > 20 && (
                                  <p className="text-red-500 text-xs mt-1">Máximo 20 unidades por produto</p>
                                )}
                              </div>

                              <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                  Preço Unitário
                                </label>
                                <input
                                  type="number"
                                  step="0.01"
                                  min="0"
                                  value={item.unitPrice}
                                  onChange={(e) => handleItemChange(index, 'unitPrice', parseFloat(e.target.value) || 0)}
                                  className={`w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                    errors[`item${index}Price`] ? 'border-red-500' : 'border-gray-300'
                                  }`}
                                  readOnly
                                />
                                <div className="text-xs text-gray-500 mt-1">
                                  <p>Total: R$ {itemTotalWithDiscount.toFixed(2)}</p>
                                  {discountPercentage > 0 && (
                                    <p className="text-green-600">
                                      Desconto: {discountPercentage}% (-R$ {itemDiscount.toFixed(2)})
                                    </p>
                                  )}
                                </div>
                              </div>
                            </div>
                          </div>
                        );
                      })}
                    </div>
                  )}
                  
                  {errors.items && (
                    <p className="text-red-500 text-sm mt-1">{errors.items}</p>
                  )}
                  {errors.quantity && (
                    <p className="text-red-500 text-sm mt-1">{errors.quantity}</p>
                  )}
                  {errors.duplicates && (
                    <p className="text-red-500 text-sm mt-1">{errors.duplicates}</p>
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
                    className="flex-1 px-4 py-2 bg-green-600 text-white rounded-lg hover:bg-green-700 disabled:bg-gray-400 disabled:cursor-not-allowed transition-colors"
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

export default CreateSaleModal; 