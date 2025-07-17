using System;
using System.Xml.Serialization;
using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ConditionalTests
    {
        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenPredicateIsTrue_CallChildrenAndReturnChildStatus(BehaviourStatus childStatus)
        {
            var child = new MockBehaviour {ReturnStatus = childStatus };

            var sut = new Conditional<MockContext>(child, p => true);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(childStatus));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void WhenPredicateIsFalse_DoNotCallChildrenAndReturnFailed()
        {
            var child = new MockBehaviour {ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new Conditional<MockContext>(child, p => false);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }
    }
}
