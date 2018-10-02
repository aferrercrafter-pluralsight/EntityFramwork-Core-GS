### Lazy Loading ###

```c#
private static void AddChildToExistingObject()
        {
            var samurai = _context.Samurais.First();            
            _context.Samurais.Include(s => s.Quotes).FirstOrDefault().Quotes.Add(new Quote { Text = "Gomu gomu nooo Jet Pistol" });            
            _context.SaveChanges();
        }
```

It seems EF Core, by default, has lazy loading behaviors, so it´s needed the Include call to insure related entities get tracked.

### Adding One to One ###

```c#
private static void AddOneToOneToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.SecretIdentity == null);
            samurai.SecretIdentity = new SecretIdentity() { RealName = "Yagamy 1" };
            _context.SaveChanges();
        }     
```

If we try to change realted one to one entity instance, will we get an error, becouse it will be trying to insert another related entity (_SecretIdentity_) in table, when each entity of that kinf should have an unique main related entity (_SamuraiId_)

SecretIdentity
Id / SamuraiId
1 / 1
2 / 1
__Imnpossible__ on One to One relation
