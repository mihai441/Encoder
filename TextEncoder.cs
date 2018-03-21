using System;
using System.Text;
using OOPBasics.Shared;
namespace OOPBasics
{
    class TextEncoder
    {
        private IEncoder encoder;
    
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
