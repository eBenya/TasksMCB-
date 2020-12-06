## Configuration of models

To take the configuration out, we need to create a configuration class that implements the **IEntityTypeConfiguration\<T>** interface, or inplementation in separate methods.<br>

Let`s say we have the following models:
```c#
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}
 
public class Product
{
    public int Ident { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}
```

ApplicationContext will be look like:
```c#
public class ApplicationContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProductAppdb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
--->    modelBuilder.Entity<Product>()
                .ToTable("Mobiles").HasKey(p => p.Ident);
--->    modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired().HasMaxLength(30);
 
--->    modelBuilder.Entity<Company>().ToTable("Manufacturers")
                .Property(c => c.Name).IsRequired().HasMaxLength(30);
    }
}
```

The imposition of the model configuration in the individual models:
```c#
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product").HasKey(p => p.Ident);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
    }
}

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("company")
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(10);
    }
}
```
Then the **OnModelCreating()** of the **ApplicationContext** class will look like:
```c#
...
public override void OnModelCreating(ModelBuilder builder)
{
    builder.ApplyConfiguration(new ProductConfiguration());
    builder.ApplyConfiguration(new CompanyConfiguration());
}
...
```