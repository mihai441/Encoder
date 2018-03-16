using System;
using System.Collections.Generic;
using System.Reflection;
using OOPBasics.Shared;
using System.IO;

namespace OOPBasics
{
    class PluginsManager
    {
        private Assembly assembly;
        private List<IPlugin> pluginsList;
        private readonly String pluginIfName = "IPlugin";

        private Type type;

        public PluginsManager(String path)
        {
            pluginsList = new List<IPlugin>();
            LoadPlugins(path);
        }

        public IEnumerable<IPlugin> Plugins
        {
            get
            {
                return pluginsList.AsReadOnly();
            }
        }


        private void LoadPlugins(String path)
        {

            //get dll files
            //foreach file in the path load assembly
            foreach (string dll in Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
            {
                assembly = Assembly.LoadFrom(dll);
                Type pluginIfType = typeof(IPlugin);
                foreach (var assemblyType in assembly.GetExportedTypes())
                {
                    if (pluginIfType.IsAssignableFrom(assemblyType))
                    {
                        pluginsList.Add((IPlugin)Activator.CreateInstance(assemblyType));
                    }
                }
            }
        }
    }
}