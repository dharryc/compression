//Group Project 1 - Compression
//Brock, Sergio, and Harry
//Starting 9/15
using System;
using System.Security.Cryptography;

public class Compression
{
    public static void Main()
    {
        Console.WriteLine("hello guys, time to write some compression!!!");
        int[] j = MakeBinaryArray(50);
        Console.WriteLine();
        for (int a = 0; a < j.Length; a++)
        {
            Console.Write(j[a]);
        }
    }

    public static int[] MakeBinaryArray(int size)
    {
        Random rnd = new Random();
        int[] j = new int[size];
        for (int a = 0; a < size; a++)
        {
            j[a] = rnd.Next(0, 2);
        }

        BrockTest();

        return j;

    }

    //Harry's idea for compression!!! Super rough draft, but the idea is just take two or three
    //combinations of 1's and 0's and represent them somehow
    public static int[] HarryCompression(int[] b)
    {
        int[] a = b;
        return a;
    }

    public static int[] HarryDeCompression(int[] b)
    {
        int[] a = b;
        return a;
    }

    public static void BrockTest()
    {
        var bc = new BrockCompression();
        var toEncode = "0100111100001101000010000111111000010010010100111100011010101011";
        Console.WriteLine($"To encode: {toEncode}");
        var compressed = bc.Compress(toEncode);
        Console.WriteLine($"Encoded: {compressed}");
        Console.WriteLine($"The length of the Brock test string is {compressed.Length}");
        var depressed = bc.Decompress(compressed);
        Console.WriteLine($"Decoded: {depressed}");
    }
}

public class BrockCompression: IBitStringCompressor
{
    Utility util = new Utility(true);

    public string Compress(string original)
    {
        // The symbolic version converts from binary to ASCII alphabet for ease of use
        var (symbolicVersion, chunkSize) = ConvertToSymbols(original);

        return symbolicVersion;
    }

    public string Decompress(string original)
    {
        return original;
    }

    // Using lowest factor function allows us to
    // use chunks of the code that don't allow for data loss
    List<int> LargestFactor(int number)
    {
        // Iterate from 1 to the square root of the number
        // If number mod i is 0, return the factor
        // Stolen from Stack Overflow

        List<int> output = new() {1};
        int max = (int)Math.Sqrt(number);

        // Start at 2 because 1 is automatically there
        for (int factor = 2; factor <= max; factor++)
        {
            if (number % factor == 0)
            {
                output.Add(factor);
            }
        }

        // Closest error exception
        return output;
    }

    // Turns the binary string into ASCII symbols for neater compression 
    // Returns the symbolic string and the size of the chunk used
    (string, int) ConvertToSymbols(string input)
    {
        string output;
        var factorList = LargestFactor(input.Length);
        // Chooses a length that is in the middle to create a chunk to work with
        int chunkSize = factorList[factorList.Count / 2];
        List<string> listOfChunks = new();

        // To iterate and create the individual chunks in a list
        // Iterates through the whole input until the size is equivalent
        for (int i = 0; i < input.Length - 1; i = i + chunkSize)
        {
            string chunk = input.Substring(i, i+chunkSize);
            listOfChunks.Add(chunk);
        }

        // TODO: Turn the chunks into a ASCII character appended to a string

        util.DebugMessage($"Symbolic version: {input}");
        return (input, chunkSize); // Does not work for now
    }

    string ConvertFromSymbols(string input)
    {
        return input;  
    }
}

class Utility
{
    bool Debug { get; set; }
    // This class allows us to activate and deactivate debug messages
    public Utility(bool debug)
    {
        Debug = debug;
    }

    // Prints a message if in debug mode
    public void DebugMessage(string message)
    {
        if (Debug)
        {
            Console.WriteLine(message);
        }
    }
}

public interface IBitStringCompressor
{
    // Assumes input and output of '0' and '1'
    string Compress(string original);
    string Decompress(string compressed);
}