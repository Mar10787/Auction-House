using AuctionHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    public class Home_Delivery: Delivery_Option
    {
        //ask for address
        public int Unit { get; set; }
        public int StreetNum {get; set; }
        public string Street { get; set; }
        public string StreetSuffix { get; set; }    
        public string City { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }


        public override string ShowPrompt() => $"\nThank you for your bid. If successful, the item will be provided via delivery to {Unit}/{StreetNum} {Street} {StreetSuffix}, {City} {State} {Postcode}";

    }
}
