# Connect to DB

* For connect to Db we must be created model classes.
  1. Creating model with your hands(problem: if the are lot of tables?)
  2. Use EF core tool ReversEngineering, which allow your automatically create all the neccesary classes
* For use function, need entered to console function Scaffold-DbContext "[arg1]" [arg2] with argumnets:
    * [arg1] info about Db, for example:
    ```
    "Server=(localdb)\mssqllocaldb;Database=firstUserAppdb;Trusted_Connection=True;"
    ```
    *  [arg2] uses packege, for example:
    ```
    Microsoft.EntityFrameworkCore.SqlServer
    ```

*  After executin command needed class will be added. And will be added data context class.
