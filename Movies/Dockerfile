FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=cinema-base /app/Movies/src/Cinema.Movies.WebAPI/bin/Release/net6.0/publish .

ENTRYPOINT ["dotnet", "Cinema.Movies.WebAPI.dll"]
