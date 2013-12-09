namespace Proligence.Orchard.Testing.Mocks
{
    using Moq;
    using global::Orchard.ContentManagement;
    using global::Orchard.Localization;
    using global::Orchard.Security;
    using global::Orchard.Security.Permissions;

    public class AuthorizerMock : Mock<IAuthorizer>
    {
        public AuthorizerMock()
        {
        }

        public AuthorizerMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
        }

        public void Allow()
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), It.IsAny<LocalizedString>())).Returns(true);
        }

        public void Allow(IContent content)
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), content, It.IsAny<LocalizedString>())).Returns(true);
        }

        public void Allow(Permission permission)
        {
            Setup(x => x.Authorize(permission, It.IsAny<LocalizedString>())).Returns(true);
        }

        public void Allow(Permission permission, IContent content)
        {
            Setup(x => x.Authorize(permission, content, It.IsAny<LocalizedString>())).Returns(true);
        }

        public void AllowWithoutMessage()
        {
            Setup(x => x.Authorize(It.IsAny<Permission>())).Returns(true);
        }

        public void AllowWithoutMessage(IContent content)
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), content)).Returns(true);
        }

        public void AllowWithoutMessage(Permission permission)
        {
            Setup(x => x.Authorize(permission)).Returns(true);
        }

        public void AllowWithoutMessage(Permission permission, IContent content)
        {
            Setup(x => x.Authorize(permission, content)).Returns(true);
        }

        public void Deny()
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), It.IsAny<LocalizedString>())).Returns(false);
        }

        public void Deny(IContent content)
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), content, It.IsAny<LocalizedString>())).Returns(false);
        }

        public void Deny(Permission permission)
        {
            Setup(x => x.Authorize(permission, It.IsAny<LocalizedString>())).Returns(false);
        }

        public void Deny(Permission permission, IContent content)
        {
            Setup(x => x.Authorize(permission, content, It.IsAny<LocalizedString>())).Returns(false);
        }

        public void DenyWithoutMessage()
        {
            Setup(x => x.Authorize(It.IsAny<Permission>())).Returns(false);
        }

        public void DenyWithoutMessage(IContent content)
        {
            Setup(x => x.Authorize(It.IsAny<Permission>(), content)).Returns(false);
        }

        public void DenyWithoutMessage(Permission permission)
        {
            Setup(x => x.Authorize(permission)).Returns(false);
        }

        public void DenyWithoutMessage(Permission permission, IContent content)
        {
            Setup(x => x.Authorize(permission, content)).Returns(false);
        }
    }
}