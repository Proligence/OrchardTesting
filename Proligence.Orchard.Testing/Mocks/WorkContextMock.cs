namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using global::Orchard;
    using global::Orchard.Security;
    using global::Orchard.Settings;

    public class WorkContextMock : WorkContext
    {
        private readonly Dictionary<string, object> _state = new Dictionary<string, object>();

        public WorkContextMock()
        {
            CurrentSiteMock = new Mock<ISite>();
            CurrentSite = CurrentSiteMock.Object;

            CurrentUserMock = new Mock<IUser>();
            CurrentUser = CurrentUserMock.Object;

            CurrentTimeZone = TimeZoneInfo.Utc;
        }

        public WorkContextMock(MockBehavior mockBehavior)
        {
            CurrentUserMock = new Mock<IUser>(mockBehavior);
            CurrentUser = CurrentUserMock.Object;

            CurrentSiteMock = new Mock<ISite>(mockBehavior);
            CurrentSite = CurrentSiteMock.Object;
        }

        public Mock<ISite> CurrentSiteMock { get; private set; }
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

        public override object Resolve(Type serviceType)
        {
            return ResolveFunc?.Invoke(serviceType);
        }

        public override bool TryResolve<T>(out T service)
        {
            service = default(T);

            if (ResolveFunc != null)
            {
                service = (T)ResolveFunc(typeof(T));
                return service != null;
            }

            return false;
        }

        public override bool TryResolve(Type serviceType, out object service)
        {
            service = null;

            if (ResolveFunc != null)
            {
                service = ResolveFunc(serviceType);
                return service != null;
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