using System;
using System.Web.Mvc;
using StructureMap;

namespace DynamicLoopIoC.IoC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)ObjectFactory.GetInstance(controllerType);
        }
    }
}