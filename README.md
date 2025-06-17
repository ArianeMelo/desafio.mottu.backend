# Mottu.Locacao.Motos

Este projeto faz o gerenciamento de locações e cadastros de veículos.

Tecnologias utilizadas: .NE 9, Dapper, PostgreSQL, RabbitMQ.

✅ Pré-requisitos
- Docker instalado
- PowerShell (ou outro terminal)

🚀 Subindo o ambiente
1. Abra o PowerShell na raiz do projeto

OBS: Pode Forçar remoção dos volumes (opcional, mas recomendado na 1ª vez)
powershell > Copiar > Colar :
docker-compose down -v
 
2. Subir os containers
powershell > Copiar > Colar : 
docker-compose up --build

Esse comando:
-Sobe o RabbitMQ
-Sobe o PostgreSQL
-Executa os scripts de banco (init.sql)
-Sobe a API ASP.NET Core

🔍 Acessos e credenciais
Serviço	URL / Porta	Acesso
API	http://localhost:5000	—
RabbitMQ	http://localhost:15672	admin / admin
PostgreSQL	localhost:5432	admin / admin123 (via app)


🧾 Observações finais
O arquivo init.sql roda automaticamente na primeira criação do container PostgreSQL.

Se precisar reexecutar os scripts, use docker-compose down -v.

A aplicação só sobe após banco e Rabbit estarem prontos, garantindo a ordem correta.