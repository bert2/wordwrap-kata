namespace wordwrap_kata {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TextHelper {
        public static string Wrap(this string text, int maxLength) => text?
            .GetWords()
            .BreakLongWords(maxLength)
            .CombineLines(maxLength)
            .PadEndings(maxLength)
            .Join(Environment.NewLine);

        private static IEnumerable<string> GetWords(this string text) => text.Split(' ');

        private static IEnumerable<string> BreakLongWords(this IEnumerable<string> words, int maxLength) => words
            .SelectMany(w => w.Length > maxLength
                ? new[] { w.Substring(0, maxLength), w.Substring(maxLength) }
                : new[] { w });

        private static IEnumerable<string> CombineLines(this IEnumerable<string> words, int length) {
            var wordsQ = new Queue<string>(words);
            var line = "";

            while (wordsQ.Any()) {
                if (line.Append(wordsQ.Peek()).Length <= length) {
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

        private static IEnumerable<string> PadEndings(this IEnumerable<string> lines, int length) => lines.Select(l => l.PadRight(length));

        private static string Join(this IEnumerable<string> lines, string separator) => string.Join(separator, lines);
    }
}
