using System;
using Xunit;
using App;
using SpaceGame.Core;

namespace SpaceGame.Tests;

public class RegisterAuthorizationStrategyTests
{
    public RegisterAuthorizationStrategyTests()
    {
        try
        {
            new App.Scopes.InitCommand().Execute();
        }
        catch
        {
        }
    }

    [Fact]
    public void Execute_ShouldRegisterAuthorizationStrategy_AndCoverAllBranches()
    {
        var target = new object();
        var token = "valid_token";

        string currentPlayerId = "player_1";
        string currentOwnerId = "player_1";

        Ioc.Resolve<App.ICommand>("IoC.Register", "Token.GetPlayerId", (Func<object[], object>)(args => currentPlayerId)).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.GetObjectOwner", (Func<object[], object>)(args => currentOwnerId)).Execute();

        var command = new RegisterAuthorizationStrategy();
        command.Execute();

        currentPlayerId = "player_1";
        currentOwnerId = "player_1";
        
        var resultTrue = Ioc.Resolve<bool>("Authorization.Check", target, token);
        Assert.True(resultTrue);

        currentPlayerId = "player_1";
        currentOwnerId = "player_2";
        
        var resultFalse = Ioc.Resolve<bool>("Authorization.Check", target, token);
        Assert.False(resultFalse);
    }
}
