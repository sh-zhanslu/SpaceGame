using Xunit;
using Moq;
using System;
using App;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class CommandInjectableCommandTests
{
    [Fact]
    public void CommandInjectable_ShouldExecuteInjectedCommand()
    {
        var injectableCommand = new CommandInjectableCommand();
        var mockCommand = new Mock<ICommand>();
        
        injectableCommand.Inject(mockCommand.Object);
        injectableCommand.Execute();
        
        mockCommand.Verify(m => m.Execute(), Times.Once);
    }

    [Fact]
    public void CommandInjectable_ShouldThrowException_WhenNoCommandInjected()
    {
        var injectableCommand = new CommandInjectableCommand();
        
        Assert.Throws<Exception>(() => injectableCommand.Execute());
    }

    [Fact]
    public void RegisterDependencyCommandInjectable_ShouldResolveAllRequiredTypes()
    {
        var registerCommand = new RegisterDependencyCommandInjectableCommand();
        registerCommand.Execute();

        var resolvedAsCommand = Ioc.Resolve<ICommand>("Commands.CommandInjectable");
        var resolvedAsInjectable = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        
        Assert.NotNull(resolvedAsCommand);
        Assert.NotNull(resolvedAsInjectable);
    }
}
