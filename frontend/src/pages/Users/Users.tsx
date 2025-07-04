import React, { useState, useEffect } from 'react';
import { Users as UsersIcon, Plus, Edit, Trash2, Eye, Search, Filter } from 'lucide-react';
import { userService } from '../../services/api';
import type { User, CreateUserRequest, UpdateUserRequest } from '../../types/api';
import Pagination from '../../components/Pagination/Pagination';
import Modal from '../../components/Modal/Modal';
import UserForm from './UserForm';

const Users: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState<string>('');
  const [roleFilter, setRoleFilter] = useState<string>('');

  // Estados dos modais
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const pageSize = 10;

  const fetchUsers = async (page: number = 1) => {
    try {
      setLoading(true);
      setError(null);
      
      const response = await userService.getAll({
        page,
        pageSize,
        sortBy: 'createdAt',
        sortDescending: true
      });

      console.log('Users response:', response);
      
      if (response.data && Array.isArray(response.data)) {
        setUsers(response.data);
        setCurrentPage(1);
        setTotalPages(1);
        setTotalCount(response.data.length);
      } else if (response.data && typeof response.data === 'object' && 'data' in response.data) {
        const paginatedData = response.data as any; // Forçar tipo para evitar erro de linter
        setUsers(paginatedData.data || []);
        setCurrentPage(paginatedData.currentPage || 1);
        setTotalPages(paginatedData.totalPages || 1);
        setTotalCount(paginatedData.totalCount || 0);
      }
    } catch (err) {
      console.error('Erro ao buscar usuários:', err);
      setError('Erro ao carregar usuários');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers(currentPage);
  }, [currentPage]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const handleCreateUser = async (userData: CreateUserRequest) => {
    try {
      setIsSubmitting(true);
      await userService.create(userData);
      setShowCreateModal(false);
      fetchUsers(currentPage);
    } catch (err) {
      console.error('Erro ao criar usuário:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleUpdateUser = async (userData: UpdateUserRequest) => {
    try {
      setIsSubmitting(true);
      await userService.update(userData.id, userData);
      setShowEditModal(false);
      setSelectedUser(null);
      fetchUsers(currentPage);
    } catch (err) {
      console.error('Erro ao atualizar usuário:', err);
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDeleteUser = async (userId: string) => {
    if (!window.confirm('Tem certeza que deseja excluir este usuário?')) {
      return;
    }

    try {
      await userService.delete(userId);
      fetchUsers(currentPage);
    } catch (err) {
      console.error('Erro ao excluir usuário:', err);
      alert('Erro ao excluir usuário');
    }
  };

  const handleViewUser = (user: User) => {
    console.log('👀 Visualizando usuário:', user);
    console.log('📅 Datas:', {
      createdAt: user.createdAt,
      updatedAt: user.updatedAt,
      createdAtType: typeof user.createdAt,
      updatedAtType: typeof user.updatedAt
    });
    setSelectedUser(user);
    setShowViewModal(true);
  };

  const handleEditUser = (user: User) => {
    setSelectedUser(user);
    setShowEditModal(true);
  };

  const getStatusLabel = (status: string) => {
    switch (status.toLowerCase()) {
      case 'active': return 'Ativo';
      case 'inactive': return 'Inativo';
      case 'suspended': return 'Suspenso';
      default: return status || 'Desconhecido';
    }
  };

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case 'active': return 'bg-green-100 text-green-800';
      case 'inactive': return 'bg-red-100 text-red-800';
      case 'suspended': return 'bg-yellow-100 text-yellow-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  };

  const getRoleLabel = (role: string) => {
    switch (role.toLowerCase()) {
      case 'customer': return 'Cliente';
      case 'manager': return 'Gerente';
      case 'admin': return 'Administrador';
      default: return role || 'Desconhecido';
    }
  };

  const filteredUsers = users.filter(user => {
    const matchesSearch = user.username.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         user.email.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesStatus = statusFilter === '' || user.status.toLowerCase() === statusFilter.toLowerCase();
    const matchesRole = roleFilter === '' || user.role.toLowerCase() === roleFilter.toLowerCase();
    
    return matchesSearch && matchesStatus && matchesRole;
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
          <UsersIcon className="h-8 w-8 text-indigo-600 mr-3" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Usuários</h1>
            <p className="text-sm text-gray-600">Gerenciar usuários do sistema</p>
          </div>
        </div>
        <button
          onClick={() => setShowCreateModal(true)}
          className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-md flex items-center"
        >
          <Plus className="h-4 w-4 mr-2" />
          Novo Usuário
        </button>
      </div>

      {/* Filtros */}
      <div className="bg-white p-4 rounded-lg shadow mb-6">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
            <input
              type="text"
              placeholder="Buscar usuários..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>
          
          <select
            value={statusFilter}
            onChange={(e) => setStatusFilter(e.target.value)}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Todos os Status</option>
            <option value="active">Ativo</option>
            <option value="inactive">Inativo</option>
            <option value="suspended">Suspenso</option>
          </select>

          <select
            value={roleFilter}
            onChange={(e) => setRoleFilter(e.target.value)}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Todas as Funções</option>
            <option value="customer">Cliente</option>
            <option value="manager">Gerente</option>
            <option value="admin">Administrador</option>
          </select>

          <div className="flex items-center text-sm text-gray-600">
            <Filter className="h-4 w-4 mr-2" />
            {filteredUsers.length} de {totalCount} usuários
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
                  Usuário
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Email
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Telefone
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Status
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Função
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Ações
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {filteredUsers.map((user) => (
                <tr key={user.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">{user.username}</div>
                    <div className="text-sm text-gray-500">ID: {user.id.substring(0, 8)}...</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{user.email}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{user.phone}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(user.status)}`}>
                      {getStatusLabel(user.status)}
                    </span>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm text-gray-900">{getRoleLabel(user.role)}</div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div className="flex space-x-2">
                      <button
                        onClick={() => handleViewUser(user)}
                        className="text-indigo-600 hover:text-indigo-900"
                        title="Visualizar"
                      >
                        <Eye className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleEditUser(user)}
                        className="text-green-600 hover:text-green-900"
                        title="Editar"
                      >
                        <Edit className="h-4 w-4" />
                      </button>
                      <button
                        onClick={() => handleDeleteUser(user.id)}
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

          {filteredUsers.length === 0 && (
            <div className="text-center py-12">
              <UsersIcon className="mx-auto h-12 w-12 text-gray-400" />
              <h3 className="mt-2 text-sm font-medium text-gray-900">Nenhum usuário encontrado</h3>
              <p className="mt-1 text-sm text-gray-500">
                {searchTerm || statusFilter !== '' || roleFilter !== ''
                  ? 'Tente ajustar os filtros de busca.'
                  : 'Comece criando um novo usuário.'}
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

      {/* Modal Criar Usuário */}
      <Modal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        title="Criar Novo Usuário"
        size="lg"
      >
        <UserForm
          onSubmit={handleCreateUser}
          onCancel={() => setShowCreateModal(false)}
          isLoading={isSubmitting}
        />
      </Modal>

      {/* Modal Editar Usuário */}
      <Modal
        isOpen={showEditModal}
        onClose={() => {
          setShowEditModal(false);
          setSelectedUser(null);
        }}
        title="Editar Usuário"
        size="lg"
      >
        {selectedUser && (
          <UserForm
            user={selectedUser}
            onSubmit={handleUpdateUser}
            onCancel={() => {
              setShowEditModal(false);
              setSelectedUser(null);
            }}
            isLoading={isSubmitting}
          />
        )}
      </Modal>

      {/* Modal Visualizar Usuário */}
      <Modal
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedUser(null);
        }}
        title="Detalhes do Usuário"
        size="lg"
      >
        {selectedUser && (
          <div className="space-y-6">
            {/* Informações Básicas */}
            <div>
              <h3 className="text-lg font-medium text-gray-900 mb-3">Informações Básicas</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">Nome de usuário</label>
                  <p className="mt-1 text-sm text-gray-900">{selectedUser.username}</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Email</label>
                  <p className="mt-1 text-sm text-gray-900">{selectedUser.email}</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Telefone</label>
                  <p className="mt-1 text-sm text-gray-900">{selectedUser.phone || 'Não informado'}</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Status</label>
                  <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(selectedUser.status)}`}>
                    {getStatusLabel(selectedUser.status)}
                  </span>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Função</label>
                  <p className="mt-1 text-sm text-gray-900">{getRoleLabel(selectedUser.role)}</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">ID</label>
                  <p className="mt-1 text-sm text-gray-900 font-mono">{selectedUser.id}</p>
                </div>
              </div>
            </div>

            {/* Nome Completo */}
            {selectedUser.name && (selectedUser.name.firstname || selectedUser.name.lastname) && (
              <div>
                <h3 className="text-lg font-medium text-gray-900 mb-3">Nome Completo</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Primeiro Nome</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.name.firstname || 'Não informado'}</p>
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Sobrenome</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.name.lastname || 'Não informado'}</p>
                  </div>
                </div>
              </div>
            )}

            {/* Endereço */}
            {selectedUser.address && (
              <div>
                <h3 className="text-lg font-medium text-gray-900 mb-3">Endereço</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Cidade</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.address.city || 'Não informado'}</p>
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Rua</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.address.street || 'Não informado'}</p>
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700">Número</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.address.number || 'Não informado'}</p>
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700">CEP</label>
                    <p className="mt-1 text-sm text-gray-900">{selectedUser.address.zipcode || 'Não informado'}</p>
                  </div>
                  {selectedUser.address.geolocation && (selectedUser.address.geolocation.lat || selectedUser.address.geolocation.long) && (
                    <>
                      <div>
                        <label className="block text-sm font-medium text-gray-700">Latitude</label>
                        <p className="mt-1 text-sm text-gray-900">{selectedUser.address.geolocation.lat || 'Não informado'}</p>
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-700">Longitude</label>
                        <p className="mt-1 text-sm text-gray-900">{selectedUser.address.geolocation.long || 'Não informado'}</p>
                      </div>
                    </>
                  )}
                </div>
              </div>
            )}

            {/* Informações do Sistema */}
            <div>
              <h3 className="text-lg font-medium text-gray-900 mb-3">Informações do Sistema</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">Criado em</label>
                  <p className="mt-1 text-sm text-gray-900">
                    {selectedUser.createdAt && !isNaN(new Date(selectedUser.createdAt).getTime()) 
                      ? new Date(selectedUser.createdAt).toLocaleString('pt-BR')
                      : 'Não informado'
                    }
                  </p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">Atualizado em</label>
                  <p className="mt-1 text-sm text-gray-900">
                    {selectedUser.updatedAt && !isNaN(new Date(selectedUser.updatedAt).getTime())
                      ? new Date(selectedUser.updatedAt).toLocaleString('pt-BR')
                      : 'Não informado'
                    }
                  </p>
                </div>
              </div>
            </div>
            
            <div className="flex justify-end space-x-3 pt-4 border-t">
              <button
                onClick={() => {
                  setShowViewModal(false);
                  setSelectedUser(null);
                }}
                className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
              >
                Fechar
              </button>
              <button
                onClick={() => {
                  setShowViewModal(false);
                  handleEditUser(selectedUser);
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

export default Users; 