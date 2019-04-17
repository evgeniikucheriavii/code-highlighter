using System;
using System.IO;

namespace TextCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to your code file: ");

            string codePath = Console.ReadLine(); //Getting code path

            if(File.Exists(codePath)) //Checking if the file exists
            {
                string code = File.ReadAllText(codePath); //Reading text from file
                code = TextHighlight.HighlightCode(code); //Adding styled span elements

                Console.WriteLine("Your Code: "); //Showing the result
                Console.WriteLine(code);

                Console.WriteLine("Do you want to save it? ");
                string answer = Console.ReadLine();
                
                if(!answer.ToLower().StartsWith("n")) //if answer is not no or such
                {
                    Console.WriteLine("Enter new name (leave empty to save with old name): ");
                    string newPath = Console.ReadLine();

                    if(newPath == string.Empty)
                    {
                        newPath = codePath; //if input is empty, using old path
                    }
                    
                    try
                    {
                        string [] docWrap = TextHighlight.GetDocumentCode(); //getting wrapper for code

                        string codeDocument = docWrap[0] + code + docWrap[1]; //wrapping code to html document

                        File.WriteAllText($"output\\{newPath}.html", codeDocument); //saving to the output folder

                        Console.WriteLine("Success!");
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine(exc.Message); //Error while saving
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no such file!"); //File does not exist!
            }
            Console.ReadKey();
        }
    }
}
