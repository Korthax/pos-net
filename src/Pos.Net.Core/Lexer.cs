using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pos.Net.Core
{
    public class Lexer
    {
        private static readonly Regex Ids = new Regex(@"(?:^|\s)[a-z0-9-]{8,45}(?:$|\s)", RegexOptions.IgnoreCase);
        private static readonly Regex Number = new Regex(@"[0-9]*\.[0-9]+|[0-9]+", RegexOptions.IgnoreCase);
        private static readonly Regex Space = new Regex(@"\s+", RegexOptions.IgnoreCase);
        private static readonly Regex Email = new Regex(@"[-!#$%&'*+\/0-9=?A-Z^_a-z{|}~](?:\.?[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~])*@[a-zA-Z0-9](?:-?\.?[a-zA-Z0-9])*(?:\.[a-zA-Z](?:-?[a-zA-Z0-9])*)+", RegexOptions.IgnoreCase);
        private static readonly Regex Urls = new Regex(@"(?:https?:\/\/)(?:[\da-z\.-]+)\.(?:[a-z\.]{2,6})(?:[\/\w\.\-\?#=]*)*\/?", RegexOptions.IgnoreCase);
        private static readonly Regex Punctuation = new Regex(@"[\/\.\,\?\!\""\'\:\;\$\(\)\#\'\`]", RegexOptions.IgnoreCase);
        private static readonly Regex Time = new Regex(@"(?:[0-9]|0[0-9]|1[0-9]|2[0-3]):(?:[0-5][0-9])\s?(?:[aApP][mM])", RegexOptions.IgnoreCase);

        public static List<string> Lex(string item)
        {
            var regexs  = new Queue<Regex>();
            regexs.Enqueue(Urls);
            regexs.Enqueue(Ids);
            regexs.Enqueue(Time);
            regexs.Enqueue(Number);
            regexs.Enqueue(Space);
            regexs.Enqueue(Email);
            regexs.Enqueue(Punctuation);

            var array = new List<string>();
            var node = Node.From(item, regexs);
            node.Populate(array);
            return array;
        }

        private class Node
        {
            private readonly MatchCollection _matches;
            private readonly List<Node> _children;
            private readonly string _value;

            public static Node From(string value, Queue<Regex> regexs)
            {
                if (regexs.Count == 0)
                    return new Node(value, new List<Node>(), Regex.Matches("no", "match"));

                var nextRegex = regexs.Dequeue();

                var matches = nextRegex.Matches(value);
                var items = nextRegex
                    .Split(value)
                    .Select(x => x.Trim())
                    .ToList();

                if (items.Count == 1)
                    return From(value, new Queue<Regex>(regexs));

                var children = items
                    .Select(item => From(item, new Queue<Regex>(regexs)))
                    .ToList();

                return new Node(value, children, matches);
            }

            private Node(string value, List<Node> children, MatchCollection matches)
            {
                _value = value;
                _children = children;
                _matches = matches;
            }

            public void Populate(ICollection<string> values)
            {
                if (_children.Count == 0)
                {
                    if (!string.IsNullOrWhiteSpace(_value))
                        values.Add(_value.Trim());
                }

                for (var i = 0; i < _children.Count; i++)
                {
                    var child = _children[i];
                    child.Populate(values);

                    if (_matches.Count <= i)
                        continue;

                    var match = _matches[i];
                    if (!string.IsNullOrWhiteSpace(match.Value))
                        values.Add(match.Value.Trim());
                }
            }
        }
    }
}
