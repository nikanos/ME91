using ME91Lib.Interfaces;
using System;

namespace ME91Lib.ParameterValueConverters
{
    class SpeedValueConverter : IParameterValueConverter<UInt16, UInt16>
    {
        public UInt16 ConvertFromInternal(UInt16 input)
        {
            checked
            {
                return (UInt16)(Math.Round((input / 128.0)));
            }    
        }

        public UInt16 ConvertToInternal(UInt16 input)
        {
            checked
            {
                return (UInt16)(input * 128);
            }
        }
    }
}
