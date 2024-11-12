# Desafio Api

Após executar o tutorial do Readme do Desafio Stack [https://github.com/eduardo-arce/desafio-stack](https://github.com/eduardo-arce/desafio-stack), voce deve acessar a raíz do projeto desafio-api, via CMD ou Power Shell, e executar os seguintes comandos:

## Instalar o dot-ef

```sh
dotnet tool install --global dotnet-ef
```

## Executar as Migrations
Gerar a tabela de Usuários e e popular a tabela de Usuários
```sh
dotnet ef migrations add DesafioProject --project Desafio.Infra --startup-project Desafio.Api --context DesafioContext

dotnet ef database update --project Desafio.Infra --startup-project Desafio.Api --context DesafioContext
```

## Observação

Essa configuração das migrations devem ser feitas via docker, por meio do dockerfile do backend, mas não consegui configurar o docker-compose, pois ele tentava criar e pupular as tabelas no banco, antes mesmo do banco ser gerado, o que causava uma quebra total.
Essa foi uma maneira alternativa que encontrei para conseguir entregar o desafio a tempo.
