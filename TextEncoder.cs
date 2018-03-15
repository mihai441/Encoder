using System;
using System.Text;

namespace Encoder
{
    class TextEncoder
    {
        private IEncoder encoder;
        private byte[] lineInBytes;
    
        public TextEncoder(IEncoder encoder)
        {
            this.encoder = encoder;
        }


        public byte[] Encode(String line)
        {

            return encoder.Encode(EncodeTextToBytes(line));

        }

        private byte[] EncodeTextToBytes(String line)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(line);
            return bytes;

        }




    }


}
