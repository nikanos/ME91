using ME91Lib.Enumerations;

namespace ME91Lib.Interfaces
{
    interface ISearchParameterLocator
    {
        T Locate<T>(ParameterType parameterType);
        T Locate<T>(ParameterType parameterType, out int index);
    }
}
