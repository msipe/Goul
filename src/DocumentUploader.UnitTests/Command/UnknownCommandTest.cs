using DocumentUploader.Core.Command;
using DocumentUploader.Core.Observer;
using Moq;
using NUnit.Framework;

namespace DocumentUploader.UnitTests.Command {
  [TestFixture]
  public class UnknownCommandTest:DocumentUploaderBaseTestCase {
    [Test]
    public void TestUnkownCommandSendsTheUnknownMessage() {
     
     
    }

    [SetUp]
    public void DoSetup() {
      mObserver = Mok<IMessageObserver>();
     mCommand = new UnknownCommand(mObserver.Object);
    }

    private Mock<IMessageObserver> mObserver;
    private ICommand mCommand;
  }
}