namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Orchard.ContentManagement;
    using global::Orchard.ContentManagement.MetaData.Models;
    using global::Orchard.ContentManagement.Records;

    public static class ContentFactory
    {
        public static ContentItem CreateContentItem(int id, string contentType, SettingsDictionary settings, params ContentPart[] parts)
        {
            var item = new ContentItem
            {
                VersionRecord = new ContentItemVersionRecord
                {
                    ContentItemRecord = new ContentItemRecord { Id = id }
                }
            };

            IEnumerable<ContentTypePartDefinition> partDefinitions = parts.Select(p => new ContentTypePartDefinition(
                new ContentPartDefinition(p.TypePartDefinition != null && p.PartDefinition != null ? p.PartDefinition.Name : p.GetType().Name),
                new SettingsDictionary()))
                .ToArray();

            if (contentType != null)
            {
                item.VersionRecord.ContentItemRecord.ContentType = new ContentTypeRecord { Name = contentType };
                item.ContentType = contentType;
                item.TypeDefinition = new ContentTypeDefinition(contentType, contentType, partDefinitions, new SettingsDictionary());
            }

            if (settings != null)
            {
                var contentTypeDefinition = new ContentTypeDefinition(contentType, contentType, partDefinitions, settings);
                item.TypeDefinition = contentTypeDefinition;
            }

            foreach (ContentPart part in parts)
            {
                item.Weld(part);
            }

            return item;
        }

        public static ContentItem CreateContentItem(int id, string contentType, params ContentPart[] parts)
        {
            return CreateContentItem(id, contentType, null, parts);
        }

        public static ContentItem CreateContentItem(int id, params ContentPart[] parts)
        {
            return CreateContentItem(id, null, parts);
        }

        public static ContentItem CreateContentItem(params ContentPart[] parts)
        {
            return CreateContentItem(0, null, parts);
        }

        public static TPart CreateContentItem<TPart>(int id, string contentType, SettingsDictionary settings, params ContentPart[] parts)
            where TPart : IContent
        {
            return CreateContentItem(id, contentType, settings, parts).As<TPart>();
        }

        public static TPart CreateContentItem<TPart>(int id, string contentType, params ContentPart[] parts)
            where TPart : IContent
        {
            return CreateContentItem(id, contentType, parts).As<TPart>();
        }

        public static TPart CreateContentItem<TPart>(int id, params ContentPart[] parts)
            where TPart : IContent
        {
            return CreateContentItem(id, parts).As<TPart>();
        }

        public static TPart CreateContentItem<TPart>(params ContentPart[] parts)
            where TPart : IContent
        {
            return CreateContentItem(parts).As<TPart>();
        }
    }
}