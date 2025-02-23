# ASP.NET Core Identity with Keycloak Authentication

1. Run [Docker Compose](./compose.yaml) file: `docker compose up -d`
2. Open [Keycloak Admin Panel](http://localhost:7080) with credentials from [.env](./.env) file 
3. Create Keycloak Client with Secret Credentials
4. Copy Secret to `KEYCLOAK_CLIENT_SECRET` property in [.env](./.env) file
5. Stop [Docker Compose](./compose.yaml) file: `docker compose down`
6. Run [Docker Compose](./compose.yaml) file: `docker compose up -d` again
7. Open [MVC Application](http://localhost:5000)
8. Register or Login Account with Keycloak Authentication
