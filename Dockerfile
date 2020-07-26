#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["SKYDDNS/SKYDDNS.csproj", "SKYDDNS/"]
RUN dotnet restore "SKYDDNS/SKYDDNS.csproj"
COPY . .
WORKDIR "/src/SKYDDNS"
RUN dotnet build "SKYDDNS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SKYDDNS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SKYDDNS.dll"]