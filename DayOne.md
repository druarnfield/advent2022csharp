# Day One

A little about myself.  I'm a 30 year old helicopter maintainer from Australia, violently attempting to teach myself how to make software in my spare time to hopefully make a career change some day.   I've decided to teach myself C# and to complete the [advent of code](https://adventofcode.com/2022/day/1) calendar from last year as a starting point.

After spending the last 2 days learning how to set up neovim and the omnisharp LSP and subsequently being disappointed that it wasn't as responsive as expected, I have decided to go through the calander with VS Code.  Day one might be a bit longer because I don't really know what I'm doing...

### Problem Summary

let's bring that Christmas magic into code! We've got an adventure that starts with the Elves making a list (and checking it twice!) of all the calories they're packing for the journey into the jungle. Each elf keeps a record of their own inventory, separated from the previous Elf's by a blank line - an elfishly organized way of doing things!

Our task is a high-stakes game of 'Hunt the Calorie King'. We'll be analyzing the elves' meticulous notes and identifying the Elf with the most calorie-dense inventory. So, with each elf listed along with their individual inventory total, you need to search for the highest caloric total - a.k.a, the champion calorie carrier.

### Example Input

```
8417
8501
5429
2112

7971
9636
4003

4697
2941
3275

....
```

### High level intuition

```pseudocode
array <- ReadFile(input) // Read in the file to an array
count <- 0 // Set a counter to 0
Totals_dynamic_array <- new dynamic array

for item in array {
  if item is whitespace {
    Totals_dynamic_array.Add(count)
    count <- 0
  } else {
    count <- count + item
  }
}

Totals_dynamic_array.Sort() // Sort the array in ascending order
Totals_dynamic_array.Reverse() // Reverse the array to get descending order
maxCalories <- Totals_dynamic_array[0] // Get the highest value

Print maxCalories // Show the total Calories carried by the Elf with the most Calories

```







### Learning outcomes

We will need to learn about these things to get through today:

- How to setup a C# console application
- Me realising I have to use object orientation for everything
- Program structure
- File I/O
- Lists, Arrays & SortedSets
- Sorting

### Setting up a console Application in C#

We will use the [dotnet CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) to scaffold the console application like this:

```C#
dotnet new console --name AdventOfCode2022
```

This does all the groundwork for us under the hood so we can start writing our application in Program.cs.

### OO For Everything

Coming from primarily python it is a bit jarring to be *highly encouraged* to use OO for everything.  The best I can tell right now is a C# application is organised into `namespaces` which have `classes`.  The 'Program' class has a Main function which is the entry point for our console application. I've set up the Program.cs like this:

```C#
namespace AdventOfCode2022 
{
  class Program
  {
    static void Main(string[] args)
    {
			Console.WriteLine("What's up fam?");
    }
  }
}
```

The curly brace convention seems like a massive waste of whitespace...

I have decided to split each day of the calendar into a seperate file in its own class, so I made DayOne.cs:

```c#
namespace AdventOfCode2022 
{
  public class DayOne
  {
    private string[]? lines;
    private SortedSet<int> elfTotals = new SortedSet<int>();
    
    public DayOne(string input)
    {
      
    }
    
    private void readFile(string input)
    {
      
    }
    
    private void FindElf
    
  }
}
```

