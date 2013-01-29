namespace Proligence.Orchard.Testing
{
    using System.Linq;
    using NUnit.Framework;
    using global::Orchard.ContentManagement.Handlers;
    using global::Orchard.ContentManagement.Records;

    public abstract class ContentHandlerTestFixture<THandler>
        where THandler : ContentHandler
    {
        public THandler Handler { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Handler = CreateHandler();
        }

        public void InvokeActivating(ActivatingContentContext context)
        {
            ((IContentHandler)Handler).Activating(context);
        }

        public void InvokeActivated(ActivatedContentContext context)
        {
            ((IContentHandler)Handler).Activated(context);
        }

        public void InvokeInitializing(InitializingContentContext context)
        {
            ((IContentHandler)Handler).Initializing(context);
        }

        public void InvokeCreating(CreateContentContext context)
        {
            ((IContentHandler)Handler).Creating(context);
        }

        public void InvokeCreated(CreateContentContext context)
        {
            ((IContentHandler)Handler).Created(context);
        }

        public void InvokeLoading(LoadContentContext context)
        {
            ((IContentHandler)Handler).Loading(context);
        }

        public void InvokeLoaded(LoadContentContext context)
        {
            ((IContentHandler)Handler).Loaded(context);
        }

        public void InvokeUpdating(UpdateContentContext context)
        {
            ((IContentHandler)Handler).Updating(context);
        }

        public void InvokeUpdated(UpdateContentContext context)
        {
            ((IContentHandler)Handler).Updated(context);
        }

        public void InvokeVersioning(VersionContentContext context)
        {
            ((IContentHandler)Handler).Versioning(context);
        }

        public void InvokeVersioned(VersionContentContext context)
        {
            ((IContentHandler)Handler).Versioned(context);
        }

        public void InvokePublishing(PublishContentContext context)
        {
            ((IContentHandler)Handler).Publishing(context);
        }

        public void InvokePublished(PublishContentContext context)
        {
            ((IContentHandler)Handler).Published(context);
        }

        public void InvokeUnpublishing(PublishContentContext context)
        {
            ((IContentHandler)Handler).Unpublishing(context);
        }

        public void InvokeUnpublished(PublishContentContext context)
        {
            ((IContentHandler)Handler).Unpublished(context);
        }

        public void InvokeRemoving(RemoveContentContext context)
        {
            ((IContentHandler)Handler).Removing(context);
        }

        public void InvokeRemoved(RemoveContentContext context)
        {
            ((IContentHandler)Handler).Removed(context);
        }

        public void InvokeIndexing(IndexContentContext context)
        {
            ((IContentHandler)Handler).Indexing(context);
        }

        public void InvokeIndexed(IndexContentContext context)
        {
            ((IContentHandler)Handler).Indexed(context);
        }

        public void InvokeImporting(ImportContentContext context)
        {
            ((IContentHandler)Handler).Importing(context);
        }

        public void InvokeImported(ImportContentContext context)
        {
            ((IContentHandler)Handler).Imported(context);
        }

        public void InvokeExporting(ExportContentContext context)
        {
            ((IContentHandler)Handler).Exporting(context);
        }

        public void InvokeExported(ExportContentContext context)
        {
            ((IContentHandler)Handler).Exported(context);
        }

        public void InvokeGetItemMetadata(GetContentItemMetadataContext context)
        {
            ((IContentHandler)Handler).GetContentItemMetadata(context);
        }

        public void InvokeBuildDisplay(BuildDisplayContext context)
        {
            ((IContentHandler)Handler).BuildDisplay(context);
        }

        public void InvokeBuildEditor(BuildEditorContext context)
        {
            ((IContentHandler)Handler).BuildEditor(context);
        }

        public void InvokeUpdateEditor(UpdateEditorContext context)
        {
            ((IContentHandler)Handler).UpdateEditor(context);
        }

        public void AssertHandlerHasStorageFilter<TRecord>()
            where TRecord : ContentPartRecord
        {
            Assert.That(Handler.Filters.Any(f =>
                f.GetType().Name == typeof(StorageVersionFilter<>).Name &&
                f.GetType().GetGenericArguments()[0].Name == typeof(TRecord).Name),
                "Expected StorageFilter for '" + typeof(TRecord).Name + "' but filter was not attached.");
        }

        public void AssertHandlerHasFilter<TFilter>()
            where TFilter : IContentFilter
        {
            Assert.That(
                Handler.Filters.Any(f => f.GetType().Name == typeof(TFilter).Name),
                "Expected filter '" + typeof(TFilter).Name + "' but filter was not attached.");
        }

        protected abstract THandler CreateHandler();
    }
}