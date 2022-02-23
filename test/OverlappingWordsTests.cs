using Xunit;
using src;
using System;

namespace test;

public class OverlappingWordsTests
{
    private readonly FST _fst;
    public OverlappingWordsTests()
    {
        _fst = new(' ');
        var lines = new[] {
            "a",
            "abc",
            "abcde",
            "abcdefg",
            "abcdefghi",
            "abcdefghijk",
        };
        foreach (var line in lines) 
        {
            _fst.Insert(line);
        }
    }

    [Theory]
    [InlineData("a", true, "a")]
    [InlineData("ab", true, "abc")]
    [InlineData("abc", true, "abc")]
    [InlineData("abcd", true, "abcde")]
    [InlineData("abcde", true, "abcde")]
    [InlineData("abcdef", true, "abcdefg")]
    [InlineData("abcdefg", true, "abcdefg")]
    [InlineData("abcdefgh", true, "abcdefghi")]
    [InlineData("abcdefghi", true, "abcdefghi")]
    [InlineData("abcdefghij", true, "abcdefghijk")]
    [InlineData("abcdefghijk", true, "abcdefghijk")]
    public void CompleteTests(string input, bool shouldMatch, string? expected) 
    {
        var didMatch = _fst.Complete(input, out string completedWord);
        Assert.Equal(shouldMatch, didMatch);
        if (expected != null) {
            Assert.Equal(expected, completedWord);
        }
    }
}