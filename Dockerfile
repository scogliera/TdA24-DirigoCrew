FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TeacherDigitalAgency.csproj", "./"]
RUN dotnet restore "TeacherDigitalAgency.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TeacherDigitalAgency.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TeacherDigitalAgency.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeacherDigitalAgency.dll"]
