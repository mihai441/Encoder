using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder
{
    class CaesarEncoder : IEncoder
    {

        private static readonly int OFFSET = 57;
        private byte[] outputLine;


        public CaesarEncoder()
        {
            outputLine = new byte[4096];

        }
        public byte[] Encode(byte[] inputData)
        {

            for (int i = 0; i < inputData.Length; i++)
            {
                outputLine[i] = ByteEncoder(inputData[i]);
            }
            return outputLine;
        }

        private byte ByteEncoder(byte byteChar)
        {
            return (byte) ((byteChar + OFFSET) % 128);
        }
    }
}
