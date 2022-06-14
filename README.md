# Swap test

Serviço que recupera todas as issues de um determinado repositório no github e retorna um JSON assincronamente (1 dia de diferença) via webhook com  as issues e contribuidores que existiam no projeto no momento da chamada.

# Request
  nome_usuario      string,
  nome_repositorio  string
  

# Response

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
