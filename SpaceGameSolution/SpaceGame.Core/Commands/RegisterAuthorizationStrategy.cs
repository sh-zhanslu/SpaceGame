using App;

namespace SpaceGame.Core;

public class RegisterAuthorizationStrategy : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Authorization.Check",
            (object[] args) =>
            {
                var target = args[0];
                var token = args[1];
                
                var playerId = Ioc.Resolve<string>("Token.GetPlayerId", token);
                var ownerId = Ioc.Resolve<string>("Game.GetObjectOwner", target);
                
                return playerId == ownerId;
            }
        ).Execute();
    }
}
