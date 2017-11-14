using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ME91Lib.Tests
{
    [TestClass]
    public class UtilitiesTests
    {
        private byte[] SEARCH_INDEXDATA = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private byte[] SEARCH_LASTINDEX_DATA = { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 6 };
        private byte[] SEARCH_DATA_ALLSAME = { 1, 1, 1 };

        private byte[] PATTERN_INDEX_POS0 = { 1, 2, 3 };
        private byte[] PATTERN_INDEX_POS6 = { 7, 8, 9 };
        private byte[] PATTERN_INDEX_POS9 = { 10 };
        private byte[] PATTERN_INDEX_NONEXISTING = { 3, 2, 1 };
        private byte[] PATTERN_LASTINDEX_POS5 = { 1, 2, 3 };
        private byte[] PATTERN_BIG = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilitiesIndexOf_NullArguement_ThrowsException()
        {
            Utilities.IndexOf(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilitiesIndexOfPattern_NullArguement_ThrowsException()
        {
            Utilities.IndexOf(SEARCH_INDEXDATA, (byte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UtilitiesIndexOfPattern_BigPattern_ThrowsException()
        {
            Utilities.IndexOf(SEARCH_INDEXDATA, PATTERN_BIG);
        }

        [TestMethod]
        public void UtilitiesIndexOfPattern_SearchPattern_Succeeds()
        {
            int pos;

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, new byte[] { SEARCH_INDEXDATA[0] });
            Assert.AreEqual(0, pos);

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, new byte[] { SEARCH_INDEXDATA[SEARCH_INDEXDATA.Length - 1] });
            Assert.AreEqual(SEARCH_INDEXDATA.Length - 1, pos);

            pos = Utilities.IndexOf(SEARCH_DATA_ALLSAME, new byte[] { SEARCH_DATA_ALLSAME[0] });
            Assert.AreEqual(0, pos);

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, PATTERN_INDEX_POS0);
            Assert.AreEqual(0, pos);

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, PATTERN_INDEX_POS6);
            Assert.AreEqual(6, pos);

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, PATTERN_INDEX_POS9);
            Assert.AreEqual(9, pos);

            pos = Utilities.IndexOf(SEARCH_INDEXDATA, PATTERN_INDEX_NONEXISTING);
            Assert.AreEqual(-1, pos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilitiesLastIndexOf_NullArguement_ThrowsException()
        {
            Utilities.LastIndexOf(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilitiesLastIndexOfPattern_NullArguement_ThrowsException()
        {
            Utilities.LastIndexOf(SEARCH_LASTINDEX_DATA, (byte[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UtilitiesLastIndexOfPattern_BigPattern_ThrowsException()
        {
            Utilities.LastIndexOf(SEARCH_LASTINDEX_DATA, PATTERN_BIG);
        }

        [TestMethod]
        public void UtilitiesLastIndexOfPattern_SearchPattern_Succeeds()
        {
            int pos;

            pos = Utilities.LastIndexOf(SEARCH_LASTINDEX_DATA, PATTERN_LASTINDEX_POS5);
            Assert.AreEqual(5, pos);

            pos = Utilities.LastIndexOf(SEARCH_DATA_ALLSAME, new byte[] { SEARCH_DATA_ALLSAME[0] });
            Assert.AreEqual(SEARCH_DATA_ALLSAME.Length - 1, pos);

            pos = Utilities.LastIndexOf(SEARCH_INDEXDATA, PATTERN_INDEX_NONEXISTING);
            Assert.AreEqual(-1, pos);
        }
    }
}
