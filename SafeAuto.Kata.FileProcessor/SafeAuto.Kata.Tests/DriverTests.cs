using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services;
using System;
using System.Linq;
using Xunit;

namespace SafeAuto.Kata.Tests
{
    public class DriverTests
    {
        private Mock<IDriverRepository> DriverRepository { get; set; } = new Mock<IDriverRepository>();
        private Fixture Fixture { get; set; } = new Fixture();

        [Fact]
        public void RegisterNewDriver()
        {
            var driverService = new DriverService(DriverRepository.Object, new Mock<ILogger<DriverService>>().Object);

            // arrange
            var driverName = "derian";
            string[] drivers = { $"Driver {driverName}" };

            var DriverObject = Fixture.Build<Driver>()
                                       .Create();
            DriverObject.Name = driverName;

            DriverRepository.Setup(x => x.RegisterNewDriver(It.IsAny<Driver>())).Returns(DriverObject);

            // act
            var driver = driverService.RegisterDrivers(drivers).FirstOrDefault();

            // assert
            driver.Should().NotBeNull();
            driver.Name.Should().Equals(driverName);
        }

        [Fact]
        public void AddDriverTripDetailsForRegisteredDriver()
        {
            // arrange
            var driverName = "derian";
            string[] drivers = { $"Driver {driverName}" };
            string[] tripDetails = { $"Trip {driverName} 15:05 15:16 38.0" };

            var DriverObject = Fixture.Build<Driver>()
                                      .With(d => d.Name, driverName)
                                      .Create();

            var TripObject = Fixture.Build<Trip>()
                                    .With(t => t.DriverName, driverName)
                                    .Create();

            // add trip object to driver
            DriverObject.TripList.Add(TripObject);

            DriverRepository.Setup(x => x.RegisterNewDriver(It.IsAny<Driver>())).Returns(DriverObject);
            DriverRepository.Setup(x => x.GetRegisteredDriver(It.IsAny<string>())).Returns(DriverObject);
            DriverRepository.Setup(x => x.AddDriverDetails(It.IsAny<Trip>()));
            
            var driverService = new DriverService(DriverRepository.Object, new Mock<ILogger<DriverService>>().Object);
            
            // act
            var driver = driverService.RegisterDrivers(drivers).FirstOrDefault();
            driverService.AddDriverTripDetails(tripDetails);

            // assert
            driver.Should().NotBeNull();
            // verify driver name
            driver.Name.Should().Equals(driverName);

            // make sure driver has a trip attached
            driver.TripList.Count.Should().Equals(1);

            // verify name associated w/trip is same as driver's name
            driver.TripList.FirstOrDefault().DriverName.Should().Equals(driverName);
        }

        [Fact]
        public void AddDriverTripDetailsForUnRegisteredDriver()
        {
            // arrange
            var driverName = "derian";
            var unregisteredDriverName = "thanh";
            string[] drivers = { $"Driver {driverName}" };
            string[] tripDetails = { $"Trip {unregisteredDriverName} 15:05 15:16 38.0" };

            var DriverObject = Fixture.Build<Driver>()
                                      .With(d => d.Name, driverName)
                                      .Create();

            var TripObject = Fixture.Build<Trip>()
                                    .With(t => t.DriverName, driverName)
                                    .Create();

            // add trip object to driver
            DriverObject.TripList.Add(TripObject);

            DriverRepository.Setup(x => x.RegisterNewDriver(It.IsAny<Driver>())).Returns(DriverObject);
            DriverRepository.Setup(x => x.AddDriverDetails(It.IsAny<Trip>()));

            var driverService = new DriverService(DriverRepository.Object, new Mock<ILogger<DriverService>>().Object);

            // act
            var driver = driverService.RegisterDrivers(drivers).FirstOrDefault();
            Exception ex = Assert.Throws<ArgumentException>(() => driverService.AddDriverTripDetails(tripDetails));

            // assert

            Assert.Equal($"Cannot add trip details for a non-existant user: {unregisteredDriverName}", ex.Message);
        }
    }
}
