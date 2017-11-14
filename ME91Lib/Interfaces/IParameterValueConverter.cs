namespace ME91Lib.Interfaces
{
    interface IParameterValueConverter<InternalRepresentation,HumanRepresentation>
    {
        HumanRepresentation ConvertFromInternal(InternalRepresentation input);
        InternalRepresentation ConvertToInternal(HumanRepresentation input);
    }
}
