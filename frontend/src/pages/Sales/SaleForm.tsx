import React, { useState, useEffect } from 'react';
import { ShoppingCart, User, Building, Plus, Minus, Trash2 } from 'lucide-react';
import type { Sale, CreateSaleRequest, UpdateSaleRequest } from '../../types/api';

interface SaleFormProps {
  sale?: Sale;
  onSubmit: (data: any) => Promise<void>;
  onCancel: () => void;
  isLoading: boolean;
}

const SaleForm: React.FC<SaleFormProps> = ({ sale, onSubmit, onCancel, isLoading }) => {
  const [formData, setFormData] = useState({
    customerId: '',
    branchId: '',
    items: [{ productId: '', quantity: 1, unitPrice: 0 }]
  });

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (sale) {
      setFormData({
        customerId: sale.customerId || '',
        branchId: sale.branchId || '',
        items: sale.items?.map(item => ({
          productId: item.productId || '',
          quantity: item.quantity || 1,
          unitPrice: item.unitPrice || 0
        })) || [{ productId: '', quantity: 1, unitPrice: 0 }]
      });
    }
  }, [sale]);

  const validateForm = () => {
    const newErrors: Record<string, string> = {};

    if (!formData.customerId.trim()) newErrors.customerId = 'Cliente é obrigatório';
    if (!formData.branchId.trim()) newErrors.branchId = 'Filial é obrigatória';
    
    if (formData.items.length === 0) {
      newErrors.items = 'Pelo menos um item é obrigatório';
    } else {
      formData.items.forEach((item, index) => {
        if (!item.productId.trim()) {
          newErrors[`item_${index}_productId`] = 'Produto é obrigatório';
        }
        if (item.quantity <= 0) {
          newErrors[`item_${index}_quantity`] = 'Quantidade deve ser maior que 0';
        }
        if (item.unitPrice <= 0) {
          newErrors[`item_${index}_unitPrice`] = 'Preço deve ser maior que 0';
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
      const saleData = {
        customerId: formData.customerId,
        branchId: formData.branchId,
        items: formData.items.map(item => ({
          productId: item.productId,
          quantity: item.quantity,
          unitPrice: item.unitPrice
        }))
      };

      if (sale) {
        // Edição
        await onSubmit({
          id: sale.id,
          ...saleData
        });
      } else {
        // Criação
        await onSubmit(saleData);
      }
    } catch (error) {
      console.error('Erro ao salvar venda:', error);
    }
  };

  const addItem = () => {
    setFormData(prev => ({
      ...prev,
      items: [...prev.items, { productId: '', quantity: 1, unitPrice: 0 }]
    }));
  };

  const removeItem = (index: number) => {
    if (formData.items.length > 1) {
      setFormData(prev => ({
        ...prev,
        items: prev.items.filter((_, i) => i !== index)
      }));
    }
  };

  const updateItem = (index: number, field: string, value: any) => {
    setFormData(prev => ({
      ...prev,
      items: prev.items.map((item, i) => 
        i === index ? { ...item, [field]: value } : item
      )
    }));
  };

  const calculateTotal = () => {
    return formData.items.reduce((total, item) => {
      return total + (item.quantity * item.unitPrice);
    }, 0);
  };

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(price);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-6">
      {/* Informações Gerais */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Cliente *
          </label>
          <div className="relative">
            <User className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              value={formData.customerId}
              onChange={(e) => setFormData(prev => ({ ...prev, customerId: e.target.value }))}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.customerId ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="ID do cliente"
            />
          </div>
          {errors.customerId && <p className="text-red-500 text-xs mt-1">{errors.customerId}</p>}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Filial *
          </label>
          <div className="relative">
            <Building className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              value={formData.branchId}
              onChange={(e) => setFormData(prev => ({ ...prev, branchId: e.target.value }))}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.branchId ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="ID da filial"
            />
          </div>
          {errors.branchId && <p className="text-red-500 text-xs mt-1">{errors.branchId}</p>}
        </div>
      </div>

      {/* Itens da Venda */}
      <div>
        <div className="flex items-center justify-between mb-4">
          <h3 className="text-lg font-medium text-gray-900">Itens da Venda</h3>
          <button
            type="button"
            onClick={addItem}
            className="bg-indigo-600 hover:bg-indigo-700 text-white px-3 py-1 rounded-md text-sm flex items-center"
          >
            <Plus className="h-4 w-4 mr-1" />
            Adicionar Item
          </button>
        </div>

        <div className="space-y-4">
          {formData.items.map((item, index) => (
            <div key={index} className="border border-gray-200 rounded-lg p-4">
              <div className="flex items-center justify-between mb-3">
                <h4 className="text-sm font-medium text-gray-700">Item {index + 1}</h4>
                {formData.items.length > 1 && (
                  <button
                    type="button"
                    onClick={() => removeItem(index)}
                    className="text-red-600 hover:text-red-800"
                  >
                    <Trash2 className="h-4 w-4" />
                  </button>
                )}
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Produto *
                  </label>
                  <input
                    type="text"
                    value={item.productId}
                    onChange={(e) => updateItem(index, 'productId', e.target.value)}
                    className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                      errors[`item_${index}_productId`] ? 'border-red-300' : 'border-gray-300'
                    }`}
                    placeholder="ID do produto"
                  />
                  {errors[`item_${index}_productId`] && (
                    <p className="text-red-500 text-xs mt-1">{errors[`item_${index}_productId`]}</p>
                  )}
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Quantidade *
                  </label>
                  <input
                    type="number"
                    min="1"
                    value={item.quantity}
                    onChange={(e) => updateItem(index, 'quantity', parseInt(e.target.value) || 1)}
                    className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                      errors[`item_${index}_quantity`] ? 'border-red-300' : 'border-gray-300'
                    }`}
                  />
                  {errors[`item_${index}_quantity`] && (
                    <p className="text-red-500 text-xs mt-1">{errors[`item_${index}_quantity`]}</p>
                  )}
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Preço Unitário *
                  </label>
                  <input
                    type="number"
                    step="0.01"
                    min="0"
                    value={item.unitPrice}
                    onChange={(e) => updateItem(index, 'unitPrice', parseFloat(e.target.value) || 0)}
                    className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                      errors[`item_${index}_unitPrice`] ? 'border-red-300' : 'border-gray-300'
                    }`}
                    placeholder="0.00"
                  />
                  {errors[`item_${index}_unitPrice`] && (
                    <p className="text-red-500 text-xs mt-1">{errors[`item_${index}_unitPrice`]}</p>
                  )}
                </div>
              </div>

              <div className="mt-2 text-right">
                <span className="text-sm text-gray-600">
                  Subtotal: <span className="font-medium">{formatPrice(item.quantity * item.unitPrice)}</span>
                </span>
              </div>
            </div>
          ))}
        </div>

        {errors.items && <p className="text-red-500 text-sm mt-2">{errors.items}</p>}
      </div>

      {/* Total */}
      <div className="bg-gray-50 p-4 rounded-lg">
        <div className="flex justify-between items-center">
          <span className="text-lg font-medium text-gray-900">Total da Venda:</span>
          <span className="text-2xl font-bold text-indigo-600">{formatPrice(calculateTotal())}</span>
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
          disabled={isLoading}
          className="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:opacity-50"
        >
          {isLoading ? 'Salvando...' : sale ? 'Atualizar' : 'Criar'}
        </button>
      </div>
    </form>
  );
};

export default SaleForm; 