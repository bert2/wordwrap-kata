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
            .Join(Environment.NewLine);

        private static IEnumerable<string> GetWords(this string text) => text.Split(' ');

        private static IEnumerable<string> BreakLongWords(this IEnumerable<string> words, int maxLength) => words
            .SelectMany(w => BreakWord(w, maxLength));

        private static IEnumerable<string> BreakWord(string word, int maxLength) => word.Length <= maxLength
            ? word.AsSingleton()
            : word.Substring(0, maxLength).AsSingleton()
                .Concat(BreakWord(word.Substring(maxLength), maxLength));

        private static IEnumerable<string> CombineWordsIntoLines(this IEnumerable<string> words, int columns) => words
            .Aggregate(
                new List<string> { string.Empty },
                (lines, word) => {
                    if (lines.Last().Append(word).Length <= columns)
                        lines.AppendToLast(word);
                    else
                        lines.Add(word);
                    return lines;
                });

        private static string Append(this string line, string word) => line.Length > 0 ? $"{line} {word}" : word;

        private static void AppendToLast(this IList<string> lines, string word) => lines[lines.Count - 1] = lines[lines.Count - 1].Append(word);

        private static IEnumerable<string> PadLineEndings(this IEnumerable<string> lines, int length) => lines
            .Select(l => l.PadRight(length));

        private static string Join(this IEnumerable<string> strs, string separator) => string.Join(separator, strs);

        private static IEnumerable<T> AsSingleton<T>(this T item) => Enumerable.Repeat(item, 1);
    }
}
