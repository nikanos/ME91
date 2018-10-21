using CommandLine;
using CommandLine.Text;
using System;

namespace ME91Lib.Demo.CLI
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input Ecu File Name")]
        public string InputFile { get; set; }

        [Option("hightemp", DefaultValue = (byte)110, HelpText = "Highest Temperature Threshold")]
        public byte HighestTemperatureThreshold { get; set; }

        [Option("lowtemp", DefaultValue = (byte)70, HelpText = "Lowest Temperature Threshold")]
        public byte LowestTemperatureThreshold { get; set; }

        [Option("speedthreshold", DefaultValue = (UInt16)5, HelpText = "Launch Control Deactivation Speed Threshold")]
        public UInt16 SpeedThreshold { get; set; }

        [Option("rpmthreshold1", DefaultValue = (UInt16)4500, HelpText = "Launch RPM Threshold1")]
        public UInt16 RPMThreshold1 { get; set; }

        [Option("rpmthreshold2", DefaultValue = (UInt16)5000, HelpText = "Launch RPM Threshold2")]
        public UInt16 RPMThreshold2 { get; set; }

        [Option("pedalposition", DefaultValue = (UInt16)85, HelpText = "Pedal Position Value")]
        public UInt16 PedalPosition { get; set; }

        [Option("ignitioncuttime", DefaultValue = (byte)200, HelpText = "Ignition Cut Time")]
        public byte IgnitionCutTime { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
