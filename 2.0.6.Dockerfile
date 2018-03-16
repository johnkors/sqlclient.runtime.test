FROM microsoft/aspnetcore:2.0.6

COPY /output ./output

RUN dotnet output/host.dll