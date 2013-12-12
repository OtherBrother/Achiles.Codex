namespace Achilles.Codex.Model
{
    public class Effect
    {
        public string EffectType { get; set; }  // bleed, critical,trauma,poison
        public int Span { get; set; }
    }
}