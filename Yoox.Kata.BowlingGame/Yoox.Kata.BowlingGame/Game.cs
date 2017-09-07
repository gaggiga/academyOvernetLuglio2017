using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.Kata.BowlingGame
{
    public class Game
    {
        private List<Frame> frames = new List<Frame>();
        private bool isFirstFrameRoll = true;

        public void Roll(int v)
        {
            if (isFirstFrameRoll)
            {
                var frame = new Frame();
                frame.First = v;
                var last = frames.LastOrDefault();

                if (!frame.IsStrike)
                {
                    isFirstFrameRoll = false;
                }

                if (last != null)
                {
                    last.NextFrame = frame;
                }

                frames.Add(frame);

            } else
            {
                frames.Last().Second = v;
                isFirstFrameRoll = true;
            }
        }

        public int Score()
        {
            var score = frames.Sum(f => f.Total);
            return score;
        }
    }
}
