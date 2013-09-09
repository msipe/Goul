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
  public class ConsoleHelpTests : BaseTestCase {
    [Test]
    public void TestHelpCommandIsSentCorrectly() {
      var factory = new Factory(new DefaultModuleConfiguration(), new ITModuleConfiguration());
      var messageObserver = (RecordingObserver)factory.Build<IMessageObserver>();
      var app = factory.Build<IApp>();
      app.Execute("help");
      Assert.That(messageObserver.GetMessages(), Is.EqualTo(BA("Goul Document Uploader Version 0.1",
                                                               "Commands:",
                                                               "setcredentials xClient_IDx xClient_Secretx | Sets the client id and the client secret to a local .txt file",
                                                               "listcredentials | lists the credentials",
                                                               "clearcredentials | deletes ALL of the credential files",
                                                               "getauthorizationurl | Retrieves a url to the Google authorization process, based on the given credentials",
                                                               "authorize x AuthorizationCode x | Creates a refresh token based on the auth code retrieved from the 'getauthorizationurl",
                                                               "upload xPathOfTheFileToUploadx x TitleOfTheFileOnGoogle x | Uploads a file from the given path, to the bound Google Account, with the given title ")));
    }
  }
}