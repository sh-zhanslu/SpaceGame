using App;

namespace SpaceGame.Core
{
    public class RegisterIoCDependencyGame : ICommand
    {
        public void Execute()
        {
            // 1.Создаём конкретную реализацию приёмника команд(очередь)
            var receiver = new QueueCommandReceiver();

            // 2. Регистрируем этот приёмник в IoC под именем "Game.Receiver"
            //    Ioc.Resolve<ICommand>("IoC.Register", имя_зависимости, фабрика) возвращает команду регистрации,
            //    у которой вызываем Execute(), чтобы регистрация вступила в силу.
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Receiver", (object[] _) => receiver).Execute();

            // 3. Создаём экземпляр игры, передавая ему созданный приёмник
            var game = new Game(receiver);

            // 4. Регистрируем экземпляр игры в IoC под именем "Game.Instance"
            //    Теперь в любом месте можно получить игру через Ioc.Resolve<Game>("Game.Instance")
            Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Instance", (object[] _) => game).Execute();
        }
    }
}