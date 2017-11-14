using ME91Lib.Enumerations;
using ME91Lib.Interfaces;
using ME91Lib.ParameterLocators;
using ME91Lib.ParameterValueConverters;
using ME91Lib.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ME91Lib.Tests
{
    [TestClass]
    public class ME91Tests
    {
        [TestMethod]
        public void HighestTemperatureThresholdValueConverter_Conversion_Succeeds()
        {
            var converter = new TemperatureValueConverter();
            byte resultFromInternal = converter.ConvertFromInternal(0xd3);
            Assert.AreEqual(110, resultFromInternal);
            byte resultToInternal = converter.ConvertToInternal(110);
            Assert.AreEqual(0xd3, resultToInternal);
        }

        [TestMethod]
        public void DefaultValueConverter_Conversion_Succeeds()
        {
            var converter = new DefaultValueConverter<UInt16, UInt16>();
            UInt16 internalValue = (UInt16)0xabcd;
            UInt16 result = converter.ConvertFromInternal(internalValue);
            Assert.AreEqual(internalValue, result);
        }

        [TestMethod]
        public void DefaultValueConverter_ConversionBetweenDifferentTypes_Succeeds()
        {
            var converter = new DefaultValueConverter<UInt32, UInt16>();
            UInt32 internalValue = (UInt32)0xabcd;
            UInt16 result = converter.ConvertFromInternal(internalValue);
            Assert.AreEqual(internalValue, result);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void DefaultValueConverter_ConversionBetweenNonFittingTypes_Throws()
        {
            var converter = new DefaultValueConverter<UInt32, UInt16>();
            UInt32 internalValue = (UInt32)0xabcdef;
            UInt16 result = converter.ConvertFromInternal(internalValue);
            Assert.AreEqual(internalValue, result);
        }

        [TestMethod]
        public void Parameter_HighestTemperature_Read_Succeeds()
        {
            Mock<ICode> code = new Mock<ICode>();
            code.SetupGet(x => x.CodeBytes).Returns(new byte[] { 0x00, 0xd3, 0xff });

            CodeParameter<byte, byte> highestTemperature = new CodeParameter<byte, byte>(ParameterType.HighestTemperatureThreshold, code.Object, indexInCode: 1, converter: new TemperatureValueConverter());
            byte result = highestTemperature.Value;
            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void Parameter_HighestTemperature_Set_Succeeds()
        {
            Mock<ICode> code = new Mock<ICode>();
            byte[] codeBytes = new byte[] { 0x01, 0x02, 0x03 };
            const int PARAM_INDEX = 1;

            code.SetupGet(x => x.CodeBytes).Returns(codeBytes);

            CodeParameter<byte, byte> highestTemperature = new CodeParameter<byte, byte>(ParameterType.HighestTemperatureThreshold, code.Object, PARAM_INDEX, new TemperatureValueConverter());
            highestTemperature.Value = 110;

            Assert.AreEqual(0xd3, codeBytes[PARAM_INDEX]);
        }

        [TestMethod]
        public void Parameter_Tmotlin_Read_Succeeds()
        {
            Mock<ICode> code = new Mock<ICode>();
            byte[] codeBytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            code.SetupGet(x => x.CodeBytes).Returns(codeBytes);
            Mock<ISearchParameterLocator> searchParameterLocator = new Mock<ISearchParameterLocator>();
            searchParameterLocator.Setup(x => x.Locate<Address>(ParameterType.TmotlinCoolantTemperatureAddress)).Returns(new Address { value = 0xd3b6a });


            SearchParameter<Address, Address> tmotlin = new SearchParameter<Address, Address>(ParameterType.TmotlinCoolantTemperatureAddress, code.Object, 0, searchParameterLocator.Object, new AddressValueConverter());
            Address result = tmotlin.Value;
            Assert.AreEqual(0xd3b5au, result.value);

            Assert.AreEqual(0x01, codeBytes[0]);
            Assert.AreEqual(0x0d, codeBytes[1]);
            Assert.AreEqual(0x3b, codeBytes[2]);
            Assert.AreEqual(0x6a, codeBytes[3]);
        }

        [TestMethod]
        public void SearchParameterLocator_LocateHelper_Succeeds()
        {
            Mock<ICode> code = new Mock<ICode>();
            byte[] codeBytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            code.SetupGet(x => x.CodeBytes).Returns(codeBytes);

            DefaultParameterLocator locator = new DefaultParameterLocator(code.Object);
            PrivateObject privateObjectHelper = new PrivateObject(locator);

            const int PARAMETER_OFFSET = 2;
            int outIndex = 1;

            List<Type> parameterTypes = new List<Type>() { typeof(ParameterType), typeof(byte[]), typeof(int), typeof(ParameterOffsetDirection), typeof(int).MakeByRefType() };
            List<Object> parameterValuesBefore = new List<Object>() { ParameterType.Undefined, new byte[] { 0x04, 0x05 }, PARAMETER_OFFSET, ParameterOffsetDirection.Before, outIndex };
            List<Object> parameterValuesAfter = new List<Object>() { ParameterType.Undefined, new byte[] { 0x04, 0x05 }, PARAMETER_OFFSET, ParameterOffsetDirection.After, outIndex };
            List<Type> typeArguments = new List<Type>() { typeof(UInt16) };

            UInt16 resultBefore = (UInt16)privateObjectHelper.Invoke("LocateHelper", parameterTypes.ToArray(), parameterValuesBefore.ToArray(), typeArguments.ToArray());
            Assert.AreEqual(0x0203, resultBefore);

            UInt16 resultAfter = (UInt16)privateObjectHelper.Invoke("LocateHelper", parameterTypes.ToArray(), parameterValuesAfter.ToArray(), typeArguments.ToArray());
            Assert.AreEqual(0x0708, resultAfter);
        }
    }
}
