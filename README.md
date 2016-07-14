# Woland ![Travis badge](https://travis-ci.org/burzyk/Woland.svg?branch=master)
ETL service to integrate information form different job sites into a single database for "your own personal use only".

# Getting Started

The easiest way to get started with this application is to install [docker](https://docs.docker.com/engine/installation/) and then get the `burzyk/woland` container from the [docker hub](https://hub.docker.com/r/burzyk/woland/).

To get the container please execute `docker pull burzyk/woland` and then `docker run -it burzyk/woland`. This will start the container and a bash terminal inside. 

To start the application in the interactive mode, please run `/opt/woland/Woland.Service --p:connectionString CONNECTION_STRING` and replace `CONNECTION_STRING` with a valid MSSQL connection string value. Please note that the database specified must exist and in order for the tables to be created the user should have the DDL change permissions granted. Once the database is initialized the regular R/W permissions are sufficient.

To start the application in the background mode, please run `docker run -td burzyk/woland /opt/woland/Woland.Service --p:connectionString CONNECTION_STRING`

If you do not wish to specify the connection string every time the application is started you may choose to save it in the `/opt/woland/config.json` configuration file.

# Development

In order to install and configure the service for developent please follow these simple steps.

* First, get the [dotnet SDK](https://www.microsoft.com/net/core).
When it's complete please open the configuration file `src/Woland.Service/config.json` and modify the connection string to point to an MSSQL Server database, for example:
```
{
  "connectionString": "Server=.\\SQLEXPRESS;Database=Woland;Integrated Security=True"
}
```
* Next, to configure the database please go to `src/Woland.DataAccess`, open the `EfDataContext.cs` and modify the connection string that is in the constructor (it should be the same as the one specified in the configuration file).
```
public EfDataContext()
{
    // This is for migrations
    this.connectionString = @"Server=DESKTOP-L481L6R\SQLEXPRESS;Database=Woland;User Id=woland;Password=woland";
}
```
* To build the application, open bash or any other console, navigate to the project root directory and execute the standard `dotnet restore`, navigate to the `src/Woland.Service` and execute `dotnet build`.
* To initialize the database please go back to the `src/Woland.DataAccess` project and execute `dotnet ef database update`. This will create all tables and other DB objects.
* After that, please copy the configuration file to the `src/Woland.Service/bin/Debug/netcoreapp1.0` folder.
* To run the application please navigate to `src/Woland.Service` and execute `dotnet run`.

That should start the service and log information should be printed on the console.
This procedure is work in progress and should be more polished as the project matures :)

