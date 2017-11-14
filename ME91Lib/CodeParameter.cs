using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.ParameterValueConverters;
using ME91Lib.Structures;
using MiscUtil.Conversion;
using System;
using System.Collections.Generic;

namespace ME91Lib
{
    class CodeParameter<InternalRepresentation, HumanRepresentation> : IParameter<HumanRepresentation>
        where InternalRepresentation : struct
        where HumanRepresentation : struct
    {
        private BigEndianBitConverter byteConverter = new BigEndianBitConverter();
        private ParameterType parameterType;
        private ICode code;
        private int indexInCode;
        private IParameterValueConverter<InternalRepresentation, HumanRepresentation> converter;

        public CodeParameter(ParameterType parameterType, ICode code, int indexInCode, IParameterValueConverter<InternalRepresentation, HumanRepresentation> converter)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            if (converter == null)
                throw new ArgumentNullException("converter");

            this.parameterType = parameterType;
            this.code = code;
            this.indexInCode = indexInCode;
            this.converter = converter;
        }

        public CodeParameter(ParameterType parameterType, ICode code, int indexInCode) :
            this(parameterType, code, indexInCode, new DefaultValueConverter<InternalRepresentation, HumanRepresentation>())
        {
        }


        public ParameterType ParameterType
        {
            get { return parameterType; }
        }

        public HumanRepresentation Value
        {
            get
            {
                return ReadValue();
            }
            set
            {
                SetValue(value);
            }
        }

        private HumanRepresentation ReadValue()
        {
            var typeMapping = new Dictionary<Type, Func<Object>>
            {
                {typeof(byte),()=>code.CodeBytes[indexInCode]},
                {typeof(Int16),()=>byteConverter.ToInt16(code.CodeBytes,indexInCode)},
                {typeof(Int32),()=>byteConverter.ToInt32(code.CodeBytes,indexInCode)},
                {typeof(UInt16),()=>byteConverter.ToUInt16(code.CodeBytes,indexInCode)},
                {typeof(UInt32),()=>byteConverter.ToUInt32(code.CodeBytes,indexInCode)},
                {typeof(Address),()=>{
                    byte[] addressBytes = new byte[4];
                    addressBytes[0] = 0;
                    addressBytes[1] = (byte)(code.CodeBytes[indexInCode + 1] & 0x0f);
                    addressBytes[2] = code.CodeBytes[indexInCode + 2];
                    addressBytes[3] = code.CodeBytes[indexInCode + 3];
                    return new Address { value = byteConverter.ToUInt32(addressBytes, 0) };
                }}
            };

            InternalRepresentation value = default(InternalRepresentation);
            value = (InternalRepresentation)Convert.ChangeType(typeMapping[typeof(InternalRepresentation)](), typeof(InternalRepresentation));
            return converter.ConvertFromInternal(value);
        }

        private void SetValue(HumanRepresentation value)
        {
            InternalRepresentation valueToSave = converter.ConvertToInternal(value);
            byteConverter.CopyBytesGeneric(valueToSave, code.CodeBytes, indexInCode);
        }

        object IParameter.Value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = (HumanRepresentation)value;
            }
        }
    }
}
