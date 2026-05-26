using App;
namespace SpaceGame.Core;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Actions.Stop",
            (object[] args) =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new ActionCommand(() =>
                {
                    var target = order["Target"];
                    var registry = Ioc.Resolve<IDictionary<object, ICommandInjectable>>("Actions.Registry");
                    if (registry.TryGetValue(target, out var injectable))
                    {
                        var emptyCommand = Ioc.Resolve<ICommand>("Commands.Empty");
                        injectable.Inject(emptyCommand);
                        registry.Remove(target);
                    }
                });
            }
        ).Execute();
    }
}