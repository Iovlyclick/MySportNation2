using Microsoft.EntityFrameworkCore;
using SportNation2.Data;
using SportNation2.Services;

namespace SportNation2.Tests
{
    public class CompetitionServiceTests
    {
        private readonly AppDbContext db;
        private readonly CompetitionService svc;

        public CompetitionServiceTests()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SportNation2")                
                .Options;
            db = new AppDbContext(opt);


            svc = new CompetitionService(db);
        }

        [Fact]
       public async Task CreateCompetitionAsync_creates_competition()
        {
            //Arrange
            //préparer les données du test
            var sportid = 1;
            var name = "test";
            var startdate = DateTime.Now.AddDays(2);

            //Act
            //executer la méthode testée
            var result = await svc.CreateCompetitionAsync(sportid, name, startdate);
            var competition = db.Competitions.FirstOrDefault();


            //Assert
            //valider le résultat de la méthode testée
            Assert.True(result > 0);

            Assert.NotNull(competition);
            Assert.Equal(name, competition.Name);
            Assert.Equal(startdate, competition.Date);
            Assert.Equal(sportid, competition.SportId);

        }



        [Fact]
        public void CreateCompetitionDataIsValid_validates_data()
        {
            //Arrange
            //préparer les données du test
            var sportid = 1;
            var name = "test";
            var startdate = DateTime.Now.AddDays(2);

            //Act
            //executer la méthode testée
            var result = svc.CreateCompetitionDataIsValid(sportid, name, startdate);
           
            //Assert
            //valider le résultat de la méthode testée
            Assert.True(result);
        }

        [Theory]
        [InlineData(0, "test", "2024-01-01")]
        [InlineData(-1, "test", "2024-01-01")]
        [InlineData(-1, "", "2024-01-01")]
        [InlineData(1, "test", "2020-01-01")]
        [InlineData(99, "test", "2020-01-01")]
        [InlineData(99, "", "2020-01-01")]
        public void CreateCompetitionDataIsValid_detectes_errors(int sportid, string name, string date)
        {
            //Arrange
            //préparer les données du test
            var startdate = DateTime.Parse(date);

            //Act
            //executer la méthode testée
            var result = svc.CreateCompetitionDataIsValid(sportid, name, startdate);

            //Assert
            //valider le résultat de la méthode testée
            Assert.False(result);
        }
    }
}