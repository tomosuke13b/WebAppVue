#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get install -y curl
RUN apt-get update \
  && apt-get install -my wget gnupg

RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs

RUN curl -fsSL  https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
RUN echo "deb https://dl.yarnpkg.com/debian/ stable main" > /etc/apt/sources.list
RUN apt-get update && apt-get install yarn

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

RUN apt-get install -y curl
RUN apt-get update \
  && apt-get install -my wget gnupg

RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs

RUN curl -fsSL  https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
RUN echo "deb https://dl.yarnpkg.com/debian/ stable main" > /etc/apt/sources.list
RUN apt-get update && apt-get install yarn


WORKDIR /src
COPY ["WebAppVue/WebAppVue.csproj", "WebAppVue/"]
RUN dotnet restore "WebAppVue/WebAppVue.csproj"
COPY . .

WORKDIR "/src/WebAppVue"
RUN dotnet build "WebAppVue.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppVue.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppVue.dll"]