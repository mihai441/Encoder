using System;
using System.IO;

namespace Encoder
{


    class StreamEncoder
    {
        private TextReader reader;
        private TextEncoder textEncoder;

        public StreamEncoder(TextEncoder textEncoder, TextReader reader)
        {

            this.reader = reader; 
            this.textEncoder = textEncoder;

        }



        public void Encode(BinaryWriter writer)
        {
            
            String line = "";

            while ((line = reader.ReadLine()) != null)
            {
                byte[] result = textEncoder.Encode(line);
                writer.Write(result);

            }
        }





    }
}
