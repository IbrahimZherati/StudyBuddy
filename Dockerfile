FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and all project files
COPY *.sln .
COPY StudyBuddy.API/*.csproj ./StudyBuddy.API/
COPY StudyBuddy.Application/*.csproj ./StudyBuddy.Application/
COPY StudyBuddy.Domain/*.csproj ./StudyBuddy.Domain/
COPY StudyBuddy.Infrastructure/*.csproj ./StudyBuddy.Infrastructure/
COPY StudyBuddy.Shared/*.csproj ./StudyBuddy.Shared/

# Restore - this will use the .sln file
RUN dotnet restore

# Copy all source code
COPY . .

# Publish
RUN dotnet publish "StudyBuddy.API/StudyBuddy.API.csproj" -c Release -o /app/published

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/published .
ENTRYPOINT ["dotnet", "StudyBuddy.API.dll"]