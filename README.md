# BehaviourTree

Forked from [DavidFidge/BehaviourTree](https://github.com/DavidFidge/BehaviourTree)
- (which was forked from [Eraclys/BehaviourTree](https://github.com/Eraclys/BehaviourTree)

@DavidFidge updated nuget references and moved to net8. This fork adds:
- updates to all projects (tests + demo) to net8
- basic async support for .Do and .Condition when using FluentBuilder
- new Conditional decorator node


## Usage (FluentBuilder)

``` cs    
var behaviourTree = FluentBuilder.Create<MyContext>()
    .Sequence("root")
        .Condition("some async condition", async ctx => {
            await Task.Delay(1000); // some async work or whatever
            return await SomeMethodThatReturnsABool();
        })
        .Do("some async action", async ctx => {
            await Task.Delay(1000); // some async work or whatever
            return BehaviourStatus.Succeeded;
        })
        .Conditional("conditional node", ctx => ctx.SomeCondition)
            .Do("action in conditional", async ctx => {
                // this will only run if ctx.SomeCondition is true
                return BehaviourStatus.Succeeded;
            })
        .End()
    .End()
    .Build();
```
