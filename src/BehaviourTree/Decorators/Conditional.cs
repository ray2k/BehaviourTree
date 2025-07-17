using System;
using System.Threading.Tasks;

namespace BehaviourTree.Decorators
{
    public sealed class Conditional<TContext> : DecoratorBehaviour<TContext>
    {
        private readonly Func<TContext, bool> _predicate;

        public Conditional(IBehaviour<TContext> child, Func<TContext, bool> predicate) 
            : this("Conditional", child, predicate)
        {
        }

        public Conditional(string name, IBehaviour<TContext> child, Func<TContext, bool> predicate) 
            : base(name, child)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public Conditional(IBehaviour<TContext> child, Func<TContext, Task<bool>> predicate)
            : this("Conditional", child, predicate)
        {
        }

        public Conditional(string name, IBehaviour<TContext> child, Func<TContext, Task<bool>> predicate)
            : base(name, child)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            _predicate = ctx => predicate(ctx).GetAwaiter().GetResult();
        }

        protected override BehaviourStatus Update(TContext context)
        {
            if (!_predicate(context))
            {
                return BehaviourStatus.Failed;
            }

            return Child.Tick(context);
        }
    }
}