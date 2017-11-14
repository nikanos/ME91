using ME91Lib.Interfaces;
using System;

namespace ME91Lib.ParameterValueConverters
{
    class DefaultValueConverter<InternalRepresentation, HumanRepresentation> : IParameterValueConverter<InternalRepresentation, HumanRepresentation>
    {
        public HumanRepresentation ConvertFromInternal(InternalRepresentation input)
        {
            return (HumanRepresentation)Convert.ChangeType(input, typeof(HumanRepresentation));
        }

        public InternalRepresentation ConvertToInternal(HumanRepresentation input)
        {
            return (InternalRepresentation)Convert.ChangeType(input, typeof(InternalRepresentation));
        }
    }
}
