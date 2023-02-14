#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Training.API/Training.API.csproj", "Training.API/"]
COPY ["Training.EFCore/Training.EFCore.csproj", "Training.EFCore/"]
COPY ["Training.Domain/Training.Domain.csproj", "Training.Domain/"]
COPY ["Training.Services/Training.Services.csproj", "Training.Services/"]
RUN dotnet restore "Training.API/Training.API.csproj"
COPY . .
WORKDIR "/src/Training.API"
RUN dotnet build "Training.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Training.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Training.API.dll"]