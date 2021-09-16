using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVBowling.Models
{
    public class Frame
    {
        public List<Roll> Rolls;
        public List<Pin> Pins;
        public int Number { get; private set; }
        public int Score { get; set; } = 0;
        public bool IsActive { get; private set; } = true;
        public bool IsStrike { get; private set; } = false;
        public bool IsSpare { get; private set; } = false;
        public Frame(int frameNumber)
        {
            Initialize(frameNumber);
        }

        private void Initialize(int frameNumber)
        {
            this.Number = frameNumber;

            Rolls = new List<Roll>();
            for (int i = 1; i <= 3; i++)
            {
                bool addRoll = false;

                if (i == 3 && frameNumber == 10)
                {
                    addRoll = true;
                }
                else if (i < 3)
                {
                    addRoll = true;
                }

                if (addRoll)
                {
                    Rolls.Add(new Roll(i));
                }

            }

            Pins = new List<Pin>();
            for (int i = 1; i <= 10; i++)
            {
                Pins.Add(new Pin(i));
            }
        }

        public void Roll()
        {
            var currentRoll = Rolls.FirstOrDefault(x => x.Score == null);

            if (currentRoll != null)
            {
                //list of standing pins before the roll
                var standingpinsBefore = Pins.Where(x => x.IsStanding == true);

                //Standing pins before the roll
                int standingpinsBeforeCount = standingpinsBefore.Count();

                //for every pin calculate a random true or false to see if it remains standing in the roll
                standingpinsBefore.ToList().ForEach(x => x.IsStanding = Convert.ToBoolean(new Random().Next(2)));

                //list of standing pins after roll
                var standingpinsAfter = Pins.Where(x => x.IsStanding == true);

                //Standing pins after the roll
                int afterPinsStandingCount = standingpinsAfter.Count();

                //Set Roll score
                currentRoll.Score = standingpinsBeforeCount - afterPinsStandingCount;

                //Last frame special rule, if the player does not have standing pins on roll 1 or 2 activate 3rd roll.
                if ((currentRoll.Number == 1 || currentRoll.Number == 2) && afterPinsStandingCount == 0 && Number == 10)
                {
                    var roll3 = Rolls.FirstOrDefault(x => x.Number == 3);
                    roll3.Score = null;
                    Pins.ForEach(x => x.IsStanding = true);
                }

                //Finish the Frame if it is the last roll
                if ((Number == 10 && currentRoll.Number == 3) || (Number < 10 && currentRoll.Number == 2))
                {
                    IsActive = false;
                }

                //Finish the Frame if it is a strike on every frame excluding frame 10
                if (Number < 10 && currentRoll.Number == 1 && currentRoll.Score == 10)
                {
                    IsActive = false;
                    IsStrike = true;
                    var roll2 = Rolls.FirstOrDefault(x => x.Number == 2);
                    roll2.Score = 0;
                }

                //Check for Spare
                if (Number < 10 && currentRoll.Number == 2 && afterPinsStandingCount == 0)
                {
                    IsSpare = true;
                }

            }
            else
            {
                IsActive = false;
            }
        }
    }
}

