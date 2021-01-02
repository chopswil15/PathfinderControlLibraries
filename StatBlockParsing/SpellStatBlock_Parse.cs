using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using PathfinderGlobals;
using StatBlockCommon.Spell_SB;
using Utilities;

namespace StatBlockParsing
{
    public class SpellStatBlock_Parse
    {
        private SpellStatBlock OneSpell;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }

        public SpellStatBlock Parse(string SpellText, ref string ErrorMessage)
        {
            OneSpell = new SpellStatBlock();

            SpellText = SpellText.Trim();
            SpellText = SpellText.Replace((char)(8217), char.Parse("'"));
            // SpellText = SpellText.Replace((char)(8224), Char.Parse("†"));
            SpellText = SpellText.Replace("†", "&#8224;");
            SpellText = SpellText.Replace((char)(8212), char.Parse("-"));
            SpellText = SpellText.Replace((char)(8211), char.Parse("-"));
            SpellText = SpellText.Replace((char)(150), char.Parse("-"));
            SpellText = SpellText.Replace((char)(151), char.Parse("-"));
            SpellText = SpellText.Replace("“", ((char)34).ToString());
            SpellText = SpellText.Replace("”", ((char)34).ToString());
            SpellText = SpellText.Replace(char.Parse("-"), char.Parse("-"));
            SpellText = SpellText.Replace(char.Parse("×"), char.Parse("x"));
            SpellText = SpellText.Replace("sorcerer/ wizard", "sorcerer/wizard");
            SpellText = SpellText.Replace("Spell R esistance", "Spell Resistance");
            SpellText = SpellText.Replace("fl ", "fl").Replace("fi ","fi");


            string Spellstr = SpellText;
            string CR = Environment.NewLine;//((char) 13).ToString();
            Spellstr = Spellstr.Replace(CR + CR, CR);
            int CRPos = Spellstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = Spellstr.IndexOf(CR);
            }


            string temp2 = Spellstr.Substring(0, CRPos);
            Spellstr = Spellstr.Replace(temp2 + CR, string.Empty).Trim();
            if (temp2.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
            {
                int ParenPos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                string temp3 = temp2.Substring(ParenPos);
                temp2 = temp2.Replace(temp3, string.Empty);
                temp3 = temp3.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                temp3 = temp3.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                OneSpell.deity = temp3.Trim();
            }
            OneSpell.name = temp2.Trim().ProperCase();

            //fix roman numerials
            List<string> numerialsBad = new List<string> { "Ii", "Iii", "Iv", "Vi", "Vii", "Vii", "Ix" };
            int index = 0;
            foreach (string bad in numerialsBad)
            {
                if (OneSpell.name.EndsWith(bad))
                {
                    OneSpell.name = OneSpell.name.Substring(0, OneSpell.name.Length - bad.Length);
                    OneSpell.name = OneSpell.name + bad.ToUpper();
                    break;
                }
                index++;
            }


            int Pos;
            string temp;
            try
            {
                Spellstr = Spellstr.Replace(";" + CR + "Level", "; Level");
                Pos = Spellstr.IndexOf("; Level");
                temp = Spellstr.Substring(0, Pos);
                Spellstr = Spellstr.Replace(temp + ";", string.Empty);
                temp = temp.Replace("School", string.Empty).Trim();
                Pos = temp.IndexOf("[");
                if (Pos > 0)
                {
                    temp2 = temp.Substring(Pos);
                    temp = temp.Replace(temp2, string.Empty).Trim();
                    temp2 = temp2.Replace("[", string.Empty);
                    temp2 = temp2.Replace("]", string.Empty);
                    OneSpell.descriptor = temp2.Trim();
                    List<string> descriptors = CommonMethods.GetSpellDescriptors();
                    descriptors.Add("see text");
                    string descriptorString = string.Join(",", descriptors.ToArray());
                    if (OneSpell.descriptor.Contains(","))
                    {
                        descriptors = OneSpell.descriptor.Split(',').ToList();
                    }
                    else if (OneSpell.descriptor.Contains("or "))
                    {
                        descriptors = OneSpell.descriptor.Split(new string[] { "or " }, StringSplitOptions.None).ToList();
                    }


                    foreach (string desc in descriptors)
                    {
                        string temp3 = desc.Trim();
                        temp3 = temp3.Replace("or ", string.Empty);
                        temp3 = temp3.Replace("; see below", string.Empty);

                        temp3 = Utility.RemoveSuperScripts(temp3);

                        if (!descriptorString.Contains(temp3))
                        {
                            ErrorMessage += "Descriptor not allowed value: " + temp3;
                            return OneSpell;
                        }
                    }
                }
                else
                {
                    OneSpell.descriptor = string.Empty;
                }

                if (OneSpell.descriptor.Length > 0)
                {
                    if (OneSpell.descriptor.Contains("acid")) OneSpell.acid = true;
                    if (OneSpell.descriptor.Contains("air")) OneSpell.air = true;
                    if (OneSpell.descriptor.Contains("chaotic")) OneSpell.chaotic = true;
                    if (OneSpell.descriptor.Contains("cold")) OneSpell.cold = true;
                    if (OneSpell.descriptor.Contains("curse")) OneSpell.curse = true;
                    if (OneSpell.descriptor.Contains("darkness")) OneSpell.darkness = true;
                    if (OneSpell.descriptor.Contains("death")) OneSpell.death = true;
                    if (OneSpell.descriptor.Contains("disease")) OneSpell.disease = true;
                    if (OneSpell.descriptor.Contains("earth")) OneSpell.earth = true;
                    if (OneSpell.descriptor.Contains("electricity")) OneSpell.electricity = true;
                    if (OneSpell.descriptor.Contains("emotion")) OneSpell.emotion = true;
                    if (OneSpell.descriptor.Contains("evil")) OneSpell.evil = true;
                    if (OneSpell.descriptor.Contains("fear")) OneSpell.fear = true;
                    if (OneSpell.descriptor.Contains("fire")) OneSpell.fire = true;
                    if (OneSpell.descriptor.Contains("force")) OneSpell.force = true;
                    if (OneSpell.descriptor.Contains("good")) OneSpell.good = true;
                    if (OneSpell.descriptor.Contains("language-dependent")) OneSpell.language_dependent = true;
                    if (OneSpell.descriptor.Contains("lawful")) OneSpell.lawful = true;
                    if (OneSpell.descriptor.Contains("light")) OneSpell.light = true;
                    if (OneSpell.descriptor.Contains("mind-affecting")) OneSpell.mind_affecting = true;
                    if (OneSpell.descriptor.Contains("pain")) OneSpell.pain = true;
                    if (OneSpell.descriptor.Contains("poison")) OneSpell.poison = true;
                    if (OneSpell.descriptor.Contains("shadow")) OneSpell.shadow = true;
                    if (OneSpell.descriptor.Contains("sonic")) OneSpell.sonic = true;
                    if (OneSpell.descriptor.Contains("water")) OneSpell.water = true;
                    if (OneSpell.descriptor.Contains("ruse")) OneSpell.ruse = true;
                    if (OneSpell.descriptor.Contains("draconic")) OneSpell.draconic = true;
                    if (OneSpell.descriptor.Contains("meditative")) OneSpell.meditative = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Issue with Level line";
                return OneSpell;
            }


            Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos > 0)
            {
                temp2 = temp.Substring(Pos);
                temp = temp.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                temp2 = temp2.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                OneSpell.subschool = temp2.Trim();
                List<string> subSchools = CommonMethods.GetSpellSubSchools();
                //  string subSchoolsString = string.Join(",", subSchools.ToArray());
                List<string> spellSubSchools = OneSpell.subschool.Split(',').ToList();

                foreach (string subS in spellSubSchools)
                {
                    if (!subSchools.Contains(subS.Trim()))
                    {
                        ErrorMessage = "SubSchool not allowed value: " + subS;
                    }
                }
            }
            else
            {
                OneSpell.subschool = string.Empty;
            }

            List<string> schools = CommonMethods.GetSpellSchools();
            schools.Add("see text");
            string schoolsString = string.Join(",", schools.ToArray()).ToLower();
            OneSpell.school = temp;
            if (!schoolsString.Contains(OneSpell.school))
            {
                ErrorMessage = "School not allowed value: " + OneSpell.school;
            }

            CRPos = Spellstr.IndexOf(CR);
            temp = Spellstr.Substring(0, CRPos);
            if (temp.EndsWith(","))
            {
                Spellstr = Spellstr.ReplaceFirst(CR, PathfinderConstants.SPACE);
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
            }
            if (temp.EndsWith("/"))
            {
                Spellstr = Spellstr.ReplaceFirst(CR, string.Empty);
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
            }
            if (temp.EndsWith("Level"))
            {
                Spellstr = Spellstr.ReplaceFirst(CR, PathfinderConstants.SPACE);
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
            }

            Spellstr = Spellstr.Replace(temp + CR, string.Empty);
            temp = temp.Replace("Level:", string.Empty);
            temp = temp.Replace("Level", string.Empty);
            temp = temp.Replace(CR, string.Empty).Trim();
            if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
            {
                temp2 = temp.Substring(temp.IndexOf(PathfinderConstants.PAREN_LEFT));
                temp = temp.Replace(temp2, string.Empty).Trim();
                temp2 = Utility.RemoveParentheses(temp2);
                OneSpell.deity = temp2;
            }
            if (temp.Contains("cleric ") && !temp.Contains("cleric/oracle ")) temp = temp.Replace("cleric", "cleric/oracle");
            if (temp.Contains("wizard/sorcerer")) temp = temp.Replace("wizard/sorcerer", "sorcerer/wizard");
            OneSpell.spell_level = temp;
            try
            {
                ParseSpellLevels();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Issue with SpellLevels- " + ex.Message;
                return OneSpell;
            }

            Spellstr = Spellstr.Replace("CASTING" + CR, string.Empty);
            Spellstr = Spellstr.Replace("casting" + CR, string.Empty);

            try
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Casting Time:", string.Empty);
                temp = temp.Replace("Casting Time", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.casting_time = temp;
            }
            catch
            {
                ErrorMessage = "Issue with Casting Time";
                return OneSpell;
            }

            CRPos = Spellstr.IndexOf(CR);
            temp = Spellstr.Substring(0, CRPos);
            Spellstr = Spellstr.Replace(temp + CR, string.Empty);
            temp = temp.Replace("Component:", string.Empty);
            temp = temp.Replace("Components:", string.Empty);
            temp = temp.Replace("Components", string.Empty);
            temp = temp.Replace("Component", string.Empty);
            temp = temp.Replace(CR, string.Empty).Trim();
            bool ComponentsFound = false;
            if (temp.Contains(" gp"))
            {
                OneSpell.costly_components = true;
                Pos = temp.IndexOf(" gp");
                string costly = temp.Substring(0, Pos).Trim(); ;
                Pos = costly.LastIndexOf(PathfinderConstants.SPACE);
                costly = costly.Substring(Pos);
                costly = costly.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                costly = costly.Replace(",", string.Empty);
                OneSpell.material_costs = int.Parse(costly);
            }
            OneSpell.components = temp;
            if (temp.Contains("V"))
            {
                OneSpell.verbal = true;
                ComponentsFound = true;
            }
            if (temp.Contains("S"))
            {
                OneSpell.somatic = true;
                ComponentsFound = true;
            }
            if (temp.Contains("M"))
            {
                OneSpell.material = true;
                ComponentsFound = true;
            }
            if (temp.Contains("DF"))
            {
                OneSpell.divine_focus = true;
                ComponentsFound = true;
            }
            if (temp.Contains("F") && OneSpell.divine_focus == false)
            {
                OneSpell.verbal = true;
                ComponentsFound = true;
            }

            if (!ComponentsFound)
            {
                ErrorMessage = "Issue with Components";
                return OneSpell;
            }

            Pos = Spellstr.IndexOf("Range");
            if (Pos >= 0)
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Range", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.range = temp;
            }


            Pos = Spellstr.IndexOf("Target, Effect, Area");
            if (Pos >= 0)
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Target, Effect, Area", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.area = temp;
                OneSpell.targets = temp;
                OneSpell.effect = temp;
            }

            Pos = Spellstr.IndexOf("Target, Effect, or Area");
            if (Pos >= 0)
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Target, Effect, or Area", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.area = temp;
                OneSpell.targets = temp;
                OneSpell.effect = temp;
            }


            Pos = Spellstr.IndexOf("Target or Area");
            if (Pos >= 0)
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Target or Area", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.area = temp;
                OneSpell.targets = temp;
            }

            Pos = Spellstr.IndexOf("Area or Target");
            if (Pos >= 0)
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Area or Target", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.area = temp;
                OneSpell.targets = temp;
            }

            Pos = Spellstr.IndexOf("Area");
            int Pos2 = Spellstr.IndexOf("Duration");

            if (Pos >= 0 && Pos < Pos2)
            {
                CRPos = Spellstr.IndexOf(CR + "Duration");

                Pos = Spellstr.IndexOf(CR + "Targets");
                if (Pos == -1)
                {
                    Pos = Spellstr.IndexOf(CR + "Target");
                }
                if (Pos < CRPos && Pos != -1)
                {
                    CRPos = Pos;
                }
                temp = Spellstr.Substring(0, CRPos);
                if (temp.Contains("Effect"))
                {
                    Pos = temp.IndexOf("Effect");
                    temp = temp.Substring(0, Pos);
                }
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                Spellstr = Spellstr.Replace(temp, string.Empty);
                temp = temp.Replace("Area", string.Empty);
                temp = temp.Replace(CR, PathfinderConstants.SPACE).Trim();
                OneSpell.shapeable = temp.Contains("(S)");
                OneSpell.area = temp;
            }



            if ((Spellstr.Contains("Target ") || Spellstr.Contains("Targets ")))
            {
                Pos = Spellstr.IndexOf("Saving Throw");
                Pos2 = Spellstr.IndexOf("Target");
                if ((Pos > 0 && Pos2 < Pos) || Pos == -1)
                {
                    CRPos = Spellstr.IndexOf(CR + "Duration");
                    if (CRPos == -1)
                    {
                        CRPos = Spellstr.IndexOf("Duration");
                    }
                    Pos = Spellstr.IndexOf(CR + "Effect");
                    if (Pos < CRPos && Pos > 0)
                    {
                        CRPos = Pos;
                    }
                    temp = Spellstr.Substring(0, CRPos);
                    Spellstr = Spellstr.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace("Targets", string.Empty);
                    temp = temp.Replace("Target", string.Empty);
                    //temp = temp.Replace(CR, PathfinderConstants.SPACE);
                    OneSpell.targets = temp.Trim();
                }
            }

            if (Spellstr.Contains("Effect") && !Spellstr.Contains(". Effect") && (Spellstr.IndexOf("Effect") < Spellstr.IndexOf("Saving Throw") || !Spellstr.Contains("Saving Throw")))
            {
                CRPos = Spellstr.IndexOf(CR + "Duration");
                if (CRPos == -1)
                {
                    CRPos = Spellstr.IndexOf(CR);
                }
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Effect", string.Empty);
                temp = temp.Replace(CR, PathfinderConstants.SPACE).Trim();
                OneSpell.effect = temp;
            }

            if (Spellstr.Contains("Duration"))
            {
                CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Duration", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                if (temp.Contains("(D)"))
                {
                    OneSpell.dismissible = true;
                    // temp = temp.Replace("(D)", string.Empty).Trim();
                }
                else
                {
                    OneSpell.dismissible = false;
                }
                OneSpell.duration = temp;
            }


            Spellstr = Spellstr.Replace("EFFECT" + CR, string.Empty);
            Spellstr = Spellstr.Replace("effect" + CR, string.Empty);
            Spellstr = Spellstr.Replace("Saving throw", "Saving Throw");

            if (Spellstr.Contains("Saving Throw") || Spellstr.Contains("Save "))
            {
                Spellstr = Spellstr.Replace(";" + CR + "Spell Resistance", "; Spell Resistance");
                Spellstr = Spellstr.Replace("; Spell" + CR + "Resistance", "; Spell Resistance");

                Pos2 = Spellstr.IndexOf("Saving Throw");
                if (Pos2 == -1) Pos2 = Spellstr.IndexOf("Save ");
                Pos = Spellstr.LastIndexOf("; Spell Resistance");
                if (Pos >= 0)
                {
                    temp = Spellstr.Substring(Pos2, Pos - Pos2);
                    Spellstr = Spellstr.Replace(temp + ";", string.Empty);
                    temp = temp.Replace("Saving Throw:", string.Empty);
                    temp = temp.Replace("Saving Throw", string.Empty).Trim();
                    temp = temp.Replace("Save ", string.Empty);
                    OneSpell.saving_throw = temp;
                }
                else
                {
                    OneSpell.saving_throw = "Error- missing semicolon";
                }
            }

            if (Spellstr.Contains("Augmented "))
            {
                CRPos = Spellstr.IndexOf("Augmented ");
                temp = Spellstr.Substring(CRPos);
                Spellstr = Spellstr.Replace(temp, string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.augmented = temp;
                OneSpell.mythic = true;
            }
            else if (Spellstr.Contains("Augmented:"))
            {
                CRPos = Spellstr.IndexOf("Augmented:");
                temp = Spellstr.Substring(CRPos);
                Spellstr = Spellstr.Replace(temp, string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.augmented = temp;
                OneSpell.mythic = true;
            }

            if (Spellstr.Contains("Mythic:"))
            {
                CRPos = Spellstr.IndexOf("Mythic:");
                temp = Spellstr.Substring(CRPos);
                Spellstr = Spellstr.Replace(temp, string.Empty);
                temp = temp.Replace("Mythic:", string.Empty);
                temp = temp.Replace(CR, string.Empty).Trim();
                OneSpell.mythic_text = temp;
                OneSpell.mythic = true;
            }

            Spellstr = Spellstr.Replace("DEScription", "DESCRIPTION");
            Spellstr = Spellstr.Replace("description" + CR, "DESCRIPTION" + CR);

            CRPos = Spellstr.IndexOf(CR + "DESCRIPTION");
            if (CRPos == -1)  CRPos = Spellstr.IndexOf(CR + CR);

            Spellstr = Spellstr.Replace("Spell Resistance" + CR, "Spell Resistance ");
            Spellstr = Spellstr.Replace("Spell Resistance yes" + CR + PathfinderConstants.PAREN_LEFT, "Spell Resistance yes (");
            Spellstr = Spellstr.Replace("Spell Resistance no" + CR + PathfinderConstants.PAREN_LEFT, "Spell Resistance no (");
            if (Spellstr.Contains("Spell Resistance"))
            {
                if (CRPos == -1) CRPos = Spellstr.IndexOf(CR);
                temp = Spellstr.Substring(0, CRPos);
                Spellstr = Spellstr.Replace(temp + CR, string.Empty);
                temp = temp.Replace("Spell Resistance:", string.Empty);
                temp = temp.Replace("Spell Resistance", string.Empty);
                temp = temp.Replace(CR, PathfinderConstants.SPACE).Trim();
                OneSpell.spell_resistence = temp;
            }


            if (Spellstr.Contains("HAUNT STATISTICS"))
            {
                Pos = Spellstr.IndexOf("HAUNT STATISTICS");
                temp = Spellstr.Substring(Pos);
                Spellstr = Spellstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                OneSpell.haunt_statistics = temp.Replace("HAUNT STATISTICS", string.Empty).Trim();
            }

            Spellstr = Spellstr.Replace("DESCRIPTION" + CR, string.Empty);

            //mark the keeper CRs
            Spellstr = Spellstr.Replace("." + CR, ".<br>");

            //remove the unwanted CRs
            Spellstr = Spellstr.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            Spellstr = Spellstr.Replace(PathfinderConstants.BREAK, Environment.NewLine);

            OneSpell.description = Spellstr.Trim();
            if (OneSpell.description.Contains("mythic")) OneSpell.mythic = true;

            StatBlockFormating.SpellStatBlock_Format SpellSB_Form = new StatBlockFormating.SpellStatBlock_Format();
            SpellSB_Form.ItalicPhrases = ItalicPhrases;
            SpellSB_Form.BoldPhrases = BoldPhrases;
            OneSpell.full_text = SpellSB_Form.CreateFullText(OneSpell);
            OneSpell.full_text = OneSpell.full_text.Trim();

            return OneSpell;
        }

        private void ParseSpellLevels()
        {
            string temp;
            string spellLeveltemp = OneSpell.spell_level;
            
            spellLeveltemp = Utility.RemoveSuperScripts(spellLeveltemp);
            
            List<string> Levels = spellLeveltemp.Split(',').ToList();
            List<int> SpellLevels = new List<int>();
            foreach (string level in Levels)
            {
                temp = level;
                if (temp.Contains("druid") )
                {
                    temp = temp.Replace("druid", string.Empty);
                    OneSpell.druid = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("bard") )
                {
                    temp = temp.Replace("bard", string.Empty);
                    OneSpell.bard = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("cleric/oracle") )
                {
                    temp = temp.Replace("cleric/oracle", string.Empty);
                    OneSpell.cleric = int.Parse(temp);
                    OneSpell.oracle = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("cleric"))
                {
                    temp = temp.Replace("cleric", string.Empty);
                    OneSpell.cleric = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("antipaladin") )
                {
                    temp = temp.Replace("antipaladin", string.Empty);
                    OneSpell.antipaladin = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("paladin"))
                {
                    temp = temp.Replace("paladin", string.Empty);
                    OneSpell.paladin = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("ranger"))
                {
                    temp = temp.Replace("ranger", string.Empty);
                    OneSpell.ranger = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("magus") )
                {
                    temp = temp.Replace("magus", string.Empty);
                    OneSpell.magus = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("witch"))
                {
                    temp = temp.Replace("witch", string.Empty);
                    OneSpell.witch = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("summoner"))
                {
                    temp = temp.Replace("summoner", string.Empty);
                    OneSpell.summoner = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("alchemist"))
                {
                    temp = temp.Replace("alchemist", string.Empty);
                    OneSpell.alchemist = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("inquisitor"))
                {
                    temp = temp.Replace("inquisitor", string.Empty);
                    OneSpell.inquisitor = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("oracle"))
                {
                    temp = temp.Replace("oracle", string.Empty);
                    OneSpell.oracle = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("sorcerer/wizard"))
                {
                    temp = temp.Replace("sorcerer/wizard", string.Empty);
                    OneSpell.sor = int.Parse(temp);
                    OneSpell.wiz = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("wizard"))
                {
                    temp = temp.Replace("wizard", string.Empty);
                    OneSpell.wiz = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("adept"))
                {
                    temp = temp.Replace("adept", string.Empty);
                    OneSpell.adept = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("bloodrager"))
                {
                    temp = temp.Replace("bloodrager", string.Empty);
                    OneSpell.bloodrager = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("shaman"))
                {
                    temp = temp.Replace("shaman", string.Empty);
                    OneSpell.shaman = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("psychic"))
                {
                    temp = temp.Replace("psychic", string.Empty);
                    OneSpell.psychic = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("medium"))
                {
                    temp = temp.Replace("medium", string.Empty);
                    OneSpell.medium = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("mesmerist"))
                {
                    temp = temp.Replace("mesmerist", string.Empty);
                    OneSpell.mesmerist = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("occultist"))
                {
                    temp = temp.Replace("occultist", string.Empty);
                    OneSpell.occultist = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("spiritualist"))
                {
                    temp = temp.Replace("spiritualist", string.Empty);
                    OneSpell.spiritualist = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("skald"))
                {
                    temp = temp.Replace("skald", string.Empty);
                    OneSpell.skald = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }
                if (temp.Contains("investigator"))
                {
                    temp = temp.Replace("investigator", string.Empty);
                    OneSpell.investigator = int.Parse(temp);
                    SpellLevels.Add(int.Parse(temp));
                    temp = string.Empty;
                    continue;
                }                

                if (temp.Length > 0)
                {
                    throw new Exception("ParseSpellLevels- Left over classes : " + temp);
                }
                if (OneSpell.spell_level.Contains("wizard") && OneSpell.wiz == null)
                {
                    throw new Exception("ParseSpellLevels- has wizard level but wiz is null");
                }
            }

            if (OneSpell.ranger >= 0 || (OneSpell.druid >= 0 && OneSpell.druid <= 6))
            {
                if (OneSpell.ranger == null) OneSpell.hunter = OneSpell.druid;
                if (OneSpell.druid == null) OneSpell.hunter = OneSpell.ranger;
                if (OneSpell.ranger == OneSpell.druid) OneSpell.hunter = OneSpell.ranger;
                if (OneSpell.ranger < OneSpell.druid) OneSpell.hunter = OneSpell.ranger;
                if (OneSpell.ranger > OneSpell.druid) OneSpell.hunter = OneSpell.druid;
            }
           

            int Max = SpellLevels.Max();
            int Min = SpellLevels.Min();

            if (Max == Min)
            {
                OneSpell.SLA_Level = Min;
            }
            else
            {
                if (OneSpell.sor != null)
                {
                    OneSpell.SLA_Level = OneSpell.sor;
                }
                else if (OneSpell.wiz != null)
                {
                    OneSpell.SLA_Level = OneSpell.wiz;
                }
                else if (OneSpell.cleric != null)
                {
                    OneSpell.SLA_Level = OneSpell.cleric;
                }
                else if (OneSpell.druid != null)
                {
                    OneSpell.SLA_Level = OneSpell.druid;
                }
                else if (OneSpell.bard != null)
                {
                    OneSpell.SLA_Level = OneSpell.bard;
                }
                else if (OneSpell.paladin != null)
                {
                    OneSpell.SLA_Level = OneSpell.paladin;
                }
                else if (OneSpell.ranger != null)
                {
                    OneSpell.SLA_Level = OneSpell.ranger;
                }
            }
        }
    }
}
