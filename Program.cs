// beginning of program that uses encapsulation
// to keep variables together for easy use.
// When you add a grade the differeent classes are used
// to do different things in the classes themselves.
/*
 * Currently only have 3 classes Program has main 
 * GradeBook has adding the grade function and doing computation
 * GradeStatistics holds the varables needed to hold the requirments needed to be called in main
 * 
 * this documentation was put together in 3 minutes 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Synthesis;
using System.IO;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            IGradeTracker book = CreateGradeBook();  // defining var of grade book    and interface

            GetBookName(book);
            /*****************delegates and events ******************/
            //book.NameChanged += new NameChangedDelegate(OnNameChanged);
            // book.NameChanged += new NameChangedDelegate(OnNameChanged2);
            // book.NameChanged += OnNameChanged2;  // dont need new keyword

         //   book.Name = "Kyle's Grade Book";
         //   book.Name = "Grade Book";
            AddGrades(book);
            SaveToFile(book);

            GradeStatistics stats = book.ComputeStatistics();
         //   Console.WriteLine(book.Name);
            WriteResult("Average: ", stats.AverageGrade);
            WriteResult("Highest: ", (int)stats.HighestGrade);
            WriteResult("Lowest: ", stats.LowestGrade);
            WriteResult("Grade: ", stats.LetterGrade);

        }

        private static IGradeTracker CreateGradeBook() // ref to abstract class
        {

            // SpeechSynthesizer synth = new SpeechSynthesizer();
            //  synth.Speak("hello this is the grade book program");

            return new ThrowAwayGradeBook(); // must be concrete type 
        }

        private static void SaveToFile(IGradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book.WriteGrades(outputFile);
                // outputFile.Close();  happens for me cause of using

            }
        }

        private static void AddGrades(IGradeTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
        }

        private static void GetBookName(IGradeTracker book)
        {
            try
            {
                Console.WriteLine("Enter a name");
                book.Name = Console.ReadLine(); // put readline into a variable and validate it on ur own time.

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Mom Something is wrong");
            }
            catch (Exception)
            {
                Console.WriteLine("Mom Something is wrong BUT you have no idea What.. Went wrong");
            }
            finally
            {
                /// ... close the file flush it
            }
        }

        /*   static void WriteResult(string description, int result)
           {
               Console.WriteLine( description + ": " + result);

           }
           */
        static void WriteResult(string description, float result)
        {
            Console.WriteLine($"{description} :  {result}");
        }
        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description} :  {result}");
        }


        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Gradebook canging name from {args.ExistingName} to {args.NewName}");
        }
        /*
                    static void OnNameChanged2(string existingName, string newName)
                    {
                        Console.WriteLine("***");
                    }
                */
    }
}
