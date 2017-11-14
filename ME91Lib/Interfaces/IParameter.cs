using ME91Lib.Enumerations;
using System;

namespace ME91Lib.Interfaces
{
    interface IParameter
    {
        Object Value { get; set; }
    }

    interface IParameter<T> : IParameter
        where T : struct
    {
        ParameterType ParameterType { get; }
        new T Value { get; set; }
    }
}
