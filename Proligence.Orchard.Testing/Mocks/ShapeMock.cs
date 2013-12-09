namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Dynamic;
    using System.Web.Mvc;
    using global::Orchard.ContentManagement;
    using global::Orchard.DisplayManagement;
    using global::Orchard.DisplayManagement.Shapes;

    public class ShapeMock : DynamicObject, IShape, IEnumerable<object>
    {
        private const string DefaultPosition = "5";

        private readonly List<string> _classes;
        private readonly Dictionary<string, string> _attributes;
        private readonly List<dynamic> _items;
        private readonly IDictionary<Tuple<string, object[]>, Func<object[], object>> _mockedMethods;

        public ShapeMock(string name)
        {
            Name = name;
            Data = new Dictionary<string, object>();
            Metadata = new ShapeMetadata();

            _classes = new List<string>();
            _attributes = new Dictionary<string, string>();
            _items = new List<dynamic>();
            _mockedMethods = new Dictionary<Tuple<string, object[]>, Func<object[], object>>();
        }

        public virtual string Id { get; set; }
        public virtual IList<string> Classes { get { return _classes; } }
        public virtual IDictionary<string, string> Attributes { get { return _attributes; } }
        public virtual IEnumerable<dynamic> Items { get { return _items; } }

        public string Name { get; private set; }
        public ShapeMetadata Metadata { get; set; }
        public ContentItem ContentItem { get; set; }
        public Dictionary<string, object> Data { get; private set; }

        public string Type
        {
            get { return Metadata.Type; } 
            set { Metadata.Type = value; }
        }

        public dynamic this[string name]
        {
            get
            {
                return Data[name];
            }
        }

        public virtual ShapeMock Add(object item, string position = null) {
            // pszmyd: Ignoring null shapes 
            if (item == null) 
            {
                return this;
            }

            try 
            {
                // todo: (sebros) this is a temporary implementation to prevent common known scenarios throwing exceptions. The final solution would need to filter based on the fact that it is a Shape instance
                if (item is MvcHtmlString ||
                    item is String) {
                    // need to implement positioned wrapper for non-shape objects
                }
                else if (item is IShape) 
                {
                    ((dynamic)item).Metadata.Position = position;
                }
            }
            catch 
            {
                // need to implement positioned wrapper for non-shape objects
            }

            _items.Add(item); // not messing with position at the moment
            return this;
        }

        public virtual ShapeMock AddRange(IEnumerable<object> items, string position = DefaultPosition)
        {
            foreach (var item in items)
                Add(item, position);
            
            return this;
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator() 
        {
            return _items.GetEnumerator();
        }

        public virtual IEnumerator GetEnumerator() 
        {
            return _items.GetEnumerator();
        }

        public virtual void MockMethodCall(Func<object[], object> factory, string methodName, params object[] parameters)
        {
            Tuple<string, object[]> key = GetMethodMockKey(methodName, parameters);

            if (key != null)
            {
                _mockedMethods.Remove(key);
            }

            _mockedMethods.Add(new KeyValuePair<Tuple<string, object[]>, Func<object[], object>>(
                new Tuple<string, object[]>(methodName, parameters),
                factory));
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Tuple<string, object[]> key = GetMethodMockKey(binder.Name, args);
            if (key != null)
            {
                result = _mockedMethods[key](args);
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Data[binder.Name] = value;
            return true;
        }

        private Tuple<string, object[]> GetMethodMockKey(string methodName, IList<object> parameters)
        {
            foreach (KeyValuePair<Tuple<string, object[]>, Func<object[], object>> mock in _mockedMethods)
            {
                if ((mock.Key.Item1 == methodName) && (mock.Key.Item2.Length == parameters.Count))
                {
                    bool allEqual = true;
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (mock.Key.Item2[i] != null)
                        {
                            if (!mock.Key.Item2[i].Equals(parameters[i]))
                            {
                                allEqual = false;
                                break;
                            }
                        }
                        else
                        {
                            if (parameters[i] != null)
                            {
                                allEqual = false;
                                break;
                            }
                        }
                    }

                    if (allEqual)
                    {
                        return mock.Key;
                    }
                }
            }

            return null;
        }
    }
}