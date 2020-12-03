# Operation logging

For logging use - LogTo() it when configuring the data context class.

### Logging level:

  * Trace: for printing detalize message.
  * Debug: for printing info that can be useful in development.
  * Information: when message level, allow you to track the follow of application execution.
  * Warning: for printing message about unexpected events.
  * Error: for printing information about errors and exceptions, that can`t be processed.
  * Critical: printing critical error, that require an  immitade response.

Example:
```c#
...
optionBuilder.LogTo(Console.WriteLine, LogLevel.Error);
...
```
<br>

### Message specification:

Each message in the log is associated with a specific event Id.
For example, spicific message SQL Server provider, described the class SqlServerEventId.<br>
General nature message described CoreEventId class.<br>
Message related to query execution, by the RelationalEventId class.<br>

Example:
```c#
optionsBuilder.LogTo(Console.WriteLine, new[] {RelationalEventId.CommandExecuted});
```
<br>

### Message category:
It`s another way to filter message.
//TODO category...