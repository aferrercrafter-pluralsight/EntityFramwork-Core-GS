
```c#
private static void ExplicitLoad()
{
    using (var context = new SamuraiContext())
    {
        var samurai = context.Samurais.FirstOrDefault();
        context.Entry(samurai).Collection(s => s.Quotes).Load();
        context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
    }
}
```

Explicit loading allows you to filter the realted data

```c#
context.Entry(object).Collection(s => s.Property)
            .Query()
            .LinqMethod(lambda)
            .Load();
context.Entry(object).Reference(s => s.Property)
            .Query()
            .LinqMethod(lambda)
            .Load();  

using (var context = new SamuraiContext())
            {
                var samurai = context.Samurais.FirstOrDefault();
                context.Entry(samurai).Collection(s => s.Quotes)
                    .Query()
                    .Where(q => q.Text.Contains("nope"))
                    .Load();
            } 
```

__Loading related data Pros and Cons__

 | Eager via Inlcude | Eager via Projection | Post-Query Explicit Load
--- | --- | --- | ---
Code | Single Query | Single Query requires a hack | Separte Method 
Limitations | No filtering, sorting, etc | 1.1 bugs prevent tracking | None taht I can think of
Database Trips | Reference : 1 Collection N + 1 (1.1)| Reference : 1 Collection N + 1 (1.1) | Initial + Load Method