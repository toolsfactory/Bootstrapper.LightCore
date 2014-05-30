using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bootstrap.LightCore;
using NUnit.Framework;
using Bootstrap;
using Bootstrap.Extensions.StartupTasks;

namespace BootStrap.LightCore.UnitTests
{
    [TestFixture]
    public class Test_Sample01
    {

        [Test]
        public void Run()
        {
            Bootstrapper.ClearExtensions();
            Bootstrapper.With.StartupTasks().And.LightCore().IncludingOnly.Assembly(Assembly.GetExecutingAssembly()).Start();

            var container = (global::LightCore.IContainer)Bootstrapper.Container;
            var list = container.ResolveAll();
            var item = Bootstrapper.ContainerExtension.Resolve<ISample01>();
            Assert.IsTrue(list.Count() > 0);
        }
    }

    public class Sample01Registration : ILightCoreRegistration
    {
        public void Register(global::LightCore.IContainerBuilder builder)
        {
            builder.Register<ISample01, Sample01>();
        }
    }

    public interface ISample01
    {
        void Hello();
    }

    public class Sample01 : ISample01
    {
        public void Hello()
        {
        }
    }
}
