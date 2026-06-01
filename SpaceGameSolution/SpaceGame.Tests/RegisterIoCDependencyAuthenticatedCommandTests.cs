using System;
using Moq;
using Xunit;
using App;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class RegisterIoCDependencyAuthenticatedCommandTests : IDisposable
{
    public RegisterIoCDependencyAuthenticatedCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterAuthenticatedCommandInIoC()
    {
        new RegisterIoCDependencyAuthenticatedCommand().Execute();

        var mockCommand = new Mock<ICommand>();
        var target = new object();
        var token = new object();

        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Authenticated", mockCommand.Object, target, token);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<AuthenticatedCommand>(resolvedCommand);
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
