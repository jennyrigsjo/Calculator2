namespace Calculator2.Tests
{
    public class CalculatorReadyShould
    {
        [Fact]
        public void ReturnTrueWhenNum1AndNum2AreSet()
        {
            Calculator sut = new()
            {
                Num1 = 3.5,
                Num2 = 5,
            };

            bool ready = sut.Ready();

            Assert.True(ready);
        }


        [Fact]
        public void ReturnTrueWhenNum1AndNumbers2AreSet()
        {
            Calculator sut = new()
            {
                Num1 = 3.5,
                Numbers2 = new double[3] { 5, 6, 7.5 },
            };

            bool ready = sut.Ready();

            Assert.True(ready);
        }


        [Fact]
        public void ReturnFalseOnInit()
        {
            Calculator sut = new();

            bool ready = sut.Ready();

            Assert.False(ready);
        }


        [Fact]
        public void ReturnFalseAfterReset()
        {
            Calculator sut = new()
            {
                Num1 = 3.5,
                Num2 = 5,
            };

            sut.Reset();

            bool ready = sut.Ready();

            Assert.False(ready);
        }
    }
}
