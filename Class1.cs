using System.Globalization;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System;



namespace DelegatesSolution
{


    public class PersonEventArgs : EventArgs
    {
        public string Name { get; set; }
    }

    public class DoSomething
    {
        public static void SomeAction(object person, PersonEventArgs e)
        {
            Console.WriteLine("We found him!!!");
        }
    }

    public class TextMessageSend
    {
        public static string TextMessageList = "";


        public static void Send(object person, PersonEventArgs e)
        {
            if (TextMessageList.Length == 0)
            {
                TextMessageList += e.Name;
            }
            else
                TextMessageList += $", {e.Name}";
        }

    }


    public class Publisher
    {
        PersonEventArgs personEventArgs = new();

        public delegate void PersonEventHandler<PersonEventArgs>(object source, PersonEventArgs e);

        public event PersonEventHandler<PersonEventArgs> OnContactNotify;


        public void CountMessages(List<string> peopleList)
        {

            Dictionary<string, int> results = new();

            foreach (string person in peopleList)
            {
                results.TryAdd(person, 1);

                if (results.ContainsKey(person))
                {
                    if (results[person] == 3)
                    {
                        results[person] = 0;

                        personEventArgs.Name = person;
                        OnNameRepeated(personEventArgs);
                    }

                    if (results[person] < 3)
                        results[person]++;
                }
            }
        }

        public void OnNameRepeated(PersonEventArgs e)
        {
            PersonEventHandler<PersonEventArgs> handler = OnContactNotify;

            handler(this, e);
        }

    }













    public class Program
    {
        public static void Main()
        {
            List<string> peopleList = new List<string>()
            {
                "Peter", "Mike", "Peter", "Bob", "Peter", "Peter", "Bob", "Mike", "Bob", "Peter", "Peter", "Mike", "Bob"
            };

            Publisher publisher = new Publisher();

            publisher.OnContactNotify += TextMessageSend.Send;
            publisher.OnContactNotify += DoSomething.SomeAction;
            publisher.CountMessages(peopleList);

            Console.WriteLine(TextMessageSend.TextMessageList);
        }
    }
}