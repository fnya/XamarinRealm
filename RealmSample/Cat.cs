using System;
using Realms;

namespace RealmSample
{
    public class Cat : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }

        [Indexed]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
