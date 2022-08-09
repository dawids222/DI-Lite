using LibLite.DI.Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibLite.DI.Lite.Tests
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
