using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achiles.Codex.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Achiles.Codex.Web.Indexes
{
    public class SearchIndex : AbstractMultiMapIndexCreationTask<SearchIndex.Result>
    {
        public class Result : CodexItem
        {
            public string ObjectType { get; set; }
        }
        
        public SearchIndex()
        {
            #region Article and Rule-set
            AddMap<Article>(items => from i in items select new Result
            {
                ObjectType = "Article",
                Id= i.Id,
                Name = i.Name,
                Description = i.Description,
                IconUrl = i.IconUrl,
                Tags = i.Tags
            });

            AddMap<RuleSet>(items => from i in items
                                     select new Result
                                     {
                                         ObjectType = "RuleSet",
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
                                         ObjectType = "Talent",
                                         Id = i.Id,
                                         Name = i.Name,
                                         Description = i.Description,
                                         IconUrl = i.IconUrl,
                                         Tags = i.Tags
                                     });
            
            AddMap<AttributeInfo>(items => from i in items
                                    select new Result
                                    {
                                        ObjectType = "Attribute",
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            
            AddMap<Rule>(items => from i in items
                                           select new Result
                                           {
                                               ObjectType = "Rule",
                                               Id = i.Id,
                                               Name = i.Name,
                                               Description = i.Description,
                                               IconUrl = i.IconUrl,
                                               Tags = i.Tags
                                           });

            AddMap<Skill>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = "Skill",
                                      Id = i.Id,
                                      Name = i.Name,
                                      Description = i.Description,
                                      IconUrl = i.IconUrl,
                                      Tags = i.Tags
                                  });
            AddMap<CombatSkill>(items => from i in items
                                   select new Result
                                   {
                                       ObjectType = "CombatSkill",
                                       Id = i.Id,
                                       Name = i.Name,
                                       Description = i.Description,
                                       IconUrl = i.IconUrl,
                                       Tags = i.Tags
                                   });
            #endregion

            #region Weapons
            AddMap<HandWeapon>(items => from i in items
                                         select new Result
                                         {
                                             ObjectType = "HandWeapon",
                                             Id = i.Id,
                                             Name = i.Name,
                                             Description = i.Description,
                                             IconUrl = i.IconUrl,
                                             Tags = i.Tags
                                         });

            AddMap<HandWeapon>(items => from i in items
                                        select new Result
                                        {
                                            ObjectType = "HandWeapon",
                                            Id = i.Id,
                                            Name = i.Name,
                                            Description = i.Description,
                                            IconUrl = i.IconUrl,
                                            Tags = i.Tags
                                        });
            AddMap<MiscellaneousItem>(items => from i in items
                                        select new Result
                                        {
                                            ObjectType = "MiscellaneousItem",
                                            Id = i.Id,
                                            Name = i.Name,
                                            Description = i.Description,
                                            IconUrl = i.IconUrl,
                                            Tags = i.Tags
                                        });

            AddMap<RangedWeapon>(items => from i in items
                                               select new Result
                                               {
                                                   ObjectType = "RangedWeapon",
                                                   Id = i.Id,
                                                   Name = i.Name,
                                                   Description = i.Description,
                                                   IconUrl = i.IconUrl,
                                                   Tags = i.Tags
                                               });
            AddMap<Ammo>(items => from i in items
                                          select new Result
                                          {
                                              ObjectType = "Ammo",
                                              Id = i.Id,
                                              Name = i.Name,
                                              Description = i.Description,
                                              IconUrl = i.IconUrl,
                                              Tags = i.Tags
                                          });
            
            AddMap<Shield>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = "Shield",
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
                                        ObjectType = "HeadArmor",
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            AddMap<BodyArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = "BodyArmor",
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });

            AddMap<ArmArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = "ArmArmor",
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });
            AddMap<LegArmor>(items => from i in items
                                      select new Result
                                      {
                                          ObjectType = "LegArmor",
                                          Id = i.Id,
                                          Name = i.Name,
                                          Description = i.Description,
                                          IconUrl = i.IconUrl,
                                          Tags = i.Tags
                                      });
            #endregion

            Indexes.Add(x=>x.Description, FieldIndexing.Analyzed);
            Indexes.Add(x => x.IconUrl, FieldIndexing.No);
            Indexes.Add(x => x.Id, FieldIndexing.No);
            Indexes.Add(x => x.ObjectType, FieldIndexing.Default);
            Indexes.Add(x => x.Name, FieldIndexing.Analyzed);
            Indexes.Add(x => x.Tags, FieldIndexing.Default);

            Analyzers.Add(x => x.Description, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
            Analyzers.Add(x => x.Name, "Lucene.Net.Analysis.Standard.StandardAnalyzer");

        }
    }
}