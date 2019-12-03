using BLL.Interface;
using BLL.Services;
using Ninject.Modules;

namespace UrlShorted.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUrlMdl>().To<UrlMdlService>();
        }
    }
}