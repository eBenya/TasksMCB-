## Managing roles

To apply access to the controller by roles, we  might to set **Role** attribute:
```C#
[Authorize(Roles="admin")]
public class UsersController : Controller
{
    //...

    [Authorize(Roles = "admin")]
    public IActionResult Index() => View(_userManager.Users.ToList());

    //...
}
```
P.S. This attribute we might use and on class.

The rest of the role work looks like familiar CRUD operations:
```C#
public class RolesController : Controller
{
    RoleManager<IdentityRole> _roleManager;
    UserManager<User> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IActionResult Index() => View(_roleManager.Roles.ToList());
 
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        //...
    }
         
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }
 
    public IActionResult UserList() => View(_userManager.Users.ToList());
 
    public async Task<IActionResult> Edit(string userId)
    {
        //...
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        //...
    }
}
```

