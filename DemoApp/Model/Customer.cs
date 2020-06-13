using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public Boolean Active { get; set; }
    }
}
