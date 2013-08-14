﻿using System.Collections.Generic;
using System.IO;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Goul.Core.FileManagement;
using Goul.Core.Tokens;
using File = Google.Apis.Drive.v2.Data.File;

namespace Goul.Core.Functionality {
  public class Uploader {
    public Uploader(Credentials credentials, RefreshToken refreshToken) {
      mService = new GetDriveService().GetService(credentials, refreshToken);
      mUpdater = new Updater(credentials, refreshToken);
      mManager = new GDriveFileManager(credentials, refreshToken);
    }

    public void UploadFile(string fileToUpload, string fileTitle) {
      if (mUpdater.IsUpdateRequired(fileTitle))
        mUpdater.Update(fileToUpload, mManager.ListAllFilesOnRootById()[0]);
      else {
        var file = new File {Title = fileTitle, Description = "123"};
        var stream = new MemoryStream(System.IO.File.ReadAllBytes(fileToUpload));
        var request = mService.Files.Insert(file, stream, "text/plain");
        request.Convert = true;
        request.Upload();
      }
    }

    public void UploadFileUsingGoogleBase(File file, string filePath) {
      var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
      var request = mService.Files.Insert(file, stream, "text/plain");
      request.Convert = true;
      request.Upload();
    }

    public void UploadFileWithFolderSet(string file, string fileTitle, string[] foldersToUpload) {
      var parent = new ParentReference {Id = "root"};
      for (var x = 0; x < foldersToUpload.Length; x++) {
        var fileToUpload = new File {Title = foldersToUpload[x], MimeType = "application/vnd.google-apps.folder", Parents = new List<ParentReference> {parent}};
        var result = mService.Files.Insert(fileToUpload).Fetch();
        parent = new ParentReference {Id = result.Id};
      }

      var myFile = new File {Title = fileTitle, Parents = new List<ParentReference> {parent}};
      UploadFileUsingGoogleBase(myFile, file);
    }

    private readonly DriveService mService;
    private readonly Updater mUpdater;
    private readonly IFileManager mManager;
  }
}