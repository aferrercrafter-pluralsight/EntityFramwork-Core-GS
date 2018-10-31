__Tracking objects with Entity.State__
* Root object set to spicified State, _regardless of Key values_
* Rest of graph is ignores
(_Important: With this behavior, EFCore __Entry.State__ give us the possibility to __track a single entity from a Graph__)
* Different behavior for EF6. It's an important change from EF6


```c#
//...
Entry(objGraph).State = EntityState
//...
```
When changing state using EntityState, EF __WILL__ follow instructions, even if later, the database fails to execute the db command.

```c#
var samuraiGraph = new Samurai() { Name = "He who change state", Id = 1 };
    using (var context = new SamuraiContext())
    {
        context.Entry(samuraiGraph).State = EntityState.Modified;
        Console.WriteLine("State changed using Entity State");
        DisplayState(context.ChangeTracker.Entries().ToList(), "InitialState");

        context.Entry(samuraiGraph).State = EntityState.Added;
        DisplayState(context.ChangeTracker.Entries().ToList(), "New State");
        context.SaveChanges();
    }
```
*Primary key is already populated when Identity_Insert is set to of, Db will throw an exception trying to execute the Insert command*

__Tracking objects with TrackGraph__
* Iterate trough graph
* Perform fuction on each entity
```c#
//...
DbContext.ChangeTracker.TrackGraph(graph, function);
//...
```

__TackGraph with State Property as Function__

```c#
//...
DbContext.ChangeTracker.TrackGraph(graph, e => e.State = EntityState.Attach);
//...
```

__Change Tracker temprary Id__

```c#
//...
var samurai = new Samurai() // Id = 0
//...
context.Samurai.Add(samurai) // Id = Int.Min +1 -2147482647
//
context.SaveChanges() // Id = (Db logic)
```

