using Xunit;
using SpaceGame.Core;
using SpaceBattle.Lib;
using System;

public class CoverageCompletionTests
{
    [Fact]
    public void TestRotateAndMoveRegistrations()
    {
        var registerRotate = new RegisterIoCDependencyRotateCommand();
        registerRotate.Execute();

        var dict = new System.Collections.Generic.Dictionary<string, object>();
        var rotateCmd = new RotateCommand(dict);
        rotateCmd.Execute();

        var registerMove = new RegisterIoCDependencyMoveCommand();
        registerMove.Execute();
    }

    [Fact]
    public void TestAngleMissingBranches()
    {
        var angle1 = new Angle(45);
        var angle2 = new Angle(90);

        var implicitInt = angle1; 

        Assert.False(angle1 == angle2);

        Assert.False(angle1.Equals("string object"));
        Assert.False(angle1.Equals(null));
    }

    [Fact]
    public void TestVectorEqualityMissingBranches()
    {
        var vector1 = new Vectors(new int[] { 1, 2 });
        var vector2 = new Vectors(new int[] { 5, 6 });

        Assert.False(vector1 == null);
        Assert.False(null == vector1);
        Assert.False(vector1 == vector2);
    }
}
