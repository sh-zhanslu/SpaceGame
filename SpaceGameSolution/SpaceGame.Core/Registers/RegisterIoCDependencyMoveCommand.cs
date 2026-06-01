using System;
using App;

namespace SpaceGame.Core
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            try
            {
                Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move", (object[] args) =>
                    new MoveCommand(Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]))).Execute();
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
