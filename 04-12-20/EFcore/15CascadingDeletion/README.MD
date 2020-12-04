## Cascading deletion

If field has property **NOT NULL** and requires a specific value, that foreign key will be generating with **ON DELETE CASCADE**.
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
When you delete a company, all users associated with it will be deleted.

In the next model, deletion will not occur:
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
              
      public int? CompanyId { get; set; }      // foregin key
                ^
                |
Nullable int ---
    
    public Company Company { get; set; }     // navigation property
}
```

In Fluent API:
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasOne(p => p.Company)
        .WithMany(t => t.Users)  
        .OnDelete(DeleteBehavior.Cascade);  //ON DELETE CASCADE
}
```
<br>

Three different scenarios that control behavior of a dependent entity when the master entity is deleted:

* Cascade: the dependent entity is deleted along with the master entity.

* SetNull: the foreign key property in the dependent entity is set to null.

* Restrict: the dependent entity does not change in any way when master entity is deleted.

##### P.S. default Entity create foreign key field as **Nullable type**
