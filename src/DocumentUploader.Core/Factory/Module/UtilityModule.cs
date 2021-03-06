﻿using Autofac;
using DocumentUploader.Core.App;
using DocumentUploader.Core.Observer;
using DocumentUploader.Core.Output;
using SupaCharge.Core.IOAbstractions;

namespace DocumentUploader.Core.Factory.Module {
  public class UtilityModule : Autofac.Module {
    protected override void Load(ContainerBuilder builder) {
      base.Load(builder);

      builder
        .RegisterType<DocUploaderApp>()
        .InstancePerLifetimeScope()
        .As<IApp>();

      builder
        .RegisterType<ConsoleWriter>()
        .SingleInstance()
        .As<IMessageObserver>();

      builder
        .RegisterType<DotNetFile>()
        .SingleInstance()
        .As<IFile>();
    }
  }
}