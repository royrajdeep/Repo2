using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontline
{
    class Program
    {
        static int SpaceCount = -1;
        static Dictionary<string, string> DicAry = new Dictionary<string, string>();
        static string TempAry = string.Empty;
        static List<string> ParentAry = new List<string>();
        static string Parent = string.Empty;
        static string Str = "(id,created,employee(id,firstname,employeeType(id), lastname),location)";
        static void Main(string[] args)
        {
			//comment
            Str = Str.Replace(" ", "");
            foreach (char elem in Str)
            {
                BuildPrefixString();

                if (elem == '(')
                {
                    AddToArray(1);
                    ParentAry.Add(TempAry);
                }
                else if (elem == ')')
                {
                    AddToArray(-1);
                    ParentAry.RemoveAt(ParentAry.Count - 1);
                }
                else if (elem == ',')
                {
                    AddToArray(0);
                }
                else
                {
                    TempAry = TempAry + elem;
                    continue;
                }
                TempAry = string.Empty;
            }
            PrintLists();
        }

        static void AddToArray(Int16 Indent)
        {
            if (!string.IsNullOrEmpty(TempAry))
                DicAry.Add(new string('-', SpaceCount) + TempAry, (Parent == TempAry ? Parent : Parent + TempAry));

            SpaceCount = SpaceCount + Indent;
        }

        static void BuildPrefixString()
        {
            Parent = String.Join("", ParentAry.ToArray());
            Parent = String.IsNullOrEmpty(Parent) ? TempAry : Parent;
        }

        static void PrintLists()
        {
            Console.WriteLine("Indented List");
            Console.WriteLine("-------------------------------");
            var SortedDict = from entry in DicAry select entry;
            foreach (KeyValuePair<string, string> item in SortedDict)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine("");
            Console.WriteLine("Ordered List");
            Console.WriteLine("-------------------------------");

            SortedDict = from entry in DicAry orderby entry.Value ascending select entry;
            foreach (KeyValuePair<string, string> item in SortedDict)
            {
                Console.WriteLine(item.Key);
            }

            Console.ReadLine();
        }
    }
}
