using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerConstructabilityTests : ContainerBaseTest
    {
        [TestMethod]
        public void IsConstructable_OneParameterDependency_ReturnsFalse()
        {
            Container.Single<ValidParameterMockDependency>();

            var result = Container.IsConstructable;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsConstructable_TwoParameterDependencies_WithWrongReference_ReturnsFalse()
        {
            Container.Single<ValidParameterlessMockDependency>();
            Container.Single<ValidParameterMockDependency>();

            var result = Container.IsConstructable;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsConstructable_TwoParameterDependencies_WithCorrectReference_ReturnsTrue()
        {
            Container.Single<IMockDependency, ValidParameterlessMockDependency>();
            Container.Single<ValidParameterMockDependency>();

            var result = Container.IsConstructable;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetConstructabilityReport_OneParameterDependency()
        {
            Container.Single<ValidParameterMockDependency>();

            var result = Container.GetConstructabilityReport();

            Assert.IsFalse(result.IsConstructable);
            Assert.AreEqual(1, result.FailedConstructabilityReports.Count());
            Assert.AreEqual(1, result.ConstructabilityReports.Count());
            var report = result.ConstructabilityReports.ElementAt(0);
            Assert.AreEqual(typeof(ValidParameterMockDependency), report.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterMockDependency), report.ConcreteType);
            Assert.AreEqual(1, report.MissingDependencies.Count());
            Assert.AreEqual(typeof(IMockDependency), report.MissingDependencies.ElementAt(0));
            Assert.IsNotNull(report.Error);
        }

        [TestMethod]
        public void GetConstructabilityReport_TwoDependencies()
        {
            Container.Single<ValidParameterMockDependency>();
            Container.Single<ValidParameterlessMockDependency>();

            var result = Container.GetConstructabilityReport();

            Assert.IsFalse(result.IsConstructable);
            Assert.AreEqual(1, result.FailedConstructabilityReports.Count());
            Assert.AreEqual(2, result.ConstructabilityReports.Count());

            var failedReport = result.ConstructabilityReports.ElementAt(0);
            Assert.AreEqual(typeof(ValidParameterMockDependency), failedReport.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterMockDependency), failedReport.ConcreteType);
            Assert.AreEqual(1, failedReport.MissingDependencies.Count());
            Assert.AreEqual(typeof(IMockDependency), failedReport.MissingDependencies.ElementAt(0));
            Assert.IsNotNull(failedReport.Error);

            var validReport = result.ConstructabilityReports.ElementAt(1);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ConcreteType);
            Assert.AreEqual(0, validReport.MissingDependencies.Count());
            Assert.IsNull(validReport.Error);
        }

        [TestMethod]
        public void ThrowIfNotConstructable_OneParameterDependency_Throws()
        {
            Container.Single<ValidParameterMockDependency>();

            void act() => Container.ThrowIfNotConstructable();

            Assert.ThrowsException<ContainerNotConstructableException>(act);
        }

        [TestMethod]
        public void ThrowIfNotConstructable_TwoParameterDependencies_WithWrongReference_Throws()
        {
            Container.Single<ValidParameterlessMockDependency>();
            Container.Single<ValidParameterMockDependency>();

            void act() => Container.ThrowIfNotConstructable();

            Assert.ThrowsException<ContainerNotConstructableException>(act);
        }

        [TestMethod]
        public void ThrowIfNotConstructable_TwoParameterDependencies_WithCorrectReference_DoesNotThrow()
        {
            Container.Single<IMockDependency, ValidParameterlessMockDependency>();
            Container.Single<ValidParameterMockDependency>();

            void act() => Container.ThrowIfNotConstructable();

            act();
        }
    }
}
