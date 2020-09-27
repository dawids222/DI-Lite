namespace Unit_Tests.Models
{
    internal interface IMockDependency
    {
        IMockDependency Inner { get; }
        void DoSomething();
    }

    internal class MockDepenedency : IMockDependency
    {
        public IMockDependency Inner { get; }

        public MockDepenedency(IMockDependency inner = null)
        {
            Inner = inner;
        }

        public void DoSomething()
        {

        }
    }
}
