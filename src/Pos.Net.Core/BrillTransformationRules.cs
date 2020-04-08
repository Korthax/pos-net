using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Pos.Net.Core
{
    public class BrillTransformationRules
    {
        private List<Action<List<PartOfSpeech>, int>> _rules = new List<Action<List<PartOfSpeech>, int>> { Rule1, Rule2, Rule3, Rule4, Rule5, Rule6, Rule7, Rule8 };

        public Action<List<PartOfSpeech>, int> GetRule(int index)
        {
            return _rules[index];
        }

        public void SetRule(int index, Action<List<PartOfSpeech>, int> rule)
        {
            _rules[index] = rule;
        }

        public void AppendRule(Action<List<PartOfSpeech>, int> rule)
        {
            _rules.Add(rule);
        }

        public void SetRules(List<Action<List<PartOfSpeech>, int>> newRules)
        {
            _rules = newRules;
        }

        public List<Action<List<PartOfSpeech>, int>> GetRules()
        {
            return _rules;
        }

        private static void Rule1(List<PartOfSpeech> taggedSentence, int index)
        {
            if (index <= 0 || taggedSentence[index - 1].Tag != "DT")
                return;

            if (taggedSentence[index].Tag == "VBD" || taggedSentence[index].Tag == "VBP" || taggedSentence[index].Tag == "VB")
                taggedSentence[index].Tag = "NN";
        }

        // rule 2: convert a noun to a number (CD) if "." appears in the word
        private static void Rule2(List<PartOfSpeech> taggedSentence, int index)
        {
            if (!taggedSentence[index].Tag.StartsWith("N"))
                return;

            if (taggedSentence[index].Word.Contains("."))
            {
                // url if there are two contiguous alpha characters
                taggedSentence[index].Tag = Regex.IsMatch(taggedSentence[index].Word, @"/[a-zA-Z]{2}/") ? "URL" : "CD";
            }

            // Attempt to convert into a number
            if (float.TryParse(taggedSentence[index].Word, out _))
                taggedSentence[index].Tag = "CD";
        }

        // rule 3: convert a noun to a past participle if words[i] ends with "ed"
        private static void Rule3(List<PartOfSpeech> taggedSentence, int index)
        {
            if (taggedSentence[index].Tag.StartsWith("N") && taggedSentence[index].Word.EndsWith("ed"))
                taggedSentence[index].Tag = "VBN";
        }

        // rule 4: convert any type to adverb if it ends in "ly";
        private static void Rule4(List<PartOfSpeech> taggedSentence, int index)
        {
            if (taggedSentence[index].Word.EndsWith("ly"))
                taggedSentence[index].Tag = "RB";
        }

        // rule 5: convert a common noun (NN or NNS) to a adjective if it ends with "al"
        private static void Rule5(List<PartOfSpeech> taggedSentence, int index)
        {
            if (taggedSentence[index].Tag.StartsWith("NN") && taggedSentence[index].Word.EndsWith("al"))
                taggedSentence[index].Tag = "JJ";
        }

        // rule 6: convert a noun to a verb if the preceding work is "would"
        private static void Rule6(List<PartOfSpeech> taggedSentence, int index)
        {
            if (index > 0 && taggedSentence[index].Tag.StartsWith("NN") && taggedSentence[index - 1].Word.ToLower() == "would")
                taggedSentence[index].Tag = "VB";
        }

        // rule 7: if a word has been categorized as a common noun and it ends with "s",
        //         then set its type to plural common noun (NNS)
        private static void Rule7(List<PartOfSpeech> taggedSentence, int index)
        {
            if (taggedSentence[index].Tag == "NN" && taggedSentence[index].Word.EndsWith("s"))
                taggedSentence[index].Tag = "NNS";
        }

        // rule 8: convert a common noun to a present participle verb (i.e., a gerund)
        private static void Rule8(List<PartOfSpeech> taggedSentence, int index)
        {
            if (taggedSentence[index].Tag.StartsWith("NN") && taggedSentence[index].Word.EndsWith("ing"))
                taggedSentence[index].Tag = "VBG";
        }
    }
}
