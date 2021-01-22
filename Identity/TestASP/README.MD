## Database initializing

For init db, create new class with еру static Initialize method(the name is arbitary):
```C#
public class UsersInitializing
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {            
        if (await roleManager.FindByNameAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }
        if (await roleManager.FindByNameAsync("user") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
        }

        string userEmail = "admin@kek.sec";
        string password = "admin";
        if (await userManager.FindByNameAsync(userEmail) == null)
        {
            User admin = new User { Email = userEmail, UserName = userEmail };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }

        userEmail = "user@kek.com";
        password = "123";
        if (await userManager.FindByNameAsync(userEmail) == null)
        {
            User user = new User { Email = userEmail, UserName = userEmail };
            IdentityResult result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}
```

Use our class into Program.cs for initializing into Main method:
```C#
var host = CreateHostBuilder(args).Build();

using(var scope = host.Services.CreateScope())
{
    //get container
    var services = scope.ServiceProvider;
    try
    {
        //get user and role manager via GetRequiredService
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await UsersInitializing.InitializeAsync(userManager, rolesManager);
    }
    catch(Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
host.Run();
```