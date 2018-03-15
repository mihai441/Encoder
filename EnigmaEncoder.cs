using System;
using System.Text;

namespace Encoder
{
    class EnigmaEncoder : IEncoder
    {

        private int n1;
        private int n2;
        private int random;
        private byte[] outputLine;

        public EnigmaEncoder(int n1, int n2,int random)
        {
            this.n1 = n1;
            this.n2 = n2;
            this.random = random;
            outputLine = new byte[4096];

        }



        public byte[] Encode(byte[] inputData)
        {
            for (int i = 0; i < inputData.Length; i++)
            {
                outputLine[i] = EncodeByte(inputData[i]);
            }
            return outputLine;
        }

        private byte EncodeByte(byte charByte) {

            byte encoded;

            encoded = (byte)(n1 + (charByte + random) % (n2 - n1));
            return encoded;
        }

    }

    
}
