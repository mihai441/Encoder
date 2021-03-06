﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace OOpBasics
{

    class PluginsManager<T>
    {
        private Assembly assembly;
        private List<T> pluginsList;

        public PluginsManager()
        {
            pluginsList = new List<T>();
        }

        public IEnumerable<T> Plugins
        {
            get
            {
                return pluginsList.AsReadOnly();
            }
        }


        public void LoadPlugins(String path)
        {
            foreach (string dll in Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
            {
                assembly = Assembly.LoadFrom(dll);
                Type pluginIfType = typeof(T);
                foreach (var assemblyType in assembly.GetExportedTypes())
                {
                    if (pluginIfType.IsAssignableFrom(assemblyType))
                    {
                        pluginsList.Add((T)Activator.CreateInstance(assemblyType));
                    }
                }
            }

        }
    }
}
