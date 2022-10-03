using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DelegatesSolution
{




    [TestFixture]
    public class Test
    {
        [Test]
        public void BasicTest()
        {
            List<string> peopleList = new List<string>()
        {
            "Peter", "Mike", "Peter", "Bob", "Peter", "Peter", "Bob", "Mike", "Bob", "Peter", "Peter", "Mike", "Bob"
        };
            TextMessageSend.TextMessageList = "";
            Publisher publisher = new Publisher();
            publisher.OnContactNotify += TextMessageSend.Send;
            publisher.CountMessages(peopleList);
            string output = TextMessageSend.TextMessageList;
            string expected = "Peter, Bob, Peter, Mike";
            StringAssert.AreEqualIgnoringCase(expected, output);
        }
    }
}