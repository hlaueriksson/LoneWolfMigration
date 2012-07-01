using AutoMoq;
using Moq;

namespace LoneWolf.Migration.Specs
{
    public abstract class MockedSpec<TClassUnderTest> : BaseSpec<TClassUnderTest> where TClassUnderTest : class
    {
        protected AutoMoqer Container;

        protected override TClassUnderTest ResolveClassUnderTest()
        {
            return Container.Resolve<TClassUnderTest>();
        }

        public Mock<T> Using<T>() where T : class
        {
            return Container.GetMock<T>();
        }

        protected override void Given()
        {
            Container = new AutoMoqer();
            base.Given();
        }
    }
}
