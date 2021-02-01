using FluentAssertions;
using OzowQuestion2_Conway;
using Xunit;
using Xunit.Abstractions;

namespace LifeUnitTests
{
    public class LifeTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public LifeTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Life_7x7_Evolve4_Success()
        {
            string expected =
                "()..()..()..()\r\n..............\r\n()..()()()..()\r\n....()..()....\r\n()..()()()..()\r\n..............\r\n()..()..()..()\r\n";
            int colNumber = 7;
            int rowNumber = 7;

            var testWorld = new Life.State[rowNumber, colNumber];
            testWorld[0, 0] = Life.State.Alive;
            testWorld[0, 1] = Life.State.Alive;
            testWorld[1, 0] = Life.State.Alive;

            testWorld[rowNumber - 2, 0] = Life.State.Alive;
            testWorld[rowNumber - 1, 0] = Life.State.Alive;
            testWorld[rowNumber - 1, 1] = Life.State.Alive;

            testWorld[0, colNumber - 2] = Life.State.Alive;
            testWorld[0, colNumber - 1] = Life.State.Alive;
            testWorld[1, colNumber - 1] = Life.State.Alive;

            testWorld[rowNumber - 2, colNumber - 1] = Life.State.Alive;
            testWorld[rowNumber - 1, colNumber - 1] = Life.State.Alive;
            testWorld[rowNumber - 1, colNumber - 2] = Life.State.Alive;

            var life = new Life(testWorld);
            
            life.Evolve(3); // Initial + 3 evolutions

            _testOutputHelper.WriteLine(life.ToString());
            _testOutputHelper.WriteLine(expected);

            life.ToString().Should().Be(expected);
        }

        [Fact]
        public void Life_7x7_Evolve4CustomChar_Success()
        {
            string expected =
                "[]__[]__[]__[]\r\n______________\r\n[]__[][][]__[]\r\n____[]__[]____\r\n[]__[][][]__[]\r\n______________\r\n[]__[]__[]__[]\r\n";
            int colNumber = 7;
            int rowNumber = 7;

            var testWorld = new Life.State[rowNumber, colNumber];
            testWorld[0, 0] = Life.State.Alive;
            testWorld[0, 1] = Life.State.Alive;
            testWorld[1, 0] = Life.State.Alive;

            testWorld[rowNumber - 2, 0] = Life.State.Alive;
            testWorld[rowNumber - 1, 0] = Life.State.Alive;
            testWorld[rowNumber - 1, 1] = Life.State.Alive;

            testWorld[0, colNumber - 2] = Life.State.Alive;
            testWorld[0, colNumber - 1] = Life.State.Alive;
            testWorld[1, colNumber - 1] = Life.State.Alive;

            testWorld[rowNumber - 2, colNumber - 1] = Life.State.Alive;
            testWorld[rowNumber - 1, colNumber - 1] = Life.State.Alive;
            testWorld[rowNumber - 1, colNumber - 2] = Life.State.Alive;

            var life = new Life(testWorld);

            life.Evolve(3); // Initial + 3 evolutions

            _testOutputHelper.WriteLine(life.ToString("[]", "__"));
            _testOutputHelper.WriteLine(expected);

            life.ToString("[]", "__").Should().Be(expected);
        }

        [Fact]
        public void Life_Empty_Success()
        {
            string expected = "";
            int colNumber = 0;
            int rowNumber = 0;

            var testWorld = new Life.State[rowNumber, colNumber];
           
            var life = new Life(testWorld);

            life.Evolve(3); // Initial + 3 evolutions

            _testOutputHelper.WriteLine(life.ToString("[]", "__"));
            _testOutputHelper.WriteLine(expected);

            life.ToString("[]", "__").Should().Be(expected);
        }
    }
}
