using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVBowling.Models
{
    public class BowlGame
    {
        public List<Frame> Frames { get; private set; }
        public bool IsActive { get; private set; } = true;
        public int Score { get; private set; }

        public BowlGame()
        {
            Initialize();
        }

        private void Initialize()
        {
            Frames = new List<Frame>();

            for (int i = 1; i <= 10; i++)
            {
                Frames.Add(new Frame(i));
            }
        }

        public void Roll()
        {
            var currentFrame = Frames.OrderBy(x => x.Number).FirstOrDefault(x => x.IsActive == true);
            if (currentFrame != null)
            {
                currentFrame.Roll();

                //Check if the game is over
                if (currentFrame.Number == 10 && currentFrame.Rolls.Where(x => x.Score == null).Count() == 0)
                {
                    IsActive = false;
                    CalculateGameScore();
                }
            }
        }
        private void CalculateGameScore()
        {
            Frames.ForEach(x => Score += CalculateFrameScore(x));
        }

        private int CalculateFrameScore(Frame frame)
        {
           //Sum all rolls score
           frame.Rolls.ForEach(x => frame.Score += x.Score != null ? (int)x.Score : 0);

           if (frame.IsSpare && frame.Number < 10)
            {
                //Sum the next frame first roll
                frame.Score += (int)Frames.FirstOrDefault(x => x.Number == frame.Number + 1).Rolls.FirstOrDefault(r => r.Number == 1).Score;

            }
           else if (frame.IsStrike && frame.Number < 10)
            {
                //Sum the next frame first roll
                frame.Score += (int)Frames.FirstOrDefault(x => x.Number == frame.Number + 1).Rolls.FirstOrDefault(r => r.Number == 1).Score;

                //Sum the next second roll
                if (frame.Score !=20)
                {
                    frame.Score += (int)Frames.FirstOrDefault(x => x.Number == frame.Number + 1).Rolls.FirstOrDefault(r => r.Number == 2).Score;
                }
                else
                {
                    frame.Score += (int)Frames.FirstOrDefault(x => x.Number == frame.Number + 2).Rolls.FirstOrDefault(r => r.Number == 1).Score;
                }

            }

            return frame.Score;
        }
    }
}
