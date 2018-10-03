### Loading Related Data ###

* Eager via Include() EF -> EF6 / EF Core
* Eager via Projection EF -> EF6 / Not Quite
* Explicit Load EF -> EF6 / EF Core
* Lazy Loading EF -> EF6 / EF Core

### Eager Load using Include ###

```c#
private static void EagerLoadWithInclude()
        {
            var samurai = _context.Samurais.First();            
            _context.Samurais.Include(s => s.Quotes).FirstOrDefault().Quotes.Add(new Quote { Text = "Gomu gomu nooo Jet Pistol" });            
            _context.SaveChanges();
        }
```
Each Samurai _(main entity)_ should come with his quotes _(realted entity)_. This is rendered as two different commnads to the DB, one for queryng the samurais _(main entity)_, and a second one to return the Quotes _(related entity)_. __(One to Many)__
EF Core create multiples queries to avoid:
* flattening data
* returning redundant results 
* better performance

__Include loads the entire ser of related objects__
(you cannot filter the related data or order it)

__Multi level Include__

```c#
private static void EagerLoadManyToManyAkaChildrenGrandChildren()
        {
            using (var context = new SamuraiContext())
            {
                var samuraiWithBattles = context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle).ToList();
            }
        }
```
`ThenInclude()` is an include for the result of the first `Include()`

__Multi Branch Include__

```c#
private static void EagerLoadingWithMultipleBranches()
        {
            using (var context = new SamuraiContext())
            {
                var samuraiWithQuotesAndSecretIdentity = context.Samurais
                .Include(s => s.Quotes)
                .Include(s => s.SecretIdentity).ToList();
            }
        }
```

Quotes _(related coletion entity)_ it´s rendered as a new commnad, but SecretIdentity _(One to One Relation)_ is included in the first command as a Left Join when getting the Samurais _(main entity)_. Only collections relateds entity need a second separate command.

__FromSql Include__

```c#
private static void EagerLoadWithFromSql()
        {
            using (var context = new SamuraiContext())
            {
                var samuraisWithQuotes = context.Samurais.FromSql("SELECT * FROM Samurais").Include(s => s.Quotes).ToList();
            }
        }
```

The FromSql query is executed first, then another command for the inlcude is executed.

### Eager Load using Projection ###

Anonymous type is a new object "on te fly" type, returned via projection.

```c#
private static void AnonymousTypeViaProjection()
        {
            using (var context = new SamuraiContext())
            {
                var quotes = context.Quotes
                    .Select(q => new { q.Id, q.Text })
                    .ToList();
            }
        }
```

__Single Type__
SQL -> Simple
Results -> As expected

```c#
private static void AnonymousTypeViaProjectionWithrelated()
{
    using (var context = new SamuraiContext())
    {
        var samurais = context.Samurais
            .Select(s => new {
                s.Id,
                s.SecretIdentity.RealName,
                QuoteCount = s.Quotes.Count
                })
            .ToList();
    }
}
```

__Scalar values across relationships__
SQL -> N+1 (seems fixted Core 2.1 version)
Results -> As expected

```c#
private static void RelatedObjectsFixUp()
{
    using (var context = new SamuraiContext())
    {
        var samurai = context.Samurais.Find(1);
        var quotes = context.Quotes.Where(q => q.SamuraiId == 1).ToList();
    }
}
```
When the samurai is retrived, it does not have the related quotes, but when quotes are retrieved,the changetracker did something called _fixingup the navigations_. It fix the samurai related data now tracked.

```c#
private static void EagerLoadViaProjectionNotQuite()
{
    using (var context = new SamuraiContext())
    {
        var samurais = context.Samurais.Select(s => new { Samurai = s, Quotes = s.Quotes }).ToList();
    }                
}
```

__Full related objects__
SQL -> N+1 (seems fixted Core 2.1 version)
Results -> Not Tracked (fixed)

All results are in memory, but navigations are not fixed up
known bug https://github.com/aspnet/EntityFrameworkCore/issues/7131
Issue fixed!

```c#
private static void FilteredEagerLoadViaProjectionNope()
{
    using (var context = new SamuraiContext())
    {
        var samurais = context.Samurais.Select(s => new {
            Samurai = s,
            Quotes = s.Quotes.Where(q => q.Text.Contains("nope")).ToList()
        })
        .ToList();
    }
}
```

Quotes are not even retrieved in query if not ToList or Linq Exec method is called

__Full related objects, filtered__
SQL -> Requires ToList or other Linq exec method
Results -> Not tracked (not fixed)


