using System;

namespace ME91Lib.Attributes
{
    class BranchIndexesAttribute : Attribute
    {
        internal BranchIndexesAttribute(int[] indexes)
        {
            Indexes = indexes;
        }
        public int[] Indexes { get; private set; }
    }
}
