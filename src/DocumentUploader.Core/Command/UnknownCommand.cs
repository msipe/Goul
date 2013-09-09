using DocumentUploader.Core.Observer;

namespace DocumentUploader.Core.Command {
  public class UnknownCommand : ICommand {
    public UnknownCommand(IMessageObserver observer) {
      mObserver = observer;
    }

    public void Execute(params string[] args) {
      mObserver.AddMessages("Invalid Command");
    }

    private readonly IMessageObserver mObserver;
  }
}