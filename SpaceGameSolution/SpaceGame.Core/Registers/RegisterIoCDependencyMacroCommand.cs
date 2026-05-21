using System.Windows.Input;
using App;

namespace SpaceGame.Core
{
    public class RegisterIoCDependencyMacroCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Macro", (object[] args) => 
            new MacroCommand((ICommand[])args[0])).Execute();
        }
    }
}