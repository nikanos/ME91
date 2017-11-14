using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.ParameterValueConverters;
using ME91Lib.Structures;
using MiscUtil.Conversion;
using System;
using System.Collections.Generic;

namespace ME91Lib
{
    class BranchParameter : IParameter<Address>
    {
        private BigEndianBitConverter byteConverter = new BigEndianBitConverter();
        private ParameterType parameterType;
        private ICode code;
        private int indexInCode;
        private int indexInEcu;
        private int[] branchIndexesInCode;
        private int branchOffsetInCode;
        private IParameterValueConverter<Address, Address> converter;
        private ISearchParameterLocator searchParameterLocator;
        private bool valueRead = false;

        public BranchParameter(ParameterType parameterType, ICode code, int indexInCode, int[] branchIndexesInCode, int branchOffsetInCode, ISearchParameterLocator searchParameterLocator)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            this.parameterType = parameterType;
            this.code = code;
            this.indexInCode = indexInCode;
            this.branchIndexesInCode = branchIndexesInCode;
            this.branchOffsetInCode = branchOffsetInCode;
            this.searchParameterLocator = searchParameterLocator;
            this.converter = new AddressValueConverter();
        }

        public void PatchEcu(ICode ecuCode)
        {
            if (ecuCode == null)
                throw new ArgumentNullException("ecuCode");

            if (!valueRead)
                ReadValue();

            int calculatedAddress = (Constants.INJECT_CODE_ADDRESS + branchOffsetInCode) - indexInEcu;
            BranchInstruction branchInstruction = new BranchInstruction();
            branchInstruction.value = (UInt32)calculatedAddress;
            byteConverter.CopyBytesGeneric(branchInstruction, ecuCode.CodeBytes, indexInEcu);
        }

        public ParameterType ParameterType
        {
            get { return parameterType; }
        }

        public Address Value
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

        public IEnumerable<BranchInstruction> BranchInstructions
        {
            get
            {
                if (!valueRead)
                    ReadValue();
                List<BranchInstruction> branchInstructions = new List<BranchInstruction>();
                foreach (int branchIndexInCode in branchIndexesInCode)
                    branchInstructions.Add(new BranchInstruction { value = byteConverter.ToUInt32(code.CodeBytes, branchIndexInCode) });
                return branchInstructions;
            }
        }

        private Address ReadValue()
        {
            if (!valueRead)
            {
                int foundIndex;
                Address foundValue = this.searchParameterLocator.Locate<Address>(parameterType, out foundIndex);
                indexInEcu = foundIndex;
                byteConverter.CopyBytesGeneric(foundValue, code.CodeBytes, indexInCode);
                foreach (int branchIndex in branchIndexesInCode)
                {
                    int calculatedAddress = (foundIndex + Constants.BRANCH_INSTRUCTION_SIZE) - (Constants.INJECT_CODE_ADDRESS + branchIndex);
                    Address branchAddress = new Address();
                    branchAddress.value = (UInt32)calculatedAddress;
                    byteConverter.CopyBytesGeneric(branchAddress, code.CodeBytes, branchIndex);
                }
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

            Address value = default(Address);
            value = (Address)Convert.ChangeType(typeMapping[typeof(Address)](), typeof(Address));
            return converter.ConvertFromInternal(value);
        }
    }
}
