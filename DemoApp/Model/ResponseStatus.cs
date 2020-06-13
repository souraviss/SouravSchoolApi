using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Model
{
    public class ResponseStatus
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public int LastInsertedId { get; set; }
    }
}
