using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerDisposeTests : DependencyProviderDisposeTestsBase
    {
        protected override DependencyProvider GetDependencyProvider() => Container;

        [TestMethod]
        public void Dispose_Single_DisposesEveryIDisposableOnce()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Single(1, new DisposableDependency(incrementValue));
            Container.Single(2, new DisposableDependency(incrementValue));

            using (var provider = GetDependencyProvider())
            {
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(2);
                provider.Get<DisposableDependency>(2);
            }

            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Dispose_Single_CreatedFromScope_DisposesEveryIDisposableOnce()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Single(1, _ => new DisposableDependency(incrementValue));
            Container.Single(2, _ => new DisposableDependency(incrementValue));

            var scope = Container.CreateScope();
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(2);
            scope.Get<DisposableDependency>(2);
            Container.Dispose();

            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Dispose_Factory_CreatedFromScope_DoesNotDisposeAnything()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Factory(1, _ => new DisposableDependency(incrementValue));
            Container.Factory(2, _ => new DisposableDependency(incrementValue));

            var scope = Container.CreateScope();
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(2);
            scope.Get<DisposableDependency>(2);
            Container.Dispose();

            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void Dispose_Scoped_CreatedFromScope_DoesNotDisposeAnything()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Scoped(1, _ => new DisposableDependency(incrementValue));
            Container.Scoped(2, _ => new DisposableDependency(incrementValue));

            var scope = Container.CreateScope();
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(2);
            scope.Get<DisposableDependency>(2);
            Container.Dispose();

            Assert.AreEqual(0, value);
        }
    }

    [TestClass]
    public class ScopedContainerDisposeTests : DependencyProviderDisposeTestsBase
    {
        protected override DependencyProvider GetDependencyProvider() => Container.CreateScope();

        [TestMethod]
        public void Dispose_Scoped_DisposesEveryIDisposableOnce()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Scoped(1, _ => new DisposableDependency(incrementValue));
            Container.Scoped(2, _ => new DisposableDependency(incrementValue));

            using (var provider = GetDependencyProvider())
            {
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(2);
                provider.Get<DisposableDependency>(2);
            }

            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Dispose_Single_DoesNotDisposeAnything()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Single(1, _ => new DisposableDependency(incrementValue));
            Container.Single(2, _ => new DisposableDependency(incrementValue));

            using (var provider = GetDependencyProvider())
            {
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(2);
                provider.Get<DisposableDependency>(2);
            }

            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void Dispose_Scoped_ContainerDispose_Single_DoesNotDisposeAnything()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Scoped(1, _ => new DisposableDependency(incrementValue));
            Container.Scoped(2, _ => new DisposableDependency(incrementValue));

            var scope = GetDependencyProvider();
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(2);
            scope.Get<DisposableDependency>(2);
            Container.Dispose();

            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void Dispose_Factory_ContainerDispose_Single_DoesNotDisposeAnything()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Factory(1, _ => new DisposableDependency(incrementValue));
            Container.Factory(2, _ => new DisposableDependency(incrementValue));

            var scope = GetDependencyProvider();
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(1);
            scope.Get<DisposableDependency>(2);
            scope.Get<DisposableDependency>(2);
            Container.Dispose();

            Assert.AreEqual(0, value);
        }
    }

    public abstract class DependencyProviderDisposeTestsBase : ContainerBaseTest
    {
        protected abstract DependencyProvider GetDependencyProvider();

        [TestMethod]
        public void Dispose_Factory_DisposesEveryIDisposableOnce()
        {
            var value = 0;
            var incrementValue = () => { value++; };
            Container.Factory(1, _ => new DisposableDependency(incrementValue));
            Container.Factory(2, _ => new DisposableDependency(incrementValue));

            using (var provider = GetDependencyProvider())
            {
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(1);
                provider.Get<DisposableDependency>(2);
                provider.Get<DisposableDependency>(2);
            }

            Assert.AreEqual(4, value);
        }
    }
}
