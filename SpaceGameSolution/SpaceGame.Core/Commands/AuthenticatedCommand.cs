using App;

namespace SpaceGame.Core;

public class AuthenticatedCommand : ICommand
{
    private readonly ICommand _command;
    private readonly object _target;
    private readonly object _token;

    public AuthenticatedCommand(ICommand command, object target, object token)
    {
        _command = command;
        _target = target;
        _token = token;
    }

    public void Execute()
    {
        var isAuthorized = Ioc.Resolve<bool>("Authorization.Check", _target, _token);
        if (!isAuthorized)
        {
            throw new System.UnauthorizedAccessException();
        }
        _command.Execute();
    }
}
