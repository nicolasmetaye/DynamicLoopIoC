using System;
using System.Linq;
using AutoMapper;
using StructureMap;

namespace DynamicLoopIoC.Models.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var container = ObjectFactory.Container;
            Mapper.Initialize(configuration =>
                                  {
                                      configuration.ConstructServicesUsing(container.GetInstance);
                                      var profiles = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
                                      foreach (var profile in profiles)
                                      {
                                          var instance = container.TryGetInstance(profile);
                                          if (instance == null)
                                          {
                                              container.Configure(c => c.AddType(profile, profile));
                                              instance = container.TryGetInstance(profile);
                                          }
                                          configuration.AddProfile(instance as Profile);
                                      }

                                  });
        }
    }
}