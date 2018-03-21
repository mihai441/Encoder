using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Encoder
{
    class PluginsManager
    {
        private Assembly assembly;
        private Type typeOfInterface;
        private List<object> ModulesList;

        public PluginsManager(String name, Type type)
        {
            Assembly assembly = Assembly.LoadFrom(name + ".dll");
            typeOfInterface = type;
        }

        public List<object> GetInstancesList()
        {
            foreach (var assemblyTypes in assembly.GetTypes())
            {
                if (typeof(assemblyTypes).GetInterfaces().Contains(typeof(typeOfInterface)))
                {
                    ModulesList.Add(Activator.CreateInstance(assemblyTypes));
                }                

            }
            return ModulesList;
        }






    }
}
