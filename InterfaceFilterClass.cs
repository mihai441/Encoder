using System;
using System.Reflection;

namespace OOPBasics
{
    class InterfaceFilterClass
    {
        private TypeFilter interfacefilter;

        public InterfaceFilterClass()
        {
            interfacefilter = new TypeFilter(InterfaceFilter);
        }

        public int getInterfaces(Type type,String nameOfInterface)
        {
            var myInterfaces = type.FindInterfaces(InterfaceFilter, nameOfInterface);
            return (int)myInterfaces.Length;
        }

        private bool InterfaceFilter(Type m, object filterCriteria)
        {
            return m.ToString() == filterCriteria.ToString();
        }
    }
}
