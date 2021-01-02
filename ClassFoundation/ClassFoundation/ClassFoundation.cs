using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using ClericDomains;
using CommonStatBlockInfo;
using OracleMysteries;
using Bloodlines;
using Patrons;

namespace ClassFoundational
{
    public class ClassFoundation
    {
        public List<string> ClassAlignments { get; set; }        
        public OnGoingSpecialAbility ClassFeature { get; set; }
        public List<OnGoingSpecialAbility> Features { get; set; }
        public List<IDomain> Domains { get; set; } //limit 2
        public List<IMystery> OracleMysteries { get; set; }
        public IBloodline Bloodline { get; set; }
        public IPatron Patron { get; set; }
        public List<string> ClassArchetypes { get; set; }
        public StatBlockInfo.WeaponProficiencies WeaponProficiencies { get; set; }
        public StatBlockInfo.ArmorProficiencies ArmorProficiencies { get; set; }
        public StatBlockInfo.ShieldProficiencies ShieldProficiencies { get; set; }
        public StatBlockInfo.BABType BABType { get; set; }
        public StatBlockInfo.SaveBonusType FortSaveType { get; set; }
        public StatBlockInfo.SaveBonusType RefSaveType { get; set; }
        public StatBlockInfo.SaveBonusType WillSaveType { get; set;}
        public StatBlockInfo.HitDiceCategories HitDiceType { get; set;}
        public int SkillRanksPerLevel { get; set;}
        public string Name { get; set; }
        public List<OnGoingPower> ClassPowers { get; set; }
        public bool CanCastSpells { get; set; }
        public bool IsNPC_Class { get; set; }
        public bool IsPrestigeClass { get; set; }
        public bool DomainSpellUse { get; set; }
        public bool MysteryUse { get; set; }
        public bool HasSpellsPerDay { get; set; }
        public string ClassDiety { get; set; }
        public bool BloodlineUse { get; set; }
        public bool PatronUse { get; set; }
        public bool Mythic { get; set; }
        public int PrestigeBABMin { get; set; }
        public enum OverloadPrestigeType
        {
            None = 0,
            Full,
            MinusOne,
            BlocksOfThree
        }



        public ClassFoundation()
        {
            Features = new List<OnGoingSpecialAbility>();
            ClassPowers = new List<OnGoingPower>() { };
            ClassArchetypes = new List<string>();
            WeaponProficiencies = StatBlockInfo.WeaponProficiencies.None;
            ArmorProficiencies = StatBlockInfo.ArmorProficiencies.None;
            ShieldProficiencies = StatBlockInfo.ShieldProficiencies.None;
            PrestigeBABMin = -1;
        }       

        public virtual void ProcessDomains(string Domains)
        {
            //DomainSpellUse = true override in class
            //otherwise not impletmented
        }

        public virtual void ProcessMysteries(string Mysteries)
        {
            //MysteryUse = true override in class
            //otherwise not impletmented
        }

        public virtual void ProcessBloodline(string Bloodline)
        {
            //BloodlineUse = true override in class
            //otherwise not impletmented
        }

        public virtual void ProcessPatron(string Patron)
        {
            //PatronUse = true override in class
            //otherwise not impletmented
        }

        public virtual List<string> GetWeaponProficienciesExtra()
        {
            return new List<string>();
        }

        public virtual List<string> GetShieldProficienciesExtra()
        {
            return new List<string>();
        }

        public virtual void ProcessPowers(int ClassLevel)
        {
            //_ClassPowers = new List<OnGoingPower>() {};
        }

        public virtual List<int> GetSpellsPerLevel(int ClassLevel)
        {
            if (!CanCastSpells)
            {
                return new List<int>();
            }
            return null;
        }

        public virtual List<int> GetSpellsPerDay(int ClassLevel)
        {
            if (!HasSpellsPerDay)
            {
                return new List<int>();
            }
            return null;
        }

        public virtual string AlternateName()
        {
            return string.Empty;
        }

        public virtual int GetDomainSpellsPerLevel(int ClassLevel)
        {
            if (!DomainSpellUse)
            {
                return 0;
            }
            return 0;
        }

        public virtual int GetMysteriesPerLevel(int ClassLevel)
        {
            if (!MysteryUse)
            {
                return 0;
            }
            return 0;
        }

        public virtual int GetBloodlineSpellsPerLevel(int ClassLevel)
        {
            if (!CanCastSpells)
            {
                return 0;
            }
            return 0;
        }      

        public virtual string GetSpellBonusAbility()
        {
            if (!CanCastSpells)
            {
                return string.Empty;
            }
            return string.Empty;
        }

        public List<OnGoingSpecialAbility> GetConstantFeatures()
        {
            List<OnGoingSpecialAbility> temp = new List<OnGoingSpecialAbility>();
            foreach (OnGoingSpecialAbility F in Features)
            {
                if (F.Activity == OnGoingSpecialAbility.SpecialAbilityActivities.Constant)
                {
                    temp.Add(F);
                }
            }
            return temp.OrderBy(x => x.StartLevel).ToList();
        }

        public virtual int ClassFeatCount(int ClassLevel, string Archetype)
        {            
            return 0;
        }

        public int BasicClassFeatCount(int TotalClassLevels)
        {
            //one new feat on odd levels
            if (TotalClassLevels % 2 == 1)
            {
                return (TotalClassLevels / 2) + 1;
            }
            else
            {
                return TotalClassLevels / 2;
            }
        }

        public virtual List<string> ClassSkills()
        {
            return new List<string>();
        }

        public virtual List<string> ClassArchetypeSkills(string Archetype)
        {
            return new List<string>();
        }

        public virtual List<string> PrestigePreReqFeats()
        {
            return new List<string>();
        }

        public virtual List<PreReqSkill> PrestigePreReqSkills()
        {
            return new List<PreReqSkill>();
        }

        public List<string> GetPatronSpells(int ClassLevel)
        {
            if (PatronUse)
            {
                return Patron.BonusSpells(ClassLevel);
            }

            return new List<string>();
        }


        public virtual void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {

        }

        public virtual List<CheckClassError> CheckClass(int classLevel)
        {
            throw new NotImplementedException();
        }

        protected int GetOverloadPrestige(int classLevel, OverloadPrestigeType overloadPrestigeType)
        {
            int overloadLevel = 0;
            switch (overloadPrestigeType)
            {
                case OverloadPrestigeType.Full:
                    overloadLevel = classLevel;
                    break;
                case OverloadPrestigeType.BlocksOfThree:
                    if (classLevel >= 2) overloadLevel++;
                    if (classLevel >= 3) overloadLevel++;
                    if (classLevel >= 4) overloadLevel++;
                    if (classLevel >= 6) overloadLevel++;
                    if (classLevel >= 7) overloadLevel++;
                    if (classLevel >= 8) overloadLevel++;
                    if (classLevel == 10) overloadLevel++;
                    break;
                case OverloadPrestigeType.MinusOne:
                    overloadLevel = classLevel - 1;
                    break;
            }
            return overloadLevel;
        }
    }
}
