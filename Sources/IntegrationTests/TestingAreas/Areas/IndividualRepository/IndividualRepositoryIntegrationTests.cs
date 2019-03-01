using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.DataAccess.EntityFramework.IntegrationTests.TestingInfrastructure.DataBuilders;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.Repositories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Factories;
using Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.Domain.Models;
using Mmu.Mlh.TestingExtensions.Areas.Common.BasesClasses;
using NUnit.Framework;

namespace Mmu.Mlh.DataAccess.EntityFramework.IntegrationTests.TestingAreas.Areas.IndividualRepository
{
    [TestFixture]
    public class IndividualRepositoryIntegrationTests : TestingBaseWithContainer
    {
        private IIndividualRepository _sut;

        [Test]
        public async Task AddingAddress_AddsAddress()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();

            var newStreets = new List<Street>
            {
                new Street("Fakestreet", 1234),
                new Street("Another Fakestreet", 432)
            };

            var newAddress = new Address("Fake Town", 1, newStreets, 0);
            individual = await _sut.SaveAsync(individual);

            // Act
            individual.AddAddress(newAddress);
            await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);
            Assert.AreEqual(3, actualIndividual.Addresses.Count);

            var actualNewAddress = actualIndividual.Addresses.Single(f => f.City == "Fake Town");
            AssertAddress(newAddress, actualNewAddress);
            CollectionAssert.AreEqual(newAddress.Streets, actualNewAddress.Streets);
        }

        [Test]
        public async Task AddingStreet_AddsStreet()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            individual = await _sut.SaveAsync(individual);

            // Act
            var addressToUpdate = individual.SearchAddress(f => f.City == "Alterswil").Reduce((Address)null);
            var newStreet = new Street("New Street", 678);
            addressToUpdate.AddStreet(newStreet);

            individual.UpdateAddress(addressToUpdate);
            await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);
            var actualNewStreet = actualIndividual.Addresses.Single(f => f.City == "Alterswil").Streets.FirstOrDefault(f => f.StreetName == "New Street");

            Assert.AreEqual(2, actualIndividual.Addresses.Count);
            Assert.IsNotNull(actualNewStreet);
            Assert.That(newStreet, Is.EqualTo(actualNewStreet));
        }

        [Test]
        public async Task CreatingIndividual_CreatesAddresses()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            var addressAlterswil = individual.Addresses.Single(f => f.City == "Alterswil");
            var addressSursee = individual.Addresses.Single(f => f.City == "Sursee");

            // Act
            individual = await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            Assert.AreEqual(2, actualIndividual.Addresses.Count);

            var actualAddressAlterswil = actualIndividual.Addresses.FirstOrDefault(f => f.City == "Alterswil");
            var actualAddressSursee = actualIndividual.Addresses.FirstOrDefault(f => f.City == "Sursee");

            AssertAddress(addressAlterswil, actualAddressAlterswil);
            AssertAddress(addressSursee, actualAddressSursee);
        }

        [Test]
        public async Task CreatingIndividual_CreatesIndividual()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();

            // Act
            individual = await _sut.SaveAsync(individual);
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            // Assert
            Assert.IsNull(actualIndividual);
            Assert.AreEqual(individual.Birthdate, actualIndividual.Birthdate);
            Assert.AreEqual(individual.FirstName, actualIndividual.FirstName);
            Assert.AreEqual(individual.Id, actualIndividual.Id);
            Assert.AreEqual(individual.LastName, actualIndividual.LastName);
            Assert.AreEqual(individual.Addresses, actualIndividual.Addresses);
        }

        [Test]
        public async Task CreatingIndividual_CreatesStreets()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            var streetsAlterswil = individual.Addresses.Single(f => f.City == "Alterswil").Streets;
            var streetsSursee = individual.Addresses.Single(f => f.City == "Sursee").Streets;

            // Act
            individual = await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            Assert.AreEqual(2, actualIndividual.Addresses.Count);

            var actualStreetsAlterswil = actualIndividual.Addresses.Single(f => f.City == "Alterswil").Streets;
            var actualStreetsSursee = actualIndividual.Addresses.Single(f => f.City == "Sursee").Streets;

            Assert.IsNotNull(actualStreetsAlterswil);
            Assert.IsNotNull(actualStreetsSursee);

            Assert.AreEqual(2, actualStreetsAlterswil.Count);
            Assert.AreEqual(2, actualStreetsSursee.Count);

            CollectionAssert.AreEqual(streetsAlterswil, actualStreetsAlterswil);
            CollectionAssert.AreEqual(streetsSursee, actualStreetsSursee);
        }

        [Test]
        public async Task DeletingIndividual_DeletesIndividual()
        {
            // Arrange
            var individualFactory = ServiceLocator.GetService<IIndividualFactory>();
            var individual = individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), new List<Address>());
            individual = await _sut.SaveAsync(individual);

            // Act
            await _sut.DeleteAsync(individual.Id);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            Assert.IsNull(actualIndividual);
        }

        [Test]
        public async Task DeletingStreet_DeletesStreet()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            individual = await _sut.SaveAsync(individual);

            // Act
            var addressToUpdate = individual.SearchAddress(f => f.City == "Alterswil").Reduce((Address)null);
            var streetToDelete = addressToUpdate.Streets.Single(f => f.StreetName == "Bonnetsacher");
            addressToUpdate.RemoveStreet(streetToDelete);
            individual.UpdateAddress(addressToUpdate);
            await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);
            var actualAddress = actualIndividual.Addresses.Single(f => f.City == "Alterswil");

            Assert.AreEqual(1, actualAddress.Streets.Count);

            var actualDeletedStreet = actualAddress.Streets.FirstOrDefault(f => f.StreetName == "Bonnetsacher");
            Assert.IsNull(actualDeletedStreet);
        }

        [Test]
        public async Task LoadingAll_LoadsAllIndividual()
        {
            // Arrange
            var individualFactory = ServiceLocator.GetService<IIndividualFactory>();
            var individual = individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), new List<Address>());
            await _sut.SaveAsync(individual);

            var individual2 = individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), new List<Address>());
            await _sut.SaveAsync(individual2);

            var individual3 = individualFactory.Create("Matthias", "Müller", new DateTime(1986, 12, 29), new List<Address>());
            await _sut.SaveAsync(individual3);

            // Act
            var actualIndividuals = await _sut.LoadAllAsync();

            // Assert
            Assert.IsNotNull(actualIndividuals);
            Assert.AreEqual(3, actualIndividuals.Count);
        }

        [Test]
        public async Task LoadingById_LoadsIndividual()
        {
            // Arrange
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            individual = await _sut.SaveAsync(individual);

            // Act
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            // Assert
            Assert.IsNotNull(actualIndividual);
            Assert.AreEqual(individual.Id, actualIndividual.Id);
        }

        [Test]
        public async Task UpdatingAddress_UpdatesAddress()
        {
            // Arrange
            const string NewCityName = "New City";
            const int NewZip = 789;
            var individual = ServiceLocator.GetService<TestIndividualBuilder>().BuildFullIndividual();
            individual = await _sut.SaveAsync(individual);

            // Act
            var addressToUpdate = individual.SearchAddress(f => f.City == "Alterswil").Reduce((Address)null);
            addressToUpdate.City = NewCityName;
            addressToUpdate.Zip = NewZip;

            individual.UpdateAddress(addressToUpdate);
            await _sut.SaveAsync(individual);

            // Assert
            var actualIndividual = await _sut.LoadByIdAsync(individual.Id);

            Assert.AreEqual(2, actualIndividual.Addresses.Count);

            var actualUpdatedAddress = actualIndividual.Addresses.FirstOrDefault(f => f.City == NewCityName);
            var actualOldAddress = actualIndividual.Addresses.FirstOrDefault(f => f.City == "Alterswil");

            Assert.IsNull(actualOldAddress);

            AssertAddress(addressToUpdate, actualUpdatedAddress);
            CollectionAssert.AreEqual(addressToUpdate.Streets, actualUpdatedAddress.Streets);
        }

        protected override TestingContainerConfiguration CreateContainerConfiguration()
        {
            return new TestingContainerConfiguration(initializeAutoMapper: true);
        }

        protected override void OnAligned()
        {
            _sut = ServiceLocator.GetService<IIndividualRepository>();
        }

        private static void AssertAddress(Address expected, Address actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(0, actual.Id);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.Streets.Count, actual.Streets.Count);
        }
    }
}