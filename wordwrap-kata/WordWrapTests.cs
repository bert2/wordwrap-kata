namespace wordwrap_kata {
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Xunit;

    public class WordWrapTests {
        [Fact]
        public void LinesEqualWordsWhenAllMatchMaxLength() {
            var result = "abc def".Wrap(3);
            LinesOf(result).Should().BeEquivalentTo("abc", "def");
        }

        [Fact]
        public void LinesAreRightPaddedWordsWhenAllAreShorterThanMaxLength() {
            var result = "abc def".Wrap(4);
            LinesOf(result).Should().BeEquivalentTo("abc ", "def ");
        }

        [Fact]
        public void KeepsWordsInOneLineWhenTheirCombinedLengthIsShorterThanMaxLength() {
            var result = "abc def".Wrap(7);
            LinesOf(result).Should().BeEquivalentTo("abc def");
        }

        [Fact]
        public void WrapsWordsLongerThanMaxLength() {
            var result = "abcdef".Wrap(3);
            LinesOf(result).Should().BeEquivalentTo("abc", "def");
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

