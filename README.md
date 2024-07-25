Saudações, 
Na raiz do projeto encontra-se o arquivo WebBlog.postman_collection com todos os métodos, basta importa-lo no postman.

Informações importantes
- Os métodos Create, Update e Delete requerem autenticação, para execução do mesmo é necessário fazer o Login com um usuário e senha válidos, utilizar o token que o mesmo devolverá como Bearer Token nas requisições.
Obs: Os tokens estão configurados para duração de 30min.


Segue instrução de configuração
Banco de dados.
- Certifique-se da conexão no appSettings esteja apontando para instância do SQL Server local, substitua o nome server se necessário. 
- Ao executar a aplicação, a mesma deve criar o banco na instância local e rodar automaticamente as migrations, caso não, basta executar o comando " dotnet ef database update ".


Websocket.
- Execute a aplicação, garantindo que a mesma esteja rodando e ouvindo na porta 5142. 
Obs: Se a porta para subir a aplicação for diferente, é necessário configurar-la no arquivo ReciveNotification.html, linha 15. 
- Execute o servidor do cliente através de um servidor web local, como por exemplo, o Live Server no Visual Studio Code e verifique a porta a qual o servidor subira, por default 5500.
Obs: Se a porta do servidor local for diferente da 5500, é necessário configurara-la na classe Program.cs, linha 30.
- Abra a página do cliente, por exemplo, para porta do Live Server(5500) http://localhost:5500/WebSocket/Clients/ReciveNotification.html.
- Criar uma nova postagem, utilizando Swagger ou Postman para enviar uma requisição de criação de postagem.
- As notificações devem aparece em tempo real na página do cliente.
