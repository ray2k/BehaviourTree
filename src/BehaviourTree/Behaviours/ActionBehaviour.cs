using System;
using System.Threading.Tasks;

namespace BehaviourTree.Behaviours
{
    public sealed class ActionBehaviour<TContext> : BaseBehaviour<TContext>
    {
        private readonly Func<TContext, BehaviourStatus> _action;

        public ActionBehaviour(string name, Func<TContext, BehaviourStatus> action) : base(name)
        {
            _action = action;
        }

        public ActionBehaviour(string name, Func<TContext, Task<BehaviourStatus>> action) : base(name)
        {
            _action = context => action(context).GetAwaiter().GetResult();
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return _action(context);
        }
    }
}
