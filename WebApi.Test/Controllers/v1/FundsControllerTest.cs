using Application.DTOs.Funds;
using Application.Exceptions;
using Application.Wrappers;
using WebApi.Controllers.Admin;
using WebApi.Controllers.v1;

namespace WebApi.Test.Controllers.v1
{
    public class FundsControllerTest
    {
        [Fact]
        public async Task TransferFundsTestWithOriginalUser()
        {
            TransferFundRequest request = new TransferFundRequest()
            {
                Amount = 1000,
                SenderId = "00525d46-2704-490b-a9be-ca3513363631",
                ReceiverId = "81097c68-0776-4f8b-a365-d2b3afe13466",
            };

            //Arrange
            var controller = new FundsController();

            //Act
            Response<string> response = (Response<string>)await controller.TransferFunds(request);

            string expectedResult = "Funds transfered successfully.";

            //Assert
            Assert.Equal(response.Data, expectedResult);
        }

        public async Task TransferFundsTestWithInvalidSender()
        {
            TransferFundRequest request = new TransferFundRequest()
            {
                Amount = 1000000,
                SenderId = "00525d46-2704-490b-a9be-ca3513363632",
                ReceiverId = "81097c68-0776-4f8b-a365-d2b3afe13466",
            };

            //Arrange
            var controller = new FundsController();

            //Act
            ApiException response = (ApiException)await controller.TransferFunds(request);

            string expectedResult = "Invalid sender.";

            //Assert
            Assert.Equal(response.Message, expectedResult);
        }

        public async Task TransferFundsTestWithNonExistingUser()
        {
            TransferFundRequest request = new TransferFundRequest()
            {
                Amount = 1000,
                SenderId = "00525d46-2704-490b-a9be-ca3513363631",
                ReceiverId = "81097c68-0776-4f8b-a365-d2b3afe15644",
            };

            //Arrange
            var controller = new FundsController();

            //Act
            ApiException response = (ApiException)await controller.TransferFunds(request);

            string expectedResult = "Receiver not found.";

            //Assert
            Assert.Equal(response.Message, expectedResult);
        }

        public async Task TransferFundsTestWithInsufficientFunds()
        {
            TransferFundRequest request = new TransferFundRequest()
            {
                Amount = 1000000,
                SenderId = "00525d46-2704-490b-a9be-ca3513363631",
                ReceiverId = "81097c68-0776-4f8b-a365-d2b3afe13466",
            };

            //Arrange
            var controller = new FundsController();

            //Act
            ApiException response = (ApiException)await controller.TransferFunds(request);

            string expectedResult = "Insufficient funds.";

            //Assert
            Assert.Equal(response.Message, expectedResult);
        }
    }
}
