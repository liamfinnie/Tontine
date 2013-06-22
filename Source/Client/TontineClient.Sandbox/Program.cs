using System;
using System.Collections.Generic;
using System.Linq;

namespace TontineClient.Sandbox
{
    public delegate void ShimDelegate();

    class Program
    {
        static void Main()
        {
            IList<string> labels = new List<string>{"one", "two", "three"};
            Work(labels);
        }

        public static void Work(IList<string> labels)
        {
            var actions = labels.Select(str => (Action) (() => Console.WriteLine(str))).ToList();

            foreach (Action action in actions)
            {
                action.Invoke();
            }
        }

    }


    class Shim
    {
        public event ShimDelegate thing = () => { };

        public void DoWork()
        {
            //thing += Shim_thing;

            //thing += delegate { Console.WriteLine("second"); };
            
            //thing += () => Console.WriteLine("third");

            //thing += () => { };

            thing.Invoke();
        }
    }
}