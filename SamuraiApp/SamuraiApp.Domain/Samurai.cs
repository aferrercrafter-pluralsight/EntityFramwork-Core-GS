﻿using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Samurai : ClientChangeTracker
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public SecretIdentity SecretIdentity { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<SamuraiBattle> SamuraiBattles {get;set;}

        public Samurai()
        {
            Quotes = new List<Quote>(); 
        }
    }
}
