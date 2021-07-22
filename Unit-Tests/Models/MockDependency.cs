namespace Unit_Tests.Models
{
    public interface IMockDependency
    {
        IMockDependency Inner { get; }
        void DoSomething();
    }

    public class ValidMockDependency : IMockDependency
    {
        public IMockDependency Inner { get; }

        public ValidMockDependency(IMockDependency inner = null)
        {
            Inner = inner;
        }

        public void DoSomething() { }
    }

    public class ValidConstructorlessMockDependency : IMockDependency
    {
        public IMockDependency Inner { get; }

        public void DoSomething() { }
    }

    public class InvalidMockDependency : IMockDependency
    {
        public IMockDependency Inner { get; }

        public InvalidMockDependency() { }

        public InvalidMockDependency(IMockDependency inner = null)
        {
            Inner = inner;
        }

        public void DoSomething() { }
    }

    public class InvalidPrivateConstructorMockDependency : IMockDependency
    {
        public IMockDependency Inner { get; }

        private InvalidPrivateConstructorMockDependency() { }

        public void DoSomething() { }
    }
}
