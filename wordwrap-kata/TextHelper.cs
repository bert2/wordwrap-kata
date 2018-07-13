namespace wordwrap_kata {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TextHelper {
        public static string Wrap(this string text, int columns) => text?
            .GetWords()
            .BreakLongWords(columns)
            .CombineWordsIntoLines(columns)
            .PadLineEndings(columns)
            .JoinLines(Environment.NewLine);

        private static IEnumerable<string> GetWords(this string text) => text.Split(' ');

        private static IEnumerable<string> BreakLongWords(this IEnumerable<string> words, int maxLength) => words
            .SelectMany(w => BreakWordIfTooLong(w, maxLength));

        private static IEnumerable<string> BreakWordIfTooLong(string word, int maxLength) => word.Length <= maxLength
            ? new[] { word }
            : new[] { word.Substring(0, maxLength) }.Concat(BreakWordIfTooLong(word.Substring(maxLength), maxLength));

        private static IEnumerable<string> CombineWordsIntoLines(this IEnumerable<string> words, int columns) {
            var wordsQ = new Queue<string>(words);
            var line = "";

            while (wordsQ.Any()) {
                if (line.Append(wordsQ.Peek()).Length <= columns) {
                    line = line.Append(wordsQ.Dequeue());
                }
                else {
                    yield return line;
                    line = "";
                }
            }

            yield return line;
        }

        private static string Append(this string line, string word) => line.Length > 0 ? $"{line} {word}" : word;

        private static IEnumerable<string> PadLineEndings(this IEnumerable<string> lines, int length) => lines
            .Select(l => l.PadRight(length));

        private static string JoinLines(this IEnumerable<string> lines, string separator) => string.Join(separator, lines);
    }
}
