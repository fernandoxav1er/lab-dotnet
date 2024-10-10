# Define a imagem base para o SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildbase

# Setando local de trabalho
WORKDIR /app

# Instala o dockerize para esperar a disponibilidade do banco de dados
RUN apt-get update && apt-get install -y wget \
    && wget https://github.com/jwilder/dockerize/releases/download/v0.6.1/dockerize-linux-amd64-v0.6.1.tar.gz \
    && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-v0.6.1.tar.gz \
    && rm dockerize-linux-amd64-v0.6.1.tar.gz

# Copiando todos os arquivos de onde o DockerFile se encontra para o container
COPY . ./

# Restaurando e buildando/publicando 
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /runtime-app

# Copiando os artefatos do build para o runtime
COPY --from=buildbase /app/out .

# Copiando o dockerize para o runtime (se necessário)
COPY --from=buildbase /usr/local/bin/dockerize /usr/local/bin/

# Definir a porta exposta
EXPOSE 8080
EXPOSE 8081

# Definir o ponto de entrada e o comando
CMD ["dockerize", "-wait", "tcp://db:3306", "-timeout", "120s", "dotnet", "Catalogo.API.dll"]
