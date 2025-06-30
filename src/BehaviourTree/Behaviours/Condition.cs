using System;
using System.Threading.Tasks;

namespace BehaviourTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition<TContext> : BaseBehaviour<TContext>
    {
        private readonly Func<TContext, bool> _predicate;

        public Condition(Func<TContext, bool> predicate) : this(null, predicate)
        {
        }

        public Condition(Func<TContext, Task<bool>> predicate) : this(null, predicate)
        {
        }

        public Condition(string name, Func<TContext, bool> predicate) : base(name ?? "Condition")
        {
            _predicate = predicate;
        }

        public Condition(string name, Func<TContext, Task<bool>> predicate) : base(name ?? "Condition")
        {
            _predicate = ctx => predicate(ctx).GetAwaiter().GetResult();
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return _predicate(context) ? BehaviourStatus.Succeeded : BehaviourStatus.Failed;
        }
    }
}
