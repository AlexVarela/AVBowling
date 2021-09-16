using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVBowling.Models
{
    public class Roll
    {
        public int? Score { get; set; } = null;
        public int Number { get; private set; }

        public Roll(int rollNumber)
        {
            this.Number = rollNumber;

            //Frame 10 3rd roll is disabled by default.
            //a roll is disable when the score is different than null
            if (rollNumber == 3)
            {
                Score = 0;
            }
        }
    }
}
