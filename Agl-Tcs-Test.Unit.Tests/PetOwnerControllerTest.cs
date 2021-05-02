using System;
using System.Collections.Generic;
using Agl_Tcs_Test.Controllers;
using Agl_Tcs_Test.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Agl_Tcs_Test.Models;

namespace Agl_Tcs_Test.Unit.Tests
{
    public class PetOwnerControllerTest
    {
        private readonly Mock<IDataSource> mockService;
        public PetOwnerControllerTest()
        {
            mockService = new Mock<IDataSource>();
        }

        [Fact]
        public async Task TestEmptyDataSource()
        {
            //Arrange
            mockService.Setup(service => service.GetDataAsync()).ReturnsAsync(String.Empty);

            var controller = new PetOwnerController(mockService.Object);
            
            //Act
            var result = await controller.GetresultAsync();

            //Assert
            Assert.Equal("[]",result);
        }

        [Fact]
        public void TestGetDistinctValues()
        {
            //Arrange
            mockService.Setup(service => service.GetDataAsync()).ReturnsAsync(String.Empty);
            var controller = new PetOwnerController(mockService.Object);
            var owners = new List<Owner>
            {
                new Owner{Age = 1,Gender = "Male",Name = "Vivek", Pets=null},
                new Owner{Age = 1,Gender = "Female",Name = "Anna", Pets=null},
                new Owner{Age = 1,Gender = "Male",Name = "Vinay", Pets=null},
                new Owner{Age = 1,Gender = "Female",Name = "Anita", Pets=null}
            };
            var expectedResult = new List<string>{"Male","Female"};

            //Act
            var result = controller.GetDistinctGenders(owners);

            //Assert
            Assert.Equal(expectedResult,result);

        }

    }
}
