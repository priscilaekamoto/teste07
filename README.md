Author: Priscila Cristina de Oliveira Vasconcelos Ekamoto

# Sistema de Controle de Gastos Residenciais

## Visão Geral

Este projeto tem como objetivo o desenvolvimento de um **sistema de controle de gastos residenciais**, permitindo o cadastro de pessoas, categorias e transações financeiras (receitas e despesas), bem como a visualização de totais e saldos consolidados. A aplicação será construída de forma modular, com separação clara entre **Back-end (Web API)** e **Front-end**, seguindo boas práticas de desenvolvimento e arquitetura.

O sistema foi projetado para atender integralmente às regras de negócio especificadas no teste técnico, priorizando clareza, legibilidade do código, documentação e aderência às tecnologias solicitadas.

---

## Arquitetura da Solução

A aplicação será dividida em dois principais módulos:

* **Back-end (Web API)**

  * Desenvolvido em **C# com .NET**
  * Responsável pelas regras de negócio, validações e persistência de dados
  * Exposição de endpoints REST para consumo pelo front-end

* **Front-end (Web)**

  * Desenvolvido em **React com TypeScript**
  * Responsável pela interface do usuário e consumo da API
  * Comunicação com o back-end via HTTP

Os dados serão persistidos em um banco de dados mysql:8.0.31, garantindo que as informações sejam mantidas mesmo após a reinicialização do sistema.

---

## Funcionalidades

### Cadastro de Pessoas

Permite o gerenciamento básico de pessoas, incluindo:

* Criação
* Listagem
* Deleção

Cada pessoa conterá os seguintes atributos:

* **Identificador**: valor único gerado automaticamente
* **Nome**: texto
* **Idade**: número inteiro positivo

> Regra de negócio: Ao deletar uma pessoa, todas as transações associadas a ela serão automaticamente removidas, garantindo consistência dos dados.

---

### Cadastro de Categorias

Permite o cadastro e listagem de categorias financeiras, utilizadas para classificar transações.

Cada categoria conterá:

* **Identificador**: valor único gerado automaticamente
* **Descrição**: texto
* **Finalidade**: despesa, receita ou ambas

As categorias serão utilizadas para restringir o tipo de transação que pode ser associada a elas.

---

### Cadastro de Transações

Permite o registro e listagem de transações financeiras.

Cada transação conterá:

* **Identificador**: valor único gerado automaticamente
* **Descrição**: texto
* **Valor**: número decimal positivo
* **Tipo**: despesa ou receita
* **Categoria**: vinculada ao cadastro de categorias
* **Pessoa**: identificador da pessoa associada

#### Regras de Negócio

* Pessoas **menores de 18 anos** poderão registrar **apenas despesas**.
* A categoria selecionada deverá ser compatível com o tipo da transação:

  * Transações do tipo **despesa** não poderão utilizar categorias com finalidade **receita**.
  * Transações do tipo **receita** não poderão utilizar categorias com finalidade **despesa**.

Todas essas validações serão tratadas no back-end, garantindo a integridade das informações.

---

## Consultas e Relatórios

### Totais por Pessoa

Será disponibilizada uma consulta que:

* Liste todas as pessoas cadastradas
* Exiba para cada pessoa:

  * Total de receitas
  * Total de despesas
  * Saldo (receitas – despesas)

Ao final da listagem, será apresentado um **resumo geral**, contendo:

* Total geral de receitas
* Total geral de despesas
* Saldo líquido consolidado

---

### Totais por Categoria (Opcional)

Consulta opcional que:

* Liste todas as categorias cadastradas
* Exiba para cada categoria:

  * Total de receitas
  * Total de despesas
  * Saldo (receitas – despesas)

Ao final, será exibido o total geral consolidado de todas as categorias.

---

## Boas Práticas e Qualidade

O projeto será desenvolvido com foco em:

* Aderência às regras de negócio
* Código limpo, organizado e legível
* Uso de boas práticas do **.NET** e **React**
* Separação de responsabilidades
* Comentários explicativos e documentação no próprio código

Essa abordagem garante facilidade de manutenção, entendimento da solução e escalabilidade futura.

---

## Considerações Finais

Esta aplicação visa demonstrar domínio técnico em desenvolvimento Full Stack, atenção aos detalhes e capacidade de traduzir requisitos de negócio em uma solução funcional, bem estruturada e de fácil compreensão.


## Passos para rodar a aplicação
Para subir o banco de dados mysql:8.0.31:
- Entre na pasta docker
- Execute o comando docker compose up -d