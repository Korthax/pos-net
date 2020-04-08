using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Pos.Net.Core.Tests
{
    public class LexerTests
    {
        [Fact]
        public void ShouldTokenizeDollarSign()
        {
            Lexer.Lex("I made $5.42 today.")
                .Should().Equal(new List<string> { "I", "made", "$", "5.42", "today", "." });
        }

        [Fact]
        public void ShouldTokenizeParentheses()
        {
            Lexer.Lex("I made $some today.")
                .Should().Equal(new List<string> { "I", "made", "$", "some", "today", "." });
        }

        [Fact]
        public void ShouldTokenizeDollarSignWhenNotFollowedByANumber()
        {
            Lexer.Lex("I made $5.42 today (but it should have been more).")
                .Should().Equal(new List<string>
                {
                    "I", "made", "$", "5.42", "today", "(", "but", "it", "should", "have", "been", "more", ")", "."
                });
        }

        [Fact]
        public void ShouldTokenizeHash()
        {
            Lexer.Lex("I'm fixing issue #6.")
                .Should().Equal(new List<string> { "I", "'", "m", "fixing", "issue", "#", "6", "." });
        }

        [Fact]
        public void ShouldTokenizeHashWhenNotFollowedByANumber()
        {
            Lexer.Lex("I'm fixing issue #tags.")
                .Should().Equal(new List<string> { "I", "'", "m", "fixing", "issue", "#", "tags", "." });
        }

        [Fact]
        public void ShouldTokenizeTimesSuchAs10Am()
        {
            Lexer.Lex("It is now 10:00am")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00am" });

            Lexer.Lex("It is now 10:00 am")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00 am" });

            Lexer.Lex("It is now 10:00pm")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00pm" });

            Lexer.Lex("It is now 10:00 pm")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00 pm" });

            Lexer.Lex("It is now 10:00AM")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00AM" });

            Lexer.Lex("It is now 10:00PM")
                .Should().Equal(new List<string> { "It", "is", "now", "10:00PM" });
        }

        [Fact]
        public void ShouldTokenizeButKeepEmailIdsAndUrlsIntact()
        {
            Lexer.Lex("my email is me@exampleabc.com, http://example.com and 19bdnznUXdHEOlp0Pnp9JY0rug6VuA2R3zK4AACdFzhE")
                .Should().Equal(new List<string> { "my", "email", "is", "me@exampleabc.com", ",", "http://example.com", "and", "19bdnznUXdHEOlp0Pnp9JY0rug6VuA2R3zK4AACdFzhE" });

            Lexer.Lex("my share url is https://docs.google.com/spreadsheets/d/19bdnznUXdHEOlp0Pnp9JY0rug6VuA2R3zK4AACdFzhE/edit?usp=sharing")
                .Should().Equal(new List<string> { "my", "share", "url", "is", "https://docs.google.com/spreadsheets/d/19bdnznUXdHEOlp0Pnp9JY0rug6VuA2R3zK4AACdFzhE/edit?usp=sharing" });
        }
    }
}
