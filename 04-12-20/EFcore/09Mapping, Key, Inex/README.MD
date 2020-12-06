## Mapping:

Table mapping:
```c#
[Table("People")]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```
Now Entity 'User' will be mapped to the "People" table.

Column mapping:
```c#
public class User
{
    [Column(user_id)]
    public int Id { get; set; }
    public string Name { get; set; }
}
```
Now properti Id will be mapped to the "user_id" column.

### Configuring key:

By default, the key is a property that matches the [class_name]Id template.
But you can to this:
```c#
public class User
{
    [Key]
    public int identifier { get; set; }
    public string Name { get; set; }
}
```

Composite key create by using the Fluent API(and configure the name of the constraint thet is set for primary key):
```c#
public class ApplicationContext : DbContext
{
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
"is it"---> modelBuilder.Entity<User>().HasKey(u => new { u.PassportSeria, u.PassportNumber});
    }
    ...
}
public class User
{
    public string PassportNumber { get; set; }
    public string PassportSeria { get; set; }
 
    public string Name { get; set; }
}
```

Or Data Annotation
```c#
public class User
{
    [Key]
    [Column(Order=1)]
    public string PassportNumber { get; set; }
    [Key]
    [Column(Order=2)]
    public string PassportSeria { get; set; }
 
    public string Name { get; set; }
}
```

### Indexes:

By default an index is create for each property, which used as foreign key.
By used Fluent API we can override this behavior in EF using the HasIndex() method:
```c#
{
    ...                                             
    modelBuilder.Entity<Person>()
        .HasIndex(p => new { p.FirstName, p.LastName})  //Create combined index
        .IsUnique()                         //The uniqueness of the index
        .HasDatabaseName("People")        //Name of the index created in the database
        .HasFilter("[Name] IS NOT NULL");   //Filter to search for in the database
    ...
}
```

EF core 5.0 and higher, we can use data Annotation:
```c#
[Index("PassportNumber", "PassportSeria", IsUnique = true, Name = "People")]
public class User
{
    [Key]
    [Column(Order=1)]
    public string PassportNumber { get; set; }
    [Key]
    [Column(Order=2)]
    public string PassportSeria { get; set; }
 
    public string Name { get; set; }
}
```