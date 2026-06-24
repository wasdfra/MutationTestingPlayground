using Cmf.Foundation.BaseTestsUtils;
using Cmf.Foundation.Common.Abstractions;
using Cmf.Navigo.BusinessObjects.Abstractions;
using Moq;
using MutationTestPlayground;

namespace MutationTestPlayground.TestProject
{
    public class CustomGetLotDetailsIntegrationHandlerTests : ActionBaseTests
    {
        private readonly CustomGetLotDetailsIntegrationHandler CustomGetLotDetailsIntegrationHandler;
        private readonly Mock<IAction> ActionMock;
        private readonly Mock<IMaterial> MaterialMock;

        private Dictionary<string, object> ResultDictionary;

        private const string Lot = "Lot";
        private const string Result = "Result";
        private const string MaterialName = "MaterialName";

        public CustomGetLotDetailsIntegrationHandlerTests()
        {
            CustomGetLotDetailsIntegrationHandler = new();
            ActionMock = new();

            MaterialMock = new();
            MaterialMock
                .Setup(mm => mm.ObjectExists())
                .Returns(true);
            MaterialMock
                .SetupGet(mm => mm.Form)
                .Returns(Lot);
            MaterialMock
                .Setup(m => m.Name)
                .Returns("Mock Name");
            AddMockToActionInput(MaterialMock);

            ResultDictionary = new Dictionary<string, object>
            {
                { Result, true }
            };
        }

        [Fact]
        public void DeeActionCode_WhenExecutingHappyPath_ShouldNotThrowException()
        {
            // Arrange
            ActionMock
                .Setup(am => am.ExecuteAction(It.IsAny<Dictionary<string, object>>()))
                .Returns(ResultDictionary);
            AddMockToActionInput(ActionMock);

            ActionInput.Add(MaterialName, MaterialMock.Object);

            // Act
            CustomGetLotDetailsIntegrationHandler.DeeActionCode(ActionInput);

            // Assert
            ActionMock.Verify(action => action.ExecuteAction(It.IsAny<Dictionary<string, object>>()), Times.Exactly(1));
        }

        [Fact]
        public void DeeActionCode_WhenMaterialDoesNotExist_ShouldNotExecuteAction()
        {
            // Arrange
            MaterialMock
                .Setup(mm => mm.ObjectExists())
                .Returns(false);
            ActionInput.Add(MaterialName, MaterialMock.Object);

            // Act
            CustomGetLotDetailsIntegrationHandler.DeeActionCode(ActionInput);

            // Assert
            ActionMock.Verify(action => action.ExecuteAction(It.IsAny<Dictionary<string, object>>()), Times.Exactly(0));
        }

        [Fact]
        public void DeeTestCondition_WhenExecutingHappyPath_ShouldNotThrowException()
        {
            // Arrange
            ActionInput.Add(MaterialName, MaterialMock.Object);

            // Act & Assert
            CustomGetLotDetailsIntegrationHandler.DeeTestCondition(ActionInput);
        }
    }
}