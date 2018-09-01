using System;
using Realms;

namespace RealmSample
{
    public class Person:RealmObject
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
