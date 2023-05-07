using System;
using System.Drawing;

namespace HedgehogTask
{
    public class HedgehogTask
    {
        public struct Parameters
        {
            public int[] hedgehogPopulation;

            public int desiredColor;
            public int otherColor1;
            public int otherColor2;

            public int meetingsNumber;

            public Parameters(int desiredColor, int[] hedgehogPopulation)
            {
                this.desiredColor = desiredColor;
                this.hedgehogPopulation = hedgehogPopulation;
                this.otherColor1 = 0;
                this.otherColor2 = 0;
                this.meetingsNumber = 0;
            }
        }

        static void Main(string[] args)
        {
            Parameters parameters = GetParams();
            AssignOtherColors(ref parameters);

            int minMeetings = CountMinMeetings(parameters);

            Console.Write("Minimum encounters: ");
            Console.Write(minMeetings);
        }

        public static Parameters GetParams()
        {
            const string invalidColorNumberException = "Number can't be less then zero, or higher then two.";
            const string populationCannotBeNegative = "Hedgehogs population cannot be negative.";

            int desiredColor;
            int[] hedgehogPopulation = new int[3];

            Console.WriteLine("Type a desired color number (0 - red, 1 - green, 2 - blue)");
            desiredColor = Int32.Parse(Console.ReadLine());

            if (desiredColor < 0 && desiredColor > 2)
            {
                throw new ArgumentException(invalidColorNumberException);
            }
            
            Console.WriteLine("How many red hedgehogs are there?");
            hedgehogPopulation[0] = Int32.Parse(Console.ReadLine());

            if (hedgehogPopulation[0] < 0)
            {
                throw new ArgumentException(populationCannotBeNegative);
            }

            Console.WriteLine("How many green hedgehogs are there?");
            hedgehogPopulation[1] = Int32.Parse(Console.ReadLine());

            if (hedgehogPopulation[1] < 0)
            {
                throw new ArgumentException(populationCannotBeNegative);
            }

            Console.WriteLine("How many blue hedgehogs are there?");
            hedgehogPopulation[2] = Int32.Parse(Console.ReadLine());

            if (hedgehogPopulation[2] < 0)
            {
                throw new ArgumentException(populationCannotBeNegative);
            }

            Parameters parameters = new(desiredColor, hedgehogPopulation);

            return parameters;
        }
        
        public static void AssignOtherColors(ref Parameters parameters)
        {
            switch (parameters.desiredColor)
            {
                case 0:
                    parameters.otherColor1 = 1;
                    parameters.otherColor2 = 2;
                    break;
                case 1:
                    parameters.otherColor1 = 0;
                    parameters.otherColor2 = 2;
                    break;
                case 2:
                    parameters.otherColor1 = 0;
                    parameters.otherColor2 = 1;
                    break;
                default:
                    break;
            }
        }
        
        public static void MeetHedgehogs(int color1, int color2, int anotherColor, ref Parameters parameters)
        {
            if (parameters.hedgehogPopulation[color1] != 0 && parameters.hedgehogPopulation[color2] != 0)
            {
                parameters.hedgehogPopulation[color1]--;
                parameters.hedgehogPopulation[color2]--;

                parameters.hedgehogPopulation[anotherColor] += 2;

                parameters.meetingsNumber++;
            }
        }

        public static int CountMinMeetings(Parameters p)
        {
            int biggerColor;
            int smallerColor;
            
            if (p.hedgehogPopulation[p.otherColor1] == p.hedgehogPopulation[p.otherColor2])
            {
                biggerColor = p.otherColor1;
                smallerColor = biggerColor;
            }

            else if (IsFirstColorBiggerByMultipleOfThree(p, p.otherColor1, p.otherColor2) 
                && IsFirstColorBigger(p, p.otherColor1, p.otherColor2)) 
            {
                biggerColor = p.otherColor1;
                smallerColor = p.otherColor2;
            }

            else if (IsFirstColorBiggerByMultipleOfThree(p, p.otherColor2, p.otherColor1)
                && IsFirstColorBigger(p, p.otherColor2, p.otherColor1))
            {
                biggerColor = p.otherColor2;
                smallerColor = p.otherColor1;
            }

            else
            {
                return -1;
            }

            while (p.hedgehogPopulation[p.otherColor1] != p.hedgehogPopulation[p.otherColor2]) 
            {
                if (p.hedgehogPopulation[p.desiredColor] > 0)
                {
                    MeetHedgehogs(biggerColor, p.desiredColor, smallerColor, ref p);
                    continue;
                }

                MeetHedgehogs(biggerColor, smallerColor, p.desiredColor, ref p);

            }

            while (p.hedgehogPopulation[p.otherColor1] > 0 || p.hedgehogPopulation[p.otherColor2] > 0)
            {
                MeetHedgehogs(p.otherColor1, p.otherColor2, p.desiredColor, ref p);
            }

            return p.meetingsNumber;
        }

        private static bool IsFirstColorBiggerByMultipleOfThree(Parameters p, int color1, int color2)
        {
            return ((p.hedgehogPopulation[color1] - p.hedgehogPopulation[color2]) % 3) == 0;
        }
        private static bool IsFirstColorBigger(Parameters p, int color1, int color2)
        {
            return (p.hedgehogPopulation[color1] > p.hedgehogPopulation[color2]);
        }
    }
}
