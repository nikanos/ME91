using System;

namespace ME91Lib.Attributes
{
    class ParameterIndexAttribute : Attribute
    {
        internal ParameterIndexAttribute(int index)
        {
            Index = index;
        }
        public int Index { get; private set; }
    }
}
