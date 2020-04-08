# Pos.Net

## About

**pos.net** is a C# port of Darius Kazemi fork of [pos-js](https://github.com/dariusk/pos-js) created by Percy Wegmann ([pos-js](https://github.com/neopunisher/pos-js)) which is a javascript port of Mark Watson's FastTag Part of Speech Tagger which was itself based on Eric Brill's trained rule set and English
lexicon.

**pos.net** includes a basic lexer that can be used to extract words and other tokens from text strings.

**pos.net** was written by [Steve Phillips](https://github.com/Korthax) and is [available on Github](https://github.com/Korthax/pos-net).

## License

**pos.net** is licensed under the GNU LGPLv3

## Install

`TODO`

## Usage

```csharp
var tagger = new PartsOfSpeechTagger(Lexicon.Default, new BrillTransformationRules());

var words = Lexer.Lex("This is some sample text. This text can contain multiple sentences.");
var tags = tagger.Tag(words);
var elapsedTime = stopwatch.ElapsedMilliseconds;

foreach(var tag in tags)
    Console.WriteLine("{0} / {1}", tag.Word, tag.Tag);

// extend the lexicon
var tagger = new PartsOfSpeechTagger(Lexicon.Default, new BrillTransformationRules());
tagger.ExtendLexicon(new Dictionary<string, string[]>
{
    ["Obama"] = new[] { "NNP" }
});
var tags = tagger.Tag(new List<string> { "Mr", "Obama" });
```

## Acknowledgements

Thanks to Darius Kazemi and Percy Wegmann for providing the basis for **pos.net** and to Mark Watson for writing FastTag originally.

## Tags
```
    CC Coord Conjuncn           and,but,or
    CD Cardinal number          one,two
    DT Determiner               the,some
    EX Existential there        there
    FW Foreign Word             mon dieu
    IN Preposition              of,in,by
    JJ Adjective                big
    JJR Adj., comparative       bigger
    JJS Adj., superlative       biggest
    LS List item marker         1,One
    MD Modal                    can,should
    NN Noun, sing. or mass      dog
    NNP Proper noun, sing.      Edinburgh
    NNPS Proper noun, plural    Smiths
    NNS Noun, plural            dogs
    POS Possessive ending       's
    PDT Predeterminer           all, both
    PP$ Possessive pronoun      my,one's
    PRP Personal pronoun        I,you,she
    RB Adverb                   quickly
    RBR Adverb, comparative     faster
    RBS Adverb, superlative     fastest
    RP Particle                 up,off
    SYM Symbol                  +,%,&
    TO to                       to
    UH Interjection             oh, oops
    VB verb, base form          eat
    VBD verb, past tense        ate
    VBG verb, gerund            eating
    VBN verb, past part         eaten
    VBP Verb, present           eat
    VBZ Verb, present           eats
    WDT Wh-determiner           which,that
    WP Wh pronoun               who,what
    WP$ Possessive-Wh           whose
    WRB Wh-adverb               how,where
    , Comma                     ,
    . Sent-final punct          . ! ?
    : Mid-sent punct.           : ; Ñ
    $ Dollar sign               $
    # Pound sign                #
    " quote                     "
    ( Left paren                (
    ) Right paren               )
```
