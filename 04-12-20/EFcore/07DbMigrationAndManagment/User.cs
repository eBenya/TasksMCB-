using System;
using System.Collections.Generic;

#nullable disable

namespace ConsoleTEST
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        //Add new property
        public string Address { get; set; }
    }
}
