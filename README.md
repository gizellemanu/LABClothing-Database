<img align="right" height="30" width="40" src="https://www.svgrepo.com/show/508668/flag-us.svg" alt="English">

# LAB-Clothing-Collection-BackEnd

## INTRODUCTION
<p style="text-align: justify;"><em>Named LAB Clothing Collection, the project aims to serve the fashion management sector, this repository presents the Back-End proposal for the software that aims to manage fashion collections and creation models in the clothing segment efficiently. This Back-End application was developed using C# and .NET utilizing the SQL Server Express database.</em></p>

#### Completion Certificate
_This project was developed during the [DEVinHouse | Audaces](https://devinhouse.tech/) course, taught by [SENAI](https://cursos.sesisenai.org.br/curso/devinhouse/525)._<br>
You can access the certificate directly [→ here ←](https://github.com/gizellemanu/LABClothing-Database/blob/main/Certificado.pdf).

## _Table of Contents_
- [_System Format_](#system-format)
- [_SQL Modeling_](#sql-modeling)
- [_Guidelines_](#guidelines)
  - [_How to Run the Project_](#how-to-run-the-project)  
  - [_Running the Rest API_](#running-the-rest-api)
- [_Directory Structure_](#directory-structure)
- [Authors](#authors)

## _System Format_
The system should contain the following types of registrations, each with its own characteristics.

### User Registration
Derived from the Person class, it has the following attributes:
- **Identifier:** _A number that should be automatically incremented._
- **Full Name:** _Should be text._
- **Gender:** _Should be text._
- **Date of Birth:** _Mandatory, valid date._
- **SSN or TIN:** _Should be text._
- **Phone:** _Should be text._

| Solution                         | Service                                                                                      |
|----------------------------------|----------------------------------------------------------------------------------------------|
| User Registration                | User registration service, whose entity must inherit from Person.                             |
| Updating User Data               | Service to change/update the data of a certain user and which the system user can change the status whenever necessary. |
| Updating User Status             | Service to change/update the status of a certain user.                                         |
| User Listing                     | Service for listing registered users.                                                          |
| User Listing by Identifier       | Service to query user by their identifier code.                                                |

### Collection Registration
Must have the following attributes:
- **Identifier:** _A number that should be automatically incremented._
- **Collection Name:** _Mandatory, should be text._
- **Responsible Identifier:** _Mandatory, should be numeric._
- **Brand:** _Mandatory, should be text._
- **Budget:** _Mandatory, should be numeric._
- **Release Year:** _Mandatory, valid date._
- **Season:** _Mandatory with the following options:_
  - Autumn
  - Winter
  - Spring
  - Summer
- **System State:** _Mandatory with the following options:_
  - Active
  - Inactive

| Solution                                    | Service                                                                                      |
|--------------------------------------------|----------------------------------------------------------------------------------------------|
| Updating Collection Data                   | Service to change/update the data of a certain collection and which the system user can change whenever necessary. |
| Updating Collection State in the system    | Service to change/update the state of the collection in the system and which the system user can change this state whenever necessary. |
| Collection Listing                         | Service for listing registered collections.                                                    |
| Collection Listing by Identifier           | Service to query collections by their identifier code.                                          |
| Collection Deletion                        | Service to delete a collection by the identifier code that will only allow the deletion of a collection that is archived (inactive) and that does not have linked models. |

### Model Registration
Must have the following attributes:
- **Identifier:** _A number that should be automatically incremented._
- **Model Name:** _Mandatory, should be text._
- **Related Collection Identifier:** _Mandatory, should be numeric._
- **Type:** _Mandatory with the following options:_
  - Shorts
  - Bikini
  - Bag
  - Cap
  - Pants
  - Footwear
  - Shirt
  - Hat
  - Skirt
- **Layout:** _Mandatory with the following options:_
  - Embroidery
  - Print
  - Plain

| Solution                               | Service                                                                                      |
|---------------------------------------|----------------------------------------------------------------------------------------------|
| Updating Model Data                   | Service to change/update the data of a certain model and which the system user can change whenever necessary. |
| Model Listing                         | Service for listing registered models.                                                         |
| Model Listing by Identifier           | Service to query models by their identifier code.                                              |
| Model Deletion                        | Service to delete a model by the identifier code.                                              |

## SQL Modeling
> ### _Person_
```sql
CREATE TABLE Person (
    Identifier INTEGER PRIMARY KEY,
    FullName VARCHAR(255),
    Gender VARCHAR(50),
    DateOfBirth DATE,
    SSNOrTIN VARCHAR(20),
    Phone VARCHAR(20)
);
```
> ### _User_
```sql
CREATE TABLE User (
    Identifier INTEGER PRIMARY KEY,
    FullName VARCHAR(255),
    Gender VARCHAR(50),
    DateOfBirth DATE,
    SSNOrTIN VARCHAR(20),
    Phone VARCHAR(20),
    Email VARCHAR(255),
    UserType VARCHAR(50),
    UserStatus VARCHAR(50),
    CONSTRAINT FK_User_Person FOREIGN KEY (Identifier) REFERENCES Person(Identifier)
);
```
> ### _Collection_
```sql
CREATE TABLE Collection (
    Identifier INTEGER PRIMARY KEY,
    CollectionName VARCHAR(255),
    ResponsibleId INTEGER,
    Brand VARCHAR(255),
    Budget DECIMAL(10, 2),
    ReleaseYear INTEGER,
    Season VARCHAR(50),
    SystemStatus VARCHAR(50),
    CONSTRAINT FK_Collection_User FOREIGN KEY (ResponsibleId) REFERENCES User(Identifier)
);
```
> ### _Model_
```sql
CREATE TABLE Model (
    Identifier INTEGER PRIMARY KEY,
    ModelName VARCHAR(255),
    CollectionId INTEGER,
    Type VARCHAR(50),
    Layout VARCHAR(255),
    CONSTRAINT FK_Model_Collection FOREIGN KEY (CollectionId) REFERENCES Collection(Identifier)
);
```
## _Guidelines_
### _How to Run the Project_
Before using this application, it's necessary to have the .NET Core SDK installed on your computer. This project was developed using .NET Core version 3.1. Follow the steps below to install and set up the development environment:

#### Installing the .NET Core SDK
1. Visit the official .NET Core website at [https://dotnet.microsoft.com/download/dotnet-core](https://dotnet.microsoft.com/download/dotnet-core).
2. Select the .NET Core SDK version corresponding to your operating system.
3. Download and run the .NET Core SDK installer. Follow the installer instructions to complete the installation.

#### Installing SQL Server Express
1. Visit the official SQL Server website at [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
2. Download the free version of SQL Server Express and follow the installer instructions to complete the installation.
3. During installation, make a note of the SQL Server instance name as you will need this information to configure the connection string in the project.

#### Cloning the Repository and Setting Up the Project
1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/your-repository.git
    ```
2. **Navigate to the project directory**:
    ```bash
    cd your-repository
    ```
3. **Open the project in Visual Studio**:
    - Open Visual Studio.
    - Click on "Open a project or solution".
    - Navigate to the folder where the project was cloned and open the solution file (.sln).

### Configuring the Connection String
1. In Visual Studio, open the `appsettings.json` file.
2. Locate the connection string and update it with the name of the SQL Server Express instance you configured during installation. The connection string should have the following format:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=instance-name;Database=labclothingcollectionbd;Trusted_Connection=True;"
      }
    }
    ```
### Running the Application
1. **Applying Migrations**:
    - Open the **Package Manager Console** in Visual Studio via `Tools` > `NuGet Package Manager` > `Package Manager Console`.
    - In the console, execute the following command to apply migrations and create the database:
      ```bash
      Update-Database
      ```
2. **Starting the API**:
    - In Visual Studio, press `F5` or go to `Debug` > `Start Debugging` to start the project. This will compile and run the application.
    - The API will be running, and you can access it via the browser at `https://localhost:5001` or `http://localhost:5000`.

3. **Testing the API**:
    - Use a tool like Postman or Insomnia to test the API endpoints.
    - Ensure that the database was created correctly and that the services are functioning as expected.

### _Running the Rest API_
<p align="justify">To begin, make sure you have SQL Server Express installed on your machine. Next, install Visual Studio 2019 or a newer version. This step is essential to proceed with the process.</p>
<p align="justify">After installing Visual Studio, create a database named "labclothingcollectionbd". This database will be used in the project you are going to configure.</p>
<p align="justify">Now, it's necessary to clone the corresponding repository from GitHub, which contains the necessary files for the project. Make sure you get the correct repository.</p>
<p align="justify">Next, open Visual Studio. On the welcome screen, click on "Open a project or solution". Navigate to the folder where the project was cloned and open the solution file (.sln).</p>
<p align="justify">Ensure that the "appsettings.json" file contains the correct connection string (labclothingcollectionbd). It's important to ensure that the configuration is correct to establish the connection to the database.</p>
<p align="justify">Finally, in Visual Studio, press F5 or go to "Debug" > "Start Debugging" to start the project. This will compile and run the project, allowing you to begin working on it. Remember that you need Visual Studio 2019 or a newer version to successfully follow these steps.</p>

## _Directory Structure_
```plaintext
LAB-Clothing-Collection-BackEnd
└── Controllers
└── Data
└── Migrations
└── Models
└── Repositories
└── Services
└── appsettings.json
└── Program.cs
└── Startup.cs
```
## Authors
```
Gizelle Emanuela da Silva
```

<br>
<hr>
<img align="right" height="30" width="40" src="https://www.svgrepo.com/show/405433/flag-for-flag-brazil.svg" alt="Portugues">

# LAB-Clothing-Collection-BackEnd

## INTRODUÇÃO
<p style="text-align: justify;"><em> Denominado LAB Clothing Collection o projeto procura atender o setor de tecnologiana gestão de moda, este repositorio apresenta a proposta de Back-End para o software que tem como objetivo gerenciar coleções de moda e os modelos de criação no segmento de vestuário de forma eficiente. Esta aplicação Back-End, foi desenvolvida utilizando o C# e .Net Utilizando o banco de dados SQL Server Express.</em></p>

#### Certificado de Conclusão
_Este projeto foi desenvolvido durante o curso [DEVinHouse | Audaces](https://devinhouse.tech/), ministrado por [SENAI](https://cursos.sesisenai.org.br/curso/devinhouse/525)._<br>
Você pode acessar o certificado diretamente [→ aqui ←](https://github.com/gizellemanu/LABClothing-Database/blob/main/Certificado.pdf). 

## _Índice_
- [_Formato do Sistema_](#formato-do-sistema)
- [_Modelagem SQL_](#modelagem-sql)
- [_Orientações_](#orientações)
  - [_Como Executar o Projeto_](#como-executar-o-projeto)  
  - [_Execução da API Rest_](#execução-da-api-rest)
- [_Estrutura de Diretorios_](#estrutura-de-diretórios)
- [Autores](#autores)

## _Formato do Sistema_
O sistema deve conter os tipos de cadastros abaixo, cada um com suas características.

### Cadastro de Usuário
Derivado da classe Pessoa, possui os seguintes atributos:
- **Identificador:** _Um número que deve ser incrementado automaticamente._
- **Nome Completo:** _Deve ser um texto._
- **Gênero:** _Deve ser um texto._
- **Data de Nascimento:** _Obrigatório, data válida._
- **CPF ou CNPJ:** _Deve ser texto._
- **Telefone:** _Deve ser texto._

| Solução                               | Serviço                                                                                      |
|---------------------------------------|----------------------------------------------------------------------------------------------|
| Cadastro de Usuário                   | Serviço de cadastro de Usuário, cuja entidade deve herdar de Pessoa.                          |
| Atualização dos dados de Usuário     | Serviço para alterar/atualizar os dados de determinado usuário e que o usuário do sistema poderá alterar o status sempre que necessário. |
| Atualização do Status de Usuário      | Serviço para alterar/atualizar o status de determinado usuário.                                |
| Listagem de Usuários                  | Serviço de listagem de usuários cadastrados.                                                  |
| Listagem de Usuários pelo identificador | Serviço de consulta de usuário pelo seu código identificador.                                 |

### Cadastro de Coleções
Deve possuir os seguintes atributos:
- **Identificador:** _Um número que deve ser incrementado automaticamente._
- **Nome da Coleção:** _Obrigatório, deve ser texto._
- **Identificador do Responsável:** _Obrigatório, deve ser numérico._
- **Marca:** _Obrigatório, deve ser texto._
- **Orçamento:** _Obrigatório, deve ser numérico._
- **Ano de Lançamento:** _Obrigatório, data válida._
- **Estação:** _Obrigatório com as seguintes opções:_
  - Outono
  - Inverno
  - Primavera
  - Verão
- **Estado no Sistema:** _Obrigatório com as seguintes opções:_
  - Ativa
  - Inativa

| Solução                                    | Serviço                                                                                      |
|--------------------------------------------|----------------------------------------------------------------------------------------------|
| Atualização dos dados de Coleções          | Serviço para alterar/atualizar os dados de determinada coleção e que o usuário do sistema poderá alterar sempre que necessário. |
| Atualização do Estado da Coleção no sistema | Serviço para alterar/atualizar o estado da coleção no sistema e que o usuário do sistema poderá alterar este estado sempre que necessário. |
| Listagem de Coleções                      | Serviço de listagem de coleções cadastradas.                                                  |
| Listagem de Coleção pelo identificador    | Serviço de consulta de coleções pelo seu código identificador.                                 |
| Exclusão de Coleção                       | Serviço para excluir uma coleção pelo código identificador que só permitirá a exclusão de uma coleção que esteja arquivada (inativa) e que não possua modelos vinculados. |

### Cadastro de Modelos
Deve possuir os seguintes atributos:
- **Identificador:** _Um número que deve ser incrementado automaticamente._
- **Nome do modelo:** _Obrigatório, deve ser texto._
- **Identificador da Coleção Relacionada:** _Obrigatório, e deve ser numérico._
- **Tipo:** _Obrigatório com as seguintes opções:_
  - Bermuda
  - Biquini
  - Bolsa
  - Boné
  - Calça
  - Calçados
  - Camisa
  - Chapéu
  - Saia
- **Layout:** _Obrigatório com as seguintes opções:_
  - Bordado
  - Estampa
  - Liso

| Solução                               | Serviço                                                                                      |
|---------------------------------------|----------------------------------------------------------------------------------------------|
| Atualização dos dados de Modelos     | Serviço para alterar/atualizar os dados de determinado modelo e que o usuário do sistema poderá alterar sempre que necessário. |
| Listagem de Modelos                   | Serviço de listagem de modelos cadastrados.                                                  |
| Listagem de Modelo pelo identificador | Serviço de consulta de modelos pelo seu código identificador.                                 |
| Exclusão de Modelo                    | Serviço para excluir um modelo pelo código identificador.                                     |


## Modelagem SQL
> ### _Pessoa_
```sql
CREATE TABLE Pessoa (
    Identificador INTEGER PRIMARY KEY,
    NomeCompleto VARCHAR(255),
    Genero VARCHAR(50),
    DataNascimento DATE,
    CPFouCNPJ VARCHAR(20),
    Telefone VARCHAR(20)
);
```
> ### _Usuario_
```sql
CREATE TABLE Usuario (
    Identificador INTEGER PRIMARY KEY,
    NomeCompleto VARCHAR(255),
    Genero VARCHAR(50),
    DataNascimento DATE,
    CPFouCNPJ VARCHAR(20),
    Telefone VARCHAR(20),
    Email VARCHAR(255),
    TipoUsuario VARCHAR(50),
    StatusUsuario VARCHAR(50),
    CONSTRAINT FK_Usuario_Pessoa FOREIGN KEY (Identificador) REFERENCES Pessoa(Identificador)
);
```
> ### _Colecao_
```sql
CREATE TABLE Colecao (
    Identificador INTEGER PRIMARY KEY,
    NomeColecao VARCHAR(255),
    ResponsavelId INTEGER,
    Marca VARCHAR(255),
    Orcamento DECIMAL(10, 2),
    AnoLancamento INTEGER,
    Estacao VARCHAR(50),
    EstadoSistema VARCHAR(50),
    CONSTRAINT FK_Colecao_Usuario FOREIGN KEY (ResponsavelId) REFERENCES Usuario(Identificador)
);
```
> ### _Modelo_
```sql
CREATE TABLE Modelo (
    Identificador INTEGER PRIMARY KEY,
    NomeModelo VARCHAR(255),
    ColecaoId INTEGER,
    Tipo VARCHAR(50),
    Layout VARCHAR(255),
    CONSTRAINT FK_Modelo_Colecao FOREIGN KEY (ColecaoId) REFERENCES Colecao(Identificador)
);
```

## _Orientações_
### _Como Executar o Projeto_
Antes de utilizar esta aplicação, é necessário ter o .NET Core SDK instalado em seu computador. Este projeto foi desenvolvido usando .NET Core versão 3.1. Siga os passos abaixo para instalar e configurar o ambiente de desenvolvimento:

#### Instalando o .NET Core SDK
1. Acesse o site oficial do .NET Core em [https://dotnet.microsoft.com/download/dotnet-core](https://dotnet.microsoft.com/download/dotnet-core).
2. Selecione a versão do .NET Core SDK correspondente ao seu sistema operacional.
3. Baixe e execute o instalador do .NET Core SDK. Siga as instruções do instalador para concluir a instalação.

#### Instalando o SQL Server Express
1. Acesse o site oficial do SQL Server em [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
2. Baixe a versão gratuita do SQL Server Express e siga as instruções do instalador para concluir a instalação.
3. Durante a instalação, anote o nome da instância do SQL Server, pois você precisará dessa informação para configurar a string de conexão no projeto.

#### Clonando o Repositório e Configurando o Projeto
1. **Clone o repositório**:
    ```bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    ```
2. **Navegue até o diretório do projeto**:
    ```bash
    cd seu-repositorio
    ```
3. **Abra o projeto no Visual Studio**:
    - Abra o Visual Studio.
    - Clique em "Open a project or solution" (Abrir um projeto ou solução).
    - Navegue até a pasta onde o projeto foi clonado e abra o arquivo de solução (.sln).

### Configurando a String de Conexão
1. No Visual Studio, abra o arquivo `appsettings.json`.
2. Localize a string de conexão e atualize-a com o nome da instância do SQL Server Express que você configurou durante a instalação. A string de conexão deve ter o seguinte formato:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=nome-da-instancia;Database=labclothingcollectionbd;Trusted_Connection=True;"
      }
    }
    ```
### Executando a Aplicação
1. **Aplicar as Migrações**:
    - Abra o **Package Manager Console** no Visual Studio através do menu `Tools` > `NuGet Package Manager` > `Package Manager Console`.
    - No console, execute o comando a seguir para aplicar as migrações e criar o banco de dados:
      ```bash
      Update-Database
      ```
2. **Iniciar a API**:
    - No Visual Studio, pressione `F5` ou vá para `Debug` > `Start Debugging` para iniciar o projeto. Isso irá compilar e executar a aplicação.
    - A API estará em execução e você poderá acessá-la através do navegador em `https://localhost:5001` ou `http://localhost:5000`.

3. **Testar a API**:
    - Utilize uma ferramenta como o Postman ou o Insomnia para testar os endpoints da API.
    - Certifique-se de que o banco de dados foi criado corretamente e que os serviços estão funcionando conforme esperado.

### _Execução da API Rest_
<p align="justify">Para começar, certifique-se de ter instalado o SQL Server Express em sua máquina. Em seguida, realize a instalação do Visual Studio 2019 ou uma versão mais recente. Essa etapa é essencial para prosseguir com o processo.</p>
<p align="justify">Após a instalação do Visual Studio, crie um banco de dados com o nome "labclothingcollectionbd". Esse banco de dados será utilizado no projeto que você irá configurar.</p>
<p align="justify">Agora, é necessário clonar o repositório correspondente do GitHub, que contém os arquivos necessários para o projeto. Certifique-se de obter o repositório correto.</p>
<p align="justify">Em seguida, abra o Visual Studio. Na tela de boas-vindas, clique em "Open a project or solution" (Abrir um projeto ou solução). Navegue até a pasta onde o projeto foi clonado e abra o arquivo de solução (.sln).</p>
<p align="justify">Verifique se o arquivo "appsettings.json" contém a string de conexão correta (labclothingcollectionbd). É importante garantir que a configuração esteja correta para estabelecer a conexão com o banco de dados.</p>
<p align="justify">Por fim, no Visual Studio, pressione F5 ou vá para "Debug" > "Start Debugging" (Depurar > Iniciar Depuração) para iniciar o projeto. Isso irá compilar e executar o projeto, permitindo que você comece a trabalhar nele. Lembre-se de que é necessário ter o Visual Studio 2019 ou uma versão mais recente para seguir essas etapas com sucesso.</p>


## _Estrutura de Diretórios_
```plaintext
LAB-Clothing-Collection-BackEnd
└── Controllers
└── Data
└── Migrations
└── Models
└── Repositories
└── Services
└── appsettings.json
└── Program.cs
└── Startup.cs
```

## Autores
```
Gizelle Emanuela da Silva
```



