# .NET Core - Tournament Manager - Torneio de Luta
[![Build status](https://ci.appveyor.com/api/projects/status/cn2mtmkok7w6rg25?svg=true)](https://ci.appveyor.com/project/gcoppini/tournamentmanager)

[![Build status](https://ci.appveyor.com/api/projects/status/cn2mtmkok7w6rg25/branch/master?svg=true)](https://ci.appveyor.com/project/gcoppini/tournamentmanager/branch/master)

Arquitetura teste para avaliação



![Tela incial](https://github.com/gcoppini/TournamentManager/blob/master/screen_inicio.png)

![Tela Resultados](https://github.com/gcoppini/TournamentManager/blob/master/screen_result.png)

## Para executar a solução
## Via shell
- git clone https://github.com/gcoppini/TournamentManager.git
- dotnet restore
- dotnet build
- dotnet run

## Para executar os testes unitários
- dotnet run test

## Para debugar os testes unitários
- export VSTEST_HOST_DEBUG=1
- dotnet run test
- Atachar VSCode no PID exibido

## Ambiente de desenvolvimento
- Linux 
3.16.0-5-amd64 #1 SMP Debian 3.16.51-3+deb8u1 (2018-01-08) x86_64 GNU/Linux

- Google Chrome 
Versão 74.0.3729.131 (Versão oficial) 64 bits

- Visual Studio Code 
Versão: Commit de 1.33.1: 51b0b28134d51361cf996d2f0a1c698247aeabd8 Data: 2019-04-11T08:20:22.771Z elétron: 3.1.6 Chrome: 66.0.3359.181 node. js: 10.2.0 V8: 6.6.346.32 OS: Linux x64 3.16.0-5-amd64

## Tecnologias
- .NET Core 2.1
- Website: MVC, C#

## Observações de design
- Descrever abordagem /
https://en.wikipedia.org/wiki/Tournament


## WIP - Work in progress
Padrões
- DDD - Domain Drive Design 
- DTO, Injeção de dependência, Repository Pattern, SOLID, CQS
- Design Patterns: Strategy
- Tratamento exceções
- Tratamento de concurrência
- Análise Thread-safe, Async, IDisposable, Logs, Complexidade ciclomática

Testes
- Mocked Unit Tests, Testes de integração, carga, user interfaces, test coverage