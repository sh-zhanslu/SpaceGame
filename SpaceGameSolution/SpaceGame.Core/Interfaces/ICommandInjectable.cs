namespace SpaceGame.Core;

public interface ICommandInjectable
{
    void Inject(ICommand command);
}
