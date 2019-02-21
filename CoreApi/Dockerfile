#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["CoreApi/CoreApi.csproj", "CoreApi/"]
RUN dotnet restore "CoreApi/CoreApi.csproj"
COPY . .
WORKDIR "/src/CoreApi"
RUN dotnet build "CoreApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoreApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreApi.dll"]