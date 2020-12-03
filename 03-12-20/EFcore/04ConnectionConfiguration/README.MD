# Connection configuration

There are 2 way to determinate the connection configuration:

* Method OnConfiguring() about context class.
* Passing the configuration to the constructor of the DbContext base class.

In the previous examples, we used the first method.

The connection string used is split into several parts:
* Server: Name server. In this case, used special engine MS SQL Swrver - localdb, for development needs. For full-fiedged MS Sql server, this parameter usualy has a value .\SQLEXPRESS
* Database: Data base file name, without the extension(.mdf).
* Trusted_Connection: Set up authentication. True - windows authentication.

The second method involves passing the object base class to constructor
Example:
```c#
public class UserApplicationContext : DbContext
{
    public DbSet<User> Users {get; set;}

    public UserApplicationContext(DbContextOptions<UserApplicationContext> options )
        :base(options)
    {
        Database.EnsureCreated();
    }
    ...
}
```
Use context class:
```c#
public static Main()
{
    string connectionString = ...;

    var optionsBuilder = new DbContextOptionsBuilder<UserApplicationContext>();
    var.options = optionsBuilder
                .UseSqlServer($"{connectionString}")
                .Options;
    //...Use code to work with database 
}
```

Connection string can be stored in a configuration file. This is usually appsetings.json
it is looks something like this:
```json
{
    "ConnectionStrings":{
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Name;Trusted_Connection=True;"
    }
}
```

For it use need include Microsoft.Extensions.Configuration.Json.<br>
When adding file, **you must set the copy to fo;der setting**.

To work with the Db in the working class

```c#
...
{
    var builder = new ConfigurationBuilder();
    
    //Set way to current catalog    
    builder.SetBasePath(Directory.GetCurrentDirectory());
    
    //Get a configuration file
    builder.AddJsonFile("appsetings.json");
    
    //Create configuration
    var config = builder.Build();
    ...
    string connectionString = config.GetConncetiocnString("DefaultConnection");
    
    //Use code for working to Db.
}
...
```
