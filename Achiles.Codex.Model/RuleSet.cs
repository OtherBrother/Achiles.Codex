
namespace Achilles.Codex.Model
{
    public class RuleSet : CodexItem
    {
        public RuleSetState State { get; set; }
        public Article[] Articles { get; set; }
    }

    public enum RuleSetState
    {
        Draft,
        Published
    }
}
