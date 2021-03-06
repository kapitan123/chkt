#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/BankProxy/BankProxy.API/BankProxy.API.csproj", "src/BankProxy/BankProxy.API/"]
COPY ["src/Common/EventBus/EventBus.csproj", "src/Common/EventBus/"]
COPY ["src/Common/DomainModels/DomainModels.csproj", "src/Common/DomainModels/"]
COPY ["src/Common/Healthchecks/Healthchecks/Healthchecks.csproj", "src/Common/Healthchecks/Healthchecks/"]
COPY ["src/MerchantPayment/MerchantPayment.API/MerchantPayment.API.csproj", "src/MerchantPayment/MerchantPayment.API/"]
COPY ["NuGet.config", "./"]
COPY ["PaymentGateway.sln", "./"]
RUN dotnet restore "src/BankProxy/BankProxy.API/BankProxy.API.csproj"

COPY . .
WORKDIR "/src/src/BankProxy/BankProxy.API"

FROM build AS publish
RUN dotnet publish "BankProxy.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankProxy.API.dll"]