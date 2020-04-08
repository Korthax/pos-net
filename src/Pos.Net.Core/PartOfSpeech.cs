namespace Pos.Net.Core
{
    public class PartOfSpeech
    {
        public string Word { get; set; }
        public string Tag { get; set; }

        private bool Equals(PartOfSpeech other)
        {
            return Word == other.Word && Tag == other.Tag;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((PartOfSpeech)obj);
        }

        public override int GetHashCode()
        {
            unchecked { return ( ( Word != null ? Word.GetHashCode() : 0 ) * 397 ) ^ ( Tag != null ? Tag.GetHashCode() : 0 ); }
        }
    }
}
