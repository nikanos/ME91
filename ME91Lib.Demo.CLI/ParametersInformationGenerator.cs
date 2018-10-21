using System.Text;

namespace ME91Lib.Demo.CLI
{
    class ParametersInformationGenerator
    {
        private enum ValueFormat
        {
            Unformatted,
            Hexadecimal
        }

        public static string GenerateInformation(InjectCode injectCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(PrintParameter(injectCode.TmotlinCoolantTemperatureAddress, "TmotlinCoolantTemperatureAddress", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.HighestTemperatureThreshold, "HighestTemperatureThreshold"));
            sb.AppendLine(PrintParameter(injectCode.LowestTemperatureThreshold, "LowestTemperatureThreshold"));
            sb.AppendLine(PrintParameter(injectCode.B_kupplClutchPedalSwitchAddress, "B_kupplClutchPedalSwitchAddress", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Vfil_wVehicleSpeedAddress, "Vfil_wVehicleSpeedAddress", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.LaunchControlDeactivationSpeedThreshold, "LaunchControlDeactivationSpeedThreshold"));
            sb.AppendLine(PrintParameter(injectCode.Nmot_wEngineSpeedAddress1, "Nmot_wEngineSpeedAddress1", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.LaunchRPMThreshold1, "LaunchRPMThreshold1"));
            sb.AppendLine(PrintParameter(injectCode.B_bremsBrakePedalSwitchSddress, "B_bremsBrakePedalSwitchSddress", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Nmot_wEngineSpeedAddress2, "Nmot_wEngineSpeedAddress2", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.LaunchRPMThreshold2, "LaunchRPMThreshold2"));
            sb.AppendLine(PrintParameter(injectCode.Wped_wAcceleratorPedalPositionAddress, "Wped_wAcceleratorPedalPositionAddress", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.PedalPositionValue, "PedalPositionValue"));
            sb.AppendLine(PrintParameter(injectCode.IgnitionCutTime, "IgnitionCutTime"));
            sb.AppendLine(PrintParameter(injectCode.Branch1Address1, "Branch1Address1", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Branch1Address2, "Branch1Address2", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Branch1Address3, "Branch1Address3", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Branch1Address4, "Branch1Address4", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Szfuba_w, "Szfuba_w", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.Szfuba_w_BranchToCode, "Szfuba_w_BranchToCode", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.CdmdForMisfireDetection, "CdmdForMisfireDetection", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.CdmdForMisfireDetection_BranchToCode1, "CdmdForMisfireDetection_BranchToCode1", ValueFormat.Hexadecimal));
            sb.AppendLine(PrintParameter(injectCode.CdmdForMisfireDetection_BranchToCode2, "CdmdForMisfireDetection_BranchToCode2", ValueFormat.Hexadecimal));

            return sb.ToString();
        }

        private static string PrintParameter<T>(T param, string paramName, ValueFormat valueFormat)
        {
            string parameterValueFormat = string.Empty;
            switch (valueFormat)
            {
                case ValueFormat.Unformatted:
                    parameterValueFormat = "{1}";
                    break;
                case ValueFormat.Hexadecimal:
                    parameterValueFormat = "0x{1:X}";
                    break;
            }
            return string.Format("{0}: " + parameterValueFormat, paramName, param);
        }

        private static string PrintParameter<T>(T param, string paramName)
        {
            return PrintParameter(param, paramName, ValueFormat.Unformatted);
        }
    }
}
