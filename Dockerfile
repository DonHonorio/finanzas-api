# 1️⃣ Imagen base para compilar la API
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto y restaurar dependencias
COPY FinanzasApi.csproj .
RUN dotnet restore "FinanzasApi.csproj"

# Copiar el resto del código fuente y compilar la API
COPY . .
RUN dotnet publish "FinanzasApi.csproj" -c Release -o /app/publish

# 2️⃣ Imagen base para ejecutar la API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Instalar herramientas de depuración y red
RUN apt-get update && apt-get install -y \
    curl \
    net-tools \
    iproute2 \
    iputils-ping \
    procps \
    lsof \
    && rm -rf /var/lib/apt/lists/*

# Copiar los archivos compilados desde la etapa de build
COPY --from=build /app/publish .

# Instalar Supervisor
RUN apt-get update && apt-get install -y supervisor && rm -rf /var/lib/apt/lists/*

# Crear el directorio de logs de Supervisor
RUN mkdir -p /var/log/supervisor && chown -R www-data:www-data /var/log/supervisor

# Copiar el archivo de configuración de Supervisor
COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Exponer el puerto de la API
EXPOSE 8080

# Ejecutar Supervisor
CMD ["/usr/bin/supervisord", "-c", "/etc/supervisor/conf.d/supervisord.conf"]
