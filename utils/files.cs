namespace AdventOfCode2022
{
    public class TextUtils
    {
        public static string[]? ReadFile(string input)
        {
            return File.ReadAllLines(input);
        
        
        }

        public static (List<string>, List<string>) ReadBeforeAndAfterBlank(string filePath)
        {
        // Lists to store lines before and after the blank line
        List<string> beforeBlank = new List<string>();
        List<string> afterBlank = new List<string>();

        // Read the file line by line
        using (StreamReader sr = new StreamReader(filePath))
        {
            bool isAfterBlankLine = false; // flag to check if we are after the blank line
            string ?line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isAfterBlankLine = true; // blank line encountered
                }
                else
                {
                    if (isAfterBlankLine)
                    {
                        afterBlank.Add(line);
                    }
                    else
                    {
                        beforeBlank.Add(line);
                    }
                }
            }
        }

        // Return the two lists in a tuple
        return (beforeBlank, afterBlank);
    }
    }
}