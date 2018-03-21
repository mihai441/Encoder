using OOpBasics;
using OOPBasics.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOPBasics
{
    class Program
    {

        private static readonly String PLUGINS_PATH_NAME = @"E:\Plugins";
        private static readonly String ENCODE_PATH_NAME = @"E:\Encode\";
        private static readonly String DECODE_PATH_NAME = @"E:\Decode\";
        private static readonly String CODER_INPUT_FILE_NAME  = "input.txt";
        private static readonly String CODER_OUTPUT_FILE_NAME = "output.bin";
        private static readonly String DECODER_INPUT_FILE_NAME = "input.bin";
        private static readonly String DECODER_OUTPUT_FILE_NAME = "output.txt";
        private static List<IEncoderPlugin> encoderPlugins;
        private static List<IDecoderPlugin> decoderPlugins;
        private static MenuManager menuManager = new MenuManager();

        static void Main(string[] args)
        {

            PluginsManager<IEncoderPlugin> EncoderpluginsManager = new PluginsManager<IEncoderPlugin>();
            PluginsManager<IDecoderPlugin> DecoderpluginsManager = new PluginsManager<IDecoderPlugin>();


            DecoderpluginsManager.LoadPlugins(PLUGINS_PATH_NAME);
            EncoderpluginsManager.LoadPlugins(PLUGINS_PATH_NAME);

            GetEncoderPlugins(EncoderpluginsManager);
            GetDecoderPlugins(DecoderpluginsManager);

            char optionNo = '1';
            foreach (var encoderPlugin in encoderPlugins)
            {
                menuManager.AddItem(new MenuItem { ShortcutChar = optionNo, Text = encoderPlugin.GetName(), ContextObject = encoderPlugin, ItemAction = new MenuItemAction(EncodeAction) });
                optionNo++;
            }

            foreach (var decoderPlugin in decoderPlugins)
            {
                menuManager.AddItem(new MenuItem { ShortcutChar = optionNo, Text = decoderPlugin.GetName(), ContextObject = decoderPlugin, ItemAction = new MenuItemAction(DecodeAction) });
                optionNo++;
            }
            menuManager.Run();



        }

        private static TextReader GetInputTextReader(String path, String fileName)
        {
            StreamReader streamreader = null;

            try
            {
                streamreader = new StreamReader(path+fileName);
            }
            catch (FileNotFoundException ex)
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

        private static BinaryWriter GetInputBinaryWriter(String path, String fileName)
        {
            BinaryWriter bw = null;
            try
            {
                bw = new BinaryWriter(File.Open(path+fileName, FileMode.Create));
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Fisierul nu a fost gasit" + ex.FileName);

            }
            catch (IOException ex)
            {
                Console.WriteLine("Eroare la scrierea fisierului" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception" + ex.Message);

            }

            return bw;
        }

        private static BinaryReader GetInputBinaryReader(String path, String fileName)
        {
            BinaryReader bw = null;
            try
            {
                bw = new BinaryReader(File.Open(path + fileName, FileMode.Open));
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Fisierul nu a fost gasit" + ex.FileName);

            }
            catch (IOException ex)
            {
                Console.WriteLine("Eroare la scrierea fisierului" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception" + ex.Message);

            }

            return bw;
        }

        private static TextWriter GetInputTextWriter(String path, String fileName)
        {

            StreamWriter streamWriter = null;

            try
            {
                streamWriter = new StreamWriter(path + fileName);
            }
            catch (FileNotFoundException ex)
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

            return streamWriter;
        }



        private static void GetEncoderPlugins(PluginsManager<IEncoderPlugin> pluginsManager)
        {
            encoderPlugins = new List<IEncoderPlugin>();
            foreach (var item in pluginsManager.Plugins)
            {
                encoderPlugins.Add(item);
            }
        }

        private static void GetDecoderPlugins(PluginsManager<IDecoderPlugin> pluginsManager)
        {
            decoderPlugins = new List<IDecoderPlugin>();
            foreach (var item in pluginsManager.Plugins)
            {
                decoderPlugins.Add(item);
            }
        }

        private static void DecodeAction(object sender, object context)
        {
            IDecoderPlugin decoderPlugin = (IDecoderPlugin)context;

            var plugin = decoderPlugin.GetDecoder();
            TextWriter decodeTextWriter = GetInputTextWriter(DECODE_PATH_NAME,DECODER_OUTPUT_FILE_NAME);
            BinaryReader decodeBinaryReader = GetInputBinaryReader(DECODE_PATH_NAME,DECODER_INPUT_FILE_NAME);
            BinaryDecoder binaryDecoder = new BinaryDecoder(plugin);
            StreamDecoder streamDecoder = new StreamDecoder(binaryDecoder, decodeBinaryReader);
            streamDecoder.Decode(decodeTextWriter);
            decodeBinaryReader.Close();
            decodeTextWriter.Close();

        }

        static private void  EncodeAction(object sender, object context)
        {
            IEncoderPlugin encoderPlugin = (IEncoderPlugin)context;
            GetParameters(encoderPlugin);
            var plugin = encoderPlugin.GetEncoder();
            TextReader EncodeTextReader = GetInputTextReader(ENCODE_PATH_NAME, CODER_INPUT_FILE_NAME);
            BinaryWriter EncodeBinaryWriter = GetInputBinaryWriter(ENCODE_PATH_NAME, CODER_OUTPUT_FILE_NAME);
            TextEncoder textEncoder = new TextEncoder(plugin);
            StreamEncoder streamEncoder = new StreamEncoder(textEncoder, EncodeTextReader);
            streamEncoder.Encode(EncodeBinaryWriter);
            EncodeTextReader.Close();
            EncodeBinaryWriter.Close();


        }

        static void GetParameters(IEncoderPlugin plugin)
        {
            int counter = 0;
            var arguments = plugin.GetRequiredArguments();
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            foreach (var pluginRequiredParameter in arguments)
            {
                Console.Write(pluginRequiredParameter + " : ");
                parameters["arg" + counter++] = Console.ReadLine(); ;
                Console.WriteLine();
            }
            plugin.SetArguments(parameters);
        }

    }
}
                  