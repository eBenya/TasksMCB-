# Logging providers

#### namespace:

    using Microsoft.EntityFrameworkCore.Infrastructure;

Using the **GetService\<ILoggerFactory>()** method, we get the built-in **ILoggerFactory** service, which we pass **MyLoggerProvider** provider via the **AddProvider()** method.
<br>

#### Global setting of logging in the data context

For globall logging configuration, we need a logger factory the **ILoggerFactory** object.

```c#
public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        //Create logger factory.
   ---> public ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new MyLoggerProvider());
        });

        public ApplicationContext()
        {
            //Database.EnsureDeleted();   //delete Db if it exist
            Database.EnsureCreated();   //create Db if it not exist
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUDApp;Trusted_Connection=True;");
    --->    optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
    }
```
<br>

#### Configuring logging
To configure logging use options from **DbLoggerCategory** class.

```c#
public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name)
        .AddProvider(new MyLoggerProvider());
});
```
And use log message levels to specify the message lvl:
```c#
public ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
{
    builder
    .AddFilter((category, lvl)=>category==DbLoggerCategory.Database.Command.Name
            && lvl == LogLevel.Information)
    .AddProvider(new MyLoggerProvider());
});
```
<br>

#### Built-in provider
To use built-in provider - include  **Microsoft.Extensions.Logging.Console** from Nugget.
After add packege call built-in **AddConsole()** method.
```C#
public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    // или так с более детальной настройкой
    //builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
    //            && level == LogLevel.Information)
    //       .AddConsole();
});
```