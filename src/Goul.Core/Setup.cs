﻿using System;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;

namespace Goul.Core {
  public class Setup {
    public IGetAuthUrlHandler SetupGetAuthUrl() {
      return new GetAuthUrlHandler(GetProvider(), GetState());
    }

    public IAuthorizerHandler SetupAuthorizerHandler() {
      return new AuthorizerHandler();
    }

    public IUploadHandler SetupUploadHandler() {
      return new UploaderHandler(GetProvider());
    }

    public void setAuthState(IAuthorizationState auth) {
      mAuthState = auth;
    }

    public void SetAuthenticator(OAuth2Authenticator<NativeApplicationClient> authenticator) {
      mAuth = authenticator;
    }

    private IAuthorizationState GetState() {
      var state = new AuthorizationState(new[] {"https://www.googleapis.com/auth/drive", "https://docs.google.com/feeds"});
      state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
      return state;
    }

    private NativeApplicationClient GetProvider() {
      return new NativeApplicationClient(GoogleAuthenticationServer.Description, "", "");
    }

    private OAuth2Authenticator<NativeApplicationClient> mAuth;
    private IAuthorizationState mAuthState;
  }
}