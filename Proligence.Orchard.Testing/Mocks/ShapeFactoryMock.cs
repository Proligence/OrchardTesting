namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using global::Orchard.DisplayManagement;

    public class ShapeFactoryMock : DynamicObject, IShapeFactory
    {
        private readonly IDictionary<string, Func<dynamic>> _mocks;

        public ShapeFactoryMock()
        {
            _mocks = new Dictionary<string, Func<dynamic>>();
        }

        public IShape Create(string shapeType)
        {
            Func<dynamic> factory;
            if (_mocks.TryGetValue(shapeType, out factory))
            {
                return factory();
            }

            return new ShapeMock(shapeType);
        }

        public IShape Create(string shapeType, INamedEnumerable<object> parameters)
        {
            dynamic shape;

            Func<dynamic> factory;
            if (_mocks.TryGetValue(shapeType, out factory))
            {
                shape = factory();
            }
            else
            {
                shape = new ShapeMock(shapeType);
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters.Named)
                {
                    shape.Data[parameter.Key] = parameter.Value;
                }
            }

            return shape;
        }

        public IShape Create(string shapeType, INamedEnumerable<object> parameters, Func<dynamic> createShape)
        {
            dynamic shape = createShape();

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters.Named)
                {
                    shape.Data[parameter.Key] = parameter.Value;
                }
            }

            return shape;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if ((binder.Name == "List") && (args.Length == 0))
            {
                result = new ShapeMock("List");
                return true;
            }

            if (!args.Any())
            {
                result = null;
                return false;
            }

            dynamic shape;

            Func<dynamic> factory;
            if (_mocks.TryGetValue(binder.Name, out factory))
            {
                shape = factory();
            }
            else
            {
                shape = new ShapeMock(binder.Name);
            }

            int index = 0;
            foreach (string name in binder.CallInfo.ArgumentNames)
            {
                if ((index < args.Length) && (args[index] != null))
                {
                    shape.Data[name] = args[index];
                }

                index++;
            }

            result = shape;
            return true;
        }

        public void Mock(string shapeType, Func<dynamic> factory)
        {
            _mocks[shapeType] = factory;
        }
    }
}