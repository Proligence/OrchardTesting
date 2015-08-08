namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Reflection;
#if (NUNIT)
    using NUnit.Framework;
#endif
    using Autofac;
    using global::Orchard.Localization;
    using global::Orchard.Tokens;
    using global::Orchard.Tokens.Implementation;

    public abstract class TokenProviderTestFixture<TProvider> : ContainerTestFixture
        where TProvider : ITokenProvider
    {
#if (XUNIT)
        protected TokenProviderTestFixture()
        {
            Initialize();
        }
#endif

        public TProvider Provider { get; private set; }
        public ITokenizer Tokenizer { get; private set; }

#if (NUNIT)
        [SetUp]
        public void Setup()
        {
            Initialize();
        }
#endif

        protected override void Register(ContainerBuilder builder)
        {
            base.Register(builder);
            builder.RegisterType<TProvider>().As<TProvider>().As<ITokenProvider>();
        }

        private void Initialize()
        {
            Provider = Container.Resolve<TProvider>();

            PropertyInfo localizerProperty = typeof(TProvider).GetProperty("T", typeof(Localizer));
            if (localizerProperty != null)
            {
                localizerProperty.SetValue(Provider, NullLocalizer.Instance, null);
            }

            Tokenizer = new Tokenizer(new TokenManager(Container.Resolve<IEnumerable<ITokenProvider>>()));
        }
    }
}