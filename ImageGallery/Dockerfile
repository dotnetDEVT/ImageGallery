#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs \ 
    && apt-get install -y npm \ 
    && npm install -g npm@latest \ 
    && apt-get install -y build-essential
COPY ["ImageGallery/ImageGallery.csproj", "ImageGallery/"]
RUN dotnet restore "ImageGallery/ImageGallery.csproj"
COPY . .
WORKDIR "/src/ImageGallery"
RUN dotnet build "ImageGallery.csproj" -c Release -o /app/build

#FROM build AS publish
#WORKDIR "/src/ImageGallery"
RUN echo "publishing docker file now "
RUN dotnet publish "ImageGallery.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir -p /ImageGallery/ImageGallery/ImageGallery/wwwroot
#/grant '$USER:(F)'
COPY ./ImageGallery/wwwroot/   /ImageGallery/ImageGallery/ImageGallery/wwwroot
RUN chown  "$USER":"$USER" /ImageGallery/ImageGallery/ImageGallery/wwwroot/ -R 
RUN chmod 775 /ImageGallery/ImageGallery/ImageGallery/wwwroot/images -R 
COPY --from=build /app/publish/ .
ENTRYPOINT ["dotnet", "ImageGallery.dll"]