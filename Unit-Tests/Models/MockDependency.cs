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

        public MockDepenedency() : this(null)
        {

        }

        public MockDepenedency(IMockDependency inner)
        {
            Inner = inner;
        }

        public void DoSomething()
        {

        }
    }
}
