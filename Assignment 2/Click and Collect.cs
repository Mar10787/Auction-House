using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    public class Click_and_Collect: Delivery_Option
    {
        //date and time pick up window
        public string starttime { get; set; }
        public string endtime { get; set; } 
         
        public override string ShowPrompt() => $"\nThank you for your bid. If successful, the item will be provided via collection between {starttime} and {endtime}";


    }
}
