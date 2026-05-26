using System;
using Moq;
using Xunit;
using SpaceGame.Core;

public class SendCommandTests
{
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

        var resolvedCommand = App.Ioc.Resolve<ICommand>(
            "Commands.Send", 
            mockCommand.Object, 
            mockReceiver.Object
        );

        Assert.NotNull(resolvedCommand);
        Assert.IsType<SendCommand>(resolvedCommand);
    }
}