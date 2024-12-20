using System;
using System.Collections.Generic;

class TopDownParser
{
    private string input;
    private int position;

    // Class to represent a parse tree node
    class ParseNode
    {
        public string Symbol { get; set; }
        public List<ParseNode> Children { get; set; } = new List<ParseNode>();

        public ParseNode(string symbol)
        {
            Symbol = symbol;
        }
    }
    char failed;
    private Dictionary<string, List<string>> grammar;

    public TopDownParser(Dictionary<string, List<string>> grammar, string input)
    {
        this.grammar = grammar;
        this.input = input;
        this.position = 0;
    }

    public bool Parse()
    {
        Console.Write("The input String: [");
        for (int i = 0; i < input.Length; i++)
        {
            Console.Write("'" + input[i] + "'");
            if (i < input.Length - 1) Console.Write(", ");
        }
        Console.WriteLine("]");

        // Start parsing and building the tree
        ParseNode root = S();
        if (root != null && position == input.Length)
        {

            Console.WriteLine("\nThe stack after checking: []");
            Console.WriteLine("The rest of unchecked String: []");
            Console.WriteLine("\nParsing successful!\nParse Tree:");
            PrintParseTree(root); // Print the tree
            return true;
        }
        else
        {
            Console.WriteLine($"\nThe stack after checking: [{failed}]");
            Console.WriteLine("The rest of unchecked String: []");
            Console.WriteLine("\nParsing failed.");
            return false;
        }
    }

    // Function to parse starting symbol S
    private ParseNode S()
    {
        return ApplyRule("S");
    }

    // Function to parse non-terminal B
    private ParseNode B()
    {
        return ApplyRule("B");
    }

    // Applies a rule to a non-terminal and constructs the parse tree
    private ParseNode ApplyRule(string nonTerminal)
    {
        if (grammar.ContainsKey(nonTerminal))
        {
            foreach (var production in grammar[nonTerminal])
            {
                int backupPosition = position; // Save position for backtracking
                var parentNode = new ParseNode(nonTerminal); // Root of this subtree
                bool matchSuccess = true;

                foreach (var symbol in production)
                {
                    if (char.IsUpper(symbol)) // Non-terminal
                    {
                        failed = symbol;

                        var childNode = ApplyRule(symbol.ToString());
                        if (childNode != null)
                        {

                            parentNode.Children.Add(childNode);
                        }
                        else
                        {
                            matchSuccess = false;
                            break;
                        }
                    }
                    else // Terminal
                    {
                        if (Match(symbol))
                        {
                            parentNode.Children.Add(new ParseNode(symbol.ToString()));
                        }
                        else
                        {
                            matchSuccess = false;
                            break;
                        }
                    }
                }

                if (matchSuccess) return parentNode; // Return the constructed tree
                position = backupPosition; // Backtrack
            }
        }
        return null; // No matching production
    }

    private bool Match(char terminal)
    {
        if (position < input.Length && input[position] == terminal)
        {
            position++;
            return true;
        }
        return false;
    }

    private void PrintParseTree(ParseNode node, string indent = "", bool isLast = true)
    {
        Console.Write(indent);
        if (isLast)
        {
            Console.Write("└─");
            indent += "  ";
        }
        else
        {
            Console.Write("├─");
            indent += "| ";
        }
        Console.WriteLine(node.Symbol);

        for (int i = 0; i < node.Children.Count; i++)
        {
            PrintParseTree(node.Children[i], indent, i == node.Children.Count - 1);
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n              Grammar          \n");

            Dictionary<string, List<string>> grammar = new Dictionary<string, List<string>>();

            // Input rules for non-terminal S
            grammar["S"] = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                Console.Write($"Enter rule {i} for 'S': ");
                grammar["S"].Add(Console.ReadLine().Trim().Replace("S ->", "").Trim());
            }

            // Input rules for non-terminal B
            grammar["B"] = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                Console.Write($"Enter rule {i} for 'B': ");
                grammar["B"].Add(Console.ReadLine().Trim().Replace("B ->", "").Trim());
            }

            // Input string to parse
           label1: Console.Write("\nEnter the input string to parse: ");
            string inputString = Console.ReadLine().Trim();

            // Create and use the parser
            TopDownParser parser = new TopDownParser(grammar, inputString);
            parser.Parse();

            // Menu
            Console.WriteLine("\n1- Another Grammar\n2- Another String\n3- Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1) continue;
            else if (choice == 2) goto label1;
            else if (choice == 3) break;
        }
    }
}
