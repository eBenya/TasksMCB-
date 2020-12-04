## Initializing the database with initial data

To initialize the database  when configuring a specific model, the **HasData()** method is called, to which the added data is passed:
```c#
    ...
    protected override void OnModelCreating(ModelBuilder br)
    {
        br.Entity<User>().HasData(
        new User[]
        {
            new User {Id==0, Name="Kek", Age=14},
            new User {Id==1, Name="Puc", Age=17},
            new User {Id==2, Name="Lol", Age=23},
        });
    }
    ...

public class User
{
    public int Id{ get; set; }
    public string Name{ get; set; }
    public int Age{ get; set; }
}
```

Attention on field **Id**, it is required for initial initiakization.

Note, that the initialization of the initial data will be performed only two case:
* When performin migration.
* When calling the database method **EnsureCreated()**, which creates the database if it is not present.
