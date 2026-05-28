using System;
using Moq;
using Xunit;
using App;
using SpaceGame.Core;

public class SendCommandTests : IDisposable
{
    public SendCommandTests()
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
    public void Execute_PassesCommandToReceiver()
    {
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        sendCommand.Execute();

        mockReceiver.Verify(r => r.Receive(mockCommand.Object), Times.Once);
    }

    [Fact]
    public void Execute_ThrowsException_WhenReceiverCannotAcceptCommand()
    {
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();

        mockReceiver
            .Setup(r => r.Receive(It.IsAny<ICommand>()))
            .Throws(new InvalidOperationException());

        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        Assert.Throws<InvalidOperationException>(() => sendCommand.Execute());
    }

    [Fact]
    public void RegisterIoCDependencySendCommand_ShouldResolveCorrectlyFromIoC()
    {
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();

        var resolvedCommand = Ioc.Resolve<ICommand>(
            "Commands.Send",
            mockCommand.Object,
            mockReceiver.Object
        );

        Assert.NotNull(resolvedCommand);
        Assert.IsType<SendCommand>(resolvedCommand);
    }
}
