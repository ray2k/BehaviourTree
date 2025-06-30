using BehaviourTree.Behaviours;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ConditionTests
    {
        [Test]
        public void WhenPredicateReturnTrue_ReturnSuccess()
        {
            var sut = new Condition<MockContext>(_ => true);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenPredicateReturnTrue_ReturnSuccessAsync()
        {
            var sut = new Condition<MockContext>(async ctx => await Task.FromResult(true));

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenPredicateReturnFalse_ReturnFailure()
        {
            var sut = new Condition<MockContext>(_ => false);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }

        [Test]
        public void WhenPredicateReturnFalse_ReturnFailureAsync()
        {
            var sut = new Condition<MockContext>(async ctx => await Task.FromResult(false));

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }
    }
}
