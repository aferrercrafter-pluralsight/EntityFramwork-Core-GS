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