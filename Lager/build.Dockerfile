FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /src
COPY ["Lager/Lager.csproj", "Lager/"]
RUN dotnet restore "Lager/Lager.csproj"
COPY . .
WORKDIR /src/Lager
RUN dotnet publish "Lager.csproj" -c Release -o /app/publish

