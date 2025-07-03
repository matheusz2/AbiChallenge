# 🟦 Sprint Backend - Dias 1 a 4

## 🎯 Objetivo
Implementar o backend em .NET 8 com PostgreSQL, cobrindo CRUD completo para vendas e aplicando regras de negócio e eventos.

---

## 📌 Entidades

- Sale
- SaleItem
- Referências externas apenas por ID:
  - Customer
  - Branch
  - Product

---

## ✅ Tarefas

---

### B1.1 - Modelagem de Domínio
- Estruturar entidades Sale e SaleItem
- Configurar o DbContext
- Mapear relacionamentos

---

### B1.2 - CRUD Sales
- `GET /sales`
- `GET /sales/{id}`
- `POST /sales`
- `PUT /sales/{id}`
- `DELETE /sales/{id}`
- Utilizar AutoMapper para DTOs

---

### B1.3 - Regras de Negócio
- Descontos automáticos
  - 4+ itens → 10%
  - 10–20 itens → 20%
  - Máximo 20 itens
  - Sem desconto <4
- Validar regras no POST e PUT
- Cobrir regras com testes unitários (xUnit)

---

### B1.4 - Eventos
- Simular via logs:
  - SaleCreated
  - SaleModified
  - SaleCancelled
  - ItemCancelled
- Utilizar ILogger

---

### B1.5 - Swagger
- Configurar Swagger
- Documentar exemplos de request/response

---

### B1.6 - Testes
- Testes unitários das regras de desconto
- Testes de integração mínima para endpoints

---

## Critérios de aceite
- CRUD de vendas 100% funcional
- Regras de negócio aplicadas
- Swagger documentado
- Logs de eventos operacionais
- Banco gerado por migration
- Testes unitários mínimos cobrindo regras
