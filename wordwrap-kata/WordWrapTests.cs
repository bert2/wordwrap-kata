namespace wordwrap_kata {
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Xunit;

    public class WordWrapTests {
        [Fact]
        public void LinesEqualWordsWhenAllMatchMaxLength() {
            var result = "abc def ghi".Wrap(3);
            LinesOf(result).Should().BeEquivalentTo("abc", "def", "ghi");
        }

        [Fact]
        public void LinesAreRightPaddedWordsWhenAllAreShorterThanMaxLength() {
            var result = "abc de fg".Wrap(4);
            LinesOf(result).Should().BeEquivalentTo("abc ", "de  ", "fg  ");
        }

        [Fact]
        public void KeepsWordsInOneLineWhenTheirCombinedLengthIsShorterThanMaxLength() {
            var result = "abc def ghi".Wrap(11);
            LinesOf(result).Should().BeEquivalentTo("abc def ghi");
        }

        [Fact]
        public void BreaksWordsLongerThanMaxLength() {
            var result = "abcdefghi".Wrap(3);
            LinesOf(result).Should().BeEquivalentTo("abc", "def", "ghi");
        }

        [Fact]
        public void ReturnsMaxLengthSpacesWhenInputIsEmpty() {
            var result = "".Wrap(3);
            result.Should().Be("   ");
        }

        [Fact]
        public void ReturnsNullWhenInputIsNull() {
            var result = ((string)null).Wrap(3);
            result.Should().Be(null);
        }

        private static IEnumerable<string> LinesOf(string s) => s.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }
}

