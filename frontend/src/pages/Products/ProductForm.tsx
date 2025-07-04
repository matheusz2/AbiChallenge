import React, { useState, useEffect } from 'react';
import { Package, DollarSign, FileText, Tag, Image } from 'lucide-react';
import type { Product, CreateProductRequest, UpdateProductRequest } from '../../types/api';

interface ProductFormProps {
  product?: Product;
  onSubmit: (data: any) => Promise<void>;
  onCancel: () => void;
  isLoading: boolean;
}

const ProductForm: React.FC<ProductFormProps> = ({ product, onSubmit, onCancel, isLoading }) => {
  const [formData, setFormData] = useState({
    title: '',
    price: '',
    description: '',
    category: '',
    image: '',
    rating: {
      rate: 0,
      count: 0
    }
  });

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (product) {
      setFormData({
        title: product.title || '',
        price: product.price?.toString() || '',
        description: product.description || '',
        category: product.category || '',
        image: product.image || '',
        rating: {
          rate: product.rating?.rate || 0,
          count: product.rating?.count || 0
        }
      });
    }
  }, [product]);

  const validateForm = () => {
    const newErrors: Record<string, string> = {};

    if (!formData.title.trim()) newErrors.title = 'Título é obrigatório';
    if (!formData.price.trim()) newErrors.price = 'Preço é obrigatório';
    if (!formData.description.trim()) newErrors.description = 'Descrição é obrigatória';
    if (!formData.category.trim()) newErrors.category = 'Categoria é obrigatória';

    const price = parseFloat(formData.price);
    if (isNaN(price) || price <= 0) {
      newErrors.price = 'Preço deve ser um número positivo';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) return;

    try {
      const productData = {
        title: formData.title,
        price: parseFloat(formData.price),
        description: formData.description,
        category: formData.category,
        image: formData.image || 'https://via.placeholder.com/300x300?text=Produto',
        rating: formData.rating
      };

      console.log('📦 Dados do produto a serem enviados:', productData);

      if (product) {
        // Edição
        const updateData = {
          id: product.id,
          ...productData
        };
        console.log('🔄 Editando produto:', updateData);
        await onSubmit(updateData);
      } else {
        // Criação
        console.log('✨ Criando produto:', productData);
        await onSubmit(productData);
      }
    } catch (error) {
      console.error('Erro ao salvar produto:', error);
    }
  };

  const formatPrice = (value: string) => {
    const numbers = value.replace(/[^\d,]/g, '');
    return numbers;
  };

  const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const formatted = formatPrice(e.target.value);
    setFormData(prev => ({ ...prev, price: formatted }));
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      {/* Título */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Título *
        </label>
        <div className="relative">
          <Package className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
          <input
            type="text"
            value={formData.title}
            onChange={(e) => setFormData(prev => ({ ...prev, title: e.target.value }))}
            className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
              errors.title ? 'border-red-300' : 'border-gray-300'
            }`}
            placeholder="Nome do produto"
          />
        </div>
        {errors.title && <p className="text-red-500 text-xs mt-1">{errors.title}</p>}
      </div>

      {/* Preço e Categoria */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Preço *
          </label>
          <div className="relative">
            <DollarSign className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              value={formData.price}
              onChange={handlePriceChange}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.price ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="0,00"
            />
          </div>
          {errors.price && <p className="text-red-500 text-xs mt-1">{errors.price}</p>}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Categoria *
          </label>
          <div className="relative">
            <Tag className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              value={formData.category}
              onChange={(e) => setFormData(prev => ({ ...prev, category: e.target.value }))}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.category ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Categoria do produto"
            />
          </div>
          {errors.category && <p className="text-red-500 text-xs mt-1">{errors.category}</p>}
        </div>
      </div>

      {/* Descrição */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Descrição *
        </label>
        <div className="relative">
          <FileText className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
          <textarea
            value={formData.description}
            onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
            rows={4}
            className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
              errors.description ? 'border-red-300' : 'border-gray-300'
            }`}
            placeholder="Descrição detalhada do produto"
          />
        </div>
        {errors.description && <p className="text-red-500 text-xs mt-1">{errors.description}</p>}
      </div>

      {/* URL da Imagem */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          URL da Imagem
        </label>
        <div className="relative">
          <Image className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
          <input
            type="url"
            value={formData.image}
            onChange={(e) => setFormData(prev => ({ ...prev, image: e.target.value }))}
            className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            placeholder="https://exemplo.com/imagem.jpg"
          />
        </div>
        <p className="text-xs text-gray-500 mt-1">
          Deixe em branco para usar uma imagem padrão
        </p>
      </div>

      {/* Rating */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Avaliação (Rate)
          </label>
          <input
            type="number"
            min="0"
            max="5"
            step="0.1"
            value={formData.rating.rate}
            onChange={(e) => setFormData(prev => ({ 
              ...prev, 
              rating: { 
                ...prev.rating, 
                rate: parseFloat(e.target.value) || 0 
              } 
            }))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            placeholder="0.0"
          />
          <p className="text-xs text-gray-500 mt-1">
            Avaliação de 0 a 5
          </p>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Número de Avaliações (Count)
          </label>
          <input
            type="number"
            min="0"
            value={formData.rating.count}
            onChange={(e) => setFormData(prev => ({ 
              ...prev, 
              rating: { 
                ...prev.rating, 
                count: parseInt(e.target.value) || 0 
              } 
            }))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            placeholder="0"
          />
          <p className="text-xs text-gray-500 mt-1">
            Quantidade de avaliações
          </p>
        </div>
      </div>

      {/* Preview da Imagem */}
      {formData.image && (
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Preview
          </label>
          <div className="w-32 h-32 border border-gray-300 rounded-md overflow-hidden">
            <img
              src={formData.image}
              alt="Preview"
              className="w-full h-full object-cover"
              onError={(e) => {
                e.currentTarget.src = 'https://via.placeholder.com/300x300?text=Erro';
              }}
            />
          </div>
        </div>
      )}

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
          {isLoading ? 'Salvando...' : product ? 'Atualizar' : 'Criar'}
        </button>
      </div>
    </form>
  );
};

export default ProductForm; 