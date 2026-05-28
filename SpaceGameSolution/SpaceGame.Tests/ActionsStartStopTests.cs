using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using App;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class ActionsStartStopTests : IDisposable
{
    public ActionsStartStopTests()
    {
        new App.Scopes.InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }

    [Fact]
    public void ActionsStart_ShouldRegisterAndQueueInjectableCommand()
    {
        var regStart = new RegisterIoCDependencyActionsStart();
        regStart.Execute();

        var regInjectable = new RegisterDependencyCommandInjectableCommand();
        regInjectable.Execute();

        var mockMoveCommand = new Mock<ICommand>();
        var mockRegCmd = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Movement.Move", (object[] args) => mockMoveCommand.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Queue.Push", (object[] args) => mockRegCmd.Object).Execute();

        var order = new Dictionary<string, object>
        {
            { "Target", "Ship_1" },
            { "Action", "Movement.Move" }
        };

        var startCommand = Ioc.Resolve<ICommand>("Actions.Start", order);
        startCommand.Execute();

        var registry = Ioc.Resolve<IDictionary<object, ICommandInjectable>>("Actions.Registry");
        Assert.True(registry.ContainsKey("Ship_1"));
    }

    [Fact]
    public void ActionsStop_ShouldReplaceCommandWithEmptyCommand()
    {
        var regStop = new RegisterIoCDependencyActionsStop();
        regStop.Execute();

        var mockEmpty = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Empty", (object[] args) => mockEmpty.Object).Execute();

        var mockInjectable = new Mock<ICommandInjectable>();
        var registry = new Dictionary<object, ICommandInjectable>
        {
            { "Ship_1", mockInjectable.Object }
        };
        Ioc.Resolve<App.ICommand>("IoC.Register", "Actions.Registry", (object[] args) => registry).Execute();

        var order = new Dictionary<string, object>
        {
            { "Target", "Ship_1" }
        };

        var stopCommand = Ioc.Resolve<ICommand>("Actions.Stop", order);
        stopCommand.Execute();

        mockInjectable.Verify(x => x.Inject(mockEmpty.Object), Times.Once);
        Assert.False(registry.ContainsKey("Ship_1"));
    }
}
