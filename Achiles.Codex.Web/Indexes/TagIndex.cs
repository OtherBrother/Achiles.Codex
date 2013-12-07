using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achilles.Codex.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Achilles.Codex.Web.Indexes
{
    public class TagStatisticsIndex : AbstractMultiMapIndexCreationTask<TagStatisticsIndex.TagStatistics>
    {
        public class TagStatistics
        {
            public string Tag { get; set; }
            public int Count { get; set; }
        }

        public TagStatisticsIndex()
        {
            AddMap<AttributeInfo>(items=> from i in items 
                                          from t in i.Tags
                                          select new TagStatistics{ Tag= t.ToLower(), Count = 1});

            AddMap<RuleSet>(items => from i in items
                                           from t in i.Tags
                                           select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            
            AddMap<Rule>(items => from i in items
                                     from t in i.Tags
                                     select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            
            AddMap<Talent>(items => from i in items
                                           from t in i.Tags
                                           select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            
            AddMap<Rule>(items => from i in items
                                     from t in i.Tags
                                     select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<Skill>(items => from i in items
                                  from t in i.Tags
                                  select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<NcgEquipmentItem>(items => from i in items
                                   from t in i.Tags
                                   select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<CombatSkill>(items => from i in items
                                   from t in i.Tags
                                   select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            
            AddMap<MeleeWeapon>(items => from i in items
                                         from t in i.Tags
                                         select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<RangedWeapon>(items => from i in items
                                        from t in i.Tags
                                        select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<Ammo>(items => from i in items
                                          from t in i.Tags
                                          select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<Shield>(items => from i in items
                                  from t in i.Tags
                                  select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            
            AddMap<HeadArmor>(items => from i in items
                                    from t in i.Tags
                                    select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            AddMap<BodyArmor>(items => from i in items
                                       from t in i.Tags
                                       select new TagStatistics { Tag = t.ToLower(), Count = 1 });
            AddMap<ArmArmor>(items => from i in items
                                       from t in i.Tags
                                       select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            AddMap<LegArmor>(items => from i in items
                                       from t in i.Tags
                                       select new TagStatistics { Tag = t.ToLower(), Count = 1 });

            Reduce = tags => from t in tags
                group t by t.Tag
                into grouped 
                select new TagStatistics { Tag = grouped.Key, Count = grouped.Sum(x => x.Count) };

            Sort(result => result.Count, SortOptions.Int);

        }
    }
}