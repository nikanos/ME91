using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.ParameterLocators;
using ME91Lib.ParameterValueConverters;
using ME91Lib.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ME91Lib
{
    public class InjectCode : ICode
    {
        private byte[] codeBytes;
        private Dictionary<ParameterType, IParameter> parameters;
        private ISearchParameterLocator searchParameterLocator;
        private ICode ecuCode;

        public InjectCode(ICode ecuCode)
        {
            this.ecuCode = ecuCode;
            searchParameterLocator = new DefaultParameterLocator(ecuCode);
            InitializeCodeBytes();
            InitializeParameters();
        }

        //TD -> Use the correct injection code
        private void InitializeCodeBytes()
        {
            List<byte> bytes = new List<byte>();

            //Line 1
            bytes.AddRange(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 });

            //Line 2
            bytes.AddRange(new byte[] { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20 });

            //Line 3
            bytes.AddRange(new byte[] { 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30 });

            //Line 4
            bytes.AddRange(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F, 0x40 });

            //Line 5
            bytes.AddRange(new byte[] { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50 });

            //Line 6
            bytes.AddRange(new byte[] { 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F, 0x60 });

            //Line 7
            bytes.AddRange(new byte[] { 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70 });

            //Line 8
            bytes.AddRange(new byte[] { 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F, 0x80 });

            //Line 9
            bytes.AddRange(new byte[] { 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F, 0x90 });

            //Line 10
            bytes.AddRange(new byte[] { 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F, 0xA0 });

            //Line 11
            bytes.AddRange(new byte[] { 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF, 0xB0 });

            //Line 12
            bytes.AddRange(new byte[] { 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF, 0xC0 });

            //Line 13
            bytes.AddRange(new byte[] { 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF, 0xD0 });

            //Line 14
            bytes.AddRange(new byte[] { 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF, 0xE0 });

            //Line 15
            bytes.AddRange(new byte[] { 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF, 0xF0 });

            //Line 16
            bytes.AddRange(new byte[] { 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC });

            codeBytes = bytes.ToArray();
        }

        private void InitializeParameters()
        {
            parameters = new Dictionary<ParameterType, IParameter>();

            parameters.Add(ParameterType.TmotlinCoolantTemperatureAddress,
                           new SearchParameter<Address, Address>(ParameterType.TmotlinCoolantTemperatureAddress,
                                                                 this,
                                                                 ParameterType.TmotlinCoolantTemperatureAddress.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter()));

            parameters.Add(ParameterType.HighestTemperatureThreshold,
                           new CodeParameter<Byte, Byte>(ParameterType.HighestTemperatureThreshold,
                                                         this,
                                                         ParameterType.HighestTemperatureThreshold.GetIndex(),
                                                         new TemperatureValueConverter()));

            parameters.Add(ParameterType.LowestTemperatureThreshold,
                           new CodeParameter<Byte, Byte>(ParameterType.LowestTemperatureThreshold,
                                                         this,
                                                         ParameterType.LowestTemperatureThreshold.GetIndex(),
                                                         new TemperatureValueConverter()));

            parameters.Add(ParameterType.B_kupplClutchPedalSwitchAddress,
                           new SearchParameter<Address, Address>(ParameterType.B_kupplClutchPedalSwitchAddress,
                                                                 this,
                                                                 ParameterType.B_kupplClutchPedalSwitchAddress.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter()));

            parameters.Add(ParameterType.Vfil_wVehicleSpeedAddress,
                           new SearchParameter<Address, Address>(ParameterType.Vfil_wVehicleSpeedAddress,
                                                                 this,
                                                                 ParameterType.Vfil_wVehicleSpeedAddress.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter()));

            parameters.Add(ParameterType.LaunchControlDeactivationSpeedThreshold,
                           new CodeParameter<UInt16, UInt16>(ParameterType.LaunchControlDeactivationSpeedThreshold,
                                                                 this,
                                                                 ParameterType.LaunchControlDeactivationSpeedThreshold.GetIndex(),
                                                                 new SpeedValueConverter()));

            parameters.Add(ParameterType.Nmot_wEngineSpeedAddress1,
                           new SearchParameter<Address, Address>(ParameterType.Nmot_wEngineSpeedAddress1,
                                                                 this,
                                                                 ParameterType.Nmot_wEngineSpeedAddress1.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter()));

            parameters.Add(ParameterType.LaunchRPMThreshold1,
                           new CodeParameter<UInt16, UInt16>(ParameterType.LaunchRPMThreshold1,
                                                                 this,
                                                                 ParameterType.LaunchRPMThreshold1.GetIndex(),
                                                                 new RpmValueConverter()));

            var b_brems = new SearchParameter<Address, Address>(ParameterType.B_bremsBrakePedalSwitchSddress,
                                                                 this,
                                                                 ParameterType.B_bremsBrakePedalSwitchSddress.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter());
            //b_brems = B_kuppl - 3
            b_brems.TransformFoundValue = a => new Address { value = a.value - 3 };
            parameters.Add(ParameterType.B_bremsBrakePedalSwitchSddress, b_brems);

            parameters.Add(ParameterType.Nmot_wEngineSpeedAddress2,
                           new SearchParameter<Address, Address>(ParameterType.Nmot_wEngineSpeedAddress2,
                                                                 this,
                                                                 ParameterType.Nmot_wEngineSpeedAddress2.GetIndex(),
                                                                 searchParameterLocator,
                                                                 new AddressValueConverter()));

            parameters.Add(ParameterType.LaunchRPMThreshold2,
                           new CodeParameter<UInt16, UInt16>(ParameterType.LaunchRPMThreshold2,
                                                                 this,
                                                                 ParameterType.LaunchRPMThreshold2.GetIndex(),
                                                                 new RpmValueConverter()));

            parameters.Add(ParameterType.Wped_wAcceleratorPedalPositionAddress,
                          new SearchParameter<Address, Address>(ParameterType.Wped_wAcceleratorPedalPositionAddress,
                                                                this,
                                                                ParameterType.Wped_wAcceleratorPedalPositionAddress.GetIndex(),
                                                                searchParameterLocator,
                                                                new AddressValueConverter()));

            parameters.Add(ParameterType.PedalPositionValue,
                           new CodeParameter<UInt16, UInt16>(ParameterType.PedalPositionValue,
                                                                 this,
                                                                 ParameterType.PedalPositionValue.GetIndex(),
                                                                 new PositionValueConverter()));

            parameters.Add(ParameterType.IgnitionCutTime,
                           new CodeParameter<Byte, Byte>(ParameterType.IgnitionCutTime,
                                                                 this,
                                                                 ParameterType.IgnitionCutTime.GetIndex()));

            parameters.Add(ParameterType.Branch1Address1,
                          new SearchParameter<Address, Address>(ParameterType.Branch1Address1,
                                                                this,
                                                                ParameterType.Branch1Address1.GetIndex(),
                                                                searchParameterLocator,
                                                                new AddressValueConverter()));

            parameters.Add(ParameterType.Branch1Address2,
                          new SearchParameter<Address, Address>(ParameterType.Branch1Address2,
                                                                this,
                                                                ParameterType.Branch1Address2.GetIndex(),
                                                                searchParameterLocator,
                                                                new AddressValueConverter()));

            parameters.Add(ParameterType.Branch1Address3,
                          new SearchParameter<Address, Address>(ParameterType.Branch1Address3,
                                                                this,
                                                                ParameterType.Branch1Address3.GetIndex(),
                                                                searchParameterLocator,
                                                                new AddressValueConverter()));

            parameters.Add(ParameterType.Branch1Address4,
                         new SearchParameter<Address, Address>(ParameterType.Branch1Address4,
                                                               this,
                                                               ParameterType.Branch1Address4.GetIndex(),
                                                               searchParameterLocator,
                                                               new AddressValueConverter()));

            parameters.Add(ParameterType.Szfuba_w,
                           new BranchParameter(ParameterType.Szfuba_w,
                                               this,
                                               ParameterType.Szfuba_w.GetIndex(),
                                               ParameterType.Szfuba_w.GetBranchIndexes(),
                                               ParameterType.Szfuba_w.GetBranchOffset(),
                                               searchParameterLocator));

            parameters.Add(ParameterType.CdmdForMisfireDetection,
                           new BranchParameter(ParameterType.CdmdForMisfireDetection,
                                               this,
                                               ParameterType.CdmdForMisfireDetection.GetIndex(),
                                               ParameterType.CdmdForMisfireDetection.GetBranchIndexes(),
                                               ParameterType.CdmdForMisfireDetection.GetBranchOffset(),
                                               searchParameterLocator));
        }

        #region Public Properties

        public UInt32 TmotlinCoolantTemperatureAddress
        {
            get { return ((Address)parameters[ParameterType.TmotlinCoolantTemperatureAddress].Value).value; }
        }

        public Byte HighestTemperatureThreshold
        {
            get { return (Byte)parameters[ParameterType.HighestTemperatureThreshold].Value; }
            set { parameters[ParameterType.HighestTemperatureThreshold].Value = value; }
        }

        public Byte LowestTemperatureThreshold
        {
            get { return (Byte)parameters[ParameterType.LowestTemperatureThreshold].Value; }
            set { parameters[ParameterType.LowestTemperatureThreshold].Value = value; }
        }

        public UInt32 B_kupplClutchPedalSwitchAddress
        {
            get { return ((Address)parameters[ParameterType.B_kupplClutchPedalSwitchAddress].Value).value; }
        }

        public UInt32 Vfil_wVehicleSpeedAddress
        {
            get { return ((Address)parameters[ParameterType.Vfil_wVehicleSpeedAddress].Value).value; }
        }

        public UInt16 LaunchControlDeactivationSpeedThreshold
        {
            get { return (UInt16)parameters[ParameterType.LaunchControlDeactivationSpeedThreshold].Value; }
            set { parameters[ParameterType.LaunchControlDeactivationSpeedThreshold].Value = value; }
        }

        public UInt32 Nmot_wEngineSpeedAddress1
        {
            get { return ((Address)parameters[ParameterType.Nmot_wEngineSpeedAddress1].Value).value; }
        }

        public UInt16 LaunchRPMThreshold1
        {
            get { return (UInt16)parameters[ParameterType.LaunchRPMThreshold1].Value; }
            set { parameters[ParameterType.LaunchRPMThreshold1].Value = value; }
        }

        public UInt32 B_bremsBrakePedalSwitchSddress
        {
            get { return ((Address)parameters[ParameterType.B_bremsBrakePedalSwitchSddress].Value).value; }
        }

        public UInt32 Nmot_wEngineSpeedAddress2
        {
            get { return ((Address)parameters[ParameterType.Nmot_wEngineSpeedAddress2].Value).value; }
        }

        public UInt16 LaunchRPMThreshold2
        {
            get { return (UInt16)parameters[ParameterType.LaunchRPMThreshold2].Value; }
            set { parameters[ParameterType.LaunchRPMThreshold2].Value = value; }
        }

        public UInt32 Wped_wAcceleratorPedalPositionAddress
        {
            get { return ((Address)parameters[ParameterType.Wped_wAcceleratorPedalPositionAddress].Value).value; }
        }

        public UInt16 PedalPositionValue
        {
            get { return (UInt16)parameters[ParameterType.PedalPositionValue].Value; }
            set { parameters[ParameterType.PedalPositionValue].Value = value; }
        }

        public Byte IgnitionCutTime
        {
            get { return (Byte)parameters[ParameterType.IgnitionCutTime].Value; }
            set { parameters[ParameterType.IgnitionCutTime].Value = value; }
        }

        public UInt32 Branch1Address1
        {
            get { return ((Address)parameters[ParameterType.Branch1Address1].Value).value; }
        }

        public UInt32 Branch1Address2
        {
            get { return ((Address)parameters[ParameterType.Branch1Address2].Value).value; }
        }

        public UInt32 Branch1Address3
        {
            get { return ((Address)parameters[ParameterType.Branch1Address3].Value).value; }
        }

        public UInt32 Branch1Address4
        {
            get { return ((Address)parameters[ParameterType.Branch1Address4].Value).value; }
        }

        public UInt32 Szfuba_w
        {
            get { return ((Address)parameters[ParameterType.Szfuba_w].Value).value; }
        }

        public UInt32 Szfuba_w_BranchToCode
        {
            get
            {
                BranchParameter branchParameter = (BranchParameter)parameters[ParameterType.Szfuba_w];
                return branchParameter.BranchInstructions.First().value;
            }
        }

        public UInt32 CdmdForMisfireDetection
        {
            get { return ((Address)parameters[ParameterType.CdmdForMisfireDetection].Value).value; }
        }

        public UInt32 CdmdForMisfireDetection_BranchToCode1
        {
            get
            {
                BranchParameter branchParameter = (BranchParameter)parameters[ParameterType.CdmdForMisfireDetection];
                return branchParameter.BranchInstructions.ToArray()[0].value;
            }
        }

        public UInt32 CdmdForMisfireDetection_BranchToCode2
        {
            get
            {
                BranchParameter branchParameter = (BranchParameter)parameters[ParameterType.CdmdForMisfireDetection];
                return branchParameter.BranchInstructions.ToArray()[1].value;
            }
        }

        public byte[] CodeBytes
        {
            get { return codeBytes; }
        }

        #endregion

        #region Methods

        public void InjectEcu()
        {
            Array.Copy(codeBytes, 0, ecuCode.CodeBytes, Constants.INJECT_CODE_ADDRESS, codeBytes.Length);
        }

        public void PatchEcu()
        {
            var branchParameters = parameters.Values.OfType<BranchParameter>();
            foreach (var branchParameter in branchParameters)
                branchParameter.PatchEcu(ecuCode);
        }
        #endregion
    }
}
