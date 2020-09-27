using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerBaseTest
    {
        protected Container Container { get; set; }

        [TestInitialize]
        public virtual void Before()
        {
            Container = new Container();
        }
    }
}
