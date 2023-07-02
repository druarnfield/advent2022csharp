namespace AdventOfCode2022
{
    class DaySeven
    {
        private static DirectoryTree directoryTree = new DirectoryTree();
        public static void Run()
        {
            string[]? input = TextUtils.ReadFile("./input/DaySeven.input");
            if (input == null)
            {
                Console.WriteLine("Input file not found.");
                return;
            }
            var tokens = Lexer.Tokenize(input);
            Parser.ParseTokens(ref directoryTree, ref tokens);

            List<int> totalSizesWithLessThan100000 = new List<int>();

            TraverseAndCalculateSizes(directoryTree.Root, totalSizesWithLessThan100000);


            Console.WriteLine(totalSizesWithLessThan100000.Min());

        }

        public static void TraverseAndCalculateSizes(DirectoryNode directoryNode, List<int> totalSizesWithLessThan100000)
        {
            // Calculate the total size assuming current directoryNode is the root
            int totalSize = CalculateTotalSize(directoryNode);

            // If totalSize is less than 100000, add it to the list
            if (totalSize > 389918 && totalSize < 500000)
            {
                totalSizesWithLessThan100000.Add(totalSize);
            }

            // Recursively call for all children
            foreach (var child in directoryNode.Children)
            {
                TraverseAndCalculateSizes(child, totalSizesWithLessThan100000);
            }
        }

        public static int CalculateTotalSize(DirectoryNode directoryNode)
        {
            int totalSize = 0;

            // Add up the file sizes in the current directory
            foreach (var file in directoryNode.Files)
            {
                totalSize += file.Size;
            }

            // Recursively add up the sizes of the subdirectories
            foreach (var child in directoryNode.Children)
            {
                totalSize += CalculateTotalSize(child);
            }

            return totalSize;
        }



    }


    // Lexer
    public enum TokenType
    {
        Command,
        Directory,
        File
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    public class Lexer
    {
        public static List<Token> Tokenize(string[] input)
        {
            List<Token> tokens = new List<Token>();
            foreach (string line in input)
            {
                if (line.StartsWith("$"))
                {
                    tokens.Add(new Token(TokenType.Command, line.Substring(1).Trim()));
                }
                else if (line.StartsWith("dir "))
                {
                    tokens.Add(new Token(TokenType.Directory, line.Substring(3).Trim()));
                }
                else if (char.IsNumber(line[0]))
                {
                    tokens.Add(new Token(TokenType.File, line.Trim()));
                }
                else
                {
                    throw new Exception("Unknown token type.");
                }
            }
            return tokens;
        }
    }

    // Parser
    public class Parser
    {
        public static void ParseTokens(ref DirectoryTree directoryTree, ref List<Token> tokens)
        {
            var currentDirectory = directoryTree.Root;
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Command:
                        if (token.Value.StartsWith("cd"))
                        {
                            var cdcommand = new CdCommand(token.Value.Substring(2).Trim());
                            cdcommand.Execute(ref currentDirectory);
                        }
                        break;
                    case TokenType.Directory:
                        var dircommand = new DirCommand(token.Value);
                        dircommand.Execute(ref currentDirectory);

                        break;
                    case TokenType.File:
                        var sizeandfile = token.Value.Split(' ');
                        if (sizeandfile.Length != 2)
                        {
                            throw new Exception("Invalid file token.");
                        }
                        var size = int.Parse(sizeandfile[0]);
                        var filename = sizeandfile[1];
                        var touchcommand = new TouchCommand(filename, size);
                        touchcommand.Execute(ref currentDirectory);
                        break;
                    default:
                        throw new Exception("Unknown token type.");
                }
            }
            Console.WriteLine("Parsing complete.");
        }

    }

    public abstract class Command
    {
        public abstract void Execute(ref DirectoryNode CurrentDirectory);
    }

    public class TouchCommand : Command
    {
        public string FileName { get; set; }
        public int Size { get; set; }

        public TouchCommand(string fileName, int size)
        {
            FileName = fileName;
            Size = size;
        }

        public override void Execute(ref DirectoryNode CurrentDirectory)
        {
            var targetFile = CurrentDirectory.Files.Find(x => x.Filename == FileName);

            // Check if the target file was found among files
            if (targetFile != null)
            {
                Console.WriteLine($"File '{FileName}' already exists.");
            }
            else
            {
                // Create a new file and add it to the files of the current directory
                var newFile = new dFile(FileName, Size);
                CurrentDirectory.Files.Add(newFile);
            }
        }
    }

    public class DirCommand : Command
    {
        public string DirectoryName { get; set; }

        public DirCommand(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public override void Execute(ref DirectoryNode CurrentDirectory)
        {
            var targetDirectory = CurrentDirectory.Children.Find(x => x.Name == DirectoryName);

            // Check if the target directory was found among children
            if (targetDirectory != null)
            {
                Console.WriteLine($"Directory '{DirectoryName}' already exists.");
            }
            else
            {
                // Create a new directory and add it to the children of the current directory
                var newDirectory = new DirectoryNode(DirectoryName, CurrentDirectory);
                CurrentDirectory.Children.Add(newDirectory);
            }

        }
    }

    public class CdCommand : Command
    {
        public string DirectoryName { get; set; }

        public CdCommand(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public override void Execute(ref DirectoryNode CurrentDirectory)
        {
            if (DirectoryName == "..")
            {
                // Ensure that the current directory has a parent before navigating up
                if (CurrentDirectory.Parent != null)
                {
                    CurrentDirectory = CurrentDirectory.Parent;
                }
                else
                {
                    // Optionally, display a message indicating that this is the root directory
                    Console.WriteLine("Already at the root directory.");
                }
            }
            else if (DirectoryName == "/")
            {
                // Navigate to the root directory
                while (CurrentDirectory.Parent != null)
                {
                    CurrentDirectory = CurrentDirectory.Parent;
                }
            }
            else
            {
                var targetDirectory = CurrentDirectory.Children.Find(x => x.Name == DirectoryName);

                // Check if the target directory was found among children
                if (targetDirectory != null)
                {
                    CurrentDirectory = targetDirectory;
                }
                else
                {
                    // Optionally, display an error message to indicate directory not found
                    Console.WriteLine($"Directory '{DirectoryName}' not found.");
                }
            }
        }
    }







    // Tree structure
    public class DirectoryNode
    {
        public string Name { get; set; }
        public DirectoryNode? Parent { get; set; }
        public List<DirectoryNode> Children { get; set; }
        public List<dFile> Files { get; set; }

        public DirectoryNode(string name, DirectoryNode? parent = null)
        {
            Name = name;
            Children = new List<DirectoryNode>();
            Files = new List<dFile>();
            Parent = parent;
        }
    }

    public class DirectoryTree
    {
        public DirectoryNode Root { get; set; }

        public DirectoryTree()
        {
            Root = new DirectoryNode("root");
        }
    }

    public class dFile
    {
        public string Filename { get; set; }
        public int Size { get; set; }

        public dFile(string filename, int size)
        {
            Filename = filename;
            Size = size;
        }
    }
}