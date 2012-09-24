using DynamicLoopIoC.Domain.IoC.Registries;
using StructureMap;

namespace DynamicLoopIoC.IoC
{
    public static class ContainerBootStrap
    {
        public static void Bootstrap()
        {
            ObjectFactory.Configure(c => c.Scan(x =>
            {
                x.AssemblyContainingType<DomainRegistry>();
                x.LookForRegistries();
            }));
        }
    }
}