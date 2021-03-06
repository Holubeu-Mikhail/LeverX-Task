using DataAccessLayer.IntegrationTests.Common;
using DataAccessLayer.IntegrationTests.Helpers;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IntegrationTests.RepositoryTests
{
    public class ProductRepositoryTest
    {
        private IRepository<Product> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
            var dbContext = new AppDbContext(optionsBuilder.Options);

            _repository = new EntityRepository<Product>(dbContext);
            BackupService.CreateDatabaseBackup();
            DataHelper.DeleteAllFromDatabase();
            TownDataHelper.FillTable();
            BrandDataHelper.FillTable();
            ProductTypeDataHelper.FillTable();
            ProductDataHelper.FillTable();
        }

        [TearDown]
        public void TearDown()
        {
            BackupService.RestoreDatabaseBackup();
        }

        [Test]
        public void GetAll_ValidCall_GetAllItemsFromDatabase()
        {
            //Arrange
            var expectedEntities = new List<Product> { new Product { Id = 1, Name = "Fish", Quantity = 1, TypeId = 1, BrandId = 1} };

            //Act
            var entities = _repository.GetAll().ToList();

            //Assert
            entities.Should().BeEquivalentTo(expectedEntities, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Get_ValidCall_GetItemFromDatabase()
        {
            //Arrange
            var expectedEntity = new Product { Id = 1, Name = "Fish", Quantity = 1, BrandId = 1, TypeId = 1 };

            //Act
            var entity = _repository.Get(1);

            //Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        public void Create_ValidCall_InsertItemInDatabase()
        {
            //Arrange
            var entities = new List<Product> { new Product { Id = 1, Name = "Fish", Quantity = 1, BrandId = 1, TypeId = 1 },
                new Product { Id = 2, Name = "Fish2", Quantity = 1, BrandId = 1, TypeId = 1 } };
            var entity = new Product { Id = 2, Name = "Fish2", Quantity = 1, BrandId = 1, TypeId = 1 };

            //Act
            _repository.Create(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Update_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entity = new Product { Id = 1, Name = "Fish3", Quantity = 1, BrandId = 1, TypeId = 1 };
            var entities = new List<Product> { entity };

            //Act
            _repository.Update(entity);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }

        [Test]
        public void Delete_ValidCall_UpdateItemInDatabase()
        {
            //Arrange
            var entities = new List<Product> { new Product { Id = 1, Name = "Fish", Quantity = 1, BrandId = 1, TypeId = 1 } };
            var entity = new Product { Id = 2, Name = "Fish2", Quantity = 1, BrandId = 1, TypeId = 1 };
            _repository.Create(entity);
            var id = 2;

            //Act
            _repository.Delete(id);

            //Assert
            _repository.GetAll().Should().BeEquivalentTo(entities);
        }
    }
}