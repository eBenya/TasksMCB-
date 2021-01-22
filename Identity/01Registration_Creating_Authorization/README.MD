## Rigistering and users managment in Identity:

To creating user it is enough to set key properties like login and password.

##### The easiest way:

Add to project new **scaffolded** item

![](scaffolded.jpg)

on **Identity** pattern

![](selectLayout.png)

and select the desired options and data base context.

##### Еhe way, do it yourself:

Add controller to work with user accounts
```C#
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {                                       // ^model from view
        if(ModelState.IsValid)
        {
            User user = new User{Email = model.Email, UserName = model.Email, Year=model.Year};
            
            var res = await _userManager.CreateAsync(user, model.Password);
            if(res.Succeeded)
            {
                //set a cocie
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }
}
```

## User athentication in Identity:

Continue immediately with the "second" method:

In **AccountController** class add next methods:
```c#
[HttpGet]
public IActionResult Login(string returnUrl = null)
{
    return View(new LoginViewModel { ReturnUrl = returnUrl });
}
 
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (ModelState.IsValid)
    {
        var result = 
            await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            // checking, if the URL belongs to the app
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            ModelState.AddModelError("", "Incorrect login and/or password");
        }
    }
    return View(model);
}
 
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Logout()
{
    // delete the authentication cookie
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
}
```

##### User managment:
CRUD operation for users:

   Create User controller:
    
```C#
public class UsersController : Controller
{
    UserManager<IdentityUser> _userManager;
 
    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
 
    public IActionResult Index() => View(_userManager.Users.ToList());

    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }
    
    public async Task<IActionResult> Edit(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var model = new EditUserViewModel {Id = user.Id, Email = user.Email, Year = user.Year };
        return View(model);
    }

    public async Task<ActionResult> Delete(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
        }
        return RedirectToAction("Index");
    }
```
To change password
* the decision in a forehead
    ```C#
    public async Task<IActionResult> ChangePassword(string id)
    {
        User user = await _userManager.FindByIdAsync(id);

        if(user == null)
            return NotFound();

        var model = new ChangePasswordViewModel { Id = user.Id /*some user data initialize*/};
        return View(model);
    }
    ```
* via **ChangePasswordAsync()**:
    ```C#
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        User user = await _userManager.FindByIdAsync(model.id);

        if(user == null)
            return NotFound();

        var res = ChangePasswordAsync(user, model.oldPass, model.newPass);
        
        //error handlinf if is`not success...

        return View(model);
    }
    ```