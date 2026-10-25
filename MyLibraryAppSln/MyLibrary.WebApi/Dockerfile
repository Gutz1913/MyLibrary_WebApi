# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MyLibrary.WebApi/MyLibrary.WebApi.csproj", "MyLibrary.WebApi/"]
RUN dotnet restore "MyLibrary.WebApi/MyLibrary.WebApi.csproj"
COPY . .
WORKDIR "/src/MyLibrary.WebApi"
RUN dotnet build "MyLibrary.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyLibrary.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLibrary.WebApi.dll"]