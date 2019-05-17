using System;

namespace TestSs
{
    public class Storeable : IStoreable
    {
        public string Name { get; set; }
        public IComparable Id { get; set; }
    }
}