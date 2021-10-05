FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

EXPOSE 7000
ENV YamahaReceiverUrl=
ENV ASPNETCORE_URLS=http://*:7000


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/Yamaha.API/Yamaha.API.csproj", "Yamaha.API/"]
RUN dotnet restore "Yamaha.API/Yamaha.API.csproj"
COPY . .
WORKDIR "/src/Yamaha.API"
RUN dotnet build "Yamaha.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yamaha.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yamaha.API.dll"]