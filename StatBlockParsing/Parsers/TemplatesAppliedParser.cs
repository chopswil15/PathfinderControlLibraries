
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PathfinderContext.PathfinderContextEnums;

namespace StatBlockParsing
{
    public class TemplatesAppliedParser : ITemplatesAppliedParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;
        private IMonsterStatBlockBusiness _monsterStatBlockBusiness;
        private IIndividualStatBlockBusiness _individualStatBlockBusiness;

        public TemplatesAppliedParser(ISBCommonBaseInput sbcommonBaseInput, IMonsterStatBlockBusiness monsterStatBlockBusiness, IIndividualStatBlockBusiness individualStatBlockBusiness)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
            _monsterStatBlockBusiness = monsterStatBlockBusiness;
            _individualStatBlockBusiness = individualStatBlockBusiness;
        }

        public void ParseTemplatesApplied(ref string temp, ref string raceLine, string raceLineHold)
        {
            if (raceLineHold.Contains(" skeleton"))
            {
                raceLine = ParseTemplatesApplied_Skeleton(raceLine, raceLineHold);
            }
            else if (raceLineHold.Contains(" skeletal champion"))
            {
                raceLine = ParseTemplatesApplied_SkeletalChampion(raceLine);
            }
            else if (ZombieTypeCheck(raceLineHold))
            {
                raceLine = ParseTemplatesApplied_Zombie(raceLine);
            }
            else
            {
                raceLine = ParseTemplatesApplied_Normal(raceLine);
            }

            if (_sbcommonBaseInput.IndvidSB.TemplatesApplied.Length > 0)
            {
                temp = temp.Replace(raceLineHold, raceLine);
                _sbcommonBaseInput.IndvidSB.TemplatesApplied = _sbcommonBaseInput.IndvidSB.TemplatesApplied.Trim();
                raceLine = ParseTemplatesApplied_Normal(raceLine.Trim());
            }
        }

        private string ParseTemplatesApplied_SkeletalChampion(string raceLine)
        {
            raceLine = ParseTemplatesApplied_SkeletonBase(raceLine.ToLower());

            if (_sbcommonBaseInput.IndvidSB.TemplatesApplied.Length > 0)
            {
                _sbcommonBaseInput.IndvidSB.TemplatesApplied = _sbcommonBaseInput.IndvidSB.TemplatesApplied.Substring(0, _sbcommonBaseInput.IndvidSB.TemplatesApplied.Length - 1);
                _sbcommonBaseInput.IndvidSB.TemplatesApplied += "@skeletal champion|";
            }
            else
            {
                _sbcommonBaseInput.IndvidSB.TemplatesApplied += "skeletal champion|";
            }
            raceLine = raceLine.Replace("skeletal champion", string.Empty);

            //check for other non-skeletal champion templates
            MonsterStatBlock tempSB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(raceLine);
            if (tempSB == null)
            {
                raceLine = ParseTemplatesApplied_Normal(raceLine);
            }
            return raceLine;
        }

        private string ParseTemplatesApplied_Skeleton(string raceLine, string raceLineHold)
        {
            if (raceLineHold.Contains(" skeleton warrior"))
            {
                _sbcommonBaseInput.IndvidSB.TemplatesApplied += "skeleton warrior|";
                raceLine = raceLine.Replace("skeleton warrior", string.Empty);
            }
            if (raceLineHold.Contains(" dread skeleton"))
            {
                _sbcommonBaseInput.IndvidSB.TemplatesApplied += "dread skeleton|";
                raceLine = raceLine.Replace("dread skeleton", string.Empty);
            }
            var templateOverrides = _individualStatBlockBusiness.GetTemplateRaceOverridesByTemplateName("skeleton");
            foreach (var one in templateOverrides)
            {
                if (raceLine.Contains(one.RaceName))
                {
                    return raceLine = ParseTemplatesApplied_Normal(raceLine);
                }
            }
            if (raceLine.Contains(" skeleton"))
            {
                raceLine = ParseTemplatesApplied_SkeletonBase(raceLine.ToLower());

                if (_sbcommonBaseInput.IndvidSB.TemplatesApplied.Length > 0)
                {
                    _sbcommonBaseInput.IndvidSB.TemplatesApplied = _sbcommonBaseInput.IndvidSB.TemplatesApplied.Substring(0, _sbcommonBaseInput.IndvidSB.TemplatesApplied.Length - 1);
                }

                raceLine = raceLine.Replace("skeleton", string.Empty);
                _sbcommonBaseInput.IndvidSB.TemplatesApplied += "@skeleton|";
            }

            //check for other non-skeleton templates
            MonsterStatBlock tempSB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(raceLine);
            if (tempSB == null)
            {
                raceLine = ParseTemplatesApplied_Normal(raceLine);
            }
            return raceLine;
        }

        private string ParseTemplatesApplied_SkeletonBase(string raceLine)
        {
            foreach (string template in CommonMethods.GetSkeletonTemplates())
            {
                if (raceLine.Contains(template.ToLower()) || raceLine.Contains(template))
                {
                    _sbcommonBaseInput.IndvidSB.TemplatesApplied += template.ToLower() + ",";
                    raceLine = raceLine.Replace(template.ToLower(), string.Empty)
                        .Replace(template, string.Empty);
                }
            }
            return raceLine;
        }

        private string ParseTemplatesApplied_Zombie(string raceLine)
        {
            foreach (string template in CommonMethods.GetZombieTemplates())
            {
                if (raceLine.Contains(template.ToLower()) || raceLine.Contains(template))
                {
                    _sbcommonBaseInput.IndvidSB.TemplatesApplied += template.ToLower() + ",";
                    raceLine = raceLine.ToLower().Replace(template.ToLower(), string.Empty);
                }
            }
            if (_sbcommonBaseInput.IndvidSB.TemplatesApplied.Length > 0)
            {
                _sbcommonBaseInput.IndvidSB.TemplatesApplied = _sbcommonBaseInput.IndvidSB.TemplatesApplied.Substring(0, _sbcommonBaseInput.IndvidSB.TemplatesApplied.Length - 1);
            }
            raceLine = raceLine.Replace("zombie", string.Empty)
                .Replace("Zombie", string.Empty);
            _sbcommonBaseInput.IndvidSB.TemplatesApplied += "@zombie|";
            return raceLine;
        }

        private string ParseTemplatesApplied_Normal(string raceLine)
        {
            foreach (string template in _individualStatBlockBusiness.GetTemplateNames())
            {
                var raceLineLower = raceLine.ToLower();
                var templateLower = template.ToLower();
                if (raceLineLower.Contains("apocalypse") && raceLineLower.Contains(" swarm") && templateLower == "apocalypse swarm")
                {
                    _sbcommonBaseInput.IndvidSB.TemplatesApplied += templateLower + "|";
                    raceLine = raceLineLower.Replace("apocalypse", string.Empty);
                }
                if (raceLineLower.Contains(" hivemind") && raceLineLower.Contains(" swarm") && templateLower == "hivemind swarm")
                {
                    _sbcommonBaseInput.IndvidSB.TemplatesApplied += templateLower + "|";
                    raceLine = raceLineLower.Replace(" hivemind", string.Empty);
                    raceLine = raceLineLower.Replace(" swarm", string.Empty);
                }

                if (template == "Negative Energy–Charged")
                {
                    raceLineLower += "";
                }

                if (template == "Girallon-Sired Orc" && raceLineLower.Contains("girallon-sired"))
                {
                    raceLineLower = raceLineLower.Replace("girallon-sired", "girallon-sired orc");
                }

                var templateOverrides = _individualStatBlockBusiness.GetAllTemplateRaceOverrides();

                bool shouldContine = false;
                if (raceLineLower.Contains(templateLower) || raceLine.Contains(template))
                {
                    bool hasNewRaceCheck = false;
                    foreach (var overrideItem in templateOverrides)
                    {
                        if (templateLower == overrideItem.TemplateName && raceLineLower.Contains(overrideItem.RaceName))
                        {
                            OverrideActionTypes overrideActionType = (OverrideActionTypes)Enum.Parse(typeof(OverrideActionTypes), overrideItem.OverrideActionTypeId.ToString());
                            switch (overrideActionType)
                            {
                                case OverrideActionTypes.Contine:
                                    shouldContine = true;
                                    break;
                                case OverrideActionTypes.NewRaceCheck:
                                    hasNewRaceCheck = true;
                                    break;
                            }
                        }
                    }

                    if (shouldContine) continue;

                    if (hasNewRaceCheck)
                    {
                        if (CheckForNewRace(raceLineLower) && !CheckForNewRace(raceLineLower.Replace(templateLower, string.Empty)))
                        {
                            _sbcommonBaseInput.IndvidSB.TemplatesApplied += templateLower + "|";
                            raceLine = raceLineLower.Replace(templateLower, string.Empty);
                        }
                    }
                    else
                    {
                        _sbcommonBaseInput.IndvidSB.TemplatesApplied += templateLower + "|";
                        raceLine = raceLineLower.Replace(templateLower, string.Empty);
                    }
                }
            }
            return raceLine;
        }

        private bool CheckForNewRace(string templateFreeRace)
        {
            MonsterStatBlock tempSB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(templateFreeRace.Trim());
            return (tempSB == null);
        }

        private bool ZombieTypeCheck(string raceLineHold)
        {
            raceLineHold = raceLineHold.ToLower();
            return raceLineHold.Contains(" zombie")
                && !raceLineHold.Contains(" zombie lord")
                && !raceLineHold.Contains(" juju zombie")
                && !raceLineHold.Contains(" dread zombie")
                && !raceLineHold.Contains(" spore zombie")
                && !raceLineHold.Contains("zombie horde")
                && !raceLineHold.Contains("brine zombie")
                && !raceLineHold.Contains("stone zombie")
                && !raceLineHold.Contains("spellgorged zombie")
                && !raceLineHold.Contains(" advanced zombie");
        }
    }
}
