using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkReadWrite
{
    class MakeTree
    {
        public string temp;
        public string dash = "-";
        public List<string> tree = new List<string>();
        public int iter = 1;

        public void TreeToConsole(string PathDirectory)
        {
            DirectoryInfo rr = new DirectoryInfo(PathDirectory);
            foreach (var item in rr.GetDirectories())
            {
                for (int i = 0; i < iter; i++)
                {
                    Console.Write(dash);
                    temp += dash;
                }
                Console.WriteLine(item);
                temp += item;
                tree.Add(temp);
                temp = null;
                iter++;
                TreeToConsole(PathDirectory + "/" + item);
            }
            foreach (var item in rr.GetFiles())
            {
                for (int i = 0; i < iter; i++)
                {
                    Console.Write(dash);
                    temp += dash;
                }
                Console.WriteLine(item);
                temp += item;
                tree.Add(temp);
                temp = null;
            }
            iter--;
        }
        public void AddObjectToList(FileSystemInfo item, List<string> tree)
        {
            for (int i = 0; i < iter; i++)
            {
                temp += dash;
            }
            temp += item;
            tree.Add(temp);
            temp = null;
        }
        public void MakeListDirectory(string FileName, string PathDirectory)
        {
            DirectoryInfo rr = new DirectoryInfo(PathDirectory);

            foreach (var item in rr.GetDirectories())
            {
                AddObjectToList(item, tree);
                iter++;
                MakeListDirectory(FileName, PathDirectory + "/" + item);
            }
            foreach (var item in rr.GetFiles())
            {
                AddObjectToList(item, tree);
            }
            iter--;
        }

        public void TreeToFile(string FileName, string PathDirectory)
        {
            if (tree.Count != 0)
                File.WriteAllLines("file.txt", tree);
            else
            {
                MakeListDirectory(FileName, PathDirectory);
                File.WriteAllLines("file.txt", tree);
            }
        }

        public void Print(string FileName, string PathDirectory)
        {
            if (tree.Count == 0)
            {
                MakeListDirectory(FileName, PathDirectory);
                TreeToFile(FileName, PathDirectory);
            }
            string[] mass = File.ReadAllLines(FileName);
            foreach (var item in mass)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var d = new MakeTree();
            Console.SetBufferSize(Console.BufferWidth, 1900);
            string str = @"E://Загрузки";
            int selection;
            do
            {
                Console.WriteLine("press 1 if you want to print tree to console ");
                Console.WriteLine("press 2 if you want  write tree to file ");
                Console.WriteLine("press 3 if you want read from file and print to console");
                Console.WriteLine("press 0 for exite");
                selection = int.Parse(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        d.TreeToConsole(str);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        d.TreeToFile("file.txt", str);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        d.Print("file.txt", str);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (selection != 0);
        }
    }
}
