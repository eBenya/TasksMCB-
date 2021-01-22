## Managing role

To apply access to the controller by roles, we  might to set **Role** attribute:
```C#
[Authorize(Roles="admin")]
public class HomeController : Controller
{
    //controller code...
}
```