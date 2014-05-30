using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Extensions;
using Bootstrap.Extensions.Containers;
using LightCore;

namespace Bootstrap.LightCore
{
    public class LightCoreExtension : BootstrapperContainerExtension
    {
        public LightCoreOptions Options { get; private set; }


        public LightCoreExtension(IRegistrationHelper registrationHelper, IBootstrapperContainerExtensionOptions options)
            : base(registrationHelper)
        {
            Options = new LightCoreOptions(options);
            Bootstrapper.Excluding.Assembly("LightCore");
        }

        protected override void InitializeContainer()
        {
            ModuleManager.Build();
            Container = ModuleManager._container;
        }

        protected override void RegisterImplementationsOfIRegistration()
        {
            CheckContainer();
            if (Options.AutoRegistration) AutoRegister();
            RegisterAll<IBootstrapperRegistration>();
            RegisterAll<ILightCoreRegistration>();
        }

        protected override void InvokeRegisterForImplementationsOfIRegistration()
        {
            CheckContainer();

            try
            {
                ModuleManager.Resolve<IEnumerable<IBootstrapperRegistration>>().ToList().ForEach(r => r.Register(this));
            }
            catch { }
            try
            {
                ModuleManager.Resolve<IEnumerable<ILightCoreRegistration>>().ToList().ForEach(r => r.Register(ModuleManager._builder));
            }
            catch { }
            ModuleManager.Build();
        }

        protected override void ResetContainer()
        {
            ModuleManager.CleanUp();
        }

        public override void RegisterAll<TTarget>()
        {
            var asm = Registrator.GetAssemblies().ToList();
            var items = Registrator
                        .GetAssemblies()
                        .SelectMany(a => Registrator.GetTypesImplementing<TTarget>(a));
            items.ForEach(t => ModuleManager.Register(typeof(TTarget), t));
            if (items.Count()>0)
                ModuleManager.Build();
        }

        public override void SetServiceLocator()
        {
            throw new NotImplementedException();
        }

        public override void ResetServiceLocator()
        {
            throw new NotImplementedException();
        }

        public override T Resolve<T>()
        {
            try
            {
                return ModuleManager.Resolve<T>();
            }
            catch (RegistrationNotFoundException ex)
            { return null; } 
        }

        public override IList<T> ResolveAll<T>()
        {
            try
            {
                return ModuleManager.Resolve<IEnumerable<T>>().ToList();
            }
            catch (RegistrationNotFoundException ex)
            { return new List<T>(); }
        }

        public override void Register<TTarget, TImplementation>()
        {
            ModuleManager.Register<TTarget, TImplementation>();
            ModuleManager.Build();
        }

        public override void Register<TTarget>(TTarget implementation)
        {
            ModuleManager.Register<TTarget>(implementation);
            ModuleManager.Build();
        }

    }
}
