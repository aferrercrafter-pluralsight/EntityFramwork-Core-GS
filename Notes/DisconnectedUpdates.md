### Web Controllers ###

```c#
public class SamuraiController : Controller {
    private SamuraiContext _context = new SamuraiContrext();

    public Samurai Get(int id) {
        return _context.Samurai.Find(id);
    }

    public void Put([FromBody] Samurai samurai) {
        _context.Samurai.Update(samurai);
        _conext.SaveChanges();
    }    
```

`DbContext` will be __new__ for each __Web Request__
No knowledge of object state tracked by other instances.
Add/Update/Attach/Remove or Entry.Sate to start tracking.

Do not create static DbContexts to enable ongoing change tracking in a Web Application. Design for short-lived DbContext in web apps.

###Update options###

```C#
DbSet.Update(entity)
DbSet.UpdateRange(entityA, entityB)
DbContext.Update(entity)
DbContext.UpdateRange(entityA, entityB)
DbContext.Entry(entity).State = EntityState.Modified
```

