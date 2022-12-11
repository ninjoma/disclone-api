FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 5610
ENV ASPNETCORE_URLS=http://+:5610
VOLUME [ "./App/logs" ]
ENTRYPOINT ["dotnet", "disclone-api.dll"]