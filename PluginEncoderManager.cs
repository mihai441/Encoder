using System;
using System.Collections.Generic;
using System.Reflection;
using OOPBasics.Shared;
using System.IO;

namespace OOPBasics
{
    class PluginEncoderManager
    {
        private Assembly assembly;
        private List<IEncoderPlugin> pluginsList;

        public PluginEncoderManager(String path)
        {
            pluginsList = new List<IEncoderPlugin>();
            LoadPlugins(path);
        }

        public IEnumerable<IEncoderPlugin> Plugins
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
                Type pluginIfType = typeof(IEncoderPlugin);
                foreach (var assemblyType in assembly.GetExportedTypes())
                {
                    if (pluginIfType.IsAssignableFrom(assemblyType))
                    {
                        pluginsList.Add((IEncoderPlugin)Activator.CreateInstance(assemblyType));
                    }
                }
            }
        }
    }
}