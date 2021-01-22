## Data validation

#### Password:

Password validation can be overridden via **Microsoft.AspNetCore.Identity.PasswordOptions** object.

Needed to change context addition code in **Sturtup.cs** in **ConfigureServices** method:
```C#
services.AddIdentity<IdentityUser, IdentityRole>(opts=> {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false 
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
```
*p.s. If password length can be changed, also need changing registration model definition.*

With a more complex validation system, need to define own class.
```C#
public class CustomPasswordValidator : IPasswordValidator<IdentityUser>
{
    public int RequiredLength { get; set; }
    
    public CustomPasswordValidator(int length)
    {
        RequiredLength = length;
    }
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        // validation code
    }
}
```
where:
```C#
public interface IPasswordValidator<T> where T : class 
{ 
    Task<IdentityResult> ValidateAsync(UserManager<T> manager, T user, string password);
}
```
Do not forget to add the validator class to the service:
```
services.AddTransient<IPasswordValidator<IdentityUser>,
            CustomPasswordValidator>(serv => new CustomPasswordValidator(6));
```

#### User:

    ```C#
    services.AddIdentity<IdentityUser, IdentityRole>(opts => {
            opts.User.RequireUniqueEmail = true;
            opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz";
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();
    ```
OR
```C#
public class CustomUserValidator : IUserValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
    {
        var errors = new List<IdentityError>();

        //data validation logic...  
        
        return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));      
    }
}
```
Where
```C#
public interface IUserValidator<TUser> where TUser : class {
    Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user);
}
```

To ConfigureServices:
```C#
    services.AddTransient<IUserValidator<IdentityUser>, CustomUserValidator>();
```

We could also inherit from the **UserValidator** class
```C#
public class CustomUserValidator : UserValidator<IdnetityUser>
{
    public override Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
    {
        var errors = new List<IdentityError>();

        //data validation logic...  
        
        return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));      
    }
}
```
**UserValidator** class already realized **IUserValidator** interface, so we override **ValidateAsync** method.