#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankProxy.API/BankProxy.API.csproj", "BankProxy.API/"]
RUN dotnet restore "BankProxy.API/BankProxy.API.csproj"
COPY . .
WORKDIR "/src/BankProxy.API"
RUN dotnet build "BankProxy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankProxy.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankProxy.API.dll"]