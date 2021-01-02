using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace Skills
{
    public class SkillData
    {
        //static SkillData instance = null;
        public static List<Skill> Skills;

        public enum SkillNames
        {
            Unknown = 0,
            Acrobatics = 1,
            Appraise,
            Bluff,
            Climb,
            Craft,
            Diplomacy,
            DisableDevice,
            Disguise,
            EscapeArtist,
            Fly,
            HandleAnimal,
            Heal,
            Intimidate,
            KnowledgeArcana,
            KnowledgeDungeoneering,
            KnowledgeEngineering,
            KnowledgeGeography,
            KnowledgeHistory,
            KnowledgeLocal,
            KnowledgeNature,
            KnowledgeNobility,
            KnowledgePlanes,
            KnowledgeReligion,
            Linguistics,
            Perception,
            Perform,
            Profession,
            Ride,
            SenseMotive,
            SleightofHand,
            Spellcraft,
            Stealth,
            Survival,
            Swim,
            UseMagicDevice,
            KnowledgeAnyOne
        }

        static SkillData()
        {
            LoadSkills();
        }

        //public static SkillData Instance
        //{
        //    get
        //    {
        //        if (instance == null) instance = new SkillData();
        //        return instance;
        //    }
        //}

        public static Skill GetSkill(string SkillName)
        {
            foreach (Skill skill in Skills)
            {
                if (skill.Name == SkillName) return skill;
            }
            return null;
        }

        private static void LoadSkills()
        {
            Skills = new List<Skill>();
            Skill skill = new Skill();

            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_ONE;
            skill.SkillName = SkillNames.KnowledgeAnyOne;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.ACROBATICS;
            skill.SkillName = SkillNames.Acrobatics;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.APPRAISE;
            skill.SkillName = SkillNames.Appraise;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.BLUFF;
            skill.SkillName = SkillNames.Bluff;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.CLIMB;
            skill.SkillName = SkillNames.Climb;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Strength;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.CRAFT;
            skill.SkillName = SkillNames.Craft;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.DIPLOMACY;
            skill.SkillName = SkillNames.Diplomacy;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.DISABLE_DEVICE;
            skill.SkillName = SkillNames.DisableDevice;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.DISGUISE;
            skill.SkillName = SkillNames.Disguise;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.ESCAPE_ARTIST;
            skill.SkillName = SkillNames.EscapeArtist;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.FLY;
            skill.SkillName = SkillNames.Fly;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.HANDLE_ANIMAL;
            skill.SkillName = SkillNames.HandleAnimal;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.HEAL;
            skill.SkillName = SkillNames.Heal;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Wisdom;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.INTIMIDATE;
            skill.SkillName = SkillNames.Intimidate;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA;
            skill.SkillName = SkillNames.KnowledgeArcana;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING;
            skill.SkillName = SkillNames.KnowledgeDungeoneering;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING;
            skill.SkillName = SkillNames.KnowledgeEngineering;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY;
            skill.SkillName = SkillNames.KnowledgeGeography;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY;
            skill.SkillName = SkillNames.KnowledgeHistory;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL;
            skill.IsKnowledge = true;
            skill.SkillName = SkillNames.KnowledgeLocal;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_NATURE;
            skill.SkillName = SkillNames.KnowledgeNature;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY;
            skill.SkillName = SkillNames.KnowledgeNobility;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_PLANES;
            skill.SkillName = SkillNames.KnowledgePlanes;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION;
            skill.SkillName = SkillNames.KnowledgeReligion;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = true;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.LINGUISTICS;
            skill.SkillName = SkillNames.Linguistics;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.PERCEPTION;
            skill.SkillName = SkillNames.Perception;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Wisdom;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.PERFORM;
            skill.SkillName = SkillNames.Perform;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.PROFESSION;
            skill.SkillName = SkillNames.Profession;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Wisdom;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.RIDE;
            skill.SkillName = SkillNames.Ride;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.SENSE_MOTIVE;
            skill.SkillName = SkillNames.SenseMotive;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Wisdom;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.SLEIGHT_OF_HAND;
            skill.SkillName = SkillNames.SleightofHand;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.SPELLCRAFT;
            skill.SkillName = SkillNames.Spellcraft;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Intelligence;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.STEALTH;
            skill.SkillName = SkillNames.Stealth;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Dexterity;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.SURVIVAL;
            skill.SkillName = SkillNames.Survival;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Wisdom;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.SWIM;
            skill.SkillName = SkillNames.Swim;
            skill.Untrained = true;
            skill.ArmorCheckPenalty = true;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Strength;
            Skills.Add(skill);

            skill = new Skill();
            skill.Name = StatBlockInfo.SkillNames.USE_MAGIC_DEVICE;
            skill.SkillName = SkillNames.UseMagicDevice;
            skill.Untrained = false;
            skill.ArmorCheckPenalty = false;
            skill.IsKnowledge = false;
            skill.Ability = StatBlockInfo.AbilityName.Charisma;
            Skills.Add(skill);            
        }
    }
}
