using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutoFac.DS
{
    public class PriorityQueuesProgram
    {
        public static void Executer()
        {
            Console.WriteLine("\nBegin Priority Queue demo");

            Console.WriteLine("\nCreating priority queue of Employee items\n");
            PriorityQueue<Employee> pq = new PriorityQueue<Employee>();

            Employee e1 = new Employee("Aiden", 1.0);
            Employee e2 = new Employee("Baker", 2.0);
            Employee e3 = new Employee("Chung", 3.0);
            Employee e4 = new Employee("Dunne", 4.0);
            Employee e5 = new Employee("Eason", 5.0);
            Employee e6 = new Employee("Flynn", 6.0);

            Console.WriteLine("Adding " + e5.ToString() + " to priority queue");
            pq.Enqueue(e5);
            Console.WriteLine("Adding " + e3.ToString() + " to priority queue");
            pq.Enqueue(e3);
            Console.WriteLine("Adding " + e6.ToString() + " to priority queue");
            pq.Enqueue(e6);
            Console.WriteLine("Adding " + e4.ToString() + " to priority queue");
            pq.Enqueue(e4);
            Console.WriteLine("Adding " + e1.ToString() + " to priority queue");
            pq.Enqueue(e1);
            Console.WriteLine("Adding " + e2.ToString() + " to priority queue");
            pq.Enqueue(e2);

            Console.WriteLine("\nPriory queue is: ");
            Console.WriteLine(pq.ToString());
            Console.WriteLine("\n");

            Console.WriteLine("Removing an employee from priority queue");
            Employee e = pq.Dequeue();
            Console.WriteLine("Removed employee is " + e.ToString());
            Console.WriteLine("\nPriory queue is now: ");
            Console.WriteLine(pq.ToString());
            Console.WriteLine("\n");

            Console.WriteLine("Removing a second employee from queue");
            e = pq.Dequeue();
            Console.WriteLine("\nPriory queue is now: ");
            Console.WriteLine(pq.ToString());
            Console.WriteLine("\n");

            Console.WriteLine("Testing the priority queue");
            TestPriorityQueue(50000);


            Console.WriteLine("\nEnd Priority Queue demo");
            Console.ReadLine();
        } // Main()

        static void TestPriorityQueue(int numOperations)
        {
            Random rand = new Random(0);
            PriorityQueue<Employee> pq = new PriorityQueue<Employee>();
            for (int op = 0; op < numOperations; ++op)
            {
                int opType = rand.Next(0, 2);

                if (opType == 0) // enqueue
                {
                    string lastName = op + "man";
                    double priority = (100.0 - 1.0) * rand.NextDouble() + 1.0;
                    pq.Enqueue(new Employee(lastName, priority));
                    if (pq.IsConsistent() == false)
                    {
                        Console.WriteLine("Test fails after enqueue operation # " + op);
                    }
                }
                else // dequeue
                {
                    if (pq.Count() > 0)
                    {
                        Employee e = pq.Dequeue();
                        if (pq.IsConsistent() == false)
                        {
                            Console.WriteLine("Test fails after dequeue operation # " + op);
                        }
                    }
                }
            } // for
            Console.WriteLine("\nAll tests passed");
        } // TestPriorityQueue

    } // class PriorityQueuesProgram

    // ===================================================================

    public class Employee : IComparable<Employee>
    {
        public string lastName;
        public double priority; // smaller values are higher priority

        public Employee(string lastName, double priority)
        {
            this.lastName = lastName;
            this.priority = priority;
        }

        public override string ToString()
        {
            return "(" + lastName + ", " + priority.ToString("F1") + ")";
        }

        public int CompareTo(Employee other)
        {
            if (this.priority < other.priority) return -1;
            else if (this.priority > other.priority) return 1;
            else return 0;
        }
    } // Employee

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        private int ParentIndex(int i)
        {
            return (i - 1) / 2;
        }

        private int LeftChildIndex(int i)
        {
            return i * 2 + 1;
        }
        private int RightChildIndex(int i)
        {
            return i * 2 + 2;
        }

        public int Count()
        {
            return data.Count;
        }
        private void Swap(int firstIndex, int secondIndex)
        {
            T temp = data[firstIndex];
            data[firstIndex] = data[secondIndex];
            data[secondIndex] = temp;
        }

        public void Enqueue(T item)
        {
            data.Add(item);  // insert at end
            int currentIndex = data.Count - 1;
            while (currentIndex > 0)
            {
                int pi = ParentIndex(currentIndex);
                if (data[currentIndex].CompareTo(data[pi]) < 0)  // If value of child is less than parent, then swap them.
                {
                    Swap(currentIndex, pi);
                    currentIndex = pi;
                }
                else
                    break;
            }
        }

        public bool IsEmpty()
        {
            return data.Count == 0;
        }
        public T Dequeue()
        {
            Swap(0, data.Count - 1); // swap first and last element
            int ci = data.Count - 1;
            T item = data[ci];
            data.RemoveAt(ci);

            ci--;
            int i = 0;
            while (true)
            {
                int lc = LeftChildIndex(i);
                if (lc > ci)
                    break;
                int rc = RightChildIndex(i);

                if (rc <= ci && data[rc].CompareTo(data[lc]) < 0)   // right is smaller than left, so consider right
                {
                    if (data[rc].CompareTo(data[i]) < 0) // if right is smaller than current , swap
                    {
                        Swap(rc, i);
                        i = rc;
                    }
                    else
                        break;
                }
                else
                {
                    if (data[lc].CompareTo(data[i]) < 0)
                    {
                        Swap(lc, i);
                        i = lc;
                    }
                    else
                        break;
                }
            }

            return item;

        }

        public bool IsConsistent()
        {
            // is the heap property true for all data?
            if (data.Count == 0) return true;
            int li = data.Count - 1; // last index
            for (int pi = 0; pi < data.Count; ++pi) // each parent index
            {
                int lci = LeftChildIndex(pi); // left child index
                int rci = RightChildIndex(pi); // right child index

                if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false; // if lc exists and it's greater than parent then bad.
                if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false; // check the right child too.
            }
            return true; // passed all checks
        } // IsConsistent
    }
}
