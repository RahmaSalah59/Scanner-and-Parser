using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
class Scanner
{
    // List of C-like language keywords
    static List<string> cKeywords = new List<string>
    {
        "auto", "break", "case", "char", "const", "continue", "default", "do",
        "double", "else", "enum", "extern", "float", "for", "goto", "if",
        "int", "long", "register", "return", "short", "signed", "sizeof", "static",
        "struct", "switch", "typedef", "union", "unsigned", "void", "volatile", "while", "include", "stdio.h"
    };

    static bool IsKeyword(string word)
    {
        return cKeywords.Contains(word);
    }

    static bool IsCharConstant(string word)
    {
        return ((word.StartsWith("\"") || word.StartsWith('\'')) && (word.EndsWith("\"") || word.EndsWith('\'')));
    }
    static bool IsNewLine(String word)
    {
        return String.Equals(word, "\n");
    }

    static bool IsNumber(string word)
    {
        return Regex.IsMatch(word, @"^[0-9]+(\.[0-9]+)?$");
    }
    static bool IsOperator(char word)
    {
        return new List<string> { "*", "/", "%", "-", "=", "+" }.Contains(word.ToString());
    }
    static bool IsRelationalOperator(string word)
    {
        return new List<string> { "<", "<=", ">", ">=", "==", "!=" }.Contains(word);
    }
    static bool IsSpecialCharacter(char ch)
    {
        return new List<char> { ';', ':', ',', '[', ']', '(', ')', '{', '}', '#', '\\' }.Contains(ch);
    }
    static bool IsWhitespace(char ch)
    {
        return char.IsWhiteSpace(ch);
    }
    static bool IsIdentifier(string word)
    {
        return Regex.IsMatch(word, @"^[A-Za-z_][A-Za-z0-9_]*$");
    }

    static string GetTokenType(string token)
    {
        if (IsKeyword(token)) return "Keyword";
        if (IsNewLine(token)) return "newline";
        if (IsCharConstant(token)) return "CharConstant";
        if (IsNumber(token)) return "Number";
        if (IsOperator(token[0])) return "Operator";
        if (token.Length == 1 && IsRelationalOperator(token)) return "RelationalOperator";
        if (token.Length == 2 && IsRelationalOperator(token)) return "RelationalOperator";
        if (token.Length == 1 && IsSpecialCharacter(token[0])) return "SpecialCharacter";
        if (IsIdentifier(token)) return "Identifier";
        return "Unknown";
    }

    static void WriteTokensToFile(string filePath, List<(string, string)> tokens)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var token in tokens)
                writer.WriteLine($"({token.Item1}, {token.Item2})");
        }
    }

    static void Main(string[] args)
    {

        string currentDirectory = Directory.GetCurrentDirectory();
        string projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
        Console.WriteLine(projectDirectory);
        string filename = $"{projectDirectory}\\C_Program.c";

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string fileContent = File.ReadAllText(filename);

        List<(string, string)> tokens = new List<(string, string)>();
        string currentToken = "";

        for (int i = 0; i < fileContent.Length; i++)
        {

            char currentChar = fileContent[i];
            char nextChar;
            try
            {  nextChar = fileContent[i + 1]; }
            catch ( Exception ex )
            { Console.WriteLine(ex.Message);
                nextChar = currentChar; }
            if (IsWhitespace(currentChar) || IsSpecialCharacter(currentChar) || IsOperator(currentChar) ||IsRelationalOperator(currentChar.ToString()))
            {
                if( currentChar == '/' && nextChar =='/')
                {
                    while(! IsNewLine(currentChar.ToString()) )
                    {
                        currentToken += currentChar;
                        i++;
                        currentChar = fileContent[i];
                        
                    }
                    tokens.Add(("comment", currentToken.ToString()));
                    currentToken = "";
                }

                if (IsSpecialCharacter(currentChar) || IsNewLine(currentChar.ToString()) || IsOperator(currentChar) || IsRelationalOperator(currentChar.ToString()))

                    tokens.Add((GetTokenType(currentChar.ToString()), currentChar.ToString()));

                if (currentToken != "")
                {
                    tokens.Add((GetTokenType(currentToken), currentToken));
                    currentToken = "";
                }
            }
            else
            {
                currentToken += currentChar;
            }
            
        }

        
        if (currentToken != "")
                tokens.Add((GetTokenType(currentToken), currentToken));

        


        WriteTokensToFile($"{projectDirectory}\\output.txt", tokens);
        Console.WriteLine("Tokens written to output.txt");

    }
}