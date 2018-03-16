FROM microsoft/aspnetcore:2.0.5

COPY /output ./output

RUN dotnet output/host.dll