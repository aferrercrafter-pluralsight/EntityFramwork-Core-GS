ASP.NET Core Dependency Injection

```c#
//Constructor expects an instance of SamuraiContext
public class SomeClass {
    private SamuraiContext _context;
    
    public SomeClass(SamuraiContext obj){
        _context = ontextInstance;
    }
}

//Configure servoces to supply (instantiate & inject) the object
public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<SamuraiContext>();
    }
}
```