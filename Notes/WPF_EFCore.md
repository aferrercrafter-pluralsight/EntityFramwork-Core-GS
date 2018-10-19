For applications with a single/connected context, rollback changes, can be archived by creating a new context

```c#
public void RevertBattleChanges(int id)
        {
            //this is the simplest way. 
            //Maybe later versions of EF will make it easier
            _context = new SamuraiContext();
        }
```

To add a many to many relatio is better to mark as added, only the join enity

```c#
private void AddDroppedSamuraiToBattle(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Samurai)))
            {
                var samurai = e.Data.GetData(typeof(Samurai)) as Samurai;
                var samuraiBattle = new SamuraiBattle
                {
                    Battle = _currentBattle,
                    Samurai = samurai
                };
                _repo.AddSamuraiBattle(samuraiBattle);
                _availableSamurais.Remove(samurai);
                NoteSamuraiMove();
            }
        }
```
```c#
public void AddSamuraiBattle(SamuraiBattle samuraiBattle)
        {
            //presumes samurai and battle always already exist
            _context.Entry(samuraiBattle).State
               = EntityState.Added;
        }
```
Instead, adding the join entity to the context, will mark as added, all the graph (including the Samurai and the Battle), causing error in the SaveChanges()
```c#
public void AddSamuraiBattle(SamuraiBattle samuraiBattle)
        {
            //presumes samurai and battle always already exist
            _context.SamuraiBattles.Add(samuraiBattle);
        }
```

AsNoTracking, ensures the retrieved objects are not added to the change tracker, using it can help performance when you are queryng a lot only read object, it will not create state objects for the entities.
```c#
public List<Samurai> SamuraisNotInBattle(int battleId)
        {
            var existingSamurais = _context.SamuraiBattles
              .Where(sb => sb.BattleId == battleId)
              .Select(sb => sb.SamuraiId).ToList();
            var samurais = _context.Samurais.AsNoTracking()
              .Where(s => !existingSamurais.Contains(s.Id))
              .ToList();

            return samurais;
        }
```