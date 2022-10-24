FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Product.API.Application/Product.API.Application.csproj" -- disable-parallel
RUN dotnet publish "./Product.API.Application/Product.API.Application.csproj" -c release -o /app --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Product.API.Application.dll"]