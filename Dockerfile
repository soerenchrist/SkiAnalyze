#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# install node
RUN curl --silent --location https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install --yes nodejs

RUN node --version

# Copy project files and restore
COPY '*.sln' .
COPY './src/SkiAnalyze.Core/*.csproj' './src/SkiAnalyze.Core/'
COPY './src/SkiAnalyze.Infrastructure/*.csproj' './src/SkiAnalyze.Infrastructure/'
COPY './src/SkiAnalyze.SharedKernel/*.csproj' './src/SkiAnalyze.SharedKernel/'
COPY './src/Util/FitDecode/*.csproj' './src/Util/FitDecode/'
COPY './src/SkiAnalyze/*.csproj' './src/SkiAnalyze/'
COPY './References/*.*' './References/'

RUN dotnet restore

# copy the rest
COPY . .

FROM build as publish
WORKDIR /src/SkiAnalyze

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001

EXPOSE 5000
EXPOSE 5001

COPY --from=publish /src/SkiAnalyze/out ./

ENTRYPOINT [ "dotnet", "SkiAnalyze.dll", "--no-launch-profile" ]