using ME91Lib.Interfaces;
using ME91Lib.Structures;

namespace ME91Lib.ParameterValueConverters
{
    class AddressValueConverter : IParameterValueConverter<Address, Address>
    {
        public Address ConvertFromInternal(Address input)
        {
            return new Address { value = input.value - 0x10 };
        }

        public Address ConvertToInternal(Address input)
        {
            return new Address { value = input.value + 0x10 };
        }
    }
}
