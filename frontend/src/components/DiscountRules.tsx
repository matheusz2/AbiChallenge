import React from 'react';
import { Info, Percent, ShoppingCart } from 'lucide-react';

interface DiscountRulesProps {
  className?: string;
}

const DiscountRules: React.FC<DiscountRulesProps> = ({ className = '' }) => {
  return (
    <div className={`bg-blue-50 border border-blue-200 rounded-lg p-4 ${className}`}>
      <div className="flex items-center gap-2 mb-3">
        <Info className="w-5 h-5 text-blue-600" />
        <h3 className="text-lg font-semibold text-blue-800">Regras de Desconto</h3>
      </div>
      
      <div className="space-y-3">
        <div className="flex items-center gap-3">
          <div className="flex items-center justify-center w-8 h-8 bg-green-100 rounded-full">
            <ShoppingCart className="w-4 h-4 text-green-600" />
          </div>
          <div>
            <span className="font-medium text-gray-700">Menos de 4 itens:</span>
            <span className="ml-2 text-gray-600">Sem desconto</span>
          </div>
        </div>
        
        <div className="flex items-center gap-3">
          <div className="flex items-center justify-center w-8 h-8 bg-yellow-100 rounded-full">
            <Percent className="w-4 h-4 text-yellow-600" />
          </div>
          <div>
            <span className="font-medium text-gray-700">4 a 9 itens:</span>
            <span className="ml-2 text-green-600 font-semibold">10% de desconto</span>
          </div>
        </div>
        
        <div className="flex items-center gap-3">
          <div className="flex items-center justify-center w-8 h-8 bg-orange-100 rounded-full">
            <Percent className="w-4 h-4 text-orange-600" />
          </div>
          <div>
            <span className="font-medium text-gray-700">10 a 20 itens:</span>
            <span className="ml-2 text-green-600 font-semibold">20% de desconto</span>
          </div>
        </div>
        
        <div className="flex items-center gap-3">
          <div className="flex items-center justify-center w-8 h-8 bg-red-100 rounded-full">
            <ShoppingCart className="w-4 h-4 text-red-600" />
          </div>
          <div>
            <span className="font-medium text-gray-700">Mais de 20 itens:</span>
            <span className="ml-2 text-red-600 font-semibold">Não permitido</span>
          </div>
        </div>
      </div>
      
      <div className="mt-4 p-3 bg-blue-100 rounded-md">
        <p className="text-sm text-blue-800">
          <strong>Observação:</strong> Os descontos são aplicados automaticamente com base na quantidade de itens na venda.
        </p>
      </div>
    </div>
  );
};

export default DiscountRules; 