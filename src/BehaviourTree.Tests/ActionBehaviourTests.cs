using BehaviourTree.Behaviours;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ActionBehaviourTests
    {
        [Test]
        public void ActionBehavior_should_execute_async_action_synchronously()
        {             
            var executed = false;
            var sut = new ActionBehaviour<MockContext>("TestAction", async ctx =>
            {
                await Task.Delay(100);
                executed = true;
                return BehaviourStatus.Succeeded;
            });
            var status = sut.Tick(new MockContext());
            Assert.AreEqual(BehaviourStatus.Succeeded, status);
            Assert.IsTrue(executed);
        }
    }
}
