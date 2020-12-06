# Basik data opperations(CRUD opperations).

Realize CRUD opperations.<br>

Need to be careful with "lazy" opperation.
Don't forget to save changes. And also update objects after saving changes.<br>

If we recieved an object in one scope,
and update it in second scope, then [db].[Data].SaveChanges() it will not work.
This is because the context does not track changes created outside the scope.

To update the state from another scope, use [db].[Data].Update(obj) to tell EF directly about the data update.
