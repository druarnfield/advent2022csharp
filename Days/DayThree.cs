namespace AdventOfCode2022
{
    class DayThree
    {

        private static Dictionary<char, int> LetterMap = new Dictionary<char, int>();

        public static void Run()
        {
            BuildLetterMap(ref LetterMap);
            try
            {
                string[]? lines = TextUtils.ReadFile("./input/DayThree.input");

                if (lines == null)
                {
                    throw new Exception("Input file is empty.");
                }
                 Console.WriteLine("The score for the input is: " + CalculateScore(lines));
                 Console.WriteLine("The score for the groups is: " + CalculateGroupScore(ref lines));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Input file not found, using test input.");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

           


        }

        private static void BuildLetterMap(ref Dictionary<char, int> letterMap)
        {
            for (int i = 0; i < 26; i++)
            {
                letterMap.Add((char)(i + 97), i + 1);
            }
            for (int i = 0; i < 26; i++)
            {
                letterMap.Add((char)(i + 65), i + 27);
            }
        }

        private static char FindCommonItem(string rucksack) 
        {
            var leftletters = new HashSet<char>();
            var rightletters = new HashSet<char>();

            for (int i = 0; i < rucksack.Length / 2; i++)
            {
                leftletters.Add(rucksack[i]);
            }

            for (int i = rucksack.Length / 2; i < rucksack.Length; i++)
            {
                rightletters.Add(rucksack[i]);
            }

            leftletters.IntersectWith(rightletters);

            return leftletters.First();

        }
        private static int CalculateScore(string[] input) {
            var score = 0;
            foreach (var rucksack in input)
            {
                score += LetterMap[FindCommonItem(rucksack)];
            }
            return score;
        }

        private static int CalculateGroupScore(ref string[] input)
        {
            var score = 0;
            for (int i = 0; i < input.Length; i += 3)
            {
                var groupsacks = new Rucksack[3];
                groupsacks[0] = new Rucksack(input[i]);
                groupsacks[1] = new Rucksack(input[i + 1]);
                groupsacks[2] = new Rucksack(input[i + 2]);

                score += LetterMap[FindGroupCommonItem(groupsacks)];
            }
            return score;
        }

        private static char FindGroupCommonItem(Rucksack[] groupsacks)
        {
            try
            {
                if (groupsacks.Length < 3)
                {
                    throw new Exception("Not enough rucksacks in group.");
                }
                else
                {
                    groupsacks[0].BuildRucksack();
                    groupsacks[1].BuildRucksack();      
                    groupsacks[2].BuildRucksack();  

                    groupsacks[0].GetAllLetters.IntersectWith(groupsacks[1].GetAllLetters);
                    groupsacks[0].GetAllLetters.IntersectWith(groupsacks[2].GetAllLetters);

                    return groupsacks[0].GetAllLetters.First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return ' ';
            }

        }
    }

    class Rucksack
    {
        private HashSet<char> leftletters = new HashSet<char>();
        private HashSet<char> rightletters = new HashSet<char>();
        private HashSet<char> allletters = new HashSet<char>();
        private string rucksackstring;
  
        public Rucksack(string rucksackstring)
        {
            this.rucksackstring = rucksackstring;
        }

        public void BuildRucksack()
        {
            foreach(var letter in rucksackstring)
            {
                allletters.Add(letter);
            }
        }

        public void BuildSplitRucksack()
        {
            for (int i = 0; i < rucksackstring.Length / 2; i++)
            {
                leftletters.Add(rucksackstring[i]);
            }

            for (int i = rucksackstring.Length / 2; i < rucksackstring.Length; i++)
            {
                rightletters.Add(rucksackstring[i]);
            }
        }

        public HashSet<char> GetAllLetters
        {
            get { return allletters; }
            set { allletters = value; }
        }
    }
}