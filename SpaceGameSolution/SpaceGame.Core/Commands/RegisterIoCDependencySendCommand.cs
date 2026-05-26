using App;
namespace SpaceGame.Core;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Send",
            (object[] args) => new SendCommand((ICommand)args[0], (ICommandReceiver)args[1])
        ).Execute();
    }
}