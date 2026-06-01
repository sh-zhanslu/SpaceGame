using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using App;
using App.Scopes;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class RegisterIoCDependencyRotateCommandTests : IDisposable
{
    public RegisterIoCDependencyRotateCommandTests()
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
    public void Execute_ShouldRegisterRotateCommandAndResolveItSuccessfully()
    {
        var mockAdapter = new Mock<IDictionary<string, object>>();
        var dummyGameObject = new object();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IRotatingObject",
            (Func<object[], object>)(args => mockAdapter.Object)
        ).Execute();

        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        var command = Ioc.Resolve<object>("Commands.Rotate", dummyGameObject);

        Assert.NotNull(command);
        Assert.IsType<RotateCommand>(command);
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