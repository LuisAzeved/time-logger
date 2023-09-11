using System.Collections.Generic;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Timelogger.App.Features;
using Timelogger.DTOs;
using Timelogger.Entities;
using Timelogger.Enums;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Tests
{
    
    [TestFixture]
    public class GetProjectsHandlerTests
    {
        private Mock<IProjectRepository> _mockProjectRepository;
        private Mock<IMapper> _mockMapper;
        private GetProjectsHandler _handler;

        private List<Project> mockProjectList = new List<Project>();
        private List<ProjectDTO> mockProjectDTOList = new List<ProjectDTO>();

        [SetUp]
        public void SetUp()
        {
            _mockProjectRepository = new Mock<IProjectRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetProjectsHandler(_mockProjectRepository.Object, _mockMapper.Object);
            var project1 = new Project
            {
                Id = 1,
                Name = "aaa"
            };
            
            var project2 = new Project
            {
                Id = 2,
                Name = "bbb"
            };
            
            var projectDTO1 = new ProjectDTO
            {
                Id = 1,
                Name = "aaa"
            };
            
            var projectDTO2 = new ProjectDTO
            {
                Id = 2,
                Name = "bbb"
            };
            
            mockProjectList.Add(project1);
            mockProjectList.Add(project2);
            mockProjectDTOList.Add(projectDTO1);
            mockProjectDTOList.Add(projectDTO2);
        }

        [Test]
        public void Handle_ReturnsMappedProjects()
        {
            // Arrange
            _mockProjectRepository.Setup(repo => repo.GetAll(It.IsAny<SortCriteria>(), It.IsAny<string>()))
                .Returns(mockProjectList);
        
            _mockMapper.Setup(m => m.Map<List<ProjectDTO>>(mockProjectList)).Returns(mockProjectDTOList);

            // Act
            var result = _handler.Handle(SortCriteria.ASC, "name");

            // Assert
            Assert.AreEqual(mockProjectDTOList.Count, result.Count);
        }
        
        [Test]
        public void Handle_CallsRepositoryWithCorrectSortCriteriaAndFieldName()
        {
            // Arrange
            var sortCriteria = SortCriteria.ASC;
            var sortFieldName = "name";

            _mockProjectRepository.Setup(repo => repo.GetAll(sortCriteria, It.IsAny<string>()))
                .Returns(mockProjectList);
        
            _mockMapper.Setup(m => m.Map<List<ProjectDTO>>(mockProjectList)).Returns(mockProjectDTOList);

            // Act
            _handler.Handle(sortCriteria, sortFieldName);

            // Assert
            _mockProjectRepository.Verify(repo => repo.GetAll(sortCriteria, "Name"), Times.Once());
        }

        [Test]
        public void Handle_HandlesEmptySortFieldName()
        {

            // Arrange
            _mockProjectRepository.Setup(repo => repo.GetAll(It.IsAny<SortCriteria>(), It.IsAny<string>()))
                .Returns(mockProjectList);
        
            _mockMapper.Setup(m => m.Map<List<ProjectDTO>>(mockProjectList)).Returns(mockProjectDTOList);

            // Act
            var result = _handler.Handle(SortCriteria.NONE, null);

            // Assert
            _mockProjectRepository.Verify(repo => repo.GetAll(SortCriteria.NONE, null), Times.Once());
            Assert.AreEqual(mockProjectDTOList.Count, result.Count);
        }
    }

}