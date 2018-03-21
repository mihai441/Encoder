using System;
using System.IO;

namespace OOPBasics
{
    class StreamDecoder
    {
        private BinaryReader reader;
        private BinaryDecoder binaryDecoder;
        private const int READ_BUFFER_SIZE = 1024;


        public StreamDecoder(BinaryDecoder textDecoder, BinaryReader reader)
        {

            this.reader = reader;
            this.binaryDecoder = textDecoder;
        }


        public void Decode(TextWriter writer)
        {            
            byte[] line;
            int length = 0;
            do
            {
                line = reader.ReadBytes(READ_BUFFER_SIZE);
                length = line.Length;
                String result = binaryDecoder.Decode(line);
                writer.Write(result);
                
            }
            while (length == READ_BUFFER_SIZE);
        }
    }
}
