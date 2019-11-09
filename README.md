
# Guaxinim-Treinos-e-Dietas
O seu gerenciador de treinos físicos e dietas.

## Sobre
O sistema é uma aplicação web com base no .Net Core 2.2 com integração do Entity Framework da mesma versão.
Por conta de como o sistema foi desenvolvido, o padrão do sistema é rodar no Windows, porém para que rode em qualquer outro sistema operacional é necessário apenas trocar qual o banco de dados para a aplicação.

### Configurações
Além da possibilidade de usar como padrão o SSMS como principal banco de dados, há a possibilidade para usar o SQLite como banco, para isso já existe a connection string como padrão do sistema.

Para trocar, é necessário apenas comentar a linha onde a conexão é feita usando o SqlServer para a linha com o SQLite no arquivo ``` Startup.cs ```.

O arquivo de banco será gerado na pasta base do sistema com o nome de ``` GTD.db ```.

### Pré-requisitos
É necessária a versão .Net Core SDK 2.2 para que o sistema seja compilado, para que exista o banco de dados a partir do Entity Framework é possível a necessidade que o sistema possua uma instalação do SSMS (SQL Server Management Studio).

O sistema rodará a partir de apenas um comando no console dentro da pasta raiz

```dotnet run```

Qualquer problema favor reportar na parte de _issues_ deste mesmo repositório.

### Planos Futuros
Já existem ideias para novas funcionalidades serem instaladas no sistema, além do que está contido na documentação, existe também a ideia de manter um jornal com o desenvolvimento do usuário conforme cada plano é concluído, essa ideia veio após o primeiro planejamento do sistema e quando foi calculado o tempo de desenvolvimento não coube junto com o calendário proposto para o trabalho ser concluído.

Outra parte é de adicionar informações sobre o usuário, com esse plano de jornal e desenvolvimento, pode-se até pensar em criar perfis para cada usuário e existir quase uma comunidade entre usuários do sistema.
 
## Ideia
A ideia do sistema é para um TCC do curso técnico em desenvolvimento de sistemas pelo SENAI Portão. O código é aberto e sem qualquer tipo de licença por ser apenas para aprendizagem e por justificativa pessoal, caso se interesse no sistema e tenha qualquer tipo de dúvida, pode entrar em contato comigo a qualquer momento que poderei esclarecer suas dúvidas.

