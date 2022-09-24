using Module6HW7.Exceptions;
using Module6HW7.Interfaces;
using Module6HW7.Models;
using Module6HW7.Services;
using Module6HW7.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Module6HW7.Tests
{
    public class TeapotServiceTests
    {
        [Fact]
        public async Task GetTeapots_IfDbIsEmpty_BusinessException()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.GetTeapots()).Returns(Task.FromResult(new List<Teapot>()));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */

            /* Assert */
            await Assert.ThrowsAsync<BusinessException>(async () => await teapotService.GetTeapots());
        }

        [Fact]
        public async Task GetTeapots_IfDbIsNotEmpty_Teapots()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.GetTeapots()).Returns(Task.FromResult(new List<Teapot>() { It.IsAny<Teapot>() }));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */
            var teapots = await teapotService.GetTeapots();

            /* Assert */
            Assert.NotEmpty(teapots);
        }

        [Fact]
        public async Task GetTeapotById_IfIsNull_BusinessException()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.GetTeapotById(It.IsAny<Guid>())).Returns(Task.FromResult<Teapot>(null));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */

            /* Assert */
            await Assert.ThrowsAsync<BusinessException>(async () => await teapotService.GetTeapotById(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetTeapotById_IfExists_Teapot()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.GetTeapotById(It.IsAny<Guid>())).Returns(Task.FromResult(new Teapot()));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */
            var recievedTeapot = await teapotService.GetTeapotById(It.IsAny<Guid>());

            /* Assert */
            Assert.NotNull(recievedTeapot);
        }

        [Fact]
        public async Task AddTeapot_IfAdded_True()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.AddTeapot(It.IsAny<Teapot>())).Returns(Task.FromResult(true));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */
            var isAdded = await teapotService.AddTeapot(new TeapotViewModel());

            /* Assert */
            Assert.True(isAdded);
        }

        [Fact]
        public async Task EditTeapotById_IfChanged_True()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();

            mockDataProvider
                .Setup(p => p.GetTeapotById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Teapot()));
            mockDataProvider
                .Setup(p => p.EditTeapot(It.IsAny<Teapot>()))
                .Returns(Task.FromResult(true));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */
            var isChanged = await teapotService.EditTeapotById(It.IsAny<Guid>(), new TeapotViewModel());

            /* Assert */
            Assert.True(isChanged);
        }

        [Fact]
        public async Task EditTeapotById_IfNotFound_BusinessException()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();

            mockDataProvider
                .Setup(p => p.GetTeapotById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Teapot>(null));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */

            /* Assert */
            await Assert.ThrowsAsync<BusinessException>(async () => await teapotService.EditTeapotById(It.IsAny<Guid>(), It.IsAny<TeapotViewModel>()));
        }

        [Fact]
        public async Task DeleteTeapotById_IfDeleted_True()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();

            mockDataProvider
                .Setup(p => p.GetTeapotById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Teapot()));
            mockDataProvider
                .Setup(p => p.DeleteTeapot(It.IsAny<Teapot>()))
                .Returns(Task.FromResult(true));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */
            var isDeleted = await teapotService.DeleteTeapotById(It.IsAny<Guid>());

            /* Assert */
            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteTeapotById_IfNotFound_BusinessException()
        {
            /* Arrange */
            var mockDataProvider = new Mock<IDataProvider>();

            mockDataProvider
                .Setup(p => p.GetTeapotById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Teapot>(null));

            var teapotService = new TeapotService(mockDataProvider.Object);
            /* Act */

            /* Assert */
            await Assert.ThrowsAsync<BusinessException>(async () => await teapotService.DeleteTeapotById(It.IsAny<Guid>()));
        }
    }
}
