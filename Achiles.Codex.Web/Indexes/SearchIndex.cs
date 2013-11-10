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
                ObjectType = "article",
                Id= i.Id,
                Name = i.Name,
                Description = i.Description,
                IconUrl = i.IconUrl,
                Tags = i.Tags
            });

            AddMap<RuleSet>(items => from i in items
                                     select new Result
                                     {
                                         ObjectType = "ruleset",
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
                                         ObjectType = "talent",
                                         Id = i.Id,
                                         Name = i.Name,
                                         Description = i.Description,
                                         IconUrl = i.IconUrl,
                                         Tags = i.Tags
                                     });
            
            AddMap<AttributeInfo>(items => from i in items
                                    select new Result
                                    {
                                        ObjectType = "attribute",
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            
            AddMap<Rule>(items => from i in items
                                           select new Result
                                           {
                                               ObjectType = "rule",
                                               Id = i.Id,
                                               Name = i.Name,
                                               Description = i.Description,
                                               IconUrl = i.IconUrl,
                                               Tags = i.Tags
                                           });

            AddMap<Skill>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = "skill",
                                      Id = i.Id,
                                      Name = i.Name,
                                      Description = i.Description,
                                      IconUrl = i.IconUrl,
                                      Tags = i.Tags
                                  });
            AddMap<CombatSkill>(items => from i in items
                                   select new Result
                                   {
                                       ObjectType = "combatskill",
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
                                             ObjectType = "handweapon",
                                             Id = i.Id,
                                             Name = i.Name,
                                             Description = i.Description,
                                             IconUrl = i.IconUrl,
                                             Tags = i.Tags
                                         });

            AddMap<HandWeapon>(items => from i in items
                                        select new Result
                                        {
                                            ObjectType = "handweapon",
                                            Id = i.Id,
                                            Name = i.Name,
                                            Description = i.Description,
                                            IconUrl = i.IconUrl,
                                            Tags = i.Tags
                                        });
            AddMap<MiscellaneousItem>(items => from i in items
                                        select new Result
                                        {
                                            ObjectType = "miscellaneousitem",
                                            Id = i.Id,
                                            Name = i.Name,
                                            Description = i.Description,
                                            IconUrl = i.IconUrl,
                                            Tags = i.Tags
                                        });

            AddMap<RangedWeapon>(items => from i in items
                                               select new Result
                                               {
                                                   ObjectType = "rangedweapon",
                                                   Id = i.Id,
                                                   Name = i.Name,
                                                   Description = i.Description,
                                                   IconUrl = i.IconUrl,
                                                   Tags = i.Tags
                                               });
            AddMap<Ammo>(items => from i in items
                                          select new Result
                                          {
                                              ObjectType = "ammo",
                                              Id = i.Id,
                                              Name = i.Name,
                                              Description = i.Description,
                                              IconUrl = i.IconUrl,
                                              Tags = i.Tags
                                          });
            
            AddMap<Shield>(items => from i in items
                                  select new Result
                                  {
                                      ObjectType = "shield",
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
                                        ObjectType = "headarmor",
                                        Id = i.Id,
                                        Name = i.Name,
                                        Description = i.Description,
                                        IconUrl = i.IconUrl,
                                        Tags = i.Tags
                                    });
            AddMap<BodyArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = "bodyarmor",
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });

            AddMap<ArmArmor>(items => from i in items
                                       select new Result
                                       {
                                           ObjectType = "armarmor",
                                           Id = i.Id,
                                           Name = i.Name,
                                           Description = i.Description,
                                           IconUrl = i.IconUrl,
                                           Tags = i.Tags
                                       });
            AddMap<LegArmor>(items => from i in items
                                      select new Result
                                      {
                                          ObjectType = "legarmor",
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