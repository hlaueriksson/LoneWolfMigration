using NUnit.Framework;

namespace LoneWolf.Migration.Specs
{
    [TestFixture]
    public abstract class BaseSpec<TClassUnderTest> where TClassUnderTest : class
    {
        protected abstract TClassUnderTest ResolveClassUnderTest();

        private TClassUnderTest _classUnderTest;
        protected TClassUnderTest ClassUnderTest
        {
            get { return _classUnderTest ?? (_classUnderTest = ResolveClassUnderTest()); }
        }

        [SetUp]
        public void Init()
        {
            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }

        [TearDown]
        public void Exit()
        {
            Clean();
        }

        protected virtual void Clean()
        {
        }
    }
}
