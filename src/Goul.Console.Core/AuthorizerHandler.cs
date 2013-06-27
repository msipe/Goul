using System;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using SupaCharge.Core.IOAbstractions;

namespace Goul.Console.Core {
  public class AuthorizerHandler : ICommandHandler {
    public void Execute(params string[] args) {
      var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description, "", "");
      GetAuthorization(provider, args[0]);
    }

    private void GetAuthorization(NativeApplicationClient appClient, string code) {
      var tokenRepository = new RefreshTokenRepository(new DotNetFile(), "refreshToken.txt" );
      System.Console.WriteLine(tokenRepository.Load()[0]);
      var state = new AuthorizationState(new[] {"https://www.googleapis.com/auth/drive", "https://docs.google.com/feeds"});
      
      state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
      appClient.ProcessUserAuthorization(code, state);
      tokenRepository.Update(state.RefreshToken);
    }
  }
}