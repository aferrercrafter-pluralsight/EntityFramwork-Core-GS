using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using SamuraiAppCore.Data;
using SamuraiAppCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SamuraiAppCore.Test
{
    public class SamuraiModelTest
    {
        private DbContextOptions<SamuraiContext> _options;

        public SamuraiModelTest()
        {
            _options = new DbContextOptionsBuilder<SamuraiContext>()
                .UseInMemoryDatabase(databaseName: "SamuraiModelTestDb").Options;
            SeedMemoryInDatabase();
        }

        private void SeedMemoryInDatabase()
        {
            using (var context = new SamuraiContext(_options))
            {
                if (!context.Samurais.Any())
                {
                    context.Samurais.AddRange(
                        new Samurai
                        {
                            Id = 1,
                            Name = "Roronoa Zoro",
                            SecretIdentity = new SecretIdentity
                            {
                                RealName = "Real Rorona"
                            },
                            Quotes = new List<Quote>
                            {
                                new Quote()
                                {
                                    Text = "Nitoriu"
                                },
                                new Quote()
                                {
                                    Text = "Shi Shin Son"
                                }
                            }
                        },
                        new Samurai
                        {
                            Id = 2,
                            Name = "Mugiwara Luffy",
                            SecretIdentity = new SecretIdentity
                            {
                                RealName = "D Luffy"
                            },
                            Quotes = new List<Quote>
                            {
                                new Quote()
                                {
                                    Text = "Gomu gomu no"
                                },
                                new Quote()
                                {
                                    Text = "Bazooka"
                                }
                            }
                        }
                        );
                    context.SaveChanges();
                }
            }
        }


        [Fact]
        public void TwoPlusTwoEqualsFor()
        {
            //Arrange
            var plus = 0;

            //Act
            plus = 2 * 2;

            //Assert
            Assert.Equal<int>(4, plus);
        }
        [Fact]
        public void CanRetrieveListOfSamuraisValues()
        {
            using (var context = new SamuraiContext(_options))
            {
                var repo = new DisconnectedData(context);
                Assert.IsType<List<KeyValuePair<int,string>>>(repo.GetSamuraiReferenceList());
                
            }
        }
        [Fact]
        public void CanRetrieveAllSamuraiValuePairs()
        {
            using (var context = new SamuraiContext(_options))
            {
                var repo = new DisconnectedData(context);
                Assert.Equal(2, repo.GetSamuraiReferenceList().Count);
            }
        }
        [Fact]
        public void CanLoadQuotesAndIdentifyForASamurai()
        {
            using (var context = new SamuraiContext(_options))
            {
                var repo = new DisconnectedData(context);
                var samuraiGraph = repo.LoadSamuraiGraph(1);
                Assert.Equal(2, samuraiGraph.Quotes.Count);
                Assert.NotNull(samuraiGraph.SecretIdentity);
            }
        }


    }
}
