using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVBowling.Models
{
    public class Pin
    {
        public bool IsStanding { get; set; } = true;
        public int PinNumber { get; private set; }

        public Pin(int pinNumber)
        {
            this.PinNumber = pinNumber;
        }
    }
}
