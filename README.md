# 📦 Mottu Locação de Motos - API

Este projeto contém uma API desenvolvida em .NET 9, utilizando PostgreSQL como banco de dados e RabbitMQ para mensageria, todos orquestrados via Docker Compose.

---

## 🧱 Tecnologias Utilizadas

- ASP.NET Core 9
- Docker & Docker Compose
- PostgreSQL 14
- RabbitMQ 3.13 Management
- Swagger (via Swashbuckle)

---

## ⚙️ Pré-requisitos

Certifique-se de que os seguintes itens estejam instalados na sua máquina:

- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

---

## 🚀 Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/mottu-locacao-motos.git
cd mottu-locacao-motos

2. Suba os containers com Docker Compose
bash
docker-compose up --build
Aguarde até que todos os containers estejam executando.

🔍 Acessando a API
Depois que os serviços estiverem em execução, acesse o Swagger UI em:

📍 http://localhost:5000/swagger


---

API REST para gerenciamento de locações de motos, entregadores, autenticação e controle de veículos.

## 🔐 Autenticação

### `POST /api/auth/logar`
Autentica um usuário.

- **Body**: `UsuarioDto`
  - `email`: string
  - `perfil`: `Admin` | `Entregador`

- **Respostas**:
  - `200 OK`
  - `400 Bad Request`

  ## 🔒 Autenticação 

- Utilizar token JWT no cabeçalho usando a palavra 'BEARER" antes do token

-