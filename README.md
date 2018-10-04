# CalculadorDeJuros
Uma Web API ASP.NET Core com documentação em [Swagger](https://swagger.io/).
O principal objetivo do projeto foi praticar alguns conceitos como:

- Design patterns (Dependency Injection, Singleton, Intercptor, DRY, SOLID, KISS, YAGNI;
- Desenvolvimento de custom middlewares em [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/);
- Desenvolvimento de custom Attributes em [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/);
- Desenvolvimento de custom Filters em [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/);
- Testes de integração com [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/);
  

## Pré requisitos
 - [ ] [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/) 2.1;
 - [ ] Alguma IDE de desenvolvimento. **Que tal** o [VS Code](https://code.visualstudio.com/);

## Instalação
 1. Faça o clone deste projeto com `git clone https://github.com/xitaocrazy/CalculadorDeJuros.git`

## Executando o projeto

 1. Acesse o diretório do projeto via prompt de comando. Ex: `cd D:\GitHub\CalculadorDeJuros\CalculadorDeJurosApi`;
 2. Execute o projeto com `dotnet run"`;
![executando_o_projeto](https://github.com/xitaocrazy/CalculadorDeJuros/blob/master/Imagens/executando_o_projeto.png)
 3. Acesse a URL http://localhost:5000/;
![calculador_de_juros](https://github.com/xitaocrazy/CalculadorDeJuros/blob/master/Imagens/calculador_de_juros.png)

Agora você já pode testar os serviços da aplicação.

## Executando os testes unitários

 1. Acesse o diretório do projeto de testes unitários via prompt de comando. Ex: `cd D:\GitHub\CalculadorDeJuros\CalculadorDeJurosApiTests`;
 2. Execute o projeto com `dotnet test"`;
![testes_unitarios](https://github.com/xitaocrazy/CalculadorDeJuros/blob/master/Imagens/testes_unitarios.png)

## Executando os testes de integração

 1. Acesse o diretório do projeto de testes de integração via prompt de comando. Ex: `cd D:\GitHub\CalculadorDeJuros\CalculadorDeJurosApiIntegrationTests`;
 2. Execute o projeto com `dotnet test"`;
![testes_de_integracao](https://github.com/xitaocrazy/CalculadorDeJuros/blob/master/Imagens/testes_de_integracao.png)

## Autor
Daniel de Souza Martins - [Linkedin](https://www.linkedin.com/in/daniel-de-souza-martins/) - [GitHub](https://github.com/xitaocrazy).

## Licensa
Este projeto está licenciado sob a licença MIT - veja o [https://github.com/xitaocrazy/CalculadorDeJuros/blob/master/LICENSE](arquivo de licença) para detalhes.