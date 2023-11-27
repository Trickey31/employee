using aspnetcore.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    [TestFixture]
    public class EmployeeValidateTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public EmployeeValidate EmployeeValidate { get; set; }


        [SetUp]
        public void SetUp()
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeValidate = Substitute.For<EmployeeValidate>(EmployeeRepository);
        }

        /// <summary>
        /// UnitTests CheckEmployeeExistAsync Success
        /// </summary>
        /// <returns></returns>
        /// Created by: VTThanh (25/09/2023)
        [Test]
        public async Task CheckEmployeeExistAsync_NotExistEmployee_Success()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = "NV-00001"
            };
            EmployeeRepository.IsExitstEmployeeCodeAsync(employee.EmployeeCode).Returns(false);

            //Act
            await EmployeeValidate.CheckEmployeeCodeAsync(employee);

            //Assert
            await EmployeeRepository.Received(1).IsExitstEmployeeCodeAsync(employee.EmployeeCode);
        }

        /// <summary>
        /// UnitTests CheckEmployeeExistAsync ThrowException
        /// </summary>
        /// <returns></returns>
        /// Created by: VTThanh (25/09/2023)
        [Test]
        public async Task CheckEmployeeExistAsync_ExistEmployee_ThrowException()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = "NV-00002"
            };
            EmployeeRepository.IsExitstEmployeeCodeAsync(employee.EmployeeCode).Returns(true);

            //Act and Assert

            Assert.ThrowsAsync<ConflictException>(async () => await EmployeeValidate.CheckEmployeeCodeAsync(employee));

            await EmployeeRepository.Received(1).IsExitstEmployeeCodeAsync(employee.EmployeeCode);
        }
    }
}
