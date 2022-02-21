using Xunit;
using src;
using System;

namespace test;

public class WordsTests
{
    private readonly FST _fst;
    public WordsTests()
    {
        _fst = new(' ');
        var lines = System.IO.File.ReadAllLines("/usr/share/dict/words");
        foreach (var line in lines) 
        {
            _fst.Insert(line);
        }
    }

    [Theory]
    [InlineData("zippier", true, "zippier")]
    [InlineData("Agamem", true, "Agamemnon")]
    public void CompleteTests(string input, bool shouldMatch, string? expected) 
    {
        var didMatch = _fst.Complete(input, out string completedWord);
        Assert.Equal(shouldMatch, didMatch);
        if (expected != null) {
            Assert.Equal(expected, completedWord);
        }
    }
}