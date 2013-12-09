namespace Proligence.Orchard.Testing.Mocks
{
    using global::Orchard.ContentManagement.MetaData.Builders;
    using global::Orchard.ContentManagement.MetaData.Models;

    public class ContentTypePartDefinitionBuilderMock : ContentTypePartDefinitionBuilder
    {
        public ContentTypePartDefinitionBuilderMock(ContentTypePartDefinition part)
            : base(part)
        {
        }

        public SettingsDictionary Settings
        {
            get { return _settings; }
        }
    }
}