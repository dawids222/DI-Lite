namespace Unit_Tests.Models
{
    public interface IMockDependency
    {
        IMockDependency Inner { get; }
        void DoSomething();
    }

    public class MockDepenedency : IMockDependency
    {
        public IMockDependency Inner { get; }

        public MockDepenedency(string x)
        {
            throw new System.Exception();
        }

        public MockDepenedency(IMockDependency inner = null)
        {
            Inner = inner;
        }

        public void DoSomething()
        {

        }
    }
}
