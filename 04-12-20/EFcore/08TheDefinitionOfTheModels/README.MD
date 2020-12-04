## The definition of the models:

Let us define the following entities:
```c#
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    // Navigation property
    public Company Manufacturer { get; set; }
}

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
}   
```

And the following data context class:
```c#
public class ApplicationContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public ApplicationContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(nameof(Country));
    }
}
```

Then be created 2 tables "Product" and "Company".

If use a different way to include an entity in the model, calling Entity() of the ModelBuilder object in the OnModelCreating() method:
```c#
public class ApplicationContext : DbContext
{
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>();
    }
    ...
}
```
Will be created 3 tables "Product", "Company" and "Country".

##### Exclusion from the model:

To exclussion entity from the model we can use the Fluent API:
```c#
public class ApplicationContext : DbContext
{
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Company>();
    }
    ...
}
```

or Data Annotation:
```c#
...
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    // Navigation property
    public Company Manufacturer { get; set; }
}
 
[NotMapped]
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}
...
```

With the exclude the Company entity, the database will have only one  Products table, and it will not contain a column thet would be maped to the Manufacturer property of the Product class.

Excluding properties from model
* Use Fluent API:
    ```c#
    public class ApplicationContext : DbContext
    {
        ...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Ignore(b => b.Description);
        }
        ...
    }
    ```
* Use Data Annotation:
    ```c#
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public string Description { get; set; }
    }
    ```
