using ME91Lib.Attributes;

namespace ME91Lib.Enumerations
{
    public enum ParameterType
    {
        Undefined,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 1 + 4)]
        TmotlinCoolantTemperatureAddress,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 1 + 11)]
        HighestTemperatureThreshold,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 2 + 3)]
        LowestTemperatureThreshold,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 2 + 8)]
        B_kupplClutchPedalSwitchAddress,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 3 + 4)]
        Vfil_wVehicleSpeedAddress,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 3 + 10)]
        LaunchControlDeactivationSpeedThreshold,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 4 + 0)]
        Nmot_wEngineSpeedAddress1,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 4 + 6)]
        LaunchRPMThreshold1,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 5 + 8)]
        B_bremsBrakePedalSwitchSddress,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 6 + 4)]
        Nmot_wEngineSpeedAddress2,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 6 + 10)]
        LaunchRPMThreshold2,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 7 + 0)]
        Wped_wAcceleratorPedalPositionAddress,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 7 + 6)]
        PedalPositionValue,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 8 + 3)]
        IgnitionCutTime,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 11 + 8)]
        Branch1Address1,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 11 + 12)]
        Branch1Address2,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 12 + 0)]
        Branch1Address3,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 12 + 4)]
        Branch1Address4,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 12 + 8)]
        [BranchIndexes(new int[] { Constants.CODE_ROW_SIZE * 12 + 12 })]
        Szfuba_w,

        [ParameterIndex(Constants.CODE_ROW_SIZE * 14 + 12)]
        [BranchIndexes(new int[] { Constants.CODE_ROW_SIZE * 15 + 0, Constants.CODE_ROW_SIZE * 15 + 8 })]
        [BranchOffset(Constants.BRANCH_INSRUCTION_OFFSET_CDMDFORMISFIREDETECTION)]
        CdmdForMisfireDetection
    }
}
