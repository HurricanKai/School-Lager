FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY . .
RUN dotnet publish "Lager.csproj" -c Release -o /app/publish

