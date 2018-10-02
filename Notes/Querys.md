You need to use LINQ execution method to make a query execute against the DB.

```C#
context.Samurais.ToList()
```

All these execution methods, have their Async counterpart. 

```C#
context.Samurais.ToListAsync()
ToList() --> ToListAsync() 
First() --> FirstAsync() 
FirstOrDefault() --> FirstOrDefaultAsync() 
Single() --> SingleAsync() 
SingleOrDefault() --> SingleOrDefaultAsync() 
Count --> CountAsync() 
LongCount --> LongCountAsync() 
Min() --> MinAsync() 
Max() --> MaxAsync() 
```

As EF Core 1.0 these are evaluated on the client side()

```C#
Last() --> LastAsync()
LastOrDefault() --> LastOrDefaultAsync()
Average() --> AverageAsync()
```

Not a LINQ method, but a DbSet method

```C#
Find(keyValue) --> FindAsync()
```

### Two Ways to Express LINQ Qerys ###

__LINQ methods__

```C#
context.Samurais.ToList()
context.Samurais.Where(s => s.Name == "Zoro").ToList()
```

__LINQ Query Syntax__

```C#
(FROM s IN context.Samurais).ToList()
(FROM s IN context.Samurais WHERE s.Name == "Zoro").ToList()
```

Database Connection Remains OPEN During Enumeration

```C#
foreach (var samurai in context.Samurai) { 
   console.WriteLine(samurai.Name)
}
```
 __minimal__ effort on enumeration, __OK__

```C#
foreach (var samurai in context.Samurai) { 
   RunSomeValidator(samurai)
   CallSomeService(samurai)
   GetSomeMoreDatabaseOn(samurai)
}
```
__Lots of work each result__. Connections stays open until last recrod is fetched

```C#
var samurais = context.Samurais.ToList()
foreach (var samurai in samurais) { 
   RunSomeValidator(samurai)
   CallSomeService(samurai)
   GetSomeMoreDatabaseOn(samurai)
}
```
__smarter__ to get results first

### Harcoding values ###

```C#
_context.Samurais.FirstOrDefault(s => s.Name == "Soro");
```
If you harcode the values directly into the quey then EF concludes that that value rnadomly comes from data entry so it safe, so it harcodes the value into the sql

```C#
var name = "Soro";
_context.Samurais.FirstOrDefault(s => s.Name == name);
```
EF parameterize the query to sql

### What happens when there aren´t any ###

```C#
First()
Single()
Last()
```

__system.InvalidOperationException: Sequence contains no element__

```C#
FirstOrFefault()
SingleOrFefault()
LastOrFefault()
```

__Returns null__

### Find vs FirstOrDefault ###

__FirstOrDefault__ method makes a reuest no matter what to the DB
__Find__ DbSet method first checks if the context is already tracking that object, and if it is, it return the object inmedietly, without making a request to the Db. If not, use a 
```C#
FirstOrDefoult()
```

### Batch Operations ###

Batch operations are limitted to common operations. You can have bunch of activities that the context its tracking update, insert and deletes. When the context save changes will batch them all.