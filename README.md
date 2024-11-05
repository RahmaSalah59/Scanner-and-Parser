# C Scanner Project
A comprehensive lexical analyzer (scanner) for C code, developed in C# to understand compiler design principles through tokenization, syntax recognition, and structure analysis

## code by
Rahma Salah Eldin Omar Hamouda

## Course Information
This project was developed as part of the Compiler Design course:
Institution: Faculty of Electronic Engineering,Menofia University

## Project Overview
This project implements a C scanner capable of reading, identifying, and categorizing tokens from C source code. The scanner analyzes various elements, including keywords, identifiers, operators, literals, and preprocessor directives, providing a structured breakdown of C code.

## Key Features
- Complete Token Recognition: Identifies all C tokens, such as keywords, symbols, and operators.
- Advanced Operator Handling: Includes recognition of complex operators and symbols.
- Comment Processing: Supports both single-line // .
- Preprocessor Directive Parsing: Recognizes #include, #define, and other directives.
- Accurate Source Code Tracking: Maintains line and column info for easy error reporting.
- Literal Support: Detects and categorizes string and character literals.

## Implementation Details
> The scanner is structured as follows:

- **Program.cs** : Core of the lexical analyzer, handling token detection and categorization , Test application, demonstrating the scanner's functionality on sample C code.
- **C_Program.c** : Identify the input C code to be scanned.
- **output.txt** : The output displays a structured list of tokens extracted from the source code. Each entry includes the token type (e.g., Keyword, Identifier, Number) followed by its value. This output reflects various elements

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/RahmaSalah59/Scanner.git
   cd Scanner
2. Open the solution: Launch the **scanner.sln** file in Visual Studio.
3. Build and run: Compile the project and execute it to start the scanner.

## Usage
- Run the Scanner: Start the Program.cs to process C_Program.c.
- Review Output: Tokens are outputted to output.txt, with details on each token type and position.

## License
This project was developed for educational purposes under the Compiler Design curriculum.


