using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using PathfinderGlobals;
using StatBlockCommon.Affliction_SB;

namespace StatBlockParsing
{
    public class AfflictionStatBlock_Parse
    {
        private string CR = Environment.NewLine;
        private AfflictionStatBlock _oneAffliction;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }

        public AfflictionStatBlock Parse(string Afflictionstr, ref string ErrorMessage)
        {
            _oneAffliction = new AfflictionStatBlock();

            Afflictionstr = Afflictionstr.Replace((char)(8217), Char.Parse("'"));
            Afflictionstr = Afflictionstr.Replace((char)(8212), Char.Parse("-"));
            Afflictionstr = Afflictionstr.Replace((char)(8211), Char.Parse("-"));
            Afflictionstr = Afflictionstr.Replace((char)(150), Char.Parse("-"));
            Afflictionstr = Afflictionstr.Replace((char)(151), Char.Parse("-"));
            Afflictionstr = Afflictionstr.Replace("á", "&#225;");
            Afflictionstr = Afflictionstr.Replace("“", ((char)34).ToString());
            Afflictionstr = Afflictionstr.Replace("TYPE", "Type");
            Afflictionstr = Afflictionstr.Replace("SAVE", "Save");
            Afflictionstr = Afflictionstr.Replace("SECONDARY EFFECT", "Secondary Efect");
            Afflictionstr = Afflictionstr.Replace("EFFECT", "Effect");
            Afflictionstr = Afflictionstr.Replace("CURE", "Cure");
            Afflictionstr = Afflictionstr.Replace("PRICE", "Price");

            Afflictionstr = Afflictionstr.Trim();
            int CRPos = Afflictionstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = Afflictionstr.IndexOf(CR);
            }

            //work your way back
            string temp = string.Empty;
            int Pos = 0;

            Pos = Afflictionstr.IndexOf("Description ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Description", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.description = temp;
            }

            Pos = Afflictionstr.IndexOf("Special ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Special", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.special = temp;
            }

            Pos = Afflictionstr.IndexOf("Spell Effect ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Spell Effect", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.spell_effect = temp;
            }

            Pos = Afflictionstr.IndexOf("Cost ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Cost", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.cost = temp;
            }

            Pos = Afflictionstr.IndexOf("Damage ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Damage", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.damage = temp;
            } 
            
            Pos = Afflictionstr.IndexOf("Cure ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("cure ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Pos = temp.IndexOf(";");
                if(Pos > 0)
                {
                   string temp2 = temp.Substring(Pos);
                   temp = temp.Replace(temp2, string.Empty);
                   Afflictionstr = Afflictionstr.Replace(temp2, string.Empty).Trim();
                   temp2 = temp2.Replace(";", string.Empty);
                   temp2 = temp2.Replace("Price", string.Empty).Trim();
                   _oneAffliction.cost = temp2;
                }
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("cure", string.Empty);
                temp = temp.Replace("Cure", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.cure = temp;
            }
            else
            {
                _oneAffliction.cure = "-";
            }

            Pos = Afflictionstr.IndexOf("Secondary Effect ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("Secondary Effect", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.secondary_effect = temp;
            }

            Pos = Afflictionstr.IndexOf("Initial Effect ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("Initial Effect", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.initial_effect = temp;
                _oneAffliction.effect = _oneAffliction.initial_effect + PathfinderConstants.SPACE + _oneAffliction.secondary_effect;
            }

            Pos = Afflictionstr.IndexOf("Effect ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("Effects ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("effect ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
               // temp = temp.Replace("effect", string.Empty);
                temp = temp.Replace("Effects", string.Empty);
                temp = temp.Replace("Effect", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.effect = temp;
            }

            Pos = Afflictionstr.IndexOf("Price ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Price", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.cost = temp;
            }

            Pos = Afflictionstr.IndexOf("Frequency ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("frequency ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("frequency", string.Empty);
                temp = temp.Replace("Frequency", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.frequency = temp;
            }
            else
            {
                _oneAffliction.frequency = "-";
            }

            Pos = Afflictionstr.IndexOf("Onset ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("onset ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("onset", string.Empty);
                temp = temp.Replace("Onset", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.onset = temp;
            }
            else
            {
                _oneAffliction.onset = "-";
            }

            Pos = Afflictionstr.IndexOf("Save ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("save ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("save", string.Empty);
                temp = temp.Replace("Save", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _oneAffliction.save = temp;
                Pos = temp.IndexOf("DC");
                temp = temp.Substring(Pos + 2).Trim();
                int tmp;
                int.TryParse(temp, out tmp);
                _oneAffliction.save_value = tmp;
            }

            Pos = Afflictionstr.IndexOf("Addiction ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Addiction", string.Empty).Trim();
                _oneAffliction.addiction = temp;
                _oneAffliction.drug = true;
            }

            Pos = Afflictionstr.IndexOf("Type ");
            if (Pos == -1) Pos = Afflictionstr.IndexOf("type ");

            if (Pos > 0)
            {
                temp = Afflictionstr.Substring(Pos);
                Afflictionstr = Afflictionstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("type", string.Empty);
                temp = temp.Replace("Type", string.Empty).Trim();
                //remove the unwanted CRs
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                if(temp.Contains("poison")) _oneAffliction.poison = true;
                if (temp.Contains("curse")) _oneAffliction.curse = true;
                if (temp.Contains("disease")) _oneAffliction.disease = true;
                _oneAffliction.type = temp;
            }

            _oneAffliction.name = Afflictionstr.ProperCase().Trim();

            StatBlockFormating.AfflictionStatBlock_Format AfflictionSB_Form = new StatBlockFormating.AfflictionStatBlock_Format();
            AfflictionSB_Form.ItalicPhrases = ItalicPhrases;
            AfflictionSB_Form.BoldPhrases = BoldPhrases;
            _oneAffliction.fulltext = AfflictionSB_Form.CreateFullText(_oneAffliction);
            _oneAffliction.fulltext = _oneAffliction.fulltext.Trim();

            return _oneAffliction;

        }
    }
}
