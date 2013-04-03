namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using Moq;
    using global::Orchard.ContentManagement;
    using global::Orchard.ContentManagement.Records;

    public class ContentManagerMock : Mock<IContentManager>
    {
        private Mock<IContentQuery<ContentItem>> _contentQuery;
        private readonly Dictionary<Type, object> _partQueries;
        private readonly Dictionary<Tuple<Type, Type>, object> _recordQueries;

        public Mock<IContentQuery<ContentItem>> ContentQueryMock
        {
            get
            {
                if (_contentQuery == null)
                {
                    _contentQuery = new Mock<IContentQuery<ContentItem>>(MockBehavior.Strict);
                }

                return _contentQuery;
            }
        }

        public ContentManagerMock()
        {
            _partQueries = new Dictionary<Type, object>();
            _recordQueries = new Dictionary<Tuple<Type, Type>, object>();
        }

        public ContentManagerMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
            _partQueries = new Dictionary<Type, object>();
            _recordQueries = new Dictionary<Tuple<Type, Type>, object>();
        }

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

        public void ExpectClear()
        {
            Setup(x => x.Clear()).Verifiable("ContentManager was not cleared");
        }

        public void VerifyClear()
        {
            Verify(x => x.Clear());
        }

        public void MockContentItem<TPart, TRecord>(IContent item, params string[] contentTypeNames)
            where TPart : ContentPart
            where TRecord : ContentPartRecord
        {
            MockContentItems<TPart, TRecord>(new[] { item }, contentTypeNames);
        }

        public void MockContentItems<TPart, TRecord>(IEnumerable<IContent> items, params string[] contentTypeNames)
            where TPart : ContentPart
            where TRecord : ContentPartRecord
        {
            if (_contentQuery == null)
            {
                _contentQuery = new Mock<IContentQuery<ContentItem>>(MockBehavior.Strict);
                Setup(x => x.Query()).Returns(_contentQuery.Object);
            }

            Mock<IContentQuery<TPart>> partQuery;
            if (!_partQueries.ContainsKey(typeof(TPart)))
            {
                partQuery = new Mock<IContentQuery<TPart>>(MockBehavior.Strict);
                _contentQuery.Setup(x => x.ForPart<TPart>()).Returns(partQuery.Object);
                _partQueries.Add(typeof(TPart), partQuery);
            }
            else
            {
                partQuery = (Mock<IContentQuery<TPart>>)_partQueries[typeof(TPart)];
            }

            Mock<IContentQuery<TPart, TRecord>> recordQuery;
            var recordQueryKey = new Tuple<Type, Type>(typeof(TPart), typeof(TRecord));
            if (!_recordQueries.ContainsKey(recordQueryKey))
            {
                recordQuery = new Mock<IContentQuery<TPart, TRecord>>(MockBehavior.Strict);
                _recordQueries.Add(recordQueryKey, recordQuery);
            }
            else
            {
                recordQuery = (Mock<IContentQuery<TPart, TRecord>>)_recordQueries[recordQueryKey];
            }

            if (contentTypeNames != null)
            {
                var newPartQuery = new Mock<IContentQuery<TPart>>(MockBehavior.Strict);
                partQuery.Setup(x => x.ForType(ItIsEquivalentArray(contentTypeNames))).Returns(newPartQuery.Object);
                partQuery = newPartQuery;
            }

            partQuery.Setup(x => x.Where(It.IsAny<Expression<Func<TRecord, bool>>>())).Returns(recordQuery.Object);
            recordQuery.Setup(x => x.List()).Returns(items.Select(x => x.As<TPart>()).Where(x => x != null));
        }

        private static T[] ItIsEquivalentArray<T>(T[] arr)
        {
            return Match<T[]>.Create(
                x =>
                    { 
                        if (arr.Length != x.Length)
                        {
                            Trace.WriteLine("Array match: " + SequenceToString(arr) + " != " + SequenceToString(x));
                            return false;
                        }

                        int index = 0;
                        foreach (T item in arr)
                        {
                            if (!ReferenceEquals(item, null))
                            {
                                if (!item.Equals(x[index++]))
                                {
                                    Trace.WriteLine("Array match: " + SequenceToString(arr) + " != " + SequenceToString(x));
                                    return false;
                                }
                            }
                            else
                            {
                                if (!ReferenceEquals(x[index++], null))
                                {
                                    Trace.WriteLine("Array match: " + SequenceToString(arr) + " != " + SequenceToString(x));
                                    return false;
                                }
                            }
                        }

                        Trace.WriteLine("Array match: " + SequenceToString(arr) + " == " + SequenceToString(x));
                        return true;
                    });
        }

        private static string SequenceToString<T>(IEnumerable<T> arr)
        {
            if (arr == null)
            {
                return "null";
            }

            return "[" + string.Join(",", arr.Select(x => !ReferenceEquals(x, null) ? x.ToString() : "null")) + "]";
        }
    }
}