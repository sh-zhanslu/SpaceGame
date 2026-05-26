using App;
namespace SpaceGame.Core;
public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        try
        {
            Ioc.Resolve<IDictionary<object, ICommandInjectable>>("Actions.Registry");
        }
        catch
        {
            var registry = new Dictionary<object, ICommandInjectable>();
            Ioc.Resolve<ICommand>("IoC.Register", "Actions.Registry", (object[] args) => registry).Execute();
        }

        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Actions.Start",
            (object[] args) =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new ActionCommand(() =>
                {
                    var target = order["Target"];
                    var actionName = (string)order["Action"];
                    var cmd = Ioc.Resolve<ICommand>(actionName, target);
                    var injectable = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");
                    injectable.Inject(cmd);
                    
                    var registry = Ioc.Resolve<IDictionary<object, ICommandInjectable>>("Actions.Registry");
                    registry[target] = injectable;
                    
                    Ioc.Resolve<ICommand>("Queue.Push", injectable).Execute();
                });
            }
        ).Execute();
    }
}