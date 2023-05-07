using NUnit.Framework;
using static HedgehogTask.HedgehogTask;

namespace HedgehogTaskTest
{
    public class Tests
    {
        private int desiredColor;
        private int[] hedgehogPopulation = new int[3];

        [Test]
        public void CountMinMeetings_WhenOtherColorsAreEqual_ShouldReturnNumberOfOtherColorHedgehogs()
        {
            int result;

            hedgehogPopulation = new int[3] { 6, 6, 2 };
            desiredColor = 2;

            Parameters parameters = new(desiredColor, hedgehogPopulation);

            AssignOtherColors(ref parameters);
            result = CountMinMeetings(parameters);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void CountMinMeetings_WhenNumberIsBiggerByMultipleOfThree_ShouldReturnNumber()
        {
            int result;

            hedgehogPopulation = new int[3] { 9, 1, 0 };
            desiredColor = 1;

            Parameters parameters = new(desiredColor, hedgehogPopulation);

            AssignOtherColors(ref parameters);
            result = CountMinMeetings(parameters);

            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void CountMinMeetings_WhenNumberIsBiggerByNotAMultipleOfThree_ShouldReturnNegativeOne()
        {
            int result;

            hedgehogPopulation = new int[3] { 7, 6, 9 };
            desiredColor = 1;

            Parameters parameters = new(desiredColor, hedgehogPopulation);

            AssignOtherColors(ref parameters);
            result = CountMinMeetings(parameters);

            Assert.That(result, Is.EqualTo(-1));
        }
        }
    }