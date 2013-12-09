namespace Proligence.Orchard.Testing.Mocks
{
    using System.Linq;
    using Moq;
    using global::Orchard.ContentManagement.MetaData;
    using global::Orchard.ContentManagement.MetaData.Models;

    public class ContentDefinitionManagerMock : Mock<IContentDefinitionManager>
    {
        public ContentDefinitionManagerMock()
        {
        }

        public ContentDefinitionManagerMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
        }

        public void ExpectGetTypeDefinition(string name, ContentTypeDefinition definition)
        {
            Setup(x => x.GetTypeDefinition(name)).Returns(definition);
        }

        public void ExpectGetTypeDefinition(string name, params string[] partNames)
        {
            ContentTypeDefinition definition = CreateTypeDefinition(name, partNames);
            Setup(x => x.GetTypeDefinition(name)).Returns(definition);
        }

        public void ExpectGetPartDefinition(string name, ContentPartDefinition definition)
        {
            Setup(x => x.GetPartDefinition(name)).Returns(definition);
        }

        public ContentTypeDefinition CreateTypeDefinition(string name, params string[] partNames)
        {
            var parts = partNames.Select(partName =>
                new ContentTypePartDefinition(
                    new ContentPartDefinition(partName),
                    new SettingsDictionary()));

            return new ContentTypeDefinition(name, name, parts, new SettingsDictionary());
        }
    }
}