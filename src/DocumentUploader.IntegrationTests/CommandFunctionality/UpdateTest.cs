﻿using DocumentUploader.Core.App;
using DocumentUploader.Core.Factory;
using DocumentUploader.Core.Factory.Module;
using DocumentUploader.Core.Models;
using DocumentUploader.Core.Observer;
using DocumentUploader.IntegrationTests.Infrastructure;
using DocumentUploader.IntegrationTests.Infrastructure.Modules;
using Goul.Core.Adapter;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace DocumentUploader.IntegrationTests.CommandFunctionality {
  [TestFixture]
  public class UpdateTest : BaseTestCase {
    [Test]
    public void TestUploadWith3ArgsUploadsAFileOnly() {
     
    }

    [SetUp]
    public void Setup() {
      mFactory = new Factory(new DefaultModuleConfiguration(), new ITModuleConfiguration());
      mObserver = (RecordingObserver)mFactory.Build<IMessageObserver>();
      mApp = mFactory.Build<IApp>();
      mFile = new DotNetFile();
      mRefreshToken = new RefreshTokenStore(mFile, "refreshToken.txt");
      mCredentials = new CredentialStore(mFile, "credentials.txt");
      mHandler = new GoulRequestHandler();

      var provider = new TestConfigurationProvider();
      provider.SetupCredentialsFile();
      provider.SetupRefreshTokenFile();
      provider.SetupDummyFile();

      mHandler.DeleteAllFiles(mCredentials.Get(), mRefreshToken.Get());
    }

    private DotNetFile mFile;
    private Factory mFactory;
    private RecordingObserver mObserver;
    private IApp mApp;
    private ICredentialStore mCredentials;
    private IRefreshTokenStore mRefreshToken;
    private IGoulRequestHandler mHandler;
  }
}