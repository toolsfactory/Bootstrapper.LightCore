using System;
using System.Collections.Generic;
using System.Linq;
using LightCore;

namespace Bootstrap.LightCore
{
    public interface ILightCoreRegistration
    {
        void Register(IContainerBuilder builder);
    }
}
