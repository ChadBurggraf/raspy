# Raspy
#### Arithmetic expression parsing and evaluation library for .NET.

Raspy is a simple arithmetic parser and evaluator written in C#, for use in any .NET project.
The library primarily consists of a C# implementation of the [Shunting-yard algorithm](http://en.wikipedia.org/wiki/Shunting-yard_algorithm) 
for expression parsing, and a postfix evaluator for evaluation.

## Building

Run the build script with MSBuild v4.0 (C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe or similar):

    MSBuild Build.proj

You can also build using the solution in Visual Studio 2010.

## Basic Usage

`Raspy.dll` is compiled against the .NET Framework v3.5 Client Profile.

Include the `Raspy` namespace in your source file. There are a few extension methods available for the
most common operations (e.g., evaluating an expression to get a result and parsing an expression to 
get a tokenized postfix expression queue). The extensions are available on the `string` class, and 
implemented in `Raspy.RaspyExpressions`:

    static ExpressionQueue Parse(this string expression);
	static object ParseAndEvaluate(this string expression);
	static bool TryParseAndEvaluate(this string expression, out object result);

You may also invoke the steps manually:
 
    Parser parser = new Parser();
	Evaluator evaluator = new Evaluator();
	string expression = "3 + 4 * 5";

	object result = evaluator.Evaluate(parser.Parse(expression));

## License

Licensed under the [MIT](http://www.opensource.org/licenses/mit-license.html) license. See LICENSE.txt.

Copyright (c) 2012 Chad Burggraf. 