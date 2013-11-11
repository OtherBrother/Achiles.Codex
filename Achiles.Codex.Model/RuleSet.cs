
namespace Achiles.Codex.Model
{
    public class RuleSet : CodexItem
    {
        public RuleSetState State { get; set; }
    }

    public enum RuleSetState
    {
        Draft,
        Published
    }
}
