using aspnetcore.Application;
using aspnetcore.Domain;
using aspnetcore.Domain.Service;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Appication.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public EmployeeService EmployeeService { get; set; }
        public IEmployeeValidate EmployeeValidate { get; set; }


        [SetUp]
        public void SetUp()
        {
            EmployeeRepository = NSubstitute.Substitute.For<IEmployeeRepository>();

            EmployeeValidate = NSubstitute.Substitute.For<IEmployeeValidate>();

            EmployeeService = NSubstitute.Substitute.For<EmployeeService>(EmployeeRepository, EmployeeValidate);

        }


        /// <summary>
        /// Unit Tests hàm lấy toàn bộ employee
        /// </summary>
        /// <returns></returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task GetAllAsync_ValidInput_Success()
        {
            //Arrange
            var employeeDto = new EmployeeDto();
            var employee = new Employee();
            var employees = new List<Employee>();

            EmployeeRepository.GetAllAsync().Returns(employees);
            EmployeeService.MapEntityToDto(employee).Returns(employeeDto);

            //Act
            var employeeDtos = await EmployeeService.GetAllAsync();

            //Assert
            await EmployeeRepository.Received(1).GetAllAsync();
        }

        /// <summary>
        /// Unit Tests hàm lấy toàn bộ employee
        /// </summary>
        /// <returns>Success</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task GetAsync_ValidInput_Success()
        {
            //Arrange
            var employee = new Employee();
            var employeeDto = new EmployeeDto();

            EmployeeRepository.GetAsync(employee.EmployeeId).Returns(employee);
            EmployeeService.MapEntityToDto(employee).Returns(employeeDto);

            //Act
            var result = await EmployeeService.GetAsync(employee.EmployeeId);

            //Assert
            await EmployeeRepository.Received(1).GetAsync(employee.EmployeeId);
        }

        /// <summary>
        /// Unit Tests hàm employee code
        /// </summary>
        /// <returns>employee code</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task GetNewEmployeeCodeAsync_ValidInput_ReturnsEmployeeCode()
        {
            // Arrange
            var employeeCode = "NV-00001";
            EmployeeRepository.GetNewEmployeeCodeAsync().Returns(employeeCode);

            // Act
            var result = await EmployeeService.GetNewEmployeeCodeAsync();

            // Assert
            await EmployeeRepository.Received(1).GetNewEmployeeCodeAsync();
        }

        /// <summary>
        /// Unit Tests hàm thêm employee
        /// </summary>
        /// <returns>Id not empty</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task InsertAsync_EmptyEmployeeId_EmployeeIdNotEmpty()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto();

            var employee = new Employee()
            {
                EmployeeId = Guid.Empty,
            };

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);


            // Act
            var employeeDto = await EmployeeService.InsertAsync(employeeCreateDto);

            // Assert
            Assert.That(employee.EmployeeId, Is.Not.EqualTo(Guid.Empty));

            await EmployeeRepository.Received(1).InsertAsync(employee);

            await EmployeeService.Received(1).ValidateCreateBussiness(employee);
        }


        /// <summary>
        /// Unit Tests hàm thêm employee
        /// </summary>
        /// <returns>Audit not null</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task InsertAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            // Arrange
            var employeCreateDto = new EmployeeCreateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.Empty
            };
            EmployeeService.MapCreateDtoToEntity(employeCreateDto).Returns(employee);

            //Act
            var result = await EmployeeService.InsertAsync(employeCreateDto);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee.CreatedBy, Is.EqualTo("VTThanh"));
                Assert.That(employee.ModifiedBy, Is.EqualTo("VTThanh"));
            });
            await EmployeeRepository.Received(1).InsertAsync(employee);
            EmployeeService.Received(1).MapCreateDtoToEntity(employeCreateDto);
        }


        /// <summary>
        /// Unit Tests hàm thêm employee
        /// </summary>
        /// <returns>Success</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task InsertAsync_ValidInput_Success()
        {
            // Arrange
            var employeCreateDto = new EmployeeCreateDto();
            var employee = new Employee();
            EmployeeService.MapCreateDtoToEntity(employeCreateDto).Returns(employee);

            //Act
            var result = await EmployeeService.InsertAsync(employeCreateDto);

            //Assert
            await EmployeeRepository.Received(1).InsertAsync(employee);
            EmployeeService.Received(1).MapCreateDtoToEntity(employeCreateDto);
            await EmployeeService.Received(1).ValidateCreateBussiness(employee);
        }

        /// <summary>
        /// Unit Tests hàm cập nhật employee
        /// </summary>
        /// <returns>Audit not null</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task UpdateAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            //Arrange
            var employeeId = new Guid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee();
            var newEmployee = new Employee();

            EmployeeRepository.GetAsync(employeeId).Returns(employee);

            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(newEmployee);

            //Act
            await EmployeeService.UpdateAsync(employeeId, employeeUpdateDto);

            //Assert
            Assert.That(newEmployee.EmployeeId, Is.EqualTo(employeeId));
            Assert.That(newEmployee.ModifiedBy, Is.EqualTo("VTThanh"));
        }

        /// <summary>
        /// Unit Tests hàm cập nhật employee
        /// </summary>
        /// <returns>Success</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task UpdateAsync_ValidInput_Success()
        {
            //Arrange
            var employeeId = new Guid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee();
            var newEmployee = new Employee();

            EmployeeRepository.GetAsync(employeeId).Returns(employee);

            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(newEmployee);

            //Act
            await EmployeeService.UpdateAsync(employeeId, employeeUpdateDto);

            //Assert
            await EmployeeService.Received(1).ValidateUpdateBussiness(newEmployee);

            await EmployeeRepository.Received(1).UpdateAsync(newEmployee);
        }

        /// <summary>
        /// Unit Tests hàm 1 employee
        /// </summary>
        /// <returns>Success</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task DeleteAsync_ValidInput_Success()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var entity = new Employee();

            EmployeeRepository.GetAsync(employeeId).Returns(entity);
            EmployeeRepository.DeleteAsync(entity).Returns(1);

            // Act
            var result = await EmployeeService.DeleteAsync(employeeId);

            // Assert
            await EmployeeRepository.Received(1).GetAsync(employeeId);
            await EmployeeRepository.Received(1).DeleteAsync(entity);
        }

        /// <summary>
        /// Unit Tests hàm xóa nhiều employee
        /// </summary>
        /// <returns>Success</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task DeleteManyAsync_ValidInput_Success()
        {
            // Arrange
            var ids = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            var entities = new List<Employee>();
            entities.Add(new Employee { EmployeeId = ids[0] });
            entities.Add(new Employee { EmployeeId = ids[1] });

            var notExistIds = new List<Guid>();

            EmployeeRepository.GetListIdsAsync(ids).Returns((entities, notExistIds));
            EmployeeRepository.DeleteManyAsync(entities).Returns(2);

            // Act
            var result = await EmployeeService.DeleteManyAsync(ids);

            // Assert
            await EmployeeRepository.Received(1).GetListIdsAsync(ids);
            await EmployeeRepository.Received(1).DeleteManyAsync(entities);
        }


        /// <summary>
        /// Unit Tests hàm xóa nhiều employee
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Exception</returns>
        /// Created by: VTThanh (25/9/2023)
        [Test]
        public async Task DeleteManyAsync_NotExistIds_ThrowsNotFoundException()
        {
        }
    }
}
