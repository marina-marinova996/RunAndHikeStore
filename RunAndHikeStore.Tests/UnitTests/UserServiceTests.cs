using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Data.Repositories;
using RunAndHikeStore.Services;
using RunAndHikeStore.Services.Contracts;

namespace RunAndHikeStore.Tests.Services.UnitTests
{
    public class UserServiceTests
    {
        private IRepository repo;
        private IUserService userService;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("RunHikeDB")
                .Options;

            dbContext = new ApplicationDbContext(contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }


        [Test]
        public async Task TestGetUserForEdit()
        {
            repo = new Repository(dbContext);
            userService = new UserService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var userForEdit = await userService.GetUserForEdit(user.Id);

            Assert.AreEqual(user.Id, userForEdit.Id);
            Assert.AreEqual(user.FirstName, userForEdit.FirstName);
            Assert.AreEqual(user.LastName, userForEdit.LastName);
            Assert.AreEqual(user.Email, userForEdit.Email);
        }
        [Test]
        public async Task TestUpdateUser()
        {
            repo = new Repository(dbContext);
            userService = new UserService(repo);

            var user = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var userForEdit = await userService.GetUserForEdit(user.Id);

            userForEdit.FirstName = "Test First Name";
            userForEdit.LastName = "Test Last Name";

            var isUpdated = await userService.UpdateUser(userForEdit);

            var dbUser = await this.repo.All<ApplicationUser>().Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            Assert.IsTrue(isUpdated);
            Assert.AreEqual(userForEdit.Id, dbUser.Id);
            Assert.AreEqual(userForEdit.FirstName, dbUser.FirstName);
            Assert.AreEqual(userForEdit.LastName, dbUser.LastName);
            Assert.AreEqual(userForEdit.Email, dbUser.Email);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
