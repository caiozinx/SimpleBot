Implementar o Bot SQL usando o projeto do dia 10/Ago como base. Nesse projeto, implementar:
* repositório de acesso a dados: Mock, SQL e MongoDB
* configurar a injeção de dependência dos serviços
1) carregar o repositório SQL quando houver configuração SQL
2) carregar o repositório MongoDB quando houver configuração MongoDB 
3) carregar o repositório Mock quando não houver nenhuma configuração
* usar as bibliotecas SqlClient com Dapper ou Entity Framework
* usar a biblioteca padrão do MongoDB

secrets.json
  {
    "MongoDB": {
      "ConnectionString": "mongodb://localhost:27017"
    },
    "ConnectionStrings": {
      "SimpleBotCore": "Server=localhost;Database=SimpleBotCore;User=SA;Password=yourpasswd"
    }
  }

appSettings.json

  "DatabaseFlag": "M"  = MongoDB
  "DatabaseFlag": "S"  = SqlServer
  "DatabaseFlag": ""   = Mock
