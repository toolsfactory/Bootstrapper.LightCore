using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Extensions;
using Bootstrap.Extensions.Containers;
using LightCore;
using LightCore.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace Bootstrap.LightCore
{
    public class LightCoreExtension : BootstrapperContainerExtension
    {
        private IContainer _Container;
        private IContainerBuilder _Builder;

        public LightCoreOptions Options { get; private set; }

        public LightCoreExtension(IRegistrationHelper registrationHelper, IBootstrapperContainerExtensionOptions options)
            : base(registrationHelper)
        {
            Options = new LightCoreOptions(options);
            Bootstrapper.Excluding.Assembly("LightCore");
            Bootstrapper.Excluding.Assembly("Microsoft");
        }

        public void InitializeContainer(IContainerBuilder builder)
        {
            _Builder = builder;
        }

        protected override void InitializeContainer()
        {
            InitializeContainer(Options.Builder ?? new ContainerBuilder());
        }

        protected override void RegisterImplementationsOfIRegistration()
        {
            if (Options.AutoRegistration) AutoRegister();
            InternalRegisterAll<IBootstrapperRegistration>(_Builder);
            InternalRegisterAll<ILightCoreRegistration>(_Builder);
        }

        protected override void InvokeRegisterForImplementationsOfIRegistration()
        {
            Build();
            _Container.Resolve<IEnumerable<IBootstrapperRegistration>>().ToList().ForEach(r => r.Register(this));
            _Container.Resolve<IEnumerable<ILightCoreRegistration>>().ToList().ForEach(r => r.Register(_Builder));
            Build();
        }

        public override void Register<TTarget>(TTarget implementation)
        {
            _Builder.Register<TTarget>(implementation);
            Build();
        }

        public override void Register<TTarget,TImplementation>()
        {
            _Builder.Register<TTarget, TImplementation>();
            Build();
        }

        public override void RegisterAll<TTarget>()
        {
            InternalRegisterAll<TTarget>(_Builder);
            Build();
        }

        protected override void ResetContainer()
        {
 	        _Builder = new ContainerBuilder();
            _Container = null;
        }

        public override T Resolve<T>()
        {
            try
            {
                return _Container.Resolve<T>();
            }
            catch (RegistrationNotFoundException ex)
            { return null; } 
        }

        public override IList<T> ResolveAll<T>()
        {
            try
            {
                return _Container.Resolve<IEnumerable<T>>().ToList();
            }
            catch (RegistrationNotFoundException ex)
            { return new List<T>(); }
        }

        public override void SetServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => new LightCoreAdapter(_Container));
        }

        public override void ResetServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => null);
        }

        public void Build()
        {
            if (_Builder == null)
                throw new InvalidOperationException();

            Container = _Container = _Builder.Build();
        }
        private void InternalRegisterAll<TTarget>(IContainerBuilder builder)
        {
            var assemblies = Registrator.GetAssemblies().ToList();
            assemblies.ForEach(a => Registrator.GetTypesImplementing<TTarget>(a).ToList()
                                .ForEach(t => builder.Register(typeof(TTarget), t)));
        }
    }
}
