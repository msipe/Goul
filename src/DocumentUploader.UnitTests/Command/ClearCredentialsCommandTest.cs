using DocumentUploader.Core.Command;
using DocumentUploader.Core.Observer;
using Moq;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace DocumentUploader.UnitTests.Command {
  [TestFixture]
  public class ClearCredentialsCommandTest : BaseTestCase {
    [Test]
    public void TestThatClearDisplaysTheCorrectMessageOnSuccesfulCompletion() {
      mFile.Setup(f => f.Exists("credentials.txt")).Returns(true);
      mFile.Setup(f => f.Delete("credentials.txt"));
      mObserver.Setup(o => o.AddMessages("Credentials Cleared"));
      mCommand.Execute("clearcredentials");
    }

    [Test]
    public void TestCredentialsAreNotClearedWhenTheFileIsMissing() {
      mFile.Setup(f => f.Exists("credentials.txt")).Returns(false);
      mObserver.Setup(o => o.AddMessages("Could not find the Credentials file"));
      mCommand.Execute("clearcredentials");
    }

    [SetUp]
    public void DoSetup() {
      mObserver = Mok<IMessageObserver>();
      mFile = Mok<IFile>();
      mCommand = new ClearCredentialsCommand(mObserver.Object, mFile.Object);
    }

    private ICommand mCommand;
    private Mock<IFile> mFile;
    private Mock<IMessageObserver> mObserver;
  }
}