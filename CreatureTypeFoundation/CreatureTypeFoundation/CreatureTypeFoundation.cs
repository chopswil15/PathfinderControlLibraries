using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;

namespace CreatureTypeFoundational
{
    public class CreatureTypeFoundation
    {
        private string _Name;
        private int _SkillRanksPerLevel;
        private bool _HasFeats;
        private StatBlockInfo.HitDiceCategories _HitDiceType;
        private StatBlockInfo.SaveBonusType _FortSaveType;
        private StatBlockInfo.SaveBonusType _RefSaveType;
        private StatBlockInfo.SaveBonusType _WillSaveType;

        public CreatureTypeFoundation()
        {
            _HasFeats = true;
        }

        public StatBlockInfo.SaveBonusType FortSaveType
        {
            get { return _FortSaveType; }
            protected set { _FortSaveType = value; }
        }

        public StatBlockInfo.SaveBonusType RefSaveType
        {
            get { return _RefSaveType; }
            protected set { _RefSaveType = value; }
        }

        public StatBlockInfo.SaveBonusType WillSaveType
        {
            get { return _WillSaveType; }
            protected set { _WillSaveType = value; }
        }

        public StatBlockInfo.HitDiceCategories HitDiceType
        {
            get { return _HitDiceType; }
            protected set { _HitDiceType = value; }
        }
        
        public int SkillRanksPerLevel
        {
            get { return _SkillRanksPerLevel; }
            protected set { _SkillRanksPerLevel = value; }
        }

        public bool HasFeats
        {
            get { return _HasFeats; }
            protected set { _HasFeats = value; }
        }

        public string Name
        {
            get { return _Name; }
            protected set { _Name = value; }
        }

        public virtual string FortMod()
        {
            return string.Empty;
        }

        public virtual List<string> ClassSkills()
        {
            return new List<string>();
        }
    }
}
