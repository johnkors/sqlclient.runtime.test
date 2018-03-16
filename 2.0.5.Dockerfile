FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app/dotnetapp
COPY . .
RUN dotnet publish ./src/host/host.csproj -o ./../../output

FROM microsoft/aspnetcore:2.0.5 as web
WORKDIR /app
COPY --from=build /app/dotnetapp/output ./
RUN dotnet host.dll