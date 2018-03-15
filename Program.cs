using System;
using System.IO;

namespace Encoder
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader textReader = GetInputTextReader();
            BinaryWriter binaryWriter = GetInputBinaryWriter();

            CaesarEncoder caesarEncoder = new CaesarEncoder();
            EnigmaEncoder enigmaEncoder = new EnigmaEncoder(117, 399, getRandomNumber());
            TextEncoder textEncoder = new TextEncoder(caesarEncoder);
            StreamEncoder stream = new StreamEncoder(textEncoder, textReader);


            stream.Encode(binaryWriter);
            textReader.Close();
            binaryWriter.Close();
        }

        private static TextReader GetInputTextReader()
        {
            StreamReader streamreader = null;

            try
            {
                streamreader = new  StreamReader("textfiles\\input.txt");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("FIle not found" + ex.FileName);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Directory not found" + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Eroare la citirea fisierului" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception" + ex.Message);
            }

            return streamreader;
        }

        private static BinaryWriter GetInputBinaryWriter()
        {
            BinaryWriter bw=null;
            try
            {
                bw = new BinaryWriter(File.Open("textfiles\\output.bin", FileMode.Create));
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Fisierul nu a fost gasit"+ ex.FileName);

            }
            catch (IOException ex)
            {
                Console.WriteLine("Eroare la scrierea fisierului" + ex.Message);
            }
            catch (Exception ex)
            {
                    Console.WriteLine("Unexpected exception"+ex.Message);
                    
            }

            return bw;
        }
    
        private static int getRandomNumber()
        {
            Random rand = new Random();
            return rand.Next();
        }
    }
}
