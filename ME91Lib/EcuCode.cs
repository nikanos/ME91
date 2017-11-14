using ME91Lib.Interfaces;
using System.IO;
using System.Linq;

namespace ME91Lib
{
    public class EcuCode : ICode
    {
        private byte[] codeBytes;

        #region Constructors
        
        public EcuCode(byte[] codeBytes)
        {
            this.codeBytes = codeBytes.ToArray();
        }

        public EcuCode(Stream stream)
        {
            using(MemoryStream ms=new MemoryStream())
            {
                stream.CopyTo(ms);
                codeBytes = ms.ToArray();
            }
        }

        public EcuCode(string fileName)
        {
            codeBytes = File.ReadAllBytes(fileName);
        }

        #endregion

        #region Properties

        public byte[] CodeBytes
        {
            get { return codeBytes; }
        }

        #endregion
    }
}
