using StructureMap.Configuration.DSL;

namespace DynamicLoopIoC.Domain.IoC.Registries
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType(typeof(DomainRegistry));
                x.WithDefaultConventions();
            });
        }
    }
}
