using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Encoder
{
    class DynamicLoader
    {
        private Assembly assembly;
        private List<Object> ModulesList;


        public DynamicLoader(String locationn, Type type)
        {
            Assembly assembly = Assembly.LoadFrom("MyNice.dll");
            ModulesList = new List<>();
            

        }




    }
}
