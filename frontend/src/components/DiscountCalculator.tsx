import React from 'react';
import { Percent, ShoppingCart, AlertTriangle } from 'lucide-react';

interface DiscountCalculatorProps {
  itemsCount: number;
  subtotal: number;
  className?: string;
  cartItems?: Array<{
    productId: number;
    quantity: number;
    unitPrice: number;
  }>;
}

const DiscountCalculator: React.FC<DiscountCalculatorProps> = ({ 
  itemsCount, 
  subtotal, 
  className = '',
  cartItems = []
}) => {
  const calculateProductDiscount = (quantity: number) => {
    if (quantity >= 10 && quantity <= 20) return 20;
    if (quantity >= 4) return 10;
    return 0;
  };

  const calculateTotalDiscount = () => {
    let totalDiscount = 0;
    let totalWithDiscount = 0;
    let totalWithoutDiscount = 0;

    cartItems.forEach(item => {
      const itemTotal = item.quantity * item.unitPrice;
      const discountPercentage = calculateProductDiscount(item.quantity);
      const itemDiscount = (itemTotal * discountPercentage) / 100;
      
      totalDiscount += itemDiscount;
      totalWithDiscount += (itemTotal - itemDiscount);
      totalWithoutDiscount += itemTotal;
    });

    return {
      totalDiscount,
      totalWithDiscount,
      totalWithoutDiscount,
      discountPercentage: totalWithoutDiscount > 0 ? (totalDiscount / totalWithoutDiscount) * 100 : 0
    };
  };

  const discountInfo = calculateTotalDiscount();
  const total = discountInfo.totalWithDiscount;

  const productsWithExcessQuantity = cartItems.filter(item => item.quantity > 20);
  const hasExcessQuantity = productsWithExcessQuantity.length > 0;

  const getDiscountColor = () => {
    if (discountInfo.discountPercentage === 0) return 'text-gray-600';
    if (discountInfo.discountPercentage > 0 && discountInfo.discountPercentage < 15) return 'text-yellow-600';
    return 'text-orange-600';
  };

  const getDiscountIcon = () => {
    if (discountInfo.discountPercentage === 0) return 'text-gray-500';
    if (discountInfo.discountPercentage > 0 && discountInfo.discountPercentage < 15) return 'text-yellow-500';
    return 'text-orange-500';
  };

  return (
    <div className={`bg-white border rounded-lg p-4 ${className}`}>
      <div className="flex items-center gap-2 mb-3">
        <ShoppingCart className="w-5 h-5 text-gray-600" />
        <h3 className="text-lg font-semibold text-gray-800">Cálculo do Desconto</h3>
      </div>
      
      <div className="space-y-3">
        <div className="flex justify-between items-center">
          <span className="text-gray-600">Quantidade de itens:</span>
          <span className="font-medium">{itemsCount}</span>
        </div>
        
        <div className="flex justify-between items-center">
          <span className="text-gray-600">Subtotal:</span>
          <span className="font-medium">R$ {discountInfo.totalWithoutDiscount.toFixed(2)}</span>
        </div>
        
        {discountInfo.discountPercentage > 0 && (
          <>
            <div className="flex justify-between items-center">
              <span className="text-gray-600">Desconto aplicado:</span>
              <div className="flex items-center gap-2">
                <Percent className={`w-4 h-4 ${getDiscountIcon()}`} />
                <span className={`font-semibold ${getDiscountColor()}`}>
                  {discountInfo.discountPercentage.toFixed(1)}%
                </span>
              </div>
            </div>
            
            <div className="flex justify-between items-center">
              <span className="text-gray-600">Valor do desconto:</span>
              <span className="font-medium text-green-600">
                -R$ {discountInfo.totalDiscount.toFixed(2)}
              </span>
            </div>
          </>
        )}
        
        <div className="border-t pt-3">
          <div className="flex justify-between items-center">
            <span className="text-lg font-semibold text-gray-800">Total:</span>
            <span className="text-lg font-bold text-green-600">
              R$ {total.toFixed(2)}
            </span>
          </div>
        </div>
      </div>
      
      <div className="mt-4 p-3 bg-blue-50 border border-blue-200 rounded-md">
        <h4 className="text-sm font-semibold text-blue-800 mb-2">Regras de Desconto:</h4>
        <ul className="text-xs text-blue-700 space-y-1">
          <li>• 4+ itens idênticos: 10% de desconto</li>
          <li>• 10-20 itens idênticos: 20% de desconto</li>
          <li>• Máximo 20 itens por produto</li>
          <li>• Sem desconto para menos de 4 itens</li>
        </ul>
      </div>
      
      {hasExcessQuantity && (
        <div className="mt-3 p-3 bg-red-50 border border-red-200 rounded-md">
          <div className="flex items-center gap-2 mb-2">
            <AlertTriangle className="w-4 h-4 text-red-600" />
            <span className="text-sm font-semibold text-red-700">Produtos com excesso de quantidade:</span>
          </div>
          <ul className="text-xs text-red-600 space-y-1">
            {productsWithExcessQuantity.map((item, index) => (
              <li key={index}>
                • Produto #{item.productId}: {item.quantity} unidades (máximo: 20)
              </li>
            ))}
          </ul>
        </div>
      )}
      
      {itemsCount > 20 && (
        <div className="mt-3 p-3 bg-red-50 border border-red-200 rounded-md">
          <p className="text-sm text-red-700">
            <strong>Atenção:</strong> Máximo de 20 itens permitido por venda.
          </p>
        </div>
      )}
    </div>
  );
};

export default DiscountCalculator; 