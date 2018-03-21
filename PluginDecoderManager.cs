using System;
using System.Collections.Generic;
using System.Reflection;
using OOPBasics.Shared;
using System.IO;

namespace OOPBasics
{
    class PluginDecoderManager
    {
        private Assembly assembly;
        private List<IDecoderPlugin> pluginsList;

        public PluginDecoderManager(String path)
        {
            pluginsList = new List<IDecoderPlugin>();
            LoadPlugins(path);
        }

        public IEnumerable<IDecoderPlugin> Plugins
        {
            get
            {
                return pluginsList.AsReadOnly();
            }
        }


        private void LoadPlugins(String path)
        {
            foreach (string dll in Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
            {
                assembly = Assembly.LoadFrom(dll);
                Type pluginIfType = typeof(IDecoderPlugin);
                foreach (var assemblyType in assembly.GetExportedTypes())
                {
                    if (pluginIfType.IsAssignableFrom(assemblyType))
                    {
                        pluginsList.Add((IDecoderPlugin)Activator.CreateInstance(assemblyType));
                    }
                }
            }
        }
    }
}