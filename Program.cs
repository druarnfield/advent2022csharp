namespace AdventOfCode2022 
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length > 1 && args[0] == "-d" && int.TryParse(args[1], out int day))
      {
        switch (day)
        {
          case 1:
            DayOne.Run("./input/DayOne.input");
            Console.WriteLine("The top elf's total is: " + DayOne.GetTopElf());
            Console.WriteLine("The top three elves' total is: " + DayOne.GetTopThreeElves());
            break;
          case 2: 
            DayTwo.Run();
            break;
          default:
            Console.WriteLine("Invalid day value provided.");
            break;
        }
      }
      else
      {
        Console.WriteLine("Usage: -d <day>");
      }
    }
  }
}
