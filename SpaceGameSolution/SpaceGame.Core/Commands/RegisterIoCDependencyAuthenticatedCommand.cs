using App;

namespace SpaceGame.Core;

public class RegisterIoCDependencyAuthenticatedCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Authenticated",
            (object[] args) => new AuthenticatedCommand((ICommand)args[0], args[1], args[2])
        ).Execute();
    }
}
