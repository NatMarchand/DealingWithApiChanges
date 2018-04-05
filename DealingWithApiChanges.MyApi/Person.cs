using System;

namespace DealingWithApiChanges.MyApi
{
    public class Person : ICanGreet
    {
        public string Name { get; set; }

        public void SayHello()
        {

        }
    }
}