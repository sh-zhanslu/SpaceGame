using System;
using Xunit;
using Moq;
using App;
using App.Scopes;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class RegisterIoCDependencyMoveCommandTests : IDisposable
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        try
        {
            new InitCommand().Execute();
        }
        catch
        {
        }

        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterMoveCommand_AndCoverLambdaInside()
    {
        var mockMovingObject = new Mock<IMovingObject>();
        var dummyGameObject = new object();

        mockMovingObject.SetupGet(m => m.Position).Returns(new Vectors(new int[] { 0, 0 }));
        mockMovingObject.SetupGet(m => m.Velocity).Returns(new Vectors(new int[] { 0, 0 }));

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IMovingObject",
            (Func<object[], object>)(args => mockMovingObject.Object)
        ).Execute();

        var registerCommand = new RegisterIoCDependencyMoveCommand();
        registerCommand.Execute();

        var command = Ioc.Resolve<object>("Commands.Move", dummyGameObject);

        Assert.NotNull(command);

        dynamic castedCommand = command;
        castedCommand.Execute();

        mockMovingObject.VerifyGet(m => m.Position, Times.Once);
    }

    [Fact]
    public void Execute_WhenIoCRegisterThrowsArgumentException_ShouldExecuteCatchBlock()
    {
        var mockCommand = new Mock<App.ICommand>();
        mockCommand.Setup(c => c.Execute()).Throws(new ArgumentException());

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "IoC.Register",
            (Func<object[], object>)(args => mockCommand.Object)
        ).Execute();

        var registerCommand = new RegisterIoCDependencyMoveCommand();

        var exception = Record.Exception(() => registerCommand.Execute());

        Assert.Null(exception);
    }

    public void Dispose()
    {
        try
        {
            Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
        }
        catch
        {
        }
    }
}
