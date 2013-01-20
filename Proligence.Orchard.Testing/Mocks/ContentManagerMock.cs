namespace Proligence.Orchard.Testing.Mocks
{
    using Moq;
    using global::Orchard.ContentManagement;

    public class ContentManagerMock : Mock<IContentManager>
    {
        public void ExpectGetItem(int id, ContentItem contentItem)
        {
            Setup(x => x.Get(id)).Returns(contentItem);
        }

        public void ExpectGetItemAnyVersion(int id, ContentItem contentItem)
        {
            Setup(x => x.Get(id, It.IsAny<VersionOptions>())).Returns(contentItem);
        }

        public void ExpectGetItemAnyVersionAnyHints(int id, ContentItem contentItem)
        {
            Setup(x => x.Get(id, It.IsAny<VersionOptions>(), It.IsAny<QueryHints>())).Returns(contentItem);
        }

        public void ExpectNewItem(string contentType, ContentItem result)
        {
            Setup(x => x.New(contentType)).Returns(result);
        }

        public void ExpectCreateItem(ContentItem contentItem)
        {
            Setup(x => x.Create(contentItem));
        }

        public void ExpectCreateItemDraft(ContentItem contentItem)
        {
            Setup(x => x.Create(contentItem, It.Is<VersionOptions>(o => o.IsDraft)));
        }

        public void ExpectPublishItem(ContentItem contentItem)
        {
            Setup(x => x.Publish(contentItem));
        }

        public void ExpectUnpublishItem(ContentItem contentItem)
        {
            Setup(x => x.Unpublish(contentItem));
        }

        public void ExpectRemoveItem(ContentItem contentItem)
        {
            Setup(x => x.Remove(contentItem));
        }

        public void ExpectFlush()
        {
            Setup(x => x.Flush());
        }

        public void ExpectClear()
        {
            Setup(x => x.Clear());
        }
    }
}