# escape=`

FROM mcr.microsoft.com/powershell:preview AS downloadnodejs
SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop';$ProgressPreference='silentlyContinue';"]
RUN Invoke-WebRequest -OutFile nodejs.zip -UseBasicParsing "https://nodejs.org/dist/latest/node-v13.9.0-win-x64.zip"; Expand-Archive nodejs.zip -DestinationPath C:\; Remove-Item nodejs.zip; Rename-Item "C:\node-v13.9.0-win-x64" c:\nodejs;


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\
WORKDIR /src
COPY ["./UI/BlogReact.csproj", "./UI/"]
COPY ["./Data/Data.csproj", "./Data/"]
RUN dotnet restore "./UI/BlogReact.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "./UI/BlogReact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./UI/BlogReact.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogReact.dll"]