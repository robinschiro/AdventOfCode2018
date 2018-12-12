using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2018CSharp
{
    public class DayEight : DayProblem
    {
        public class Node
        {
            int numMetadata;
            int[] metadata;
            int numChildren;
            Node[] children;
        }

        public void RunSolution()
        {
            CreateTree();
        }

        public static Node CreateTree()
        {
            Node root = new Node();
            string inputStream = File.ReadAllText(@"..\..\..\DayEight\input.txt");
            int[] input = inputStream.Split(' ').Select<string, int>(c => Convert.ToInt32(c)).ToArray();

            return root;
        }
    }
}
