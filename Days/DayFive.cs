namespace AdventOfCode2022
{
    class DayFive
    {
        private static List<string> beforeSpace = new List<string>();

        private static List<string> afterSpace = new List<string>();

        private static Dictionary<int, Stack<char>> Stacks = new Dictionary<int, Stack<char>>();

        public static void Run(char part)
        {
            (beforeSpace, afterSpace) = TextUtils.ReadBeforeAndAfterBlank("./input/DayFive.input");

            List<int> stackIndices = GetStackIndices(beforeSpace.Last());

            Stacks = BuildDictionary(ref stackIndices);

            BuildStacks(ref Stacks, ref stackIndices, ref beforeSpace);

            // Part A

            if (part == 'a')
            {

                foreach (string line in afterSpace)
                {
                    MoveItems(extractMovements(line));
                }

                PrintTopItems(ref Stacks);

            }

            else if (part == 'b')
            {
                // Part B
                foreach (string line in afterSpace)
                {
                    MoveMultipleItems(extractMovements(line));
                }

                PrintTopItems(ref Stacks);
            }

        }

        private static Dictionary<int, Stack<char>> BuildDictionary(ref List<int> stackIndices)
        {
            Dictionary<int, Stack<char>> Stacks = new Dictionary<int, Stack<char>>();

            for (int i = 0; i < stackIndices.Count; i++)
            {
                Stacks.Add(i + 1, new Stack<char>());
            }

            return Stacks;
        }

        public static void PrintTopItems(ref Dictionary<int, Stack<char>> stacksDictionary)
        {
            foreach (var kvp in stacksDictionary)
            {
                int key = kvp.Key;
                Stack<char> stack = kvp.Value;

                if (stack.Count > 0)
                {
                    char topItem = stack.Peek();
                    Console.WriteLine($"Top item of stack with key {key}: {topItem}");
                }
                else
                {
                    Console.WriteLine($"Stack with key {key} is empty.");
                }
            }
        }

        private static void BuildStacks(ref Dictionary<int, Stack<char>> Stacks, ref List<int> stackIndices, ref List<string> stackRows)
        {
            for (int i = (stackRows.Count - 1); i >= 0; i--)
            {
                for (int j = 0; j < stackIndices.Count; j++)
                {
                    if (char.IsUpper(stackRows[i][stackIndices[j]]))
                    {
                        Stacks[j + 1].Push(stackRows[i][stackIndices[j]]);
                    }
                }
            }


        }

        private static List<int> GetStackIndices(string stackrow)
        {
            List<int> stackIndices = new List<int>();

            for (int i = 0; i < stackrow.Length; i++)
            {
                if (char.IsDigit(stackrow[i]))
                {
                    stackIndices.Add(i);
                }
            }
            return stackIndices;
        }

        private static void MoveItems(params int[] movements)
        {
            int stackfrom = movements[1];
            int stackto = movements[2];
            int multiplier = movements[0];

            for (int i = 0; i < multiplier; i++)
            {
                Stacks[stackto].Push(Stacks[stackfrom].Pop());
            }
        }

        private static void MoveMultipleItems(params int[] movements)
        {
            int stackfrom = movements[1];
            int stackto = movements[2];
            int multiplier = movements[0];

            Stack<char> tempStack = new Stack<char>();

            for (int i = 0; i < multiplier; i++)
            {
                tempStack.Push(Stacks[stackfrom].Pop());
            }

            for (int i = 0; i < multiplier; i++)
            {
                Stacks[stackto].Push(tempStack.Pop());
            }
        }

        private static int[] extractMovements(string line)
        {
            int[] movements = new int[3];

            string[] splitLine = line.Split(' ');

            movements[0] = int.Parse(splitLine[1]);
            movements[1] = int.Parse(splitLine[3]);
            movements[2] = int.Parse(splitLine[5]);

            return movements;
        }

    }
}