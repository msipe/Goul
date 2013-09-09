using Goul.Core.Tokens;
using NUnit.Framework;

namespace DocumentUploader.UnitTests.Models {
  [TestFixture]
  public class CredentialsFileTest {
    [Test]
    public void TestDefaultsToNull() {
      var credentials = new Credentials();
      Assert.Null(credentials.ClientID);
      Assert.Null(credentials.ClientSecret);
    }
  }
}