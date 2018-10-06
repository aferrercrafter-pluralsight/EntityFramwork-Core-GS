__Different Change Tracker Behaviors__

__Completly Disconnected__ Connecting entities when __none__ are being tracked 
__Partially Disconnected__ Connecting entities when __some__ are already tracked

###Completly Disconnected###

web apps, api, server base activities _(Disconnected Scenarios)_

Behaviors has changed from EF6

It applays all!

DbSet.Add    --> DbContext.Add    --> DbSet.AddRange    --> DbContext.AddRange
DbSet.Attach --> DbContext.Attach --> DbSet.AttachRange --> DbContext.AttachRange
DbSet.Update --> DbContext.Update --> DbSet.UpdateRange --> DbContext.UpdateRange
DbSet.Remove --> DbContext.Remove --> DbSet.RemoveRange --> DbContext.RemoveRange

__Add__
All objects are mared __Added__ regardless of key values

__Attach__
Empty store-generated keys __Added__
Root* and rest are marked __Unchanged__

__Update__
Empty store-generated keys __Added__
Root* and rest are marked __Modified__

__Remove__
Empty store-generated keys throw Exception
_System.InvalidOperationException_: 'The property 'Id' on entity type 'Samurai' has a temporary value while attempting to change the entity's state to 'Deleted'. Either set a permanent value explicitly or ensure that the database is configured to generate values for this property.'
Only root is marked __Deleted__, rest is marked as __Unchanged__





