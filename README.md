# Swap test

Serviço que recupera todas as issues de um determinado repositório no github e retorna um JSON assincronamente (1 dia de diferença) via webhook com  as issues e contribuidores que existiam no projeto no momento da chamada.

# Request
  nome_usuario      string,
  nome_repositorio  string
  

# Objeto webhook

{ user: nome_usuario
  repository: nome_repositorio
  issues: [
    {title, author and labels},
    {title, author and labels},
    ...
  ],
  contributors: [
    {name, user, qtd_commits},
    {name, user, qtd_commits},
    ....
  ]

 }

# Inicializar a Aplicação

 - IDE para .netcore 3.1
 - Restore nuget packages
 - Para rodar a aplicação é necessário ter instalado localmente o MongoDB  (https://www.mongodb.com/docs/manual/administration/install-community/)
    - Será necessário setar a connectionstring e databasename  no appsettings dos projetos 
      - Swap.GithubTracker.Services.Api
      - Swap.GithubTracker.Services.Worker
          - MongoConnectionString: connectionstring mongo
          - NomeDataBase: databasename criado
          - WebhookUrl: Url para qual a resposta deve ser enviada passadas X horas
          - DiffIntervalHours: X horas de diferença para envio da resposta
- Para o Worker Swap.GithubTracker.Services.Worker temos uma configuração adicional no  appsettings
          - Active: Ativa ou desativa o worker
          - IntervalMinutes: Periodicidade na qual o worker verifica se há algum dado para ser enviado ao Webhook
