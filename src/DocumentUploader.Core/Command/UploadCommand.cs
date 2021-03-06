﻿using System.Linq;
using DocumentUploader.Core.Models;
using DocumentUploader.Core.Observer;
using Goul.Core.Adapter;
using SupaCharge.Core.Collections.Extensions;

namespace DocumentUploader.Core.Command {
  public class UploadCommand : ICommand {
    public UploadCommand(IMessageObserver observer, IGoulRequestHandler gRequestHandler, ICredentialStore credentialStore, IRefreshTokenStore refreshStore) {
      mObserver = observer;
      mHandler = gRequestHandler;
      mCredentialStore = credentialStore;
      mRefreshStore = refreshStore;
    }

    public void Execute(params string[] args) {
      //args[2].TrimStart('\\');
      var foldersAndFile = args[2].Split(new[] {'\\'});

      var fileTitle = foldersAndFile.Last();
      var folders = foldersAndFile.Truncate(1).ToArray();

      mHandler.UploadFileWithFolder(args[1], fileTitle, folders, mCredentialStore.Get(), mRefreshStore.Get());
      mObserver.AddMessages("File uploaded");
    }

    private readonly ICredentialStore mCredentialStore;
    private readonly IGoulRequestHandler mHandler;
    private readonly IMessageObserver mObserver;
    private readonly IRefreshTokenStore mRefreshStore;
  }
}