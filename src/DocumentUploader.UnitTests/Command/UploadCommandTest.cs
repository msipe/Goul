using DocumentUploader.Core.Command;
using DocumentUploader.Core.Models;
using DocumentUploader.Core.Observer;
using Goul.Core.Adapter;
using Goul.Core.Tokens;
using Moq;
using NUnit.Framework;

namespace DocumentUploader.UnitTests.Command {
  [TestFixture]
  public class UploadCommandTest : DocumentUploaderBaseTestCase {
    [Test]
    public void TestUploadMessageIsSentWithNoFoldersAdded() {
      var credentials = CA<Credentials>();
      var refreshToken = CA<RefreshToken>();
      mCredentialStore.Setup(r => r.Get()).Returns(credentials);
      mRefreshTokenStore.Setup(s => s.Get()).Returns(refreshToken);
      mHandler.Setup(h => h.UploadFileWithFolder("file",
                                                 "fileTitle", 
                                                 new string[] {}, 
                                                 credentials,
                                                 refreshToken));
      mObserver.Setup(o => o.AddMessages("File uploaded"));
      mCommand.Execute("upload", "file", "fileTitle");
    }

    [Test]
    public void TestCommandWithFoldersAdded() {
      var credentials = CA<Credentials>();
      var refreshToken = CA<RefreshToken>();
      mCredentialStore.Setup(r => r.Get()).Returns(credentials);
      mRefreshTokenStore.Setup(s => s.Get()).Returns(refreshToken);
      mHandler.Setup(h => h.UploadFileWithFolder("file",
                                                 "fileTitle",
                                                 new [] {"folder" },
                                                 credentials,
                                                 refreshToken));
      mObserver.Setup(o => o.AddMessages("File uploaded"));
      mCommand.Execute("upload", "file", @"folder\fileTitle");
    }

    [Test]
    public void TestCommandWhereAFolderHasTheSameNameAsTheFile() {
      var credentials = CA<Credentials>();
      var refreshToken = CA<RefreshToken>();
      mCredentialStore.Setup(r => r.Get()).Returns(credentials);
      mRefreshTokenStore.Setup(s => s.Get()).Returns(refreshToken);
      mHandler.Setup(h => h.UploadFileWithFolder("file",
                                                 "fileTitle",
                                                new [] {"fileTitle", "folder"}, 
                                                 credentials,
                                                 refreshToken));
      mObserver.Setup(o => o.AddMessages("File uploaded"));
      mCommand.Execute("upload", "file", @"fileTitle\folder\fileTitle");
    }

    [Test]
    public void TestCommandWhereThereAre3Folders() {
      var credentials = CA<Credentials>();
      var refreshToken = CA<RefreshToken>();
      mCredentialStore.Setup(r => r.Get()).Returns(credentials);
      mRefreshTokenStore.Setup(s => s.Get()).Returns(refreshToken);
      mHandler.Setup(h => h.UploadFileWithFolder("file",
                                                 "fileTitle",
                                                new[] { "folder1", "folder2", "folder3" },
                                                 credentials,
                                                 refreshToken));
      mObserver.Setup(o => o.AddMessages("File uploaded"));
      mCommand.Execute("upload", "file", @"folder1\folder2\folder3\fileTitle");
    }

    [Test]
    public void TestCommandWhereTheFileNameIsPrefixedWithASlash() {
      var credentials = CA<Credentials>();
      var refreshToken = CA<RefreshToken>();
      mCredentialStore.Setup(r => r.Get()).Returns(credentials);
      mRefreshTokenStore.Setup(s => s.Get()).Returns(refreshToken);
      mHandler.Setup(h => h.UploadFileWithFolder("file",
                                                 "fileTitle",
                                                 new[] { ""},
                                                 credentials,
                                                 refreshToken));
      mObserver.Setup(o => o.AddMessages("File uploaded"));
      mCommand.Execute("upload", "file", @"\fileTitle");
    }

    [SetUp]
    public void DoSetup() {
      mObserver = Mok<IMessageObserver>();
      mHandler = Mok<IGoulRequestHandler>();
      mRefreshTokenStore = Mok<IRefreshTokenStore>();
      mCredentialStore = Mok<ICredentialStore>();
      mCommand = new UploadCommand(mObserver.Object, mHandler.Object, mCredentialStore.Object, mRefreshTokenStore.Object);
    }

    private Mock<IMessageObserver> mObserver;
    private Mock<IGoulRequestHandler> mHandler;
    private Mock<ICredentialStore> mCredentialStore;
    private Mock<IRefreshTokenStore> mRefreshTokenStore;
    private ICommand mCommand;
  }
}