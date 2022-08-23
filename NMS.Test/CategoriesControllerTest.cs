using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NMS.Controllers;
using NMS.Core.IRepositories;
using NMS.Core.Repositories;
using NMS.Data;
using NMS.Models;

namespace NMS.Test
{
    public class CategoriesControllerTest 
    {

        [Fact]
        public async Task TestGetAllCategories()
        {
            //Arrange
            var categoryRepoMock = new Mock<ICategoryRepository>();
            categoryRepoMock
                .Setup(m=>m.GetAllCategories())
                .ReturnsAsync(GetCategories);

            var controller = new CategoriesController(categoryRepoMock.Object, null);
            //Act
            var result = await controller.GetAllCategories();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }


        private List<Category> GetCategories()
        {
            var session = new List<Category>();
            session.Add(new Category()
            {
                Title = "TestCategory"
            });

            return session;
        }
    }
}