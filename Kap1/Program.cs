using System;
using static System.Net.Mime.MediaTypeNames;

namespace Chapter
{
    class Program
    {
        static void One()
        {
            Console.WriteLine("Adrian.");
        }

        static void Two(string t = "1")
        {
            Console.WriteLine($"Case {t}: Start");
            switch (t)
            {
                case "1":
                    Console.WriteLine("Detta är ett program gjort i C#.\nProgrammet skriver ut text på flera rader.\nDetta är programmets sista utskrift.\n");
                    break;

                case "2":
                    Console.WriteLine("Detta är ett program gjort i C#.");
                    Console.WriteLine("Programmet skriver ut text på flera rader.");
                    Console.WriteLine("Detta är programmets sista utskrift.");
                    Console.WriteLine();
                    break;

                case "3":
                    Console.WriteLine(	"Detta är ett program gjort i C#.\n" +
                                        "Programmet skriver ut text på flera rader.\n" +
                                        "Detta är programmets sista utskrift.\n"
                                     );
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            Console.WriteLine("End\n");
        }

        static void Three()
        {
            Console.WriteLine("\"Hej\" hörde jag någon säga på stan.\nEfter en stund svarade någon annan också med ett \"Hej!\".");
        }

        static void Four()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("SVERIGE!!!!");
        }

        static void Five()
        {
            Console.WriteLine("Du är nästan klar med kapitel 1.\nDu har lärt dig skriva ut tecken som \" och \\.");
        }

        static void Six()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("MemoryError: inte plats för mer golf Janne hole-in-one videos. (441/5 platser)");
        }

        static void Seven()
        {
            Console.WriteLine("   /\\");
            Console.WriteLine("  /  \\");
            Console.WriteLine(" / \"\" \\");
            Console.WriteLine("/______\\");
        }


        static void Main(string[] args)
        {
            /// 1.1 
            One();
            ProceedCheck();

            /// 1.2
            Two("1");
            Two("2");
            Two("3");
            Two("Non integer input");
            ProceedCheck();

            /// 1.3
            Three();
            ProceedCheck();

            /// 1.4
            Four();
            ProceedCheck();

            /// 1.5
            Five();
            ProceedCheck();

            /// 1.6
            Six();
            ProceedCheck();

            /// 1.7
            Seven();
            ProceedCheck();

        }

        static void ProceedCheck()
        {
            Console.WriteLine("Proceed? (y/n)");
            string a = Console.ReadLine();

            switch (a)
            {
                case "y":
                    Console.Clear();
                    Console.ResetColor();
                    break;
                case "n":
                    ProceedCheck();
                    break;
                default:
                    break;
            }
        }
    }
}