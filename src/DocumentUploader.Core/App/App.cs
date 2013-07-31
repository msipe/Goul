﻿namespace DocumentUploader.Core.App {
  public class App:IApp {
    public App(ICommand command) {
      mCommand = command;
    }

    public void Execute(string[] commands) {
      if (commands[0] == "help") {
        mCommand.Execute(commands);
      }
    }

    private readonly ICommand mCommand;
  }
}