using System.Linq;
using Achiles.Codex.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Achiles.Codex.Web.Indexes
{
    public class SearchIndex : AbstractMultiMapIndexCreationTask<SearchIndex.Result>
    {
        public class Result : CodexItem
        {
            public CodexItemType ObjectType { get; set; }
        }
        
        public SearchIndex()
        {
            #region Article and Rule-set
            AddMap<Article>(items => from i in items select new Result
            {
                ObjectType = CodexItemType.Article,
                Id= i.Id,
                Name = i.Name,
                Description = i.Description,
                IconUrl = i.IconUrl,
                Tags = i.Tags
            });

            AddMap<RuleSet>(items => from i in items
                                     select new Result
                                     {
                                         ObjectType = CodexItemType.RuleSet,
                                         Id = i.Id,
                                         Name = i.Name,
                                         Description = i.Description,
                                         IconUrl = i.IconUrl,
                                         Tags = i.Tags
                                     });
            #endregion

            #region Core codex items
            AddMap<Talent>(items => from i in items
                                     select new Result
                                     {
                                         ObjectType = CodexItemType.Talent,
                                         Id = i.Id,
                                         Name = i.Name,
                                         Description = i.Description,
                                         IconUrl = i.IconUrl,
                                         Tags = i.Tags
                                     });
            
            AddMap<AttributeInfo>(items => from i in items
                                    select new Result
                                    {
                                        ObjectType = CodexItemType.AttributeInfo,
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            
            AddMap<Rule>(items => from i in items
                                           select new Result
                                           {
                                               ObjectType = CodexItemType.Rule,
                                               Id = i.Id,
                                               Name = i.Name,
                                               Description = i.Description,
                                               IconUrl = i.IconUrl,
                                               Tags = i.Tags
                                           });

            AddMap<Skill>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = CodexItemType.Skill,
                                      Id = i.Id,
                                      Name = i.Name,
                                      Description = i.Description,
                                      IconUrl = i.IconUrl,
                                      Tags = i.Tags
                                  });
            AddMap<CombatSkill>(items => from i in items
                                   select new Result
                                   {
                                       ObjectType = CodexItemType.CombatSkill,
                                       Id = i.Id,
                                       Name = i.Name,
                                       Description = i.Description,
                                       IconUrl = i.IconUrl,
                                       Tags = i.Tags
                                   });
            #endregion

            #region Weapons
            AddMap<MeleeWeapon>(items => from i in items
                                         select new Result
                                         {
                                             ObjectType = CodexItemType.MeleeWeapon,
                                             Id = i.Id,
                                             Name = i.Name,
                                             Description = i.Description,
                                             IconUrl = i.IconUrl,
                                             Tags = i.Tags
                                         });

            AddMap<MiscellaneousItem>(items => from i in items
                                        select new Result
                                        {
                                            ObjectType = CodexItemType.MiscellaneousItem,
                                            Id = i.Id,
                                            Name = i.Name,
                                            Description = i.Description,
                                            IconUrl = i.IconUrl,
                                            Tags = i.Tags
                                        });

            AddMap<RangedWeapon>(items => from i in items
                                               select new Result
                                               {
                                                   ObjectType = CodexItemType.RangedWeapon,
                                                   Id = i.Id,
                                                   Name = i.Name,
                                                   Description = i.Description,
                                                   IconUrl = i.IconUrl,
                                                   Tags = i.Tags
                                               });
            AddMap<Ammo>(items => from i in items
                                          select new Result
                                          {
                                              ObjectType = CodexItemType.Ammo,
                                              Id = i.Id,
                                              Name = i.Name,
                                              Description = i.Description,
                                              IconUrl = i.IconUrl,
                                              Tags = i.Tags
                                          });
            
            AddMap<Shield>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = CodexItemType.Shield,
                                      Id = i.Id,
                                      Name = i.Name,
                                      Description = i.Description,
                                      IconUrl = i.IconUrl,
                                      Tags = i.Tags
                                  });
            #endregion

            #region Armor

            AddMap<HeadArmor>(items => from i in items
                                    select new Result
                                    {
                                        ObjectType = CodexItemType.HeadArmor,
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            AddMap<BodyArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = CodexItemType.BodyArmor,
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });

            AddMap<ArmArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = CodexItemType.ArmArmor,
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });
            AddMap<LegArmor>(items => from i in items
                                      select new Result
                                      {
                                          ObjectType = CodexItemType.LegArmor,
                                          Id = i.Id,
                                          Name = i.Name,
                                          Description = i.Description,
                                          IconUrl = i.IconUrl,
                                          Tags = i.Tags
                                      });
            #endregion

            Indexes.Add(x=>x.Description, FieldIndexing.Analyzed);
            Indexes.Add(x => x.IconUrl, FieldIndexing.NotAnalyzed);
            Indexes.Add(x => x.Id, FieldIndexing.NotAnalyzed);
            Indexes.Add(x => x.ObjectType, FieldIndexing.NotAnalyzed);
            Indexes.Add(x => x.Name, FieldIndexing.Analyzed);
            Indexes.Add(x => x.Tags, FieldIndexing.NotAnalyzed);

            Analyzers.Add(x => x.Description, "Lucene.Net.Analysis.Standard.StandardAnalyzer");

            Store(x=>x.Id, FieldStorage.Yes);
            Store(x => x.Tags, FieldStorage.Yes);
            Store(x => x.ObjectType, FieldStorage.Yes);
            Store(x => x.IconUrl, FieldStorage.Yes);
            
            }
    }
}