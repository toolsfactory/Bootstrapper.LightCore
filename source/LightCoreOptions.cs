using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Extensions.Containers;
using LightCore;

namespace Bootstrap.LightCore
{
    public class LightCoreOptions : BootstrapperOption, IBootstrapperContainerExtensionOptions
    {
        private readonly IBootstrapperContainerExtensionOptions options;

        public IContainerBuilder Builder { get; set; }
        public bool AutoRegistration { get { return options.AutoRegistration; } }

        public LightCoreOptions(IBootstrapperContainerExtensionOptions options)
        {
            this.options = options;
        }

        public LightCoreOptions WithBuilder(IContainerBuilder builder)
        {
            Builder = builder;
            return this;
        }

        public IBootstrapperOption UsingAutoRegistration()
        {
            options.UsingAutoRegistration();
            return this;
        }
    }

}
