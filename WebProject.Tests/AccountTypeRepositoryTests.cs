// using Microsoft.Extensions.Configuration;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using WebProject.Models;

// namespace WebProject.Tests
// {
//     public class AccountTypeRepositoryTests
//     {
//         private readonly Mock<IConfiguration> _configMock;
//         private readonly Mock<AccountTypeRepository> _repositoryMock;

//         public AccountTypeRepositoryTests()
//         {
//             _configMock = new Mock<IConfiguration>();
//             _repositoryMock = new Mock<AccountTypeRepository>(_configMock.Object) { CallBase = true };
//         }

//         [Fact]
//         public async Task AccountTypeBelongToUser_AllIdsExist_ReturnsTrue()
//         {
//             // Arrange
//             int userId = 1;
//             int[] ids = new int[] { 1, 2, 3 };
//             var accountTypes = new List<AccountType>
//             {
//                 new AccountType { Id = 1, UserId = userId },
//                 new AccountType { Id = 2, UserId = userId },
//                 new AccountType { Id = 3, UserId = userId },
//                 new AccountType { Id = 4, UserId = userId }
//             };

//             _repositoryMock.Setup(r => r.GetAllByUserIdAsync(userId))
//                 .ReturnsAsync(accountTypes);

//             // Act
//             var result = await _repositoryMock.Object.AccountTypeBelongToUser(ids, userId);

//             // Assert
//             Assert.True(result);
//         }

//         [Fact]
//         public async Task AccountTypeBelongToUser_SomeIdsDoNotExist_ReturnsFalse()
//         {
//             // Arrange
//             int userId = 1;
//             int[] ids = new int[] { 1, 2, 5 }; // 5 doesn't exist
//             var accountTypes = new List<AccountType>
//             {
//                 new AccountType { Id = 1, UserId = userId },
//                 new AccountType { Id = 2, UserId = userId },
//                 new AccountType { Id = 3, UserId = userId }
//             };

//             _repositoryMock.Setup(r => r.GetAllByUserIdAsync(userId))
//                 .ReturnsAsync(accountTypes);

//             // Act
//             var result = await _repositoryMock.Object.AccountTypeBelongToUser(ids, userId);

//             // Assert
//             Assert.False(result);
//         }

//         [Fact]
//         public async Task AccountTypeBelongToUser_EmptyIdArray_ReturnsTrue()
//         {
//             // Arrange
//             int userId = 1;
//             int[] ids = new int[] { };
//             var accountTypes = new List<AccountType>
//             {
//                 new AccountType { Id = 1, UserId = userId },
//                 new AccountType { Id = 2, UserId = userId }
//             };

//             _repositoryMock.Setup(r => r.GetAllByUserIdAsync(userId))
//                 .ReturnsAsync(accountTypes);

//             // Act
//             var result = await _repositoryMock.Object.AccountTypeBelongToUser(ids, userId);

//             // Assert
//             Assert.True(result);
//         }

//         [Fact]
//         public async Task AccountTypeBelongToUser_NoAccountTypesForUser_ReturnsFalseWhenIdsProvided()
//         {
//             // Arrange
//             int userId = 1;
//             int[] ids = new int[] { 1, 2 };
//             var accountTypes = new List<AccountType>();

//             _repositoryMock.Setup(r => r.GetAllByUserIdAsync(userId))
//                 .ReturnsAsync(accountTypes);

//             // Act
//             var result = await _repositoryMock.Object.AccountTypeBelongToUser(ids, userId);

//             // Assert
//             Assert.False(result);
//         }
//     }
// }