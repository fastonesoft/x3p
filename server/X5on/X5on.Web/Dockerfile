FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["X5on.Web/X5on.Web.csproj", "X5on.Web/"]
COPY ["X5on.Data/X5on.Data.csproj", "X5on.Data/"]
RUN dotnet restore "X5on.Web/X5on.Web.csproj"
COPY . .
WORKDIR "/src/X5on.Web"
RUN dotnet build "X5on.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "X5on.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "X5on.Web.dll"]