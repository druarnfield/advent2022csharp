
namespace AdventOfCode2022
{
    public class DayOne
    {
        private static string[]? lines;
        private static List<int> elfTotals = new List<int>();

        public static void Run(string input)
        {
            lines = TextUtils.ReadFile(input);
            FindElfTotals();
        }

        private static void FindElfTotals()
        {
            int currentTotal = 0;
            try
            {
                if (lines == null)
                {
                    throw new InvalidOperationException("The input file has not been read.");
                }

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        elfTotals.Add(currentTotal);
                        currentTotal = 0;
                    }
                    else
                    {
                        currentTotal += int.Parse(line);
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }

            elfTotals.Add(currentTotal); // To add the last total even if the last line isn't blank.
            elfTotals.Sort();
            elfTotals.Reverse();
        }

        public static int GetTopElf()
        {
            return elfTotals[0];
        }

        public static int GetTopThreeElves()
        {
            return elfTotals[0] + elfTotals[1] + elfTotals[2];
        }
    }
}
