namespace Unit_Tests.Models
{
    internal interface IMockDependency
    {
        void DoSomething();
    }

    internal class MockDepenedency : IMockDependency
    {
        public void DoSomething()
        {

        }
    }
}
