﻿using DocumentUploader.Core.App;
using DocumentUploader.Core.Factory;
using DocumentUploader.Core.Factory.Module;
using DocumentUploader.Core.Observer;
using DocumentUploader.IntegrationTests.Infrastructure;
using DocumentUploader.IntegrationTests.Infrastructure.Modules;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace DocumentUploader.IntegrationTests.CommandFunctionality {
  [TestFixture]
  public class SetCredentialsTest : BaseTestCase {
    [Test]
    public void TestThatCustomCredentialsAreSet() {
      mApp.Execute("setcredentials", "val1", "val2");
      var credentialsFileLines = mFile.ReadAllLines("credentials.txt");
      Assert.That(credentialsFileLines, Is.EqualTo(BA("val1", "val2")));
      Assert.That(mObserver.GetMessages(), Is.EqualTo(BA("Credentials Set")));
    }

    [Test]
    public void TestThatTheAppThrowsCorrectExceptionWhenGivenIncorrectNumberOfArgs() {
      mApp.Execute("setcredentials");
      Assert.That(mObserver.GetMessages(), Is.EqualTo(BA("Invalid amount of arguments")));
      mApp.Execute("setcredentials", "1");
      Assert.That(mObserver.GetMessages(), Is.EqualTo(BA("Invalid amount of arguments")));
      mApp.Execute("setcredentials", "1", "2", "3");
      Assert.That(mObserver.GetMessages(), Is.EqualTo(BA("Invalid amount of arguments")));
    }

    [SetUp]
    public void DoSetup() {
      mFactory = new Factory(new DefaultModuleConfiguration(), new ITModuleConfiguration());
      mObserver = (RecordingObserver)mFactory.Build<IMessageObserver>();
      mApp = mFactory.Build<IApp>();
      mFile = new DotNetFile();
    }

    [TearDown]
    public void DoTearDown() {
      mFile.Delete("credentials.txt");
    }

    private DotNetFile mFile;
    private Factory mFactory;
    private RecordingObserver mObserver;
    private IApp mApp;
  }
}