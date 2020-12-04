## Database migration and managment

If we want to change one or more models, then to continue interacting with the database, we need to make the same changes to it.

The are several waysto do this:
1. Make changes directly in the database itself.<br>
In constructor db it looks like:
```sql
CREATE TABLE[dbo].[User](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NULL,
    [Age] INT NOT NULL,
--->[Address] NVARCHAR(MAX) NULL,<---new field
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
2. Use ***Database.EnsureCreated()*** in cunjunction with ***Database.EnsureDeleted()***, "Deleted" put it ahead.
3. Migration. Migration is essentially a plan for moving the databese from the old schema to the new one.
To create migration use:
```cmd
Add-Migration <migration_name>
```
<br>

After created, <big>**it must be completed**:</big>
```cmd
Update-database
```

For use migration must be add packege *Microsoft.EntityFrameworkCore.Tools* to progect from Nuget

After creating migration, a folder will appear that contains 3 file:
* XXXXXXXXXXXXXX_InitialCreate.cs:<br>
    The main migration file that  contains alll the applied actions.
* XXXXXXXXXXXXXX_InitialCreate.Designer.cs:<br>
    Migration metadata file that is use by the EF.
* [data_context_name]ModelSnapshot.cs:<br>
    Contains the current state of the model, used when creating the next migration.
<br>

##### Migration in the console:
To create:
```cmd
dotnet ef migrations add InitialCreate
```
To perform:
```cmd
dotnet ef database update
```
<br>

##### Migrate() method:

In some cases(in app with local db(sqlLite in UWP)):
```c#
myDbContext.Database.Migrate();
```
**Keep in mind that** before calling this method, you **should not call the EnsureCreated** method!

##### If context constructor accepts the DbContextOptions parametr:
If the only constructor in the context class accepts the DbContext parametr:
```c#
public ApplicationContext(DbContextOptions<UserContext> options) 
            : base(options){ }
```
In this case when perfoming a migration the EF tool looks for a class that implements the **IDesignTimeDbContextFactory** interface and that set the context configurating.
```c#
public class SampleContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
             
    // getting configuration from file appsettings.json
    ConfigurationBuilder builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory());
    builder.AddJsonFile("appsettings.json");
    IConfigurationRoot config = builder.Build();
 
    // getting the connection string from file appsettings.json
    string connectionString = config.GetConnectionString("DefaultConnection");
    optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
    
    //Return created data context object.
    return new UserContext(optionsBuilder.Options);
}
```