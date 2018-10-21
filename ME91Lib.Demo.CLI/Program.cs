using System;
using System.IO;

namespace ME91Lib.Demo.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                try
                {
                    EcuCode ecuCode = new EcuCode(options.InputFile);
                    InjectCode injectCode = new InjectCode(ecuCode)
                    {
                        HighestTemperatureThreshold = options.HighestTemperatureThreshold,
                        LowestTemperatureThreshold = options.LowestTemperatureThreshold,
                        LaunchControlDeactivationSpeedThreshold = options.SpeedThreshold,
                        LaunchRPMThreshold1 = options.RPMThreshold1,
                        LaunchRPMThreshold2 = options.RPMThreshold2,
                        PedalPositionValue = options.PedalPosition,
                        IgnitionCutTime = options.IgnitionCutTime
                    };

                    string parametersInformation = ParametersInformationGenerator.GenerateInformation(injectCode);
                    Console.WriteLine(parametersInformation);
                    injectCode.InjectEcu();
                    injectCode.PatchEcu();
                    File.WriteAllBytes("out.bin", ecuCode.CodeBytes);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("An error occured:");
                    Console.Error.Write(ex.ToString());
                }

            }
        }
    }
}
