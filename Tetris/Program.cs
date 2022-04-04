using System;
using System.IO;
using System.Text;
using Tetris.TetrisEngine;
using System.Threading.Tasks;

namespace TetrisGame
{   
    internal class Program
    {
        /// <summary>
        /// This is a basic tetris engine main method
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            // get the file to read
            StreamReader inputFile = new StreamReader("../../Resource/Input/input.txt");

            // get the file to write
            StreamWriter outputFile = new StreamWriter("../../Resource/Output/output.txt");

            try
            {
                // where to start reading from the file
                inputFile.BaseStream.Seek(0, SeekOrigin.Begin);

                String[] inputArray;
                string str = inputFile.ReadLine();

                // To read the whole file line by line
                // main loop
                while (str != null)
                {
                    // splitting one line
                    inputArray = str.Split(',');

                    // writting output
                    ITetrisEngine tetrisEngine = new TetrisBaseEngine();
                    string append = await tetrisEngine.Play(inputArray);
                    outputFile.WriteLine(append);

                    // The next line...
                    str = inputFile.ReadLine();
                }
                Console.WriteLine("Printing the outputs in Resource/Output/output.txt");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error playing game: {ex.Message}");
            }

            finally
            {
                // Close the stream
                inputFile.Close();
                outputFile.Close();
            }
            
            Console.ReadLine();
        }
    }
}
