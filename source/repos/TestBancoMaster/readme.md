# Orientações
## Clone
Clonar [repositório](https://github.com/CBordini/TestBancoMaster.git) para maquina local.

## Build
### Após o clone  caso necessário.
#### Executar a limpeza da solução seguindo os seguintes passos:
1. Clicar com o botão direito na solução.
2. Compilação em Lotes...
3. Na janela que se abrirá clicar Selecionar todos.
4. Então clicar em Limpar.
5. Repetir o processo até o passo 3.
6. Então clicar em Recompilar.
## Testes
### Após o build completo, seguir para os testes.
1. Clicar no menu Testes.
2. Clicar na aba Windows.
3. Clicar em Gerenciador de Testes.
4. Na janela que se abrirá, executar os teste com Ctrl+R,A ou clicar na seta de play com a legenda: Executar todos.

Após essa execução é esperado que todos os testes fiquem com o icone verde de sucesso.

## Executando a aplicação
### Após a execução dos teste, executar a aplicação seguindo os seguintes passos.
#### Caso a solução "TestBancoMaster" não estiver selecionado, seguir os seguintes passos:
1. Clicar com o botão direito na Solução "TestBancoMaster" depois em "Configurar Projetos de Inicialização..." .
2. Em "Propriedades comuns" na aba "Projeto de Inicialização" selecionar a opção "Varios projetos de inicialização".
3. Selecionar a opção "Iniciar" na coluna "Ação" nos projetos "TestBancoMaster.ConsoleApp" e "TestBancoMaster.Api", clicar em "Aplicar" e depois em "OK".
#### Após a verificação, seguir os seguites passos:
Precionar a teclar F5 ou clicar em Iniciar no menu superior.
Realizar a criação do arquivo rotas.csv em diretório local, o arquivo deve ser criado no padrão, origem,destino,valor passando diretamente os valores sem cabeçalho conforme exemplo enviado:

| :---         |
| GRU,BRC,10  | 
| BRC,SCL,5    | 
| GRU,CDG,75   | 
| GRU,SCL,20   | 
| GRU,ORL,56   | 
| ORL,CDG,5   | 
| SCL,ORL,20    | 


Ao dar inicio na aplicação ambas aplicações(console App e Api Rest) serão iniciadas simutaneamente.

Na primeira linha de comando na console app informar o diretório onde está o orquivo .csv.

Informar apenas o caminho sem a necessidade de informar o nome do arquivo.

Após infomar o diretorio, será solicitado a rota para consulta da rota com o menor valor, após o retorno da consulta apertar qualquer tecla para realizar uma nova consulta.

Aplicação console app só ira parar depois de encerrada.

Aplicação Rest está disponivel via swagger para realização de cadastro de novas rotas no endpoint post

[
  {
    "origin": "string",
    "destination": "string",
    "value": 0
  }
]

E para consultas no endpoint get informando origem e destino.

# Sobre o projeto
## Estrutura dos arquivos/pacotes
O projeto está separado da seguinte forma:
### 01-Console App
Aplicação console que tem como finalidade fazer a intermediação entre o usuário e o sistema.
### 02-Api
Aplicação Rest que tem como finalidade realizar o cadastro de novas rotas e a consulta do rota com o menor valor.
### 03-Services
Projeto contendo as regras para adicionar novas rotas e gerar a consultas da rotas mais barata.
### 04-Domain Model	
Essa camada pode ser considerada o core da aplicação, aonde possui todas as classes, interfaces e objetos de transferência.
### 05-Infra
Projeto contendo repositorio que realiza a leitura e escrita no arquivo csv.
### 06-Tests	
Projeto de tests unitarios.

# Design Adotadas para a solução
Foi realizado uma modelagem baseada no DDD(Domain Driven Design) dividindo as responsabilidades em camadas, utilizando boas praticas de desenvolvimento e os pricipios do SOLID,utilizando inversão de dependência e segregação de interface.

# Api Rest
Também baseada na modelagem DDD,foi criado dois endpoints:

api/addRoutes - Endpoint responsavel por receber uma lista de objeto do tipo rotas e salvar no arquivo csv.

Modelo objeto json:

[
  {
    "origin": "string",
    "destination": "string",
    "value": 0
  }
]

api/getCheapestRoute - Endpoint responsavel por receber dois atributos("origem" e "destino") e realizar a consulta da rota de menor valor.



