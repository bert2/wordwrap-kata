namespace wordwrap_kata {
    using System;
    using FluentAssertions;
    using FluentAssertions.Collections;
    using FluentAssertions.Primitives;
    using Xunit;

    public class WordWrapTests {

        [Fact] public void LinesEqualWordsWhenAllMatchMaxLength() =>
            "abc def ghi"
            .Wrap(3)
            .Should().HaveLines("abc", "def", "ghi");

        [Fact] public void LinesAreRightPaddedWordsWhenAllAreShorterThanMaxLength() =>
            "abc de fg"
            .Wrap(4)
            .Should().HaveLines("abc ", "de  ", "fg  ");

        [Fact] public void KeepsWordsInOneLineWhenTheirCombinedLengthIsShorterThanMaxLength() =>
            "abc def ghi"
            .Wrap(11)
            .Should().HaveLines("abc def ghi");

        [Fact] public void BreaksWordsLongerThanMaxLength() =>
            "abcdefghi"
            .Wrap(3)
            .Should().HaveLines("abc", "def", "ghi");

        [Fact] public void ReturnsMaxLengthSpacesWhenInputIsEmpty() =>
            ""
            .Wrap(3)
            .Should().Be("   ");

        [Fact] public void ReturnsNullWhenInputIsNull() =>
            ((string)null)
            .Wrap(3)
            .Should().Be(null);
    }

    public static class Ext {
        public static AndConstraint<StringCollectionAssertions> HaveLines(this StringAssertions a, params string[] lines) => a
            .Subject
            .Split(Environment.NewLine)
            .Should().BeEquivalentTo(lines);
    }
}

