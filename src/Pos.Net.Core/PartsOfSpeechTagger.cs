using System;
using System.Collections.Generic;

namespace Pos.Net.Core
{
    public class PartsOfSpeechTagger
    {
        private readonly BrillTransformationRules _transformationRules;
        private readonly Lexicon _lexicon;

        public PartsOfSpeechTagger(Lexicon lexicon, BrillTransformationRules transformationRules)
        {
            _lexicon = lexicon;
            _transformationRules = transformationRules;
        }

        public bool WordInLexicon(string word)
        {
            var ss = _lexicon[word];
            if (ss != null)
                return true;

            ss = _lexicon[word.ToLower()];
            return ss != null;
        }

        public List<PartOfSpeech> Tag(List<string> words)
        {
            var taggedSentence = new List<PartOfSpeech>();

            for (var i = 0; i < words.Count; i++)
            {
                taggedSentence.Add(new PartOfSpeech { Word = words[i] });

                var ss = _lexicon[words[i]] ?? _lexicon[words[i].ToLower()];
                if (ss != null)
                {
                    if(words[i].Length == 1)
                        taggedSentence[i].Tag = words[i] + "^";
                    else
                        taggedSentence[i].Tag = ss[0];
                }
                else
                    taggedSentence[i].Tag = "NN";
            }

            for (var index = 0; index < taggedSentence.Count; index++)
            {
                foreach (var rule in _transformationRules.GetRules())
                    rule(taggedSentence, index);
            }

            return taggedSentence;
        }

        public void PrettyPrint(IEnumerable<PartOfSpeech> taggedWords)
        {
            foreach(var taggedWord in taggedWords)
                Console.WriteLine(taggedWord.Word + "(" + taggedWord.Tag + ")");
        }

        public void ExtendLexicon(Dictionary<string, string[]> lexicon)
        {
            foreach(var item in lexicon)
            {
                if (!_lexicon.ContainsKey(item.Key))
                    _lexicon[item.Key] = lexicon[item.Key];
            }
        }
    }
}
