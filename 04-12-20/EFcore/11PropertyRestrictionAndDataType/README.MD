## Property restriction

#### Required attribute:
This propperty is requared for installation, meaning it will have NOT NULL definition it the database.

```c#
public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
```
The Fluent equivalent is the **IsRequired()**

#### MaxLength/MinLength attribute:
Limits the length of the field.

```c#
public class User
{
    public int Id { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
}
```
The Fluent equivalent is the **HasMaxLength()**

## Data types:
By default, the provider of a particular database managment system selected data types for columns based on the data types of the entity properties.

You can use the **Column** attribute to set the data type for the database.

```c#
public class User
{
    public int Id { get; set; }
--> [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }
}
```
The Fluent equivalent is the **HasColumnType()**