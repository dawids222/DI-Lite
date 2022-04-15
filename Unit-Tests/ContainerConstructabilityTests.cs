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
        public void IsConstructable_DependencyRequiresTag_DependencyWithTagDoesNotExist_ReturnsFalse()
        {
            Container.Single<IMockDependency, ValidParameterlessMockDependency>();
            Container.Single<ValidDependencyWithTag>();

            var result = Container.IsConstructable;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsConstructable_DependencyRequiresTag_DependencyWithTagExists_ReturnsTrue()
        {
            Container.Single<IMockDependency, ValidParameterlessMockDependency>(ValidDependencyWithTag.TAG);
            Container.Single<ValidDependencyWithTag>();

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
            Assert.AreEqual(typeof(IMockDependency), report.MissingDependencies.ElementAt(0).Type);
            Assert.AreEqual(null, report.MissingDependencies.ElementAt(0).Tag);
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
            Assert.AreEqual(typeof(IMockDependency), failedReport.MissingDependencies.ElementAt(0).Type);
            Assert.AreEqual(null, failedReport.MissingDependencies.ElementAt(0).Tag);
            Assert.IsNotNull(failedReport.Error);

            var validReport = result.ConstructabilityReports.ElementAt(1);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ConcreteType);
            Assert.AreEqual(0, validReport.MissingDependencies.Count());
            Assert.IsNull(validReport.Error);
        }

        [TestMethod]
        public void GetConstructabilityReport_DependencyRequiresTag_DependencyWithTagDoesNotExist()
        {
            Container.Single<IMockDependency, ValidDependencyWithTag>();
            Container.Single<ValidParameterlessMockDependency>();

            var result = Container.GetConstructabilityReport();

            Assert.IsFalse(result.IsConstructable);
            Assert.AreEqual(1, result.FailedConstructabilityReports.Count());
            Assert.AreEqual(2, result.ConstructabilityReports.Count());

            var failedReport = result.ConstructabilityReports.ElementAt(0);
            Assert.AreEqual(typeof(IMockDependency), failedReport.ReferenceType);
            Assert.AreEqual(typeof(ValidDependencyWithTag), failedReport.ConcreteType);
            Assert.AreEqual(1, failedReport.MissingDependencies.Count());
            Assert.AreEqual(typeof(IMockDependency), failedReport.MissingDependencies.ElementAt(0).Type);
            Assert.AreEqual(ValidDependencyWithTag.TAG, failedReport.MissingDependencies.ElementAt(0).Tag);
            Assert.IsNotNull(failedReport.Error);

            var validReport = result.ConstructabilityReports.ElementAt(1);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), validReport.ConcreteType);
            Assert.AreEqual(0, validReport.MissingDependencies.Count());
            Assert.IsNull(validReport.Error);
        }

        [TestMethod]
        public void GetConstructabilityReport_DependencyRequiresTag_DependencyWithTagExists()
        {
            Container.Single<IMockDependency, ValidDependencyWithTag>();
            Container.Single<IMockDependency, ValidParameterlessMockDependency>(ValidDependencyWithTag.TAG);

            var result = Container.GetConstructabilityReport();

            Assert.IsTrue(result.IsConstructable);
            Assert.AreEqual(0, result.FailedConstructabilityReports.Count());
            Assert.AreEqual(2, result.ConstructabilityReports.Count());

            var firstReport = result.ConstructabilityReports.ElementAt(0);
            Assert.AreEqual(typeof(IMockDependency), firstReport.ReferenceType);
            Assert.AreEqual(typeof(ValidDependencyWithTag), firstReport.ConcreteType);
            Assert.AreEqual(0, firstReport.MissingDependencies.Count());
            Assert.IsNull(firstReport.Error);

            var secondReport = result.ConstructabilityReports.ElementAt(1);
            Assert.AreEqual(typeof(IMockDependency), secondReport.ReferenceType);
            Assert.AreEqual(typeof(ValidParameterlessMockDependency), secondReport.ConcreteType);
            Assert.AreEqual(0, secondReport.MissingDependencies.Count());
            Assert.IsNull(secondReport.Error);
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
        public void ThrowIfNotConstructable_DependencyRequiresTag_DependencyWithTagDoesNotExis_Throws()
        {
            Container.Single<IMockDependency, ValidDependencyWithTag>();
            Container.Single<ValidParameterlessMockDependency>();

            void act() => Container.ThrowIfNotConstructable();

            Assert.ThrowsException<ContainerNotConstructableException>(act);
        }

        [TestMethod]
        public void ThrowIfNotConstructable_DependencyRequiresTag_DependencyWithTagExists_DoesNotThrow()
        {
            Container.Single<IMockDependency, ValidDependencyWithTag>();
            Container.Single<IMockDependency, ValidParameterlessMockDependency>(ValidDependencyWithTag.TAG);

            void act() => Container.ThrowIfNotConstructable();

            act();
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
