using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Extensions;
using Bootstrap.Extensions.Containers;
using LightCore;

namespace Bootstrap.LightCore
{
    public static class LightCoreConvenienceExtensions
    {
        public static LightCoreOptions LightCore(this BootstrapperExtensions extensions)
        {
            var extension = new LightCoreExtension(Bootstrapper.RegistrationHelper, new BootstrapperContainerExtensionOptions());
            extensions.Extension(extension);
            return extension.Options;
        }
    }
}
