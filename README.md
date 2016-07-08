# Woland ![Travis badge](https://travis-ci.org/burzyk/Woland.svg?branch=master)
ETL service to integrate information form different job sites into a single database for "your own personal use only".

# Installation

In order to install and configure the service please follow these simple steps.

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

