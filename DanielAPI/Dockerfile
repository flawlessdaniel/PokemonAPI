FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
RUN mkdir /app/images
EXPOSE 44369
ENV ASPNETCORE_URLS=http://+:44369

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["DanielAPI/DanielAPI.csproj", "DanielAPI/"]
RUN dotnet restore "DanielAPI/DanielAPI.csproj"
COPY . .
WORKDIR "/src/DanielAPI"
RUN dotnet build "DanielAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DanielAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DanielAPI.dll"]