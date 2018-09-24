DbContext can only delete objects it is aware of, i.e., already tracking.

It not exist a `Db.Delete(int Id)`

Alternate ideas for delete by ID
* Buid a proxy object in memory using the key value. (Bad, can couse unexpected side effects)
* Use a store procedure