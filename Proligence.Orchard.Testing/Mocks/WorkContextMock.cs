namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using global::Orchard;
    using global::Orchard.Security;

    public class WorkContextMock : WorkContext
    {
        private readonly Dictionary<string, object> _state = new Dictionary<string, object>();

        public WorkContextMock()
        {
            CurrentUserMock = new Mock<IUser>();
            CurrentUser = CurrentUserMock.Object;
            CurrentTimeZone = TimeZoneInfo.Utc;
        }

        public WorkContextMock(MockBehavior mockBehavior)
        {
            CurrentUserMock = new Mock<IUser>(mockBehavior);
            CurrentUser = CurrentUserMock.Object;
        }

        public Mock<IUser> CurrentUserMock { get; private set; }
        public Func<Type, object> ResolveFunc { get; set; }

        public override T Resolve<T>()
        {
            if (ResolveFunc != null)
            {
                return (T)ResolveFunc(typeof(T));
            }

            return default(T);
        }

        public override bool TryResolve<T>(out T service)
        {
            service = default(T);

            if (ResolveFunc != null)
            {
                service = (T)ResolveFunc(typeof(T));
                return (service != null);
            }

            return false;
        }

        public override T GetState<T>(string name)
        {
            if (!_state.ContainsKey(name))
            {
                return default(T);
            }

            return (T)_state[name];
        }

        public override void SetState<T>(string name, T value)
        {
            _state[name] = value;
        }
    }
}