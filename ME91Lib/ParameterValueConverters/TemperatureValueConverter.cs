using ME91Lib.Interfaces;
using System;

namespace ME91Lib.ParameterValueConverters
{
    class TemperatureValueConverter : IParameterValueConverter<Byte, Byte>
    {
        public Byte ConvertFromInternal(Byte input)
        {
            checked
            {
                return (Byte)(Math.Round((input * 0.75) - 48));
            }
        }

        public Byte ConvertToInternal(Byte input)
        {
            checked
            {
                return (Byte)(Math.Round((input + 48.0) / 0.75));
            }
        }
    }
}
