FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["BlogReact.csproj", ""]
RUN dotnet restore "./BlogReact.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BlogReact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogReact.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogReact.dll"]