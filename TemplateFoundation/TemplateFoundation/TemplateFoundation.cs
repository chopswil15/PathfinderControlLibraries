using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;

namespace TemplateFoundational
{
    public class TemplateFoundation
    {        
        private string _Name;
        private FoundationType _FoundationType;
        public enum FoundationType
        {
            None = 0,
            Acquired = 1,
            Inherited = 2,
            Simple = 3
        }

        public FoundationType TemplateFoundationType
        {
            get { return _FoundationType; }
            protected set { _FoundationType = value; }
        }

        public string Name
        {
            get { return _Name; }
            protected set { _Name = value; }
        }       

        public virtual MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            //handled by individual template
            return null;
        }       

        public virtual MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, List<string> StackableTemplates)
        {
            //handled by individual template
            return null;
        }

        public virtual MonsterStatBlock ApplyAdvancedHDTemplate(MonsterStatBlock MonSB, string NewSize, int HighCR, int LowCR)
        {
            //handled by individual template
            return null;
        }
    }
}
