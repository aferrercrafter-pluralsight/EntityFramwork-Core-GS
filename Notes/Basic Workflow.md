### Basic Workflow

1. Define Model 
2. Express and execute query  
    `code(from p in people select p).ToList()` 
3. EF determines and execute SQL 
    `SELECT * FROM people`
4. EF transform results into your types
5. User modifies data 
6. Code triggers save 
    `DbContext.SaveChanges()`
7. EF determines and execute SQL 
    `UPDATE people SET name = 'Julie WHERE id = 3'`