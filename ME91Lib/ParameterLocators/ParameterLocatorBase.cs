using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.Structures;
using MiscUtil.Conversion;
using System;
using System.Collections.Generic;

namespace ME91Lib.ParameterLocators
{
    abstract class ParameterLocatorBase : ISearchParameterLocator
    {
        private BigEndianBitConverter byteConverter = new BigEndianBitConverter();
        private ICode ecuCode;

        protected ParameterLocatorBase(ICode ecuCode)
        {
            this.ecuCode = ecuCode;
        }

        abstract public T Locate<T>(ParameterType parameterType);
        abstract public T Locate<T>(ParameterType parameterType, out int index);

        protected T LocateHelper<T>(ParameterType parameterType, byte[] searchPattern, int parameterLocationOffset, ParameterOffsetDirection parameterOffsetDirection, out int index)
        {
            int foundIndex = ecuCode.CodeBytes.IndexOf(searchPattern);
            if (foundIndex == -1)
                throw new ApplicationException("Parameter " + parameterType.ToString() + " not found!");

            if (parameterOffsetDirection == ParameterOffsetDirection.Before)
                foundIndex = foundIndex - parameterLocationOffset;
            else
                foundIndex = foundIndex + (searchPattern.Length - 1) + parameterLocationOffset;

            if (foundIndex < 0 || foundIndex >= ecuCode.CodeBytes.Length)
                throw new ApplicationException("Parameter " + parameterType.ToString() + " invalid index");

            var typeMapping = new Dictionary<Type, Func<Object>>
            {
                {typeof(byte),()=>ecuCode.CodeBytes[foundIndex]},
                {typeof(Int16),()=>byteConverter.ToInt16(ecuCode.CodeBytes,foundIndex)},
                {typeof(Int32),()=>byteConverter.ToInt32(ecuCode.CodeBytes,foundIndex)},
                {typeof(UInt16),()=>byteConverter.ToUInt16(ecuCode.CodeBytes,foundIndex)},
                {typeof(UInt32),()=>byteConverter.ToUInt32(ecuCode.CodeBytes,foundIndex)},
                {typeof(Address),()=>{
                    byte[] addressBytes = new byte[4];
                    addressBytes[0] = 0;
                    addressBytes[1] = (byte)(ecuCode.CodeBytes[foundIndex + 1] & 0x0f);
                    addressBytes[2] = ecuCode.CodeBytes[foundIndex + 2];
                    addressBytes[3] = ecuCode.CodeBytes[foundIndex + 3];
                    return new Address { value = byteConverter.ToUInt32(addressBytes, 0) };
                }}
            };

            T value = default(T);
            value = (T)Convert.ChangeType(typeMapping[typeof(T)](), typeof(T));
            index = foundIndex;
            return value;
        }
    }
}
