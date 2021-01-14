#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM 192.168.3.224/zhaoxi/mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM 192.168.3.224/zhaoxi/mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Zhaoxi.NET5Project.Web.csproj", ""]
RUN dotnet restore "./Zhaoxi.NET5Project.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Zhaoxi.NET5Project.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zhaoxi.NET5Project.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zhaoxi.NET5Project.Web.dll"]