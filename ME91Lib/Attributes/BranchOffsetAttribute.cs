using System;

namespace ME91Lib.Attributes
{
    class BranchOffsetAttribute : Attribute
    {
        internal BranchOffsetAttribute(int offset)
        {
            Offset = offset;
        }
        public int Offset { get; private set; }
    }
}
