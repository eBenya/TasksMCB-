#  Loading related data. The include method.

For load related data we have a three load strategy:
* Eager loading
* Explicit loading
* Lazy loading

## Eager loading:
Loads an entity with related entityes in a single request.
Allows to load related data using the **Include()** method, to which the navigation property is passed.
> Specifies related entities to include in the query results. The navigation property to be included is specified starting with the type of entity being queried (TEntity). If you wish to include additional types based on the navigation properties of the type being included, then chain a call to **ThenInclude()** after this call.

**Include()** and **ThenInclud()** ruterns a new query with the related data included.
It`s equivalent **LEFT JOIN** from SQL.

If we work with data in current(inside scoped) context(create and adding or requiring and loading of data), it doesn`t make sense to pull objects using **Include()**.
```c#
...
using (ApplicationContext db = new ApplicationContext())
{
    var companies = db.Companies.ToList();
    // getting users
    var users = db.Users
        //.Include(u => u.Company)  // loading data companyes
        .ToList();
    foreach (var user in users)
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");
}
...
```

## Explicit loading:

Loads only the primary entity, to load related entityes need to make a separate request.
Data is explicity loaded using the **Load()** method.

For loading all users from database we might to use **db.Users.Load()** 
Also, for loading related data we might to use methods **Collection()** and **Reference()**
* Collection() - if the navigation property represents a collection.
* Reference() - if the navigation property represent a single object.

## Lazy loading:
Assumes implicit automatic loading of related data when accessing the navigation property.
To use lazyloading, need install **Microsoft.EntityFrameworkCore.Proxies** packege
```PowerShell
Install-Package Microsoft.EntityFrameworkCore.Proxies
```
and enable it by calling **UseLazyLoadingProxies()** method
```c#
public class ApplicationContext : DbContext
{
...
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
      --->  .UseLazyLoadingProxies()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");
        ...
    }
...
}
```

Also all navigation properties must be **virtual**:
```c#
public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        ...

        public int? PositionId { get; set; }
  --->  public virtual Position Position { get; set; }
    }
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

  --->  public virtual List<User> Users { get; set; } = new List<User>();
    }
```
<br>

In the example project in the "Lazy loading" part in Program file:<br>

Six users are passed throught, but only four requests(2 to Company and 2 to Possition) are executed. It because afterthe object is loaded into the context, it`s taken from this(loaded) context.

#### P.S.
@
    When using **Lazy loadin**, keep in mind that if any changes occured in the database, (for example, another user changed the data) the data is not reloaded. The context continues to use the data that was loaded eqrlier.