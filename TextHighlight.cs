using System;

namespace TextCode
{
    public static class TextHighlight
    {

        public static string[] GetSpecialWordsArray()
        {
            string[] WordsArray = new string[]
            {
                "public", "var", "int", "string", "char",
                "bool", "void", "function", "private", "protected",
                "sealed", "abstract", "using", "namespace", "static",
                "const", "new", "class"
            };

            return WordsArray;
        }

        public static string[] GetConstructionsArray() {
            string [] ConstructionsArray = new string[]
            {
                "if", "else", "try", "catch", "return",
                "switch", "for", "do", "while"
            };
            
            return ConstructionsArray;
        }

        public static string[] GetSymbolsArray() {
            string [] SymbolsArray = new string[]
            {
                " > ", " < ", "{", "}", " = ",
                "-", "+", "*", " / ", "(",
                ")", "|", "&", "[", "]",
                ",", "%", ".", "!", ";",
                " >= ", "<= ", " == ", " != "
            };
            
            return SymbolsArray;
        }

        public static string HighlightCode(string text) 
        {
            string newText = text;
            string span = "";
            bool isStarted = false;
            bool dotStarted = false;

            for(int start = 0; start < newText.Length; start++) 
            {
                Console.WriteLine(newText.Length);
                if(newText[start] == '"')
                {
                    if(isStarted)
                    {
                        span = "</span>";
                        newText = newText.Insert(start + 1, span);
                        start += 1 + span.Length;
                        isStarted = false;
                    }
                    else
                    {
                        span = "<span class='code__quote'>";
                        newText = newText.Insert(start, span);
                        start += 1 + span.Length;
                        isStarted = true;
                    }
                }
                if(newText[start] == '.' || newText[start] == ' ' || newText[start] == '(')
                {
                    if(dotStarted) 
                    {
                        span = "</span>";
                        newText = newText.Insert(start + 1, span);
                        start += 1 + span.Length;
                        dotStarted = false;
                    }
                    if(newText[start] == '.' && newText[start - 1] != ' ' && newText[start + 1] != ' ')
                    {
                        span = "<span class='code__dot'>";
                        newText = newText.Insert(start, span);
                        start += 1 + span.Length;
                        dotStarted = true;
                    }
                }
            }

            string[] SymbolsArray = GetSymbolsArray();
            string temp = string.Empty;
            foreach(string val in SymbolsArray) 
            {
                temp = val;

                if(temp == "<") 
                {
                    temp = "<";
                    //change to code
                }

                if(temp == ">") 
                {
                    temp = ">";
                    //change to code
                }
                newText = newText.Replace($"{val}", $"<span class='code__symbol'>{temp}</span>");
            }

            string[] ConstructionsArray = GetConstructionsArray();
            
            foreach(string val in ConstructionsArray) 
            {
                newText = newText.Replace($"{val} ", $"<span class='code__construction'>{val}</span> ");
            }

            string[] WordsArray = GetSpecialWordsArray();

            foreach(string val in WordsArray) 
            {
                newText = newText.Replace($"{val} ", $"<span class='code__special-key'>{val}</span> ");
            }


            //add class types

            return newText;
        }

        public static string[] GetDocumentCode() 
        {
            string[] documentCode = new string[]
            {
                @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>This code was highlighted</title>
                        <link rel='stylesheet' href='highlight.css'>
                    </head>
                    <body>
                        <div class='code'>
                            <div class='code__content'>",

                            @"</div>
                        </div>
                    </body>
                </html>"
            };
            return documentCode;
        }
    }
}