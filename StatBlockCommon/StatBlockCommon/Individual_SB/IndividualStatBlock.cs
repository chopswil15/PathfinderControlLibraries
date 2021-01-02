using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using StatBlockCommon.Monster_SB;


namespace StatBlockCommon.Individual_SB
{
    [Serializable]
    [XmlRootAttribute(ElementName = "Individual", IsNullable = false)]
    public class IndividualStatBlock : MonsterStatBlock
    {
        public IndividualStatBlock()
        {
            Gear = string.Empty;
            OtherGear = string.Empty;
            BeforeCombat = string.Empty;
            DuringCombat = string.Empty;
            Morale = string.Empty;
            Bloodline = string.Empty;
            Gender = string.Empty;
            ProhibitedSchools = string.Empty;
            ThassilonianSpecialization = string.Empty;
            BaseStatistics = string.Empty;
            AgeCategory = "Adult";
            FocusedSchool = string.Empty;
            AdditionalExtractsKnown = string.Empty;
        }     
    }
}
