# PeopleImportAPI
Test API

## Requisitos
- Desenvolver API usando Tecnologias: .NET 8, MongoDB, Bearer Token e Container Docker Linux.
- Usar Padrão de arquitetura Hexagonal.
- Sistema deve ter um endpoint para importar arquivos Excel(implementado csv).
- Arquivo importado deve ter no minimo 10.000 itens.
- Arquivo deve conter os seguintes campos (nome,CPF,endereço,cidade, estado, DDD e telefone).
- Os dados extraidos do arquivo devem ser salvos no MongoDB.
- Sistema deve ter um endpoint para consultar os status de processamento e conclusão dos eventos de importação.
- Sistema deve gerar token para autenticação usando JWT.
- Sistema deve ter Testes unitários com mock da camada de dados do produto.
- Sistema deve ter relatorio diario dos dados processados.

Requisitos não atingidos:
- Relatório de dados
- Conexão com Mongo usando Docker

# Como executar o projeto:

## Requisitos: MongoDb instalado e VisualStudio ou VSCode

  1 - Faça o restore do database do mongo localizado na pasta dump;

  2 - Execute o arquivo Secret.Bat para inserir as secrets do produto;

  3 - Abra a IDE de preferência e execute o sistema;

  4 - Use a collection do postman para validar as requisições;
