using System;
using System.Text;
using OOPBasics.Shared;
namespace OOPBasics
{
    class BinaryDecoder
    {
        private IDecoder decoder;
    
        public BinaryDecoder(IDecoder decoder)
        {
            this.decoder = decoder;
        }


        public String Decode(byte[] line)
        {

            return DecodeBytesToText(decoder.Decode((line)));

        }

        private String DecodeBytesToText(byte[] line)
        {
            String bufferText = Encoding.ASCII.GetString(line);

            return bufferText;

        }




    }


}
