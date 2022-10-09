#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Location/Location.API/Location.API.csproj", "Services/Location/Location.API/"]
COPY ["Services/Location/Location.Data/Location.Data.csproj", "Services/Location/Location.Data/"]
COPY ["Services/Location/Location.Service/Location.Service.csproj", "Services/Location/Location.Service/"]
RUN dotnet restore "Services/Location/Location.API/Location.API.csproj"
COPY . .
WORKDIR "/src/Services/Location/Location.API"
RUN dotnet build "Location.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Location.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Location.API.dll"]