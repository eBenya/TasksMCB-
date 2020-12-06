## Foregin key and navigation properties

Working with following model:
```c#
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
     
    public List<User> Users { get; set; }   // navigation property
}
 
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
 
    public int CompanyId { get; set; }      // foregin key
    public Company Company { get; set; }    // navigation property
}
```


##### Rules:
* Or the main entity must have a link in the form of list of dependent entities.
* Or dependent entity must have a navigation property on the main entity(link on **ID** will be an optional field, but it will be created in the database anyway).<br> Rule(not mandatory) to creating a foreign key name to a dependent entity:
  * *\<Navigation_property_name>* + *<Key_name>*
  * *\<Class_name_of_the_associated_entity* + *Key_name*

#### Configuring a key using data annotations
To configuring key, use **[ForeignKey]** attribute:
```c#
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    //public int CompanyId_shka { get; set; }      // foreign key
    
    [ForeignKey("CompanyId_shka")]  <-- will be generate foreign key with this name ("CompanyId_shka")
    public Company Company { get; set; }    // navigation property
}
```
Fluent API:
```c#
...
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasOne(p => p.Company)         // one -
        .WithMany(t => t.Users)         // - to - many
        .HasForeignKey(p => p.CompanyId_shka);
}
...
```