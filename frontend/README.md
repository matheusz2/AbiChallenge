# ABI Challenge - Frontend

Frontend desenvolvido em React TypeScript com Vite para o projeto ABI Challenge.

## Tecnologias Utilizadas

- **React 18** - Biblioteca JavaScript para construção de interfaces
- **TypeScript** - Superset do JavaScript com tipagem estática
- **Vite** - Build tool rápido e moderno
- **Tailwind CSS** - Framework CSS utilitário
- **React Router DOM** - Roteamento para React
- **Axios** - Cliente HTTP para comunicação com a API
- **Lucide React** - Ícones modernos

## Funcionalidades

### Autenticação
- Login/logout com JWT
- Proteção de rotas
- Gerenciamento de sessão

### Gestão de Usuários
- CRUD completo de usuários
- Listagem paginada
- Filtros por status e função
- Validação de formulários

### Gestão de Produtos
- CRUD completo de produtos
- Listagem paginada
- Filtros por categoria
- Upload de imagens
- Gerenciamento de categorias

### Gestão de Vendas
- CRUD completo de vendas
- Listagem paginada
- Cálculo automático de descontos
- Informações de cliente e filial

### Gestão de Carrinhos
- CRUD completo de carrinhos
- Listagem paginada
- Cálculo de totais
- Gerenciamento de itens

### Dashboard
- Métricas principais
- Ações rápidas
- Visão geral do sistema

## Configuração e Execução

### Pré-requisitos
- Node.js (versão 16 ou superior)
- npm ou yarn
- Backend da aplicação rodando

### Instalação

```bash
# Instalar dependências
npm install

# Executar em modo desenvolvimento
npm run dev

# Build para produção
npm run build

# Preview do build
npm run preview
```

### Configuração da API

A URL da API está configurada em `src/config/api.ts`:

```typescript
export const API_CONFIG = {
  BASE_URL: 'https://localhost:7181/api',
  TIMEOUT: 10000,
};
```

### Problema de CORS Resolvido

O problema de CORS foi resolvido adicionando a configuração no backend (`Program.cs`):

```csharp
// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Usar CORS antes de outros middlewares
app.UseCors("AllowFrontend");
```

### Executando Backend e Frontend

1. **Backend** (em um terminal):
```bash
cd backend
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

2. **Frontend** (em outro terminal):
```bash
cd frontend
npm run dev
```

3. **Acessar a aplicação**:
   - Frontend: http://localhost:5173
   - Backend API: https://localhost:7181/api
   - Swagger: https://localhost:7181/swagger

## Credenciais de Teste

Para testar a aplicação, use as seguintes credenciais:

- **Email**: admin@test.com
- **Senha**: Test@123

## Estrutura do Projeto

```
src/
├── components/          # Componentes reutilizáveis
│   ├── Layout/         # Layout principal
│   ├── Pagination/     # Componente de paginação
│   └── ProtectedRoute/ # Proteção de rotas
├── config/             # Configurações
│   └── api.ts          # Configuração da API
├── contexts/           # Contextos do React
│   └── AuthContext.tsx # Contexto de autenticação
├── pages/              # Páginas da aplicação
│   ├── Dashboard/      # Dashboard principal
│   ├── Login/          # Página de login
│   ├── Users/          # Gestão de usuários
│   ├── Products/       # Gestão de produtos
│   ├── Sales/          # Gestão de vendas
│   └── Carts/          # Gestão de carrinhos
├── services/           # Serviços de API
│   └── api.ts          # Cliente HTTP e serviços
├── types/              # Definições de tipos
│   └── api.ts          # Tipos da API
└── App.tsx             # Componente principal
```

## Funcionalidades Implementadas

### ✅ Autenticação
- [x] Login com JWT
- [x] Logout
- [x] Proteção de rotas
- [x] Interceptor de requisições

### ✅ Usuários
- [x] Listagem paginada
- [x] Criar usuário
- [x] Editar usuário
- [x] Excluir usuário
- [x] Visualizar detalhes

### ✅ Produtos
- [x] Listagem paginada
- [x] Criar produto
- [x] Editar produto
- [x] Excluir produto
- [x] Filtrar por categoria
- [x] Gerenciar categorias

### ✅ Vendas
- [x] Listagem paginada
- [x] Criar venda
- [x] Editar venda
- [x] Excluir venda
- [x] Visualizar detalhes

### ✅ Carrinhos
- [x] Listagem paginada
- [x] Criar carrinho
- [x] Editar carrinho
- [x] Excluir carrinho
- [x] Visualizar detalhes

### ✅ Interface
- [x] Design responsivo
- [x] Navegação intuitiva
- [x] Loading states
- [x] Tratamento de erros
- [x] Feedback visual

## Troubleshooting

### Erro de CORS
Se você encontrar erros de CORS, verifique se:
1. O backend está rodando
2. A configuração de CORS foi adicionada no `Program.cs`
3. As URLs estão corretas

### Erro de Autenticação
Se houver problemas de autenticação:
1. Verifique se o token JWT está sendo enviado
2. Confirme as credenciais de teste
3. Verifique se o backend está processando a autenticação

### Problemas de Conexão
Se não conseguir conectar com a API:
1. Verifique se o backend está rodando na porta correta
2. Confirme a URL da API em `src/config/api.ts`
3. Verifique se não há problemas de certificado SSL

## Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature
3. Commit suas mudanças
4. Push para a branch
5. Abra um Pull Request

## Licença

Este projeto está sob a licença MIT.
