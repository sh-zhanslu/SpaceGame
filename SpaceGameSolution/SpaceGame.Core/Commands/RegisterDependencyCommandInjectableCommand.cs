using App;
namespace SpaceGame.Core;

public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Func<object[], object> factory = (object[] args) => new CommandInjectableCommand();
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.CommandInjectable", factory).Execute();
        Ioc.Resolve<ICommand>("IoC.Register", "Commands.CommadInjectable", factory).Execute();
    }
}