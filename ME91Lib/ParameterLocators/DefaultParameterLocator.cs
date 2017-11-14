using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using System;

namespace ME91Lib.ParameterLocators
{
    class DefaultParameterLocator : ParameterLocatorBase
    {
        public DefaultParameterLocator(ICode ecuCode)
            : base(ecuCode)
        {
        }
        public override T Locate<T>(ParameterType parameterType)
        {
            int unused;
            return Locate<T>(parameterType, out unused);
        }

        //TD -> Use the correct search patterns and offsets for the parameters
        public override T Locate<T>(ParameterType parameterType, out int index)
        {
            switch (parameterType)
            {
                case ParameterType.TmotlinCoolantTemperatureAddress:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.B_kupplClutchPedalSwitchAddress:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Vfil_wVehicleSpeedAddress:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Nmot_wEngineSpeedAddress1:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Nmot_wEngineSpeedAddress2:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.B_bremsBrakePedalSwitchSddress:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Wped_wAcceleratorPedalPositionAddress:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Branch1Address1:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Branch1Address2:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Branch1Address3:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Branch1Address4:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.Szfuba_w:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
                case ParameterType.CdmdForMisfireDetection:
                    return LocateHelper<T>(parameterType, new byte[] { 0x0a, 0x0b }, 1, ParameterOffsetDirection.After, out index);
            }
            throw new ApplicationException("Unhandled parameter type " + parameterType.ToString());
        }
    }
}
