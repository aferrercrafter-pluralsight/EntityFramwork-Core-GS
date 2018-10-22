###Tracking or Non-tracking Queries###

__Track all querys results(default)__
* Default behavior of EF1 since v1
* EF tracks result of all queries

__Disabe by query__:
```c#
_context.Samurais.AsNoTracking().ToList();
```

__Tracking per context instance__
* New to EF Core
```c#
ChangeTracker.QueryTrackingBehavior

/*
QueryTrackingBehavior enum
    .TrackAll (default)
    .NoTracking
*/

context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBeavior.NoTracking
```

___EnableTrackingByQuery__
```c#
context.Samurais.AsTracking().ToList()
```