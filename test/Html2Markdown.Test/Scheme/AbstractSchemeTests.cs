using System.Collections.Generic;
using Html2Markdown.Replacement;
using Html2Markdown.Scheme;

namespace Html2Markdown.Test.Scheme;

public class AbstractSchemeTests
{
    [Test]
    public void Replacers_WhenCalled_ReturnsReplacerCollection()
    {
        // arrange
        const int expectedCount = 1;
        var scheme = new TestScheme();

        // act
        var replacers = scheme.Replacers();

        // assert
        Assert.That(replacers.Count, Is.EqualTo(expectedCount));
    }
    
    [Test]
    public void Replacers_WhenCalled_ReturnsReplacerCollectionWithTestReplacer()
    {
        // arrange
        var scheme = new TestScheme();

        // act
        var replacers = scheme.Replacers();

        // assert
        Assert.That(replacers[0], Is.InstanceOf<TestReplacer>());
    }
    
    [Test]
    public void Replacers_WhenCalled_ReturnsReplacerCollectionWithTestReplacerReplaceMethod()
    {
        // arrange
        const string expected = "test";
        var scheme = new TestScheme();

        // act
        var replacers = scheme.Replacers();
        var result = replacers[0].Replace(string.Empty);

        // assert
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void AddReplacer_WhenCalled_AddsReplacerToReplacerCollection()
    {
        // arrange
        const int expectedCount = 2;
        var scheme = new TestScheme();

        // act
        scheme.AddReplacer(new TestReplacer());
        var replacers = scheme.Replacers();

        // assert
        Assert.That(replacers.Count, Is.EqualTo(expectedCount));
    }
    
    [Test]
    public void AddReplacer_WhenCalled_AddsReplacerToReplacerCollectionWithTestReplacer()
    {
        // arrange
        var scheme = new TestScheme();

        // act
        scheme.AddReplacer(new TestReplacer());
        var replacers = scheme.Replacers();

        // assert
        Assert.That(replacers[1], Is.InstanceOf<TestReplacer>());
    }
}

public class TestScheme : AbstractScheme
{
    public TestScheme()
    {
        AddReplacementGroup(ReplacerCollection, new TestReplacementGroup());
    }
}

public class TestReplacementGroup : IReplacementGroup
{
    public IEnumerable<IReplacer> Replacers()
    {
        return new List<IReplacer>
        {
            new TestReplacer()
        };
    }
}

public class TestReplacer : IReplacer
{
    public string Replace(string html)
    {
        return "test";
    }
}