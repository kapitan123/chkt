#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/BankProxy/BankProxy.API/BankProxy.API.csproj", "src/BankProxy/BankProxy.API/"]
COPY ["src/Common/EventBus.csproj", "src/Common/"]
COPY ["src/MerchantPayment/MerchantPayment.API/MerchantPayment.API.csproj", "src/MerchantPayment/MerchantPayment.API/"]
COPY ["NuGet.config", "./"]
COPY ["PaymentGateway.sln", "./"]
RUN dotnet restore "src/MerchantPayment/MerchantPayment.API/MerchantPayment.API.csproj"

COPY . .
WORKDIR "/src/src/MerchantPayment/MerchantPayment.API"
RUN dotnet build "MerchantPayment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MerchantPayment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MerchantPayment.API.dll"]