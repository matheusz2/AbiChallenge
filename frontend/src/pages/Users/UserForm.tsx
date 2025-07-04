import React, { useState, useEffect } from 'react';
import { User, Mail, Phone, MapPin, Building2 } from 'lucide-react';
import type { User as UserType, CreateUserRequest, UpdateUserRequest } from '../../types/api';

interface UserFormProps {
  user?: UserType;
  onSubmit: (data: any) => Promise<void>;
  onCancel: () => void;
  isLoading: boolean;
}

const UserForm: React.FC<UserFormProps> = ({ user, onSubmit, onCancel, isLoading }) => {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    phone: '',
    password: '',
    confirmPassword: '',
    firstname: '',
    lastname: '',
    city: '',
    street: '',
    number: '',
    zipcode: '',
    lat: '',
    long: '',
    status: 'Active',
    role: 'Customer'
  });

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (user) {
      console.log('📋 Dados do usuário para edição:', user);
      
      // Para edição, converter strings vindas da API para strings do enum correto
      const statusString = typeof user.status === 'string' ? user.status : statusNumberToString(user.status as number);
      const roleString = typeof user.role === 'string' ? user.role : roleNumberToString(user.role as number);
      
      // Extrair dados com fallbacks seguros
      const firstname = user.name?.firstname || '';
      const lastname = user.name?.lastname || '';
      const city = user.address?.city || '';
      const street = user.address?.street || '';
      const number = user.address?.number ? user.address.number.toString() : '';
      const zipcode = user.address?.zipcode || '';
      const lat = user.address?.geolocation?.lat || '';
      const long = user.address?.geolocation?.long || '';
      
      setFormData({
        username: user.username || '',
        email: user.email || '',
        phone: user.phone || '',
        password: '',
        confirmPassword: '',
        firstname,
        lastname,
        city,
        street,
        number,
        zipcode,
        lat,
        long,
        status: statusString,
        role: roleString
      });
      
      console.log('📝 Campos preenchidos:', {
        name: { firstname, lastname },
        address: { city, street, number, zipcode },
        geolocation: { lat, long },
        status: statusString,
        role: roleString
      });
    }
  }, [user]);

  const formatPhone = (value: string) => {
    const numbers = value.replace(/\D/g, '');
    if (numbers.length <= 2) return numbers;
    if (numbers.length <= 7) return `(${numbers.slice(0, 2)}) ${numbers.slice(2)}`;
    if (numbers.length <= 11) return `(${numbers.slice(0, 2)}) ${numbers.slice(2, 7)}-${numbers.slice(7)}`;
    return `(${numbers.slice(0, 2)}) ${numbers.slice(2, 7)}-${numbers.slice(7, 11)}`;
  };

  const handlePhoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const formatted = formatPhone(e.target.value);
    setFormData(prev => ({ ...prev, phone: formatted }));
  };

  const validateForm = () => {
    const newErrors: Record<string, string> = {};

    // Campos básicos
    if (!formData.username.trim()) newErrors.username = 'Nome de usuário é obrigatório';
    if (!formData.email.trim()) newErrors.email = 'Email é obrigatório';
    if (!formData.phone.trim()) newErrors.phone = 'Telefone é obrigatório';
    
    // Nome
    if (!formData.firstname.trim()) newErrors.firstname = 'Primeiro nome é obrigatório';
    if (!formData.lastname.trim()) newErrors.lastname = 'Sobrenome é obrigatório';
    
    // Endereço
    if (!formData.city.trim()) newErrors.city = 'Cidade é obrigatória';
    if (!formData.street.trim()) newErrors.street = 'Rua é obrigatória';
    if (!formData.number.trim()) newErrors.number = 'Número é obrigatório';
    if (!formData.zipcode.trim()) newErrors.zipcode = 'CEP é obrigatório';
    
    // Senha (apenas para criação)
    if (!user) {
      if (!formData.password) newErrors.password = 'Senha é obrigatória';
      if (formData.password !== formData.confirmPassword) {
        newErrors.confirmPassword = 'Senhas não coincidem';
      }
      if (formData.password && formData.password.length < 6) {
        newErrors.password = 'Senha deve ter pelo menos 6 caracteres';
      }
    }

    // Validação de telefone
    const phoneNumbers = formData.phone.replace(/\D/g, '');
    if (phoneNumbers.length !== 11) {
      newErrors.phone = 'Telefone deve ter 11 dígitos';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) return;

    try {
      if (user) {
        // Edição - converter strings para números
        const statusNumber = statusStringToNumber(formData.status);
        const roleNumber = roleStringToNumber(formData.role);
        
        console.log('🔄 Editando usuário:', {
          status: { string: formData.status, number: statusNumber },
          role: { string: formData.role, number: roleNumber }
        });
        
        const updateData: UpdateUserRequest = {
          id: user.id,
          email: formData.email,
          username: formData.username,
          password: formData.password || undefined, // Opcional
          name: {
            firstname: formData.firstname || 'Nome',
            lastname: formData.lastname || 'Sobrenome'
          },
          address: {
            city: formData.city || 'Cidade',
            street: formData.street || 'Rua',
            number: parseInt(formData.number) || 0,
            zipcode: formData.zipcode || '00000-000',
            geolocation: {
              lat: formData.lat || '0',
              long: formData.long || '0'
            }
          },
          phone: formData.phone,
          status: statusNumber,
          role: roleNumber
        };
        
        console.log('📤 Dados enviados para backend (UPDATE):', updateData);
        await onSubmit(updateData);
      } else {
        // Criação - agora inclui status e role
        const statusNumber = statusStringToNumber(formData.status);
        const roleNumber = roleStringToNumber(formData.role);
        
        console.log('✨ Criando usuário:', {
          status: { string: formData.status, number: statusNumber },
          role: { string: formData.role, number: roleNumber }
        });
        
        const createData: CreateUserRequest = {
          email: formData.email,
          username: formData.username,
          password: formData.password,
          name: {
            firstname: formData.firstname || 'Nome',
            lastname: formData.lastname || 'Sobrenome'
          },
          address: {
            city: formData.city || 'Cidade',
            street: formData.street || 'Rua',
            number: parseInt(formData.number) || 0,
            zipcode: formData.zipcode || '00000-000',
            geolocation: {
              lat: formData.lat || '0',
              long: formData.long || '0'
            }
          },
          phone: formData.phone,
          status: statusNumber,
          role: roleNumber
        };
        
        console.log('📤 Dados enviados para backend (CREATE):', createData);
        await onSubmit(createData);
      }
    } catch (error) {
      console.error('Erro ao salvar usuário:', error);
    }
  };

  // Funções de mapeamento entre strings e números
  // UserStatus: Unknown=0, Active=1, Inactive=2, Suspended=3
  const statusStringToNumber = (status: string): number => {
    switch (status.toLowerCase()) {
      case 'active': return 1;     // Active = 1
      case 'inactive': return 2;   // Inactive = 2  
      case 'suspended': return 3;  // Suspended = 3
      default: return 1;           // Default para Active
    }
  };

  // UserRole: None=0, Customer=1, Manager=2, Admin=3
  const roleStringToNumber = (role: string): number => {
    switch (role.toLowerCase()) {
      case 'customer': return 1;  // Customer = 1
      case 'manager': return 2;   // Manager = 2
      case 'admin': return 3;     // Admin = 3
      default: return 1;          // Default para Customer
    }
  };

  const statusNumberToString = (status: number): string => {
    switch (status) {
      case 1: return 'Active';     // 1 = Active
      case 2: return 'Inactive';   // 2 = Inactive
      case 3: return 'Suspended';  // 3 = Suspended
      default: return 'Active';    // Default para Active
    }
  };

  const roleNumberToString = (role: number): string => {
    switch (role) {
      case 1: return 'Customer';   // 1 = Customer
      case 2: return 'Manager';    // 2 = Manager  
      case 3: return 'Admin';      // 3 = Admin
      default: return 'Customer';  // Default para Customer
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      {/* Informações básicas */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Nome de usuário *
          </label>
          <div className="relative">
            <User className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              value={formData.username}
              onChange={(e) => setFormData(prev => ({ ...prev, username: e.target.value }))}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.username ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Nome de usuário"
            />
          </div>
          {errors.username && <p className="text-red-500 text-xs mt-1">{errors.username}</p>}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Email *
          </label>
          <div className="relative">
            <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="email"
              value={formData.email}
              onChange={(e) => setFormData(prev => ({ ...prev, email: e.target.value }))}
              className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.email ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="email@exemplo.com"
            />
          </div>
          {errors.email && <p className="text-red-500 text-xs mt-1">{errors.email}</p>}
        </div>
      </div>

      {/* Informações pessoais */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Primeiro Nome *
          </label>
          <input
            type="text"
            value={formData.firstname}
            onChange={(e) => setFormData(prev => ({ ...prev, firstname: e.target.value }))}
            className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
              errors.firstname ? 'border-red-300' : 'border-gray-300'
            }`}
            placeholder="Primeiro nome"
          />
          {errors.firstname && <p className="text-red-500 text-xs mt-1">{errors.firstname}</p>}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Sobrenome *
          </label>
          <input
            type="text"
            value={formData.lastname}
            onChange={(e) => setFormData(prev => ({ ...prev, lastname: e.target.value }))}
            className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
              errors.lastname ? 'border-red-300' : 'border-gray-300'
            }`}
            placeholder="Sobrenome"
          />
          {errors.lastname && <p className="text-red-500 text-xs mt-1">{errors.lastname}</p>}
        </div>
      </div>

      {/* Endereço */}
      <div className="space-y-4">
        <h3 className="text-lg font-medium text-gray-900 flex items-center">
          <MapPin className="h-5 w-5 mr-2" />
          Endereço
        </h3>
        
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Cidade *
            </label>
            <input
              type="text"
              value={formData.city}
              onChange={(e) => setFormData(prev => ({ ...prev, city: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.city ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Cidade"
            />
            {errors.city && <p className="text-red-500 text-xs mt-1">{errors.city}</p>}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Rua *
            </label>
            <input
              type="text"
              value={formData.street}
              onChange={(e) => setFormData(prev => ({ ...prev, street: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.street ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Nome da rua"
            />
            {errors.street && <p className="text-red-500 text-xs mt-1">{errors.street}</p>}
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Número *
            </label>
            <input
              type="number"
              value={formData.number}
              onChange={(e) => setFormData(prev => ({ ...prev, number: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.number ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="123"
            />
            {errors.number && <p className="text-red-500 text-xs mt-1">{errors.number}</p>}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              CEP *
            </label>
            <input
              type="text"
              value={formData.zipcode}
              onChange={(e) => setFormData(prev => ({ ...prev, zipcode: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.zipcode ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="00000-000"
            />
            {errors.zipcode && <p className="text-red-500 text-xs mt-1">{errors.zipcode}</p>}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Telefone *
            </label>
            <div className="relative">
              <Phone className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="tel"
                value={formData.phone}
                onChange={handlePhoneChange}
                maxLength={15}
                className={`w-full pl-10 pr-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                  errors.phone ? 'border-red-300' : 'border-gray-300'
                }`}
                placeholder="(XX) XXXXX-XXXX"
              />
            </div>
            {errors.phone && <p className="text-red-500 text-xs mt-1">{errors.phone}</p>}
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Latitude (opcional)
            </label>
            <input
              type="text"
              value={formData.lat}
              onChange={(e) => setFormData(prev => ({ ...prev, lat: e.target.value }))}
              className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
              placeholder="-23.5505"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Longitude (opcional)
            </label>
            <input
              type="text"
              value={formData.long}
              onChange={(e) => setFormData(prev => ({ ...prev, long: e.target.value }))}
              className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
              placeholder="-46.6333"
            />
          </div>
        </div>
      </div>

      {/* Status e Role */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Status *
          </label>
          <select
            value={formData.status}
            onChange={(e) => setFormData(prev => ({ ...prev, status: e.target.value }))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="Active">Ativo</option>
            <option value="Inactive">Inativo</option>
            <option value="Suspended">Suspenso</option>
          </select>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Função *
          </label>
          <select
            value={formData.role}
            onChange={(e) => setFormData(prev => ({ ...prev, role: e.target.value }))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="Customer">Cliente</option>
            <option value="Manager">Gerente</option>
            <option value="Admin">Administrador</option>
          </select>
        </div>
      </div>

      {/* Senhas (apenas para criação) */}
      {!user && (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Senha *
            </label>
            <input
              type="password"
              value={formData.password}
              onChange={(e) => setFormData(prev => ({ ...prev, password: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.password ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Mínimo 6 caracteres"
            />
            {errors.password && <p className="text-red-500 text-xs mt-1">{errors.password}</p>}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Confirmar Senha *
            </label>
            <input
              type="password"
              value={formData.confirmPassword}
              onChange={(e) => setFormData(prev => ({ ...prev, confirmPassword: e.target.value }))}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                errors.confirmPassword ? 'border-red-300' : 'border-gray-300'
              }`}
              placeholder="Confirme a senha"
            />
            {errors.confirmPassword && <p className="text-red-500 text-xs mt-1">{errors.confirmPassword}</p>}
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
          {isLoading ? 'Salvando...' : user ? 'Atualizar' : 'Criar'}
        </button>
      </div>
    </form>
  );
};

export default UserForm; 