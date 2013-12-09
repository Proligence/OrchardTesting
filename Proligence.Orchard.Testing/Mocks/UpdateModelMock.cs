namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using Moq;
    using global::Orchard.ContentManagement;
    using global::Orchard.Localization;

    public class UpdateModelMock : Mock<IUpdateModel>
    {
        public UpdateModelMock()
        {
        }

        public UpdateModelMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
        }

        public void SetupUpdate<TModel>(Action<TModel> callback) 
            where TModel : class
        {
            Setup(x => x.TryUpdateModel(
                It.IsAny<TModel>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>()))
                .Callback<TModel, string, string[], string[]>((p1, p2, p3, p4) => callback(p1))
                .Returns(true);
        }

        public void SetupUpdateFailed<TModel>()
            where TModel : class
        {
            Setup(x => x.TryUpdateModel(
                It.IsAny<TModel>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>()))
                .Returns(false);
        }

        public void SetupUpdateFailed<TModel>(string expectedErrorKey, LocalizedString expectedErrorMessage)
            where TModel : class
        {
            Setup(x => x.TryUpdateModel(
                It.IsAny<TModel>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>()))
                .Returns(false);

            Setup(x => x.AddModelError(expectedErrorKey, expectedErrorMessage));
        }

        public void ExpectModelError(string key, string message)
        {
            Setup(x => x.AddModelError(key, It.Is<LocalizedString>(s => s.Text.Equals(message))));
        }
    }
}