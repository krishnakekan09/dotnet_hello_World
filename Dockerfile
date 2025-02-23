# Use the official .NET SDK image as the build environment (for .NET 6.0 or 7.0)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the solution file and restore the dependencies
COPY dotnet-hello-world.sln .
COPY hello-world-api/hello-world-api.csproj hello-world-api/

# Restore the dependencies
RUN dotnet restore

# Copy the rest of the source code into the container
COPY . .

# Publish the app to the /app directory
RUN dotnet publish hello-world-api/hello-world-api.csproj -c Release -o /app

# Use the official .NET Runtime image for running the app (for .NET 6.0 or 7.0)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Set the environment variable to make the app listen on port 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

# Copy the published files from the build environment
COPY --from=build /app .

# Expose port 5000 for the container
EXPOSE 5000

# Set the entry point for the container
ENTRYPOINT ["dotnet", "hello-world-api.dll"]
