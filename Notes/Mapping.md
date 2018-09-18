**Data annotations** has only a subset of **Fluent API** features.
**Data annotations** in class
`[Key]`
`pubic int SamuraiKey { get; set; }`

`[Column("Spoken_Quote")`
`public String Text { get; set; }`


**Fluent API** via DBContext
`modelBuilder.Entity<Samuray>().HasKey(s => s.SamurayKey);`
`modelBuilder.Entity<Quote>().Property(q => q.Text).HasColumnName("Spoken_Quote")`

It's better not express the data schema in domain classes, so it's better to use **Fluent API** in the DbContext

##One to One Relationship##

###Trough EF6###
* Requieres Navigation properties on both ends
* __Primary Key__ of _dependant_ is also __FK__ of _principal_
* Mark __dependant navigation__ as _required_, otherwise 1:0..1
* Or: Fluent Api to specify _principal_  and _dependent_

###EF Core###
* Requieres Navigation properties on both ends
* __FK__ property used to infer _principal_ and _dependant_
* Mark __dependant navigation__ as _required_, otherwise 1:0..1
* Or: Simpler Fuent API: HasOne, WithOne

##Many to Many##

* Requieres JoinTable
* Requieres List of JoinTable Navigation property in both Entities of the many to many relation
* The JoinTable need in the Fluent API the specification of the Primary Key, formed by the to FK

`modelBuilder.Entity<SamuraiBattle>().HasKey(sb => new { sb.BattleId, sb.SamuraiId });`