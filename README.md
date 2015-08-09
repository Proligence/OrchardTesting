# Proligence.Orchard.Testing
Proligence.Orchard.Testing is a library which aids in unit testing Orchard modules by providing implementation of mocks for many components commonly used in Orchard.

The library supports two unit testing frameworks - [NUnit](http://www.nunit.org/) and [XUnit](http://xunit.github.io/). Mocking functionality depends on the [Moq](https://github.com/Moq/moq4) library.

# Getting Started

To best way to use Proligence.Orchard.Testing library in your Orchard projects is to check out the source code to some directory and map that directory to your Orchard source code directory using a hard link. This can be easily done using the [MapToOrchard.cmd](MapToOrchard.cmd) script. After cloning the repository for Proligence.Orchard.Testing, execute the script from cmd.exe and specify the path to the root directory of your Orchard project.

For example, let's say that Proligence.Orchard.Testing is located in `C:\Projects\OrchardTesting` and your Orchard solution is located in `C:\Projects\Orchard`. Just open `cmd.exe` and run:

    cd C:\Projects\OrchardTesting
    MapToOrchard.cmd C:\Projects\Orchard
  
This will create a hardlink in C:\Projects\Orchard\src\Tools\Proligence.Orchard.Testing pointing to C:\Projects\OrchardTesting\Proligence.Orchard.Testing. Next, you need to add the Proligence.Orchard.Testing project to your Orchard solution. Add `Proligence.Orchard.Testing.NUnit.csproj` if you are using NUNit or add `Proligence.Orchard.Testing.XUnit.csproj` if you are using XUnit.

## Mocking ILogger

The `LoggerMock` class can be used to easily mock out the `ILogger` interface, assert that specific messages should be written to the logger and validate these assertions after you run your test code. Just create an instance of `LoggerMock`, set up message assertions, pass the mock to the code you are testing (the `LoggerMock.Object` property returns a mock instance of `ILogger` type) and after the code finishes executing, verify the assertions by calling `VerifyAll` on the logger mock object. For example:

    var loggerMock = new LoggerMock();
    loggerMock.ExpectInformation("Operation completed successfully.");
            
    // TODO: Invoke tested code here.
    
    loggerMock.VerifyAll();

## Mocking INotifier

The `NotifierMock` class can be used to easily mock out the `INotifier` interface, assert specific notification messages should be displayed and validate these assertions after you run your test code. Just create an instance of `NotifierMock`, set up notification assertions, pass the mock to the code you are testing (the `NotifierMock.Object` property returns a mock instance of `INotifier` type) and after the code finishes executing, verify the assertions by calling `VerifyAll` on the notifier mock object. For example:

    var notifierMock = new NotifierMock();
    notifierMock.ExpectInformation("Operation completed successfully.");
    
    // TODO: Invoke tested code here.
    
    notifierMock.VerifyAll();

## Mocking IAuthorizer

The `AuthorizerMock` class can be used to easily mock out authorization code which uses the `IAuthorizer` interface. You can assert that specific permissions are checked and validate these assertions after you run your test code. Just create an instance of `AuthorizerMock`, set up permission assertions, pass the mock to the code you are testing (the `AuthorizerMock.Object` property returns a mock instance of `IAuthorizer` type) and after the code finishes executing, verify the assertions by calling `VerifyAll` on the mock object. For example:

    var authorizerMock = new AuthorizerMock();
    authorizerMock.Allow(Permissions.ViewContent, contentItem);
    
    // TODO: Invoke tested code here.
    
    authorizerMock.VerifyAll();

Note, that if you are using the `IAuthorizer.Authorize` overload which takes a `LocalizedString` parameter in your test code, then you must use `AuthorizerMock.Allow` and `AuthorizerMock.Deny` methods to set up your assertions. If you are using the `IAuthorizer.Authorize` overload without the `LocalizedString` parameter, you must use `AuthorizerMock.AllowWithoutMessage` and `AuthorizerMock.DenyWithoutMessage`.

## Mocking the Transaction Manager

The `TransactionManagerMock` class can be used to mock out the `ITransactionManager` interface. Use the `ExpectDemand` method to assert that the `ITransactionManager.Demand` method should be called. Use the `ExpectCancel` method to assert that the `ITransactionManager.Cancel` method should be called. After the test code finishes executing, you can verify the assertions by calling `VerifyAll` on the mock object. For example:

    var transactionManager = new TransactionManagerMock();
    transactionManager.ExpectCancel();
    
    // TODO: Invoke tested code here.
            
    transactionManager.VerifyAll();

## Mocking Shapes

Sometimes when unit testing Orchard applications, you need to test whether correct data is passed to shapes (for example in content drivers). As shapes are dynamic objects, which heavily rely on conventions implemented in Orchard core, they are difficult to mock.

The `ShapeFactoryMock` class provides an implementation of the `IShapeFactory` interface, which builds shape objects which you can examine in your unit tests. All shapes built by `ShapeFactoryMock` are of type `ShapeMock`, which expose all data passed to the shape. 

Although you can use the `ShapeFactoryMock` directly, usually the easiest way is to use the extension methods in `ContentShapeResultExtensions` and `CombinedResultExtensions` to build a mock shape from a `ContentShapeResult` or `CombinedResult`. The following example illustrates how shape mocking can be used in your unit tests.

    // TODO: Invoke code which builds a shape result
    
    Assert.That(result, Is.InstanceOf<ContentShapeResult>());
    var shapeResult = (ContentShapeResult)result;
    ShapeMock shapeMock = shapeResult.BuildShapeMock();
    Assert.That(shapeMock.Type, Is.EqualTo("My_Shape_Type"));
    Assert.That(shapeMock["TemplateName"], Is.SameAs("Parts/My.Shape"));
    Assert.That(shapeMock["Model"], Is.SameAs(model));
    Assert.That(shapeMock["Prefix"], Is.EqualTo("MyPrefix"));

For more details about testing content drivers, see *Testing Content Drivers* later in this document.

## Creating Mock Content Items

The `ContentFactory` class can be used to generate fake, memory-only content items in your unit tests. While it is possible to manually build content items, initializing all the underlying structures is quite complex and error prone. The `ContentFactory` class takes care of that for you.

For example, the following code creates a content item of content type `Foo` with a `TitlePart` and a `CommonPart`. Additionally, `123` is assigned as the content item's identifier.

    var contentItem = ContentFactory.CreateContentItem(123, "Foo". new TitlePart(), new CommonPart());
    Assert.Equal(123, contentItem.Id);
    Assert.Equal("Foo", contentItem.ContentType);
    Assert.NotNull(item.As<TitlePart>());
    Assert.NotNull(item.As<CommonPart>());

## Mocking the Content Definition Manager and Content Manager

You can use the `ContentDefinitionManagerMock` class to mock the `IContentDefinitionManager` interface. Once you create an instance of `ContentDefinitionManagerMock` you can easily setup expectations for content definitions using methods the `ExpectGetTypeDefinition` and `ExpectGetPartDefinition`. To create a mock content type definition, you can use the `CreateTypeDefinition`.

For example, the following code creates a dummy content type definition (for test purposes only) for a content type called `Foo` which contains the `TitlePart` and `CommonPart`. Next, it asserts that the `ContentDefinitionManagerMock` should return the definition and verifies this assertion after running the test code.

    _contentDefinitionManager = new ContentDefinitionManagerMock();
    var definition = _contentDefinitionManager.CreateTypeDefinition("Foo", "TitlePart", "CommonPart");
    _contentDefinitionManager.ExpectGetTypeDefinition("Foo", definition);
    
    // TODO: Invoke tested code here.
    
    _contentDefinitionManager.VerifyAll();

The `ContentManagerMock` class enables mocking out simple operations on content items through the `IContentManager` interface. The class implements methods such as `ExpectGetItem`, `ExpectNewItem`, `ExpectPublishItem`, etc. which can be used to set up expected operations on the mock. After the test code finishes executing, you can verify the assertions by calling `VerifyAll` on the mock object.

If you need any assertions which are not covered by the API exposed by the `ContentManagerMock`, you can use the regular Moq API on the `ContentManagerMock` (such as the `Setup` method).

The following code illustrates a simple test which mocks out getting a content item with ID 123 from the content manager:

    var contentManager = new ContentManagerMock();
    var contentItem = ContentFactory.CreateContentItem(123);
    contentManager.ExpectGetItem(123, contentItem);
    
    // TODO: Execute code which uses the content manager to get the content item with ID 123.

    contentManager.VerifyAll();

## Testing Content Drivers

For testing content drivers, you can use the `ContentDriverTestFixture` class as a base class for your test fixture. This base class automatically provides some useful components, mostly important the special implementation of the shape factory which can be accessed using the `ShapeFactory` property.

You can override the `Setup` method and create an instance of you content driver there. Make sure to call the base implementation of the `Setup` method.

In your tests, you can easily invoke your content driver logic using the extensions methods in `ContentPartDriverExtensions` - `InvokeDisplay` and `InvokeEditor`. As the shape helper (factory), pass the object returned from the `ShapeFactory`, which is implemented in the base class. As the result of a content driver is usually a shape, you will probably want to use the extension methods in `ContentShapeResultExtensions` to build a mock shape and verify the data passed to the shape.

The following code illustrates a single test fixture for testing a content driver.

    [TestFixture]
    public class MyPartDriverTests : ContentDriverTestFixture
    {
        private MyPartDriver _driver;
        
        public override void Setup()
        {
            base.Setup();
            _driver = new MyPartDriver();
        }
        
        [Test]
        public void EditorShouldReturnShapeWithValidData()
        {
            var part = new MyPart();
            
            DriverResult result = _driver.InvokeEditor(part, ShapeFactory);
            
            Assert.That(result, Is.InstanceOf<ContentShapeResult>());
            var shapeResult = (ContentShapeResult)result;
            var shape = shapeResult.BuildShapeMock();
            Assert.That(shape.Type, Is.EqualTo("Parts_My_Edit"));
            Assert.That(shape["TemplateName"], Is.SameAs("Parts/My.Edit"));
            Assert.That(shape["Model"], Is.SameAs(part));
            Assert.That(shape["Prefix"], Is.EqualTo("MyPart"));
        }
    }

## Testing Content Handlers

For testing content handlers, you can use the `ContentHandlerTestFixture<THandler>` class as a base class for your test fixture and specify the type of your content handler as the generic parameter. This base class provides methods to invoke specific content handler events. For example, the `InvokeActivating` method calls the `OnActivated` handler method, `InvokePublished` calls `OnPublished`, and so on.

If you need any per-test initialization code, you can override the `Setup` method. Make sure to call the base implementation, which takes care of creating an instance of your content handler.

The following example illustrates a simple test fixture containing a test which checks if the `OnPublished` content handler initializes the title of the content item to a non-null value.

    [TestFixture]
    public class MyPartHandlerTests : ContentHandlerTestFixture<MyPartHandler>
    {
        public override void Setup()
        {
            // TODO: Perform per-test initialization
            base.Setup();
        }
        
        [Test]
        public void PublishedShouldInitializeTitle()
        {
            var contentItem = ContentFactory.CreateContentItem(123, "Foo", new TitlePart());
            this.InvokePublished(new PublishContentContext(contentItem, null));
            Assert.NotNull(contentItem.As<TitlePart>().Title);
        }
    }

## Testing Tokens

The `TokenProviderTestFixture<T>` class is a base class for fixtures which tests token implementations. This base class providers an infrastructure to expand specific tokens, with the specified parameters, using Orchard's token engine.

When creating a test fixture for your token provider, specify the type of the token provider as the generic type parameter of the `TokenProviderTestFixture<T>` base class. If your token provider relies on any components, make sure to override the `Register` method and register these components. In your tests, you can access the tokenizer using the `Tokenizer` property.

The following code illustrates a simple test fixture testing date tokens.

    [TestFixture]
    public class DateTokensTests : TokenProviderTestFixture<DateTokens>
    {
        private Mock<IWorkContextAccessor> _wca;
        private Mock<IClock> _clock;
        
        protected override void Register(ContainerBuilder builder)
        {
            base.Register(builder);
            
            var ctx = new WorkContextMock();
            ctx.CurrentCulture = CultureInfo.InvariantCulture.Name;
            _wca = new Mock<IWorkContextAccessor>();
            _wca.Setup(x => x.GetContext()).Returns(ctx);
            builder.RegisterInstance(_wca.Object).As<IWorkContextAccessor>();
            
            _clock = new Mock<IClock>();
            builder.RegisterInstance(_clock.Object).As<IClock>();
            
            builder.RegisterType<DefaultDateLocalizationServices>().As<IDateLocalizationServices>();
            builder.RegisterType<CultureDateTimeFormatProvider>().As<IDateTimeFormatProvider>();
            builder.RegisterType<DefaultDateFormatter>().As<IDateFormatter>();
            builder.RegisterType<DefaultCalendarManager>().As<ICalendarManager>();
        }
        
        [Test]
        public void ShouldReturnYearPartOfDate()
        {
            var dateDt = DateTime.Parse("06/22/2014", CultureInfo.InvariantCulture);
            var result = Tokenizer.Replace("{Date.Year}", new { Date = dateDt });
            Assert.Equal("2014", result);
        }
        
        [Test]
        public void ShouldReturnMonthPartOfDate()
        {
            var dateDt = DateTime.Parse("06/22/2014", CultureInfo.InvariantCulture);
            var result = Tokenizer.Replace("{Date.Month}", new { Date = dateDt });
            Assert.Equal("6", result);
        }
        
        [Test]
        public void ShouldReturnDayPartOfDate()
        {
            var dateDt = DateTime.Parse("06/22/2014", CultureInfo.InvariantCulture);
            var result = Tokenizer.Replace("{Date.Day}", new { Date = dateDt });
            Assert.Equal("22", result);
        }
    }