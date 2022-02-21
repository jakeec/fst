using Xunit;
using src;
using System;

namespace test;

public class MonthNameTests
{
    private readonly FST _fst;
    public MonthNameTests()
    {
        _fst = new(' ');
        _fst.Insert("january");
        _fst.Insert("february");
        _fst.Insert("march");
        _fst.Insert("april");
        _fst.Insert("may");
        _fst.Insert("june");
        _fst.Insert("july");
        _fst.Insert("august");
        _fst.Insert("september");
        _fst.Insert("october");
        _fst.Insert("november");
        _fst.Insert("december");
    }

    [Theory]
    [InlineData("j", false, null)]
    [InlineData("ja", true, "january")]
    [InlineData("jan", true, "january")]
    [InlineData("janu", true, "january")]
    [InlineData("janua", true, "january")]
    [InlineData("januar", true, "january")]
    [InlineData("january", true, "january")]
    [InlineData("f", true, "february")]
    [InlineData("fe", true, "february")]
    [InlineData("feb", true, "february")]
    [InlineData("febr", true, "february")]
    [InlineData("febru", true, "february")]
    [InlineData("februa", true, "february")]
    [InlineData("februar", true, "february")]
    [InlineData("february", true, "february")]
    [InlineData("m", false, null)]
    [InlineData("ma", false, null)]
    [InlineData("mar", true, "march")]
    [InlineData("marc", true, "march")]
    [InlineData("march", true, "march")]
    [InlineData("a", false, null)]
    [InlineData("ap", true, "april")]
    [InlineData("apr", true, "april")]
    [InlineData("apri", true, "april")]
    [InlineData("april", true, "april")]
    [InlineData("may", true, "may")]
    [InlineData("j", false, null)]
    [InlineData("ju", false, null)]
    [InlineData("jun", true, "june")]
    [InlineData("june", true, "june")]
    [InlineData("jul", true, "july")]
    [InlineData("july", true, "july")]
    [InlineData("au", true, "august")]
    [InlineData("aug", true, "august")]
    [InlineData("augu", true, "august")]
    [InlineData("augus", true, "august")]
    [InlineData("august", true, "august")]
    [InlineData("s", true, "september")]
    [InlineData("se", true, "september")]
    [InlineData("sep", true, "september")]
    [InlineData("sept", true, "september")]
    [InlineData("septe", true, "september")]
    [InlineData("septem", true, "september")]
    [InlineData("septemb", true, "september")]
    [InlineData("septembe", true, "september")]
    [InlineData("september", true, "september")]
    [InlineData("o", true, "october")]
    [InlineData("oc", true, "october")]
    [InlineData("oct", true, "october")]
    [InlineData("octo", true, "october")]
    [InlineData("octob", true, "october")]
    [InlineData("octobe", true, "october")]
    [InlineData("october", true, "october")]
    [InlineData("n", true, "november")]
    [InlineData("d", true, "december")]
    public void CompleteTests(string input, bool shouldMatch, string? expected) 
    {
        var didMatch = _fst.Complete(input, out string completedWord);
        Assert.Equal(shouldMatch, didMatch);
        if (expected != null) {
            Assert.Equal(expected, completedWord);
        }
    }
}