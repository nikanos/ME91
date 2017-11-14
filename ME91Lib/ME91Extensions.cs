using ME91Lib.Attributes;
using ME91Lib.Enumerations;
using MiscUtil.Conversion;
using System;
using System.Linq;

namespace ME91Lib
{
    public static class ME91Extensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            return type.GetField(name)
                       .GetCustomAttributes(false)
                       .OfType<TAttribute>()
                       .SingleOrDefault();
        }

        public static int IndexOf(this byte[] array, byte byteToFind)
        {
            return Utilities.IndexOf(array, byteToFind);
        }

        public static int LastIndexOf(this byte[] array, byte byteToFind)
        {
            return Utilities.LastIndexOf(array, byteToFind);
        }

        public static int IndexOf(this byte[] array, byte[] patternToFind)
        {
            return Utilities.IndexOf(array, patternToFind);
        }

        public static int LastIndexOf(this byte[] array, byte[] patternToFind)
        {
            return Utilities.LastIndexOf(array, patternToFind);
        }

        public static void CopyBytesGeneric<T>(this EndianBitConverter converter, T t, byte[] buffer, int index)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Byte:
                    buffer[index] = (byte)Convert.ChangeType(t, typeof(byte));
                    break;
                case TypeCode.Int16:
                    converter.CopyBytes((Int16)Convert.ChangeType(t, typeof(Int16)), buffer, index);
                    break;
                case TypeCode.Int32:
                    converter.CopyBytes((Int32)Convert.ChangeType(t, typeof(Int32)), buffer, index);
                    break;
                case TypeCode.UInt16:
                    converter.CopyBytes((UInt16)Convert.ChangeType(t, typeof(UInt16)), buffer, index);
                    break;
                case TypeCode.UInt32:
                    converter.CopyBytes((UInt32)Convert.ChangeType(t, typeof(UInt32)), buffer, index);
                    break;
                case TypeCode.Object:
                    if (t is Structures.Address)
                    {
                        Structures.Address address = (Structures.Address)Convert.ChangeType(t, typeof(Structures.Address));
                        byte[] addressBytes = converter.GetBytes(address.value);
                        buffer[index + 1] = (byte)((buffer[index + 1] & 0xf0) | (addressBytes[1] & 0x0f));
                        buffer[index + 2] = addressBytes[2];
                        buffer[index + 3] = addressBytes[3];
                        break;
                    }
                    if (t is Structures.BranchInstruction)
                    {
                        Structures.BranchInstruction branchInstruction = (Structures.BranchInstruction)Convert.ChangeType(t, typeof(Structures.BranchInstruction));
                        byte[] instructionBytes = converter.GetBytes(branchInstruction.value);
                        buffer[index + 0] = Constants.BRANCH_MNEMONIC;
                        buffer[index + 1] = instructionBytes[1];
                        buffer[index + 2] = instructionBytes[2];
                        buffer[index + 3] = instructionBytes[3];
                        break;
                    }
                    throw new ApplicationException("CopyBytesGeneric<T>() unsupported type");
                default:
                    throw new ApplicationException("CopyBytesGeneric<T>() unsupported type");
            }
        }

        public static int GetIndex(this ParameterType value)
        {
            var parameterIndex = value.GetAttribute<ParameterIndexAttribute>();
            if (parameterIndex == null)
                throw new ApplicationException(string.Format("{0} did not specify a parameter index", value.ToString()));
            return parameterIndex.Index;
        }

        public static int[] GetBranchIndexes(this ParameterType value)
        {
            var parameterIndex = value.GetAttribute<BranchIndexesAttribute>();
            if (parameterIndex == null)
                throw new ApplicationException(string.Format("{0} did not specify any branch index", value.ToString()));
            return parameterIndex.Indexes;
        }

        public static int GetBranchOffset(this ParameterType value)
        {
            var branchOffset = value.GetAttribute<BranchOffsetAttribute>();
            if (branchOffset == null)
                return 0;
            return branchOffset.Offset;
        }
    }
}
