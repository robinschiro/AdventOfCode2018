using System;
using System.Linq;

namespace AdventOfCode2018CSharp
{
    public class DayNine : DayProblem
    {
        private class Node
        {
            public long Data;
            public Node Next;
            public Node Prev;

            public Node(long data, Node next)
            {
                this.Data = data;
                this.Next = next;
            }

            public Node(long data, Node next, Node prev)
            {
                this.Data = data;
                this.Next = next;
                this.Prev = prev;
            }
        }

        public void RunSolution()
        {
            long highScore = FindHighScoreWithLinkedList(30, 5807 , 23, 7);
            Console.WriteLine($"Answer: {highScore}");
            Console.ReadLine();
        }

        public long FindHighScoreAnalytical(long numPlayers, long highestMarble, long scoreTrigger, long shiftBehind)
        {
            long score = 0;
            long[] players = new long[numPlayers];

            for (long i = scoreTrigger; i <= highestMarble; i += scoreTrigger)
            {
                long bonus = (((i - 1) - (long)Math.Round(shiftBehind / 2.0)) / 2);
                players[i % numPlayers] += i + bonus;
            }

            return players.Max();
        }

        public long FindHighScoreWithLinkedList(long numPlayers, long highestMarble, long scoreTrigger, long shiftBehind)
        {
            long[] players = new long[numPlayers];

            //Node circleTail = new Node(1, null);
            //Node currentNode = new Node(2, circleTail);
            //Node circleHead = new Node(0, currentNode);
            //circleTail.Next = circleHead;

            //circleHead.Prev = circleTail;
            //currentNode.Prev = circleHead;
            //circleTail.Prev = currentNode;

            Node currentNode = new Node(0, null);
            currentNode.Next = currentNode;
            currentNode.Prev = currentNode;
            
            for (long i = 1; i <= highestMarble; i++)
            {
                if (0 == (i % scoreTrigger))
                {
                    Node nodeToRemove = currentNode;

                    // Find node that is {triggerShift} positions before current node.
                    for (long j = 0; j < shiftBehind; j++)
                    {
                        nodeToRemove = nodeToRemove.Prev;
                    }

                    // Remove node, update links.
                    currentNode = nodeToRemove.Next;
                    nodeToRemove.Prev.Next = nodeToRemove.Next;
                    nodeToRemove.Next.Prev = nodeToRemove.Prev;

                    players[i % numPlayers] += i + nodeToRemove.Data;
                }
                else
                {
                    Node newNode = new Node(i, next: currentNode.Next.Next, prev: currentNode.Next);
                    currentNode.Next.Next.Prev = newNode;
                    currentNode.Next.Next = newNode;
                    currentNode = newNode;
                }
            }

            return players.Max();
        }
    }
}
