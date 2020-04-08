using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Pos.Net.Core
{
    public class Lexicon : IEnumerable<string>
    {
        public static Lexicon Default => _instance.Value;
        public static Lexicon Empty => new Lexicon(new Dictionary<string, string[]>());
        private static readonly Lazy<Lexicon> _instance = new Lazy<Lexicon>(InitialiseLexicon, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lexicon InitialiseLexicon()
        {
            using (var stream = typeof(Lexicon).Assembly.GetManifestResourceStream("Pos.Net.Core.lexicon.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        var serializer = new JsonSerializer();
                        return new Lexicon(serializer.Deserialize<Dictionary<string, string[]>>(jsonReader));
                    }
                }
            }
        }

        private readonly Dictionary<string, string[]> _words;

        private Lexicon(Dictionary<string, string[]> words)
        {
            _words = words;
        }

        public string[] this[string word]
        {
            get => _words.TryGetValue(word, out var value) ? value : null;
            set => _words[word] = value;
        }

        public bool ContainsKey(string word)
        {
            return _words.ContainsKey(word);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _words.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
