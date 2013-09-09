using System;
using DocumentUploader.Core.App;
using DocumentUploader.Core.Factory;
using DocumentUploader.Core.Factory.Module;
using DocumentUploader.Core.Observer;
using DocumentUploader.IntegrationTests.Infrastructure;
using DocumentUploader.IntegrationTests.Infrastructure.Modules;
using NUnit.Framework;
using SupaCharge.Testing;

namespace DocumentUploader.IntegrationTests.CommandFunctionality {
  [TestFixture]
  public class UnknownCommand : BaseTestCase {
    [Test]
    public void TestUnknownCommandDisplaysCorrectMessage() {
      mApp.Execute("Invalid Command");
      Assert.That(mObserver.GetMessages(), Is.EqualTo(BA("Invalid Command")));
    }

    [SetUp]
    public void Setup() {
      mFactory = new Factory(new DefaultModuleConfiguration(), new ITModuleConfiguration());
      mObserver = (RecordingObserver)mFactory.Build<IMessageObserver>();
      mApp = mFactory.Build<IApp>();
    }

    private Factory mFactory;
    private RecordingObserver mObserver;
    private IApp mApp;
  }
}