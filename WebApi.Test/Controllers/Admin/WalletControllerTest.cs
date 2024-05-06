using Application.DTOs.Funds;
using Application.Wrappers;
using MediatR;
using Moq;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApi.Controllers.Admin;

namespace WebApi.Test.Controllers.Admin
{
    public class WalletControllerTest
    {
        [Fact]
        public async Task AddBalanceTestWithOriginalUser()
        {
            AccountDetails request = new AccountDetails()
            {
                Amount = 100,
                UserId = "00525d46-2704-490b-a9be-ca3513363631",
                WalletAccount = "03333004258"
            };

            //Arrange
            var controller = new WalletController();
            
            //Act
            Response<string> response = (Response<string>)await controller.Balance(request);

            string expectedResult = "Balance added successfully.";

            //Assert
            Assert.Equal(response.Data, expectedResult);
        }

        [Fact]
        public async Task AddBalanceTestWithNonExistingUser()
        {
            AccountDetails request = new AccountDetails()
            {
                Amount = 100,
                UserId = "00525d73-2704-490b-a9be-ca3513363631",
                WalletAccount = "03333004258"
            };


            //Arrange
            var controller = new WalletController();

            //Act
            Response<string> response = (Response<string>)await controller.Balance(request);

            string expectedResult = "no user found.";
            //Assert
            Assert.Equal(response.Data, expectedResult);
        }
    }
}
