//Group Project 1 - Compression

//Brock, Sergio, and Harry

//Starting 9/15

//Our idea for compression is to break the string into chunks of 4 bits, convert those into ACII characters,
//then turn them back into binary with one extra bit at the end signifying if the chunk repeated or not

//For decompression, run through chunks of size 5, if the ending number is 1, repeat the first four, if it's
//0, only write the first four
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Compression
{
    public static void Main()
    {
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

public class BrockCompression : IBitStringCompressor
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

        List<int> output = new() { 1 };
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
        //string output;
        var factorList = LargestFactor(input.Length);
        // Chooses a length that is in the middle to create a chunk to work with
        int chunkSize = factorList[factorList.Count / 2];
        List<string> listOfChunks = new();

        // To iterate and create the individual chunks in a list
        // Iterates through the whole input until the size is equivalent
        for (int i = 0; i < input.Length - 1; i = i + chunkSize)
        {
            string chunk = input.Substring(i, i + chunkSize);
            listOfChunks.Add(chunk);
        }

        // TODO: Turn the chunks into a ASCII character appended to a string

        util.DebugMessage($"Symbolic version: {input}");
        return (input, chunkSize); // Does not work for now
    }

    // Return string and chunk size
    (string, int) QuickConvertToSymbols(string input)
    {
        const int chunkSize = 4;

        // Create a list of chunks to interpret
        List<string> listOfChunks = new();

        // Create the list of chunks
        for (int i = 0; i < input.Length - 1; i = i + chunkSize)
        {
            string chunk = input.Substring(i, chunkSize);
            listOfChunks.Add(chunk);
        }

        foreach (var chunk in listOfChunks)
        {
            util.DebugMessage(chunk);
        }
        // Convert the list of chunks

        return (input, chunkSize);
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


public class HarrysCompression
{
    public static string Compression(string binary)
    {
        //the first thing I do is convert the input string into an array as integers, I find integers easier to work with
        char[] charList = binary.ToCharArray();
        int[] intList = Array.ConvertAll(charList, c => (int)Char.GetNumericValue(c));
        //then break that string into smaller "chunks" of 4 bits at a time
        List<int[]> chunks = Chunkmaker(intList);
        //now "compress" those chunks into letters, and store them in a list
        List<string> translatedInts = [];
        foreach (int[] a in chunks)
        {
            translatedInts.Add(Translation(a));
        }
        int lengthCount = 0;
        foreach (string a in translatedInts)
        {
            lengthCount++;
        }
        string[] checkTime = new string[lengthCount];
        for (int a = 0; a < checkTime.Length; a++)
        {
            checkTime[a] = translatedInts[a];
        }
        string compressedString = string.Join("", ActuallyCompressPlease(translatedInts));
        return compressedString;
    }

    //I'm beginning to think this might be totally unneccesary, which would suck, but we will see
    public static List<int[]> Chunkmaker(int[] binary)
    {
        int overflow = binary.Length % 4;
        List<int[]> b = [];
        return b;
    }
    public static string Translation(int[] fourBits)
    {
        if (fourBits[0] == 0 || fourBits[1] == 0 || fourBits[2] == 0 || fourBits[3] == 0)
        {
            return "a";
        }
        if (fourBits[0] == 0 || fourBits[1] == 0 || fourBits[2] == 0 || fourBits[3] == 1)
        {
            return "b";
        }
        if (fourBits[0] == 0 || fourBits[1] == 0 || fourBits[2] == 1 || fourBits[3] == 0)
        {
            return "c";
        }
        if (fourBits[0] == 0 || fourBits[1] == 0 || fourBits[2] == 1 || fourBits[3] == 1)
        {
            return "d";
        }
        if (fourBits[0] == 0 || fourBits[1] == 1 || fourBits[2] == 0 || fourBits[3] == 0)
        {
            return "e";
        }
        if (fourBits[0] == 0 || fourBits[1] == 1 || fourBits[2] == 0 || fourBits[3] == 1)
        {
            return "f";
        }
        if (fourBits[0] == 0 || fourBits[1] == 1 || fourBits[2] == 1 || fourBits[3] == 0)
        {
            return "g";
        }
        if (fourBits[0] == 0 || fourBits[1] == 1 || fourBits[2] == 1 || fourBits[3] == 1)
        {
            return "h";
        }
        if (fourBits[0] == 1 || fourBits[1] == 0 || fourBits[2] == 0 || fourBits[3] == 0)
        {
            return "i";
        }
        if (fourBits[0] == 1 || fourBits[1] == 0 || fourBits[2] == 0 || fourBits[3] == 1)
        {
            return "j";
        }
        if (fourBits[0] == 1 || fourBits[1] == 0 || fourBits[2] == 1 || fourBits[3] == 0)
        {
            return "k";
        }
        if (fourBits[0] == 1 || fourBits[1] == 0 || fourBits[2] == 1 || fourBits[3] == 1)
        {
            return "l";
        }
        if (fourBits[0] == 1 || fourBits[1] == 1 || fourBits[2] == 0 || fourBits[3] == 0)
        {
            return "m";
        }
        if (fourBits[0] == 1 || fourBits[1] == 1 || fourBits[2] == 0 || fourBits[3] == 1)
        {
            return "n";
        }
        if (fourBits[0] == 1 || fourBits[1] == 1 || fourBits[2] == 1 || fourBits[3] == 0)
        {
            return "o";
        }
        if (fourBits[0] == 1 || fourBits[1] == 1 || fourBits[2] == 1 || fourBits[3] == 1)
        {
            return "p";
        }
        else
        {
            return "x";
        }
    }
    public static List<string> ActuallyCompressPlease(List<string> binaryAsLetters)
    {
        List<string> shouldBeCompressed = [];
        int lengthCount = 0;
        foreach (string a in binaryAsLetters)
        {
            lengthCount++;
        }
        for (int a = 0; a < lengthCount; a++)
        {
            if (binaryAsLetters[a] == binaryAsLetters[a + 1])
            {
                shouldBeCompressed.Add(DidRepeat(binaryAsLetters[a]));
                a++;
            }
            else
            {
                shouldBeCompressed.Add(DidNotRepeat(binaryAsLetters[a]));
            }
        }
        return shouldBeCompressed;
    }
    public static string DidRepeat(string a)
    {
        if (a == "a")
        {
            return "00001";
        }
        if (a == "b")
        {
            return "00011";
        }
        if (a == "c")
        {
            return "00101";
        }
        if (a == "d")
        {
            return "00111";
        }
        if (a == "e")
        {
            return "01001";
        }
        if (a == "f")
        {
            return "01011";
        }
        if (a == "g")
        {
            return "01101";
        }
        if (a == "h")
        {
            return "01111";
        }
        if (a == "i")
        {
            return "10001";
        }
        if (a == "j")
        {
            return "10011";
        }
        if (a == "k")
        {
            return "10101";
        }
        if (a == "l")
        {
            return "10111";
        }
        if (a == "m")
        {
            return "11001";
        }
        if (a == "n")
        {
            return "11011";
        }
        if (a == "o")
        {
            return "11101";
        }
        if (a == "p")
        {
            return "11111";
        }
        else
        {
            return null;
        }
    }
    public static string DidNotRepeat(string a)
    {
        if (a == "a")
        {
            return "00000";
        }
        if (a == "b")
        {
            return "00010";
        }
        if (a == "c")
        {
            return "00100";
        }
        if (a == "d")
        {
            return "00110";
        }
        if (a == "e")
        {
            return "01000";
        }
        if (a == "f")
        {
            return "01010";
        }
        if (a == "g")
        {
            return "01100";
        }
        if (a == "h")
        {
            return "01110";
        }
        if (a == "i")
        {
            return "10000";
        }
        if (a == "j")
        {
            return "10010";
        }
        if (a == "k")
        {
            return "10100";
        }
        if (a == "l")
        {
            return "10110";
        }
        if (a == "m")
        {
            return "11000";
        }
        if (a == "n")
        {
            return "11010";
        }
        if (a == "o")
        {
            return "11100";
        }
        if (a == "p")
        {
            return "11110";
        }
        else
        {
            return null;
        }
    }
}