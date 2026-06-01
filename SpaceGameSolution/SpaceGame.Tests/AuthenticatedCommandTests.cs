using System;
using Moq;
using Xunit;
using App;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class AuthenticatedCommandTests : IDisposable
{
    public AuthenticatedCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_WhenAuthorized_ShouldExecuteDecoratedCommand()
    {
        var target = new object();
        var token = new object();
        var mockCommand = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Authorization.Check", (object[] args) => (object)true).Execute();

        var authCommand = new AuthenticatedCommand(mockCommand.Object, target, token);
        authCommand.Execute();

        mockCommand.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_WhenNotAuthorized_ShouldThrowUnauthorizedAccessExceptionAndNotExecute()
    {
        var target = new object();
        var token = new object();
        var mockCommand = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Authorization.Check", (object[] args) => (object)false).Execute();

        var authCommand = new AuthenticatedCommand(mockCommand.Object, target, token);

        Assert.Throws<UnauthorizedAccessException>(() => authCommand.Execute());
        mockCommand.Verify(c => c.Execute(), Times.Never);
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
