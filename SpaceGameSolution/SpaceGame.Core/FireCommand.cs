using App;
namespace SpaceGame.Core
{
    public class FireCommand : ICommand
    {
        private readonly Vectors _position;
        private readonly Vectors _fireDirection;
        private readonly double _speed;
        public FireCommand(Vectors position, Vectors fireDirection, double speed = 1.0)
        {
            _position = position;
            _fireDirection = fireDirection;
            _speed = speed;
        }
        public void Execute()
        {
            var weaponId = Guid.NewGuid().ToString();
            var weaponDict = Ioc.Resolve<IDictionary<string, object>>("Weapon.Create", weaponId);
            var weapon = Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", weaponDict["Id"]);
            Ioc.Resolve<ICommand>("Weapon.Setup", weapon, _position, _fireDirection, _speed).Execute();
            Ioc.Resolve<ICommand>("Game.Item.Add", weaponId, weapon).Execute();
            var moveCommandWeapon = Ioc.Resolve<ICommand>("Commands.Move", weapon);
            var receiver = Ioc.Resolve<ICommandReceiver>("Game.Receiver");
            var startCommandWeapon = Ioc.Resolve<ICommand>("Actions.Start", new Dictionary<string, object>
            {
                { "Cmd", moveCommandWeapon}, { "Receiver", receiver},
                { "Action", "Move"}, { "Target", weaponDict}
            });
            startCommandWeapon.Execute();
        }
    }
}