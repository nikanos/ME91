using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.ParameterValueConverters;
using ME91Lib.Structures;
using MiscUtil.Conversion;
using System;
using System.Collections.Generic;

namespace ME91Lib
{
    class SearchParameter<InternalRepresentation, HumanRepresentation> : IParameter<HumanRepresentation>
        where InternalRepresentation : struct
        where HumanRepresentation : struct
    {
        public delegate InternalRepresentation TransformFoundValueDelegate(InternalRepresentation input);

        private BigEndianBitConverter byteConverter = new BigEndianBitConverter();
        private ParameterType parameterType;
        private ICode code;
        private int indexInCode;
        private IParameterValueConverter<InternalRepresentation, HumanRepresentation> converter;
        private ISearchParameterLocator searchParameterLocator;
        private bool valueRead = false;
        private TransformFoundValueDelegate transformFoundValue;

        public SearchParameter(ParameterType parameterType, ICode code, int indexInCode, ISearchParameterLocator searchParameterLocator, IParameterValueConverter<InternalRepresentation, HumanRepresentation> converter)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            if (converter == null)
                throw new ArgumentNullException("converter");

            this.parameterType = parameterType;
            this.code = code;
            this.indexInCode = indexInCode;
            this.searchParameterLocator = searchParameterLocator;
            this.converter = converter;
        }

        public SearchParameter(ParameterType parameterType, ICode injectCode, int indexInCode, ISearchParameterLocator searchParameterLocator) :
            this(parameterType, injectCode, indexInCode, searchParameterLocator, new DefaultValueConverter<InternalRepresentation, HumanRepresentation>())
        {
        }

        public ParameterType ParameterType
        {
            get { return parameterType; }
        }

        public TransformFoundValueDelegate TransformFoundValue
        {
            get { return transformFoundValue; }
            set { transformFoundValue = value; }
        }

        public HumanRepresentation Value
        {
            get
            {
                return ReadValue();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private HumanRepresentation ReadValue()
        {
            if (!valueRead)
            {
                InternalRepresentation foundValue = this.searchParameterLocator.Locate<InternalRepresentation>(parameterType);

                //Do we need transformation?
                if (transformFoundValue != null)
                    foundValue = transformFoundValue(foundValue);

                byteConverter.CopyBytesGeneric(foundValue, code.CodeBytes, indexInCode);
                valueRead = true;
            }

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

        object IParameter.Value
        {
            get
            {
                return Value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
