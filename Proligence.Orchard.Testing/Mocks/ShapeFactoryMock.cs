namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using global::Orchard.DisplayManagement;

    public class ShapeFactoryMock : DynamicObject, IShapeFactory
    {
        public IShape Create(string shapeType, INamedEnumerable<object> parameters)
        {
            throw new NotImplementedException();
        }

        public IShape Create(string shapeType, INamedEnumerable<object> parameters, Func<dynamic> createShape)
        {
            throw new NotImplementedException();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (!args.Any())
            {
                result = null;
                return false;
            }

            var shape = new ShapeMock(binder.Name);

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
    }
}