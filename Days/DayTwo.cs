using System.Linq;

namespace AdventOfCode2022
{
    public class DayTwo
    {

        // Some intuition about rock paper scissors:
        // 1 -> 2 : win
        // 2 -> 1 : loss
        // 1 -> 1 : draw
        // 2 -> 2 : draw
        // 1 -> 3 : loss
        // 3 -> 1 : win
        // 2 -> 3 : win
        // 3 -> 2 : loss
        // 3 -> 3 : draw 

        // Generalisation:
        // if the absolute value of the difference between the two numbers is 0, it's a draw.
        // If the absolute value of the difference between the two numbers is 1, the higher number wins.
        // If the absolute value of the difference between the two numbers is 2, the lower number wins.

        private static string[]? lines = TextUtils.ReadFile("./input/DayTwo.input");
        private static Dictionary<char, int> PlayerKey = new Dictionary<char, int>();
        private static Dictionary<char, WinLossOrDraw> PlayerKeyPartTwo = new Dictionary<char, WinLossOrDraw>();
        private static Dictionary<char, int> OpponentKey = new Dictionary<char, int>();

        private enum WinLossOrDraw
        {
            Win,
            Loss,
            Draw
        }


        public static void Run()
        {
            // Opponent Values
            OpponentKey.Add('A', 1);
            OpponentKey.Add('B', 2);
            OpponentKey.Add('C', 3);

            // Player Values
            PlayerKey.Add('X', 1);
            PlayerKey.Add('Y', 2);
            PlayerKey.Add('Z', 3);

            Console.WriteLine("The player's total for part A is: " + playGames());

            PlayerKeyPartTwo.Add('X', WinLossOrDraw.Loss);
            PlayerKeyPartTwo.Add('Y', WinLossOrDraw.Draw);
            PlayerKeyPartTwo.Add('Z', WinLossOrDraw.Win);

            Console.WriteLine("The player's total for part B is: " + playGamesPartTwo());


        }

        private static int CalculateScore(int opponent, int player)
        {
            var absoluteDifference = Math.Abs(opponent - player);

            switch (absoluteDifference)
            {
                case 0:
                    return player + 3;
                case 1:
                    if (player > opponent)
                    {
                        return player + 6;
                    }
                    break;
                case 2:
                    if (player < opponent)
                    {
                        return player + 6;
                    }
                    break;
            }

            return player; // Default case or case where absoluteDifference > 2
        }

        private static int winLossOrDraw(int opponent, WinLossOrDraw player)
        {
            switch (player)
            {
                case WinLossOrDraw.Win:
                    switch (opponent)
                    {
                        case 1:
                        case 2:
                            return opponent + 1;
                        case 3:
                            return 1;
                    }
                    break;
                case WinLossOrDraw.Loss:
                    switch (opponent)
                    {
                        case 1:
                            return 3;
                        case 2:
                        case 3:
                            return opponent - 1;
                    }
                    break;
                case WinLossOrDraw.Draw:
                    return opponent;
                default:
                    return 0;
            }
            return 0;

        }

        private static int playGames()
        {
            try
            {
                if (lines == null)
                {
                    throw new InvalidOperationException("The input file has not been read.");
                }
                var totalScore = lines.Select(line => line.Split(' '))
                .Select(line => CalculateScore(OpponentKey[line[0][0]], PlayerKey[line[1][0]]))
                .Sum();

                return totalScore;
            }

            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return 0;
            }


        }

        private static int playGamesPartTwo()
        {
            try
            {
                if (lines == null)
                {
                    throw new InvalidOperationException("The input file has not been read.");
                }
                var totalScore = lines.Select(line => line.Split(' '))
                .Select(line => CalculateScore(OpponentKey[line[0][0]], winLossOrDraw(OpponentKey[line[0][0]], PlayerKeyPartTwo[line[1][0]])))
                .Sum();

                return totalScore;
            }

            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

    }
}