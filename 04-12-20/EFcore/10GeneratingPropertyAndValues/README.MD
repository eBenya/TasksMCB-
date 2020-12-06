## Generating property and values:

Default, for primaty key property that represent types int or GUID and that have default value, value is generated when inserted into the database. No default values are generated for all other properties.

### DatabaseGeneratedAttribute attribute

It is anotation that allows to change the behavior of the database when it is added or modified.

For disable auto-generation when adding, use **[DatabaseGenerated(DatabaseGeneratedOption.None)]**
Now when you try to add more than one objec, an exception will be throw.
```c#
    ...
    db.Users.Add(new User { Name = "Kek" });
    db.Users.Add(new User { Name = "Puc" });  // Ошибка
    db.SaveChanges();
    ...

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
}
```
Atribute value **DatabaseGeneratedOption.Identity** on the contary automatically generates a value.

For default value use Fluent API method - **HasDefaultValue()**
```c#
 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().Property(u => u.Age)
            .HasDefaultValue(18)
}
```

**HasDefaultValueSql()** method also determines the generation of the default value. Only the value itself is set based on the SQL code that is passed to this method.
```c#
 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().Property(u => u.CreatedAt)
 --->       .HasDefaultValueSql("GETDATE()");
}
```
Similarly, the **HasComputedColumnSql()** method, calculates a value based on columns.
 
```c#
 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().Property(u => u.Name)
 --->       .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
}
public class User
{
    public int Id { get; set; }
    public string Name { get;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
```