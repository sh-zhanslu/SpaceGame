using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using App;
using App.Scopes;
using SpaceGame.Core;

namespace SpaceGame.Test;

public class FireCommandTests : IDisposable
{
    public FireCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }

    [Fact]
    public void Execute_ShouldSuccessfullyCreateAndStartWeapon()
    {
        var position = new Vectors(new int[] { 0, 0 });
        var direction = new Vectors(new int[] { 1, 1 });
        double speed = 2.0;

        var mockWeaponDict = new Dictionary<string, object> { { "Id", "weapon_id_123" } };
        Ioc.Resolve<App.ICommand>("IoC.Register", "Weapon.Create", (object[] args) => mockWeaponDict).Execute();

        var mockMovingObject = new Mock<IMovingObject>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) => mockMovingObject.Object).Execute();

        var mockSetupCmd = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Weapon.Setup", (object[] args) => mockSetupCmd.Object).Execute();

        var mockAddItemCmd = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Item.Add", (object[] args) => mockAddItemCmd.Object).Execute();

        var mockMoveCmd = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move", (object[] args) => mockMoveCmd.Object).Execute();

        var mockReceiver = new Mock<ICommandReceiver>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Receiver", (object[] args) => mockReceiver.Object).Execute();

        var mockStartCmd = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Actions.Start", (object[] args) => mockStartCmd.Object).Execute();

        var fireCommand = new FireCommand(position, direction, speed);

        fireCommand.Execute();

        mockSetupCmd.Verify(c => c.Execute(), Times.Once);
        mockAddItemCmd.Verify(c => c.Execute(), Times.Once);
        mockStartCmd.Verify(c => c.Execute(), Times.Once);
    }
    [Fact]
    public void RegisterFireDependencies_Execute_ShouldRegisterCommandsFireInIoC()
    {
        var position = new Vectors(new int[] { 1, 2 });
        var direction = new Vectors(new int[] { 3, 4 });
        double speed = 5.5;

        var registerDependencies = new RegisterFireDependencies();
        registerDependencies.Execute();

        var resolvedCommand = Ioc.Resolve<object>("Commands.Fire", position, direction, speed);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<FireCommand>(resolvedCommand);
    }
}