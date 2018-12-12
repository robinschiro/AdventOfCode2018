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
            public int[] Metadata;
            public Node[] Children;
            public int Value;
        }

        public void RunSolution()
        {
            int[] input = ParseInput();

            var treeCreationResult = CreateTree(input, 0);

            Console.WriteLine("Part 1 Solution: " + SumMetadata(treeCreationResult.Item1));
            Console.WriteLine("Part 2 Solution: " + FindValue(treeCreationResult.Item1));
        }

        public static int[] ParseInput()
        {
            string inputStream = File.ReadAllText(@"..\..\..\DayEight\input.txt");
            int[] input = inputStream.Split(' ').Select<string, int>(c => Convert.ToInt32(c)).ToArray();

            return input;
        }

        public static Tuple<Node, int> CreateTree(int[] input, int index)
        {
            Node head = new Node();
            int curIndex = index;

            // Get num children and num metadata.
            head.Children = new Node[input[curIndex]];
            curIndex += 1;
            head.Metadata = new int[input[curIndex]];
            curIndex += 1;

            for (int i = 0; i < head.Children.Length; i++)
            {
                Tuple<Node, int> childCreationResult = CreateTree(input, curIndex);
                head.Children[i] = childCreationResult.Item1;
                curIndex = childCreationResult.Item2;
            }

            for (int i = 0; i < head.Metadata.Length; i++ )
            {
                head.Metadata[i] = input[curIndex];
                curIndex += 1;
            }

            return new Tuple<Node, int>(head, curIndex);
        }

        public static int SumMetadata(Node head)
        {
            return head.Metadata.Sum() + head.Children.Sum(child => SumMetadata(child));
        }

        public static int FindValue(Node head)
        {
            if (0 != head.Value)
            {
                return head.Value;
            }

            if (0 == head.Children.Length)
            {
                head.Value = head.Metadata.Sum();
                return head.Value;
            }

            int value = 0;
            foreach (int entry in head.Metadata)
            {
                int index = entry - 1;
                if (index < head.Children.Length)
                {
                    value += FindValue(head.Children[index]);
                }
            }

            head.Value = value;
            return value;
        }
    }
}
