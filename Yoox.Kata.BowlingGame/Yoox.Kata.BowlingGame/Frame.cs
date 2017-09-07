namespace Yoox.Kata.BowlingGame
{
    internal class Frame
    {
        public int First { get; set; }
        public int Second { get; set; }
        public Frame NextFrame { get; set; }

        public int Total
        {
            get
            {
                var total = 0;

                if(NextFrame != null)
                {
                    if (IsSpare)
                    {
                        total += NextFrame.First;
                    } else if (IsStrike)
                    {
                        if (NextFrame.IsStrike && (NextFrame.NextFrame?.IsStrike ?? false))
                        {
                            total += 20;
                        } else
                        {
                            total += NextFrame.First + NextFrame.Second;
                        }
                    }
                }

                total += First + Second;
                return total;
            }
        }

        public bool IsSpare
        {
            get
            {
                return (First + Second) == 10 && !IsStrike;
            }
        }

        public bool IsStrike
        {
            get
            {
                return First == 10;
            }
        }
    }
}