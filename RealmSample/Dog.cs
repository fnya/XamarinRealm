using System;
using Realms;

namespace RealmSample
{
    public class Dog : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

    }
}
