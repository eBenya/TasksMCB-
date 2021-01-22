# Identity

When adding identity, all  the functional is placed in **"Areas/Identity"** folder.
For all pages to fullfill their role, the identity configuration is added to Startup.cs class.
```c#
public void ConfigureServices(IServiceCollection services)
{
    //.......................     
    // add identity db context
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
 
    // add services Idenity
    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>(); 
    //........................
}
```

And in **Configure()** method all functional authorization and  authentification based on the Identity system
becomes available over invocation methods as **UseAuthentication()** and **UseAuthorization()**.

For starts work we must do apply migrations:
```
Udate-Database
```

#### Nuances when creating a project from scratch:

**Model** - user class inherited about **IdentityUser**

**Context** - *Context inherited about **Identity**DbContext<User> **not** DbContext!

```c#
services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();
```
AddIdentity - setup initial(first) config. 

Type **User** it`s created class user(or default class - **IdentityUser** from  **Microsoft.AspNetCore.Identity.EntityFrameworkCore** namespace).

Type IdentityRole it`s default rolle class from  **Microsoft.AspNetCore.Identity.EntityFrameworkCore** namespace.

### Main classes in ASP.NET Core Identity:

#### data context Identity(**IdentityDbContext**), contained:

* Users: a set of IdentityUser objects, corresponding to the user table.

* Roles: a set of IdentityRole objects, corresponding to the role table.

* RoleClaims: a set of IdentityRoleClaim objects, corresponds to the claims role and object relationship table.

* UserLogins: a set of IdentityUserLogin objects, corresponds to the table of users ' association with their logins of their external services.

* UserClaims: a set of IdentityUserClaim objects, corresponds to the user-object relationship table claims.

* UserRoles: a set of IdentityUserRole objects, corresponds to a table that maps users and their roles.

* UserTokens: a set of IdentityUserToken objects, corresponding to the user tokens table

#### IdentityUser:
    
* Id: unic user id

* UserName: user nickname 

* Email: user eMail

* Logins: login collection, that use user for enter via third-party services (Google, Facebook и т.д.)

* Claims: claim collection or addition objects, which used for user authorization.

* PasswordHash: passwor hash. The data base does not  store password directly, and only it hash.

* Roles: roles collection, to which user belong.

* PhoneNumber: phone number.

* SecurityStamp: some special value, what changes when you change your authentication data, for example, password.

* AccessFailedCount: the number of failed logins user to the system.

* EmailConfirmed: if you email address confirmed.

* PhoneNumberConfirmed: if you phone number confirmed.

#### UserManager:
As a rule, to user managed used a special class - **UserManager\<T>** from Microsoft.AspNetCore.Identity namespace.

The main properties and methods:

* Users: returns all users.

* ChangePasswordAsync(user, old, new): changed user password.

* CreateAsync(user): creates a new user.

* DeleteAsync(user): deletes the user.

* FindByIdAsync(id): finds user on id.

* FindByEmailAsync(email): finds user on email.

* FindByNameAsync(name): finds user in nickname.

* UpdateAsync(user): updates the user.

* AddToRoleAsync(user, role): add **role** for **user**.

* GetRolesAsync (user): returns a list of role, that the user belongs to.

* IsInRoleAsync(user, name): returns true, if the **user** belongs to the **name** role.

* RemoveFromRoleAsync(user, name): removes the role **name** from **user**.

#### Just as briefly about IdentityRole and RoleManager:

Default property IdentityRole:

* Id: role id
* Name: role name
* Users: user collection, that is includes in this role.

And by analogy with *UserManager* - **RoleManager\<T>**:

* Roles: returns all roles
* CreateAsync(role): creates a new role
* DeleteAsync(role): deletes the role
* FindByIdAsync(id): returns the role by id
* FindByNameAsync(name): returns the role by name
* RoleExistsAsync(name): returns true, if a role with the given name exists
* UpdateAsync(role): updates the role

To use the class **RoleManager** need to change the connection of Identity services in the Startup class in the ConfigureServices method:
```C#
    service.AddIdentity<IdentityUser, IdentityRole>()//...
        ;                           //^
``` 

