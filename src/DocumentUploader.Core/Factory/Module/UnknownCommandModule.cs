using Autofac;
using DocumentUploader.Core.Command;

namespace DocumentUploader.Core.Factory.Module {
  internal class UnknownCommandModule : Autofac.Module {
    protected override void Load(ContainerBuilder builder) {
      base.Load(builder);

      builder
        .RegisterType<UnknownCommand>()
        .InstancePerLifetimeScope()
        .Keyed<ICommand>("unknown");
    }
  }
}