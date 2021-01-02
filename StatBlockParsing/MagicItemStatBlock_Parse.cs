using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CommonStrings;
using CommonStatBlockInfo;
using PathfinderGlobals;
using StatBlockCommon.MagicItem_SB;

namespace StatBlockParsing
{
    public class MagicItemStatBlock_Parse
    {
        private string CR = Environment.NewLine;
        private MagicItemStatBlock OneMagicItem;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }
        public string SelectedSlot { get; set; }

        public MagicItemStatBlock Parse(string MIstr, ref string ErrorMessage)
        {
            OneMagicItem = new MagicItemStatBlock();
            int CRPos = MIstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = MIstr.IndexOf(CR);
            }
            MIstr = FindReplaceMIString(MIstr);
           

            if (MIstr.Contains("; CL") && MIstr.Contains("; Weight"))
                return ParseOrigonalLayout(MIstr, ref ErrorMessage);
            else
                return ParseUltimateEquipemntLayoutLayout(MIstr, ref ErrorMessage);
        }

        private string FindReplaceMIString(string MIstr)
        {
            MIstr = MIstr.Trim();
            MIstr = MIstr.Replace((char)(8217), char.Parse("'")).Replace((char)(8216), char.Parse("'"))
                .Replace((char)(8212), char.Parse("-")).Replace((char)(8211), char.Parse("-"))
                .Replace((char)(150), char.Parse("-")).Replace((char)(151), char.Parse("-"));
            MIstr = MIstr.Replace("ë", "&#235;");
            MIstr = MIstr.Replace("é", "&#233;");
            MIstr = MIstr.Replace("°", "&#176;");
            MIstr = MIstr.Replace("“", ((char)34).ToString());
            MIstr = MIstr.Replace("”", ((char)34).ToString());
            MIstr = MIstr.Replace(char.Parse("-"), char.Parse("-"));
            MIstr = MIstr.Replace(char.Parse("×"), char.Parse("x"));
            MIstr = MIstr.Replace("(artifact)", "(Artifact)").Replace("(artifact )", "(Artifact)");
            MIstr = MIstr.Replace("CONSTRUCTION REQUIREMENTS", "Construction Requirements");
         
            MIstr = MIstr.Replace("DESTRUCTION ", "Destruction").Replace("DESCRIPTION ", "Description").Replace("DESTRUCTION", "Destruction")
                .Replace("DESCRIPTION", "Description");
            MIstr = MIstr.Replace("WEIGHT ", "Weight ").Replace("AURA ", "Aura ").Replace("SLOT ", "Slot ").Replace("PRICE ", "Price ")
                .Replace("COST ", "Cost ").Replace("WEIGHT ", "Weight ").Replace(" GP", " gp").Replace(" SP", " sp");
            MIstr = MIstr.Replace("MAJOR ARTIFACT ", "Major Artifact");
            MIstr = MIstr.Replace("MAJOR ARTIFACT", "Major Artifact");
            MIstr = MIstr.Replace("MINOR ARTIFACT", "Minor Artifact");
         
            MIstr = MIstr.Replace("DEScription" + CR, "DESCRIPTION" + CR).Replace("description" + CR, "DESCRIPTION" + CR)
                .Replace("Desc ription" + CR, "DESCRIPTION" + CR).Replace("Descript ion" + CR, "DESCRIPTION" + CR)
                .Replace("Descriptio n" + CR, "DESCRIPTION" + CR).Replace("Descr iption" + CR, "DESCRIPTION" + CR)
                .Replace("Descri pti on" + CR, "DESCRIPTION" + CR);
         
        
            MIstr = MIstr.Replace("CONSTRUCTION" + CR, "Construction" + CR).Replace("Constructio n" + CR, "Construction" + CR)
                .Replace("Constr uct ion" + CR, "Construction" + CR).Replace("Constr ucti on" + CR, "Construction" + CR)
                .Replace("Constr uction", "Construction").Replace("Const ruction", "Construction")
                     .Replace("CONSTRUCTION ", "Construction");

            MIstr = MIstr.Replace("REQUIREMENTS", "Requirements").Replace("REQUIREMENTS" + CR, "Requirements" + CR);
            MIstr = MIstr.Replace("STATISTICS" + CR, "Statistics" + CR);
            MIstr = MIstr.Replace("STATISTICS " + CR, "Statistics" + CR);
            MIstr = MIstr.Replace("STATISTICS " + CR, "Statistics" + CR);
            MIstr = MIstr.Replace("DESTRUCTION" + CR, "Destruction" + CR);
            return MIstr;
        }

        private MagicItemStatBlock ParseUltimateEquipemntLayoutLayout(string MIstr, ref string ErrorMessage)
        {
            //SLOT xx PRICE xx GP   AURA xx CL xx WEIGHT xx
            //PRICE xx GP SLOT xx CL xx WEIGHT xx AURA xx 
            OneMagicItem = new MagicItemStatBlock();
            bool findSlot = false;
            bool cursed = false;

            if (SelectedSlot == null)
                findSlot = true;
            else
                OneMagicItem.Slot = SelectedSlot;

            int CRPos = MIstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = MIstr.IndexOf(CR);
            }

            string temp = MIstr.Substring(0, CRPos);
            string temp4;

            if (temp.Contains("LEGENDARY WEAPON"))
            {
                temp = temp.Replace("LEGENDARY WEAPON", string.Empty);
                MIstr = MIstr.Replace("LEGENDARY WEAPON", string.Empty);
                OneMagicItem.LegendaryWeapon = true;
                OneMagicItem.Mythic = true;
            }

            if (temp.Contains("Artifact"))
            {
                temp4 = ParseArtifact(MIstr, temp);
                MIstr = MIstr.Replace(temp, string.Empty);
                if (OneMagicItem.MinorArtifactFlag) temp = temp.Replace("(Minor Artifact)", string.Empty);
                if (OneMagicItem.MajorArtifactFlag) temp = temp.Replace("(Major Artifact)", string.Empty);
            }
            else
            {
                MIstr = MIstr.Replace(temp, string.Empty);
            }

            

            int Pos = temp.IndexOf("SCALING");
            if (Pos >= 0)
            {
                string temp3 = temp.Substring(Pos);
                temp = temp.Replace(temp3, string.Empty);
                temp3 = temp3.Replace("SCALING", string.Empty).Trim();
                OneMagicItem.Scaling = temp3;
            }

            try
            {
                ParseAura(ref temp);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Issue with ParseAura section: " + ex.Message;
                return OneMagicItem;
            }

            string temp2;
            try
            {
                temp = ParseWeight(temp, out temp2);
            }
            catch
            {
                ErrorMessage = "Issue with ParseWeight section ";
                return OneMagicItem;
            }

            try
            {
                ComputeWeightValue(temp2);
            }
            catch
            {
                ErrorMessage = "Issue with ComputeWeightValue section Not Numeric: " + temp2;
                return OneMagicItem;
            }

            Pos = MIstr.IndexOf("Statistics" + CR);

            if (Pos >= 0) //intelligent items only
            {
                // STATISTICS
                //ALIGNMENT neutral good	SENSES 30 ft.
                //INTELLIGENCE 10	WISDOM 12	CHARISMA 14	EGO 7
                //LANGUAGE empathy
                MIstr = MIstr.Replace("LANGUAGES", "Languages").Replace("LANGUAGE", "Language").Replace("ALIGNMENT", "Alignment")
                    .Replace("SENSES", "Senses").Replace("INTELLIGENCE", "Intelligence").Replace("WISDOM", "Wisdom")
                    .Replace("CHARISMA", "Charisma");
                MIstr = MIstr.Replace("Intelligence", StatBlockInfo.INT).Replace("Wisdom", StatBlockInfo.WIS).Replace("Charisma", StatBlockInfo.CHA)
                    .Replace("WIS", StatBlockInfo.WIS).Replace("CHA", StatBlockInfo.CHA).Replace("EGO", "Ego")
                    .Replace("SPECIAL PURPOSE", "Special Purpose").Replace("SPELL-LIKE ABILITIES", "Spell-Like Abilities")
                    .Replace("COMMUNICATION", "Communication");

                int Pos3 = MIstr.IndexOf("Description");
                if (Pos3 == -1) Pos3 = MIstr.IndexOf("Construction");
                if (Pos3 == -1) Pos3 = MIstr.IndexOf("Destruction" + CR);
                if (Pos3 == -1) Pos3 = MIstr.IndexOf("INTENDED MAGIC ITEM");

                try
                {
                    //assumes language is last and takes all the text before that
                    string temp3 = MIstr.Substring(Pos, Pos3 - Pos);
                    Pos = temp3.IndexOf("Language");
                    if (Pos == -1) Pos = temp3.IndexOf("Spell-Like Abilities");
                    if (Pos == -1) Pos = temp3.IndexOf("Special Purpose");
                    if (Pos == -1) Pos = temp3.IndexOf("Powers");
                    Pos = temp3.IndexOf(CR, Pos);
                    temp3 = temp3.Substring(0, Pos);
                    MIstr = MIstr.Replace(temp3, string.Empty);
                    temp = temp.Replace(temp3, string.Empty);
                    ParseUEStatistics(temp3);
                }
                catch 
                {
                    ErrorMessage = "Issue with Statistics section";
                    return OneMagicItem;
                }
            }

            ParseCL(ref temp);

            if (!OneMagicItem.MinorArtifactFlag && !OneMagicItem.MajorArtifactFlag && !OneMagicItem.LegendaryWeapon)
            {
                if (MIstr.Contains("INTENDED MAGIC ITEM"))
                {
                    cursed = true;
                    temp = temp.Replace("Price", string.Empty).Replace("-", string.Empty);
                }

                if (!cursed)               
                {
                    try
                    {
                        Pos = temp.IndexOf("Price");
                        temp2 = temp.Substring(Pos);
                        Pos = temp2.IndexOf("gp");
                        if (Pos == -1)
                        {
                            if (temp2.Contains("varies"))
                            {
                                OneMagicItem.PriceValue = -1;
                                temp = temp.Replace("Price", string.Empty);
                                temp = temp.Replace("varies", string.Empty);
                            }
                        }
                        else
                        {
                            temp2 = temp2.Substring(0, Pos + 2);
                            temp = temp.Replace(temp2, string.Empty);
                            temp2 = temp2.Replace("Price", string.Empty);
                            OneMagicItem.Price = temp2.Trim();
                            temp2 = temp2.Replace("gp", string.Empty)
                                .Replace(",", string.Empty);
                            Pos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                            if (Pos >= 0)
                            {
                                temp2 = temp2.Substring(0, Pos);
                            }
                            if (temp2.ToLower().Contains("varies"))
                            {
                                ErrorMessage = " Varies in Price";
                                return OneMagicItem;
                            }
                            OneMagicItem.PriceValue = Convert.ToInt32(temp2);
                        }
                    }
                    catch
                    {
                        ErrorMessage = "Issue with Price section";
                        return OneMagicItem;
                    }
                }
            }

            if (findSlot)
            {
                try
                {
                    Pos = temp.IndexOf("Slot");
                    temp2 = temp.Substring(Pos);
                    temp = temp.Replace(temp2, string.Empty);
                    temp2 = temp2.Replace("Slot", string.Empty);
                    OneMagicItem.Slot = temp2.ToLower().Trim();
                }
                catch
                {
                    ErrorMessage = "Issue with Slot section";
                    return OneMagicItem;
                }
            }

            OneMagicItem.name = temp.ProperCase().Trim();

            if (!OneMagicItem.MinorArtifactFlag && !OneMagicItem.MajorArtifactFlag && !OneMagicItem.LegendaryWeapon)
            {
                if (cursed)
                {
                    Pos = MIstr.IndexOf("INTENDED MAGIC ITEM");
                    temp = MIstr.Substring(0, Pos);
                    MIstr = MIstr.Replace(temp, string.Empty);
                    temp = KeepCRs(CR, temp);
                    temp = temp.Replace("Description", string.Empty);
                    OneMagicItem.Description = temp.Trim();

                    MIstr = MIstr.Replace("INTENDED MAGIC ITEM", string.Empty);
                    OneMagicItem.MagicItems = MIstr.Trim();
                }
                else
                {
                    try
                    {
                        Pos = MIstr.IndexOf("Construction Requirements");
                        temp = MIstr.Substring(0, Pos);
                        MIstr = MIstr.Replace(temp, string.Empty);
                        temp = KeepCRs(CR, temp);
                        temp = temp.Replace("Description", string.Empty);
                        OneMagicItem.Description = temp.Trim();

                        CRPos = MIstr.IndexOf(CR);
                        temp = MIstr.Substring(CRPos);
                        MIstr = MIstr.Replace(temp, string.Empty);
                        OneMagicItem.Requirements = temp.Trim();
                        OneMagicItem.Mythic = temp.Contains("Mythic Crafter");
                    }
                    catch
                    {
                        ErrorMessage = "Issue with Construction section";
                        return OneMagicItem;
                    }

                    try
                    {
                        Pos = MIstr.IndexOf("Cost");
                        temp = MIstr.Substring(Pos);
                        temp = temp.Replace("Cost", string.Empty);
                        OneMagicItem.Cost = temp.Trim();
                        if (OneMagicItem.Cost == "varies")
                        {
                            OneMagicItem.CostValue = -1;
                        }
                        else
                        {
                            temp = temp.Substring(0, temp.IndexOf("gp"));
                            temp = temp.Replace(", ", string.Empty)
                                .Replace(",", string.Empty);
                            OneMagicItem.CostValue = Convert.ToInt32(temp);
                        }
                    }
                    catch
                    {
                        ErrorMessage = "Issue with Cost section";
                        return OneMagicItem;
                    }
                }              
            }
            else
            {
                try
                {
                    Pos = MIstr.IndexOf("Destruction" + CR);
                    temp = MIstr.Substring(0, Pos);
                    MIstr = MIstr.Replace(temp, string.Empty);

                    Pos = temp.IndexOf("Special Purpose");
                    if (Pos >= 0)
                    {
                        temp2 = temp.Substring(Pos);
                        temp = temp.Replace(temp2, string.Empty).Trim();
                        temp2 = temp2.Replace("Special Purpose:", string.Empty)
                            .Replace(CR, PathfinderConstants.SPACE);
                        OneMagicItem.SpecialPurpose = temp2.Trim();
                    }

                    temp = KeepCRs(CR, temp);

                    OneMagicItem.Description = temp.Trim();
                    OneMagicItem.Mythic = temp.Contains("mythic ");

                    CRPos = MIstr.IndexOf(CR);
                    temp = MIstr.Substring(CRPos);
                    MIstr = MIstr.Replace(temp, string.Empty);
                    OneMagicItem.Destruction = temp.Trim();
                }
                catch
                {
                    ErrorMessage = "Issue with Artifact Description section";
                    return OneMagicItem;
                }
            }

            CreateFullText();

            return OneMagicItem;
        }

        private void ParseAura(ref string temp)
        {
            int Pos = temp.IndexOf("Aura");
            if (Pos == -1)
            {
                throw new Exception("Aura not found String Value:" + temp);
            }
            string temp2 = temp.Substring(Pos);
            temp = temp.Replace(temp2, string.Empty);
            ComputeAura(ref temp2);
        }

        private void ParseCL(ref string temp)
        {
            int Pos = temp.LastIndexOf("CL");
            string temp2 = temp.Substring(Pos);
            temp = temp.Replace(temp2, string.Empty);
            ComputeCL(ref temp2);
        }

        private string ParseWeight(string temp, out string temp2)
        {
            int Pos;
            int Pos2 = 0;
            var tempLower = temp.ToLower();
            if (tempLower.Contains("of weight"))  Pos2 = tempLower.IndexOf("of weight") + "of weight".Length;

            Pos = temp.IndexOf("Weight", Pos2);
            if (Pos == -1) throw new Exception("ParseWeight--Weight section missing");

            temp2 = temp.Substring(Pos);
            temp = temp.Replace(temp2, string.Empty);
            temp2 = temp2.Replace("Weight", string.Empty).Trim();
            OneMagicItem.Weight = temp2;
            return temp;
        }

        private MagicItemStatBlock ParseOrigonalLayout(string MIstr, ref string ErrorMessage)
        {
            int CRPos = MIstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = MIstr.IndexOf(CR);
            }
            string temp = MIstr.Substring(0, CRPos);
            if (temp.Contains("Artifact") )
            {
                ParseArtifact(MIstr, temp);
                if (OneMagicItem.MinorArtifactFlag)
                {
                    temp = temp.Replace("(Minor Artifact)", string.Empty);
                    MIstr = MIstr.Replace("(Minor Artifact)", string.Empty);
                }
                if (OneMagicItem.MajorArtifactFlag)
                {
                    temp = temp.Replace("(Major Artifact)", string.Empty);
                    MIstr = MIstr.Replace("(Major Artifact)", string.Empty);
                }
            }
            OneMagicItem.name = temp.ProperCase().Trim();
            MIstr = MIstr.ReplaceFirst(temp.Trim(), string.Empty).Trim();

            int Pos = MIstr.IndexOf(";");
            temp = MIstr.Substring(0, Pos);
            MIstr = MIstr.Replace(temp + ";", string.Empty).Trim();
            ComputeAura(ref temp);

            CRPos = MIstr.IndexOf(CR);
            temp = MIstr.Substring(0, CRPos);
            if (temp.Length > 0)  MIstr = MIstr.Replace(temp + CR, string.Empty);
            ComputeCL(ref temp);

            Pos = MIstr.IndexOf(";");
            temp = MIstr.Substring(0, Pos);
            MIstr = MIstr.Replace(temp + ";", string.Empty);
            temp = temp.Replace("Slot", string.Empty).Trim();
            OneMagicItem.Slot = temp;

            int Pos2 = MIstr.IndexOf("Weight");
            if (Pos2 == -1)
            {
                ErrorMessage = "Missing Weight section";
                return OneMagicItem;
            }
            Pos = MIstr.LastIndexOf("Price", Pos2);
            string hold;
            if (Pos >= 0)
            {
                Pos = MIstr.IndexOf(";");
                temp = MIstr.Substring(0, Pos);
                MIstr = MIstr.Replace(temp + ";", string.Empty);
                temp = temp.Replace("Price", string.Empty).Trim()
                    .Replace(CR, PathfinderConstants.SPACE);
                OneMagicItem.Price = temp;
                temp = temp.Replace("gp", string.Empty)
                    .Replace(",", string.Empty);
                Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                hold = temp;
                if (Pos >= 0)  temp = temp.Substring(0, Pos);
                OneMagicItem.PriceValue = Convert.ToInt32(temp);
                temp = hold;
            }

            CRPos = MIstr.IndexOf(CR);
            temp = MIstr.Substring(0, CRPos);
            MIstr = MIstr.Replace(temp + CR, string.Empty);
            temp = temp.Replace("Weight", string.Empty)
                .Replace(CR, string.Empty).Trim();
            OneMagicItem.Weight = temp;

            try
            {
                ComputeWeightValue(temp);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return OneMagicItem;
            }           

            Pos = MIstr.IndexOf("Statistics" + CR);
            if (Pos >= 0) //intelligent items only
            {
                Pos = MIstr.IndexOf("DESCRIPTION" + CR);
                if (Pos == -1) Pos = MIstr.IndexOf("Description" + CR);
                if (Pos == -1) Pos = MIstr.IndexOf("Construction" + CR);
                if (Pos == -1) Pos = MIstr.IndexOf("Destruction" + CR);
                temp = MIstr.Substring(0, Pos);
                MIstr = MIstr.Replace(temp, string.Empty);
                ParseStatistics(temp);
            }

            MIstr = MIstr.Replace("Description" + CR, string.Empty)
                .Replace("CREATION", "Creation")
                .Replace("Creati on", "Creation")
                .Replace("Creation " + CR, "Creation" + CR);
            Pos = MIstr.IndexOf("Creation" + CR);

            if (Pos >= 0) //cursed item
            {
                temp = MIstr.Substring(Pos);
                MIstr = MIstr.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Creation", string.Empty)
                    .Replace("Magic Items", string.Empty).Trim();
                OneMagicItem.MagicItems = temp;
                Pos = MIstr.Length;
            }
            else
            {
                Pos = MIstr.IndexOf("Construction" + CR);

                if (Pos == -1)
                {
                    if (!OneMagicItem.MinorArtifactFlag && !OneMagicItem.MajorArtifactFlag)
                    {
                        if (MIstr.Contains("Destruction" + CR) )
                        {
                            OneMagicItem.MinorArtifactFlag = true; //default minor
                        }
                    }

                    if (OneMagicItem.MinorArtifactFlag || OneMagicItem.MajorArtifactFlag)
                    {
                        Pos = MIstr.IndexOf("Destruction" + CR);
                        temp = MIstr.Substring(Pos);
                        MIstr = MIstr.Replace(temp, string.Empty).Trim();
                        temp = temp.Replace("Destruction", string.Empty).Trim();
                        temp = KeepCRs(CR, temp);
                        OneMagicItem.Destruction = temp.Trim();
                    }
                    //description only                 
                    temp = MIstr.Replace("Description", string.Empty).Trim();
                    temp = KeepCRs(CR, temp);                 

                    OneMagicItem.Description = temp;
                    CreateFullText();
                    return OneMagicItem;
                }
            }
            temp = MIstr.Substring(0, Pos);
            MIstr = MIstr.Replace(temp, string.Empty);
            temp = KeepCRs(CR, temp);
            temp = temp.Replace("Description", string.Empty);

            OneMagicItem.Description = temp.Trim();

            if (OneMagicItem.MagicItems.Length == 0)
            {
                MIstr = MIstr.Replace("Construction", "CONSTRUCTION")
                    .Replace("CONSTRUCTION" + CR, string.Empty);

                Pos = MIstr.IndexOf(";");
                temp = MIstr.Substring(0, Pos);
                MIstr = MIstr.Replace(temp + ";", string.Empty);
                temp = temp.Replace("Requirements", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Requirements = temp;

                temp = MIstr;
                temp = temp.Replace("Cost", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Cost = temp;
                temp = temp.Substring(0, temp.IndexOf("gp"));
                temp = temp.Replace(", ", string.Empty)
                    .Replace(",", string.Empty);
                OneMagicItem.CostValue = Convert.ToInt32(temp);
            }

            CreateFullText();

            return OneMagicItem;
        }

        private string ParseArtifact(string MIstr, string temp)
        {
            int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            string temp3 = temp.Substring(Pos);
            temp = temp.Replace(temp3, string.Empty);
            MIstr = MIstr.Replace(" (Artifact)", string.Empty)
                .Replace(" (Minor Artifact)", string.Empty)
                .Replace(" (Major Artifact)", string.Empty);
            OneMagicItem.MinorArtifactFlag =  temp3.Contains("Minor");
            OneMagicItem.MajorArtifactFlag =  temp3.Contains("Major");
            if (!OneMagicItem.MinorArtifactFlag && !OneMagicItem.MajorArtifactFlag)  OneMagicItem.MinorArtifactFlag = true;
           
            return temp;
        }

        private void ComputeAura(ref string temp)
        {
            temp = temp.Replace("Aura", string.Empty).Trim();
            OneMagicItem.AuraStrength = "none";
            List<string> AuraStrengths = new List<string>() { "faint", "moderate", "strong" };
            foreach (string auraS in AuraStrengths)
            {
                if (temp.Contains(auraS))  OneMagicItem.AuraStrength = auraS;
            }
            
            foreach (string school in CommonMethods.GetSpellSchools())
            {
                if (temp.Contains(school.ToLower()))
                {
                    switch (school)
                    {
                        case "Abjuration":
                            OneMagicItem.Abjuration = true;
                            break;
                        case "Conjuration":
                            OneMagicItem.Conjuration = true;
                            break;
                        case "Divination":
                            OneMagicItem.Divination = true;
                            break;
                        case "Enchantment":
                            OneMagicItem.Enchantment = true;
                            break;
                        case "Evocation":
                            OneMagicItem.Evocation = true;
                            break;
                        case "Illusion":
                            OneMagicItem.Illusion = true;
                            break;
                        case "Necromancy":
                            OneMagicItem.Necromancy = true;
                            break;
                        case "Transmutation":
                            OneMagicItem.Transmutation = true;
                            break;
                       case "Universal":
                            OneMagicItem.Universal = true;
                            break;
                        default:
                            throw new Exception("ComputeAura-- missing aura: " + school);
                    }
                }
            }
            OneMagicItem.Aura = temp;
        }

        private void ComputeCL(ref string temp)
        {
            temp = temp.Replace("CL", string.Empty).Replace("st", string.Empty).Replace("nd", string.Empty)
                .Replace("rd", string.Empty).Replace("th", string.Empty).Replace(CR, string.Empty).Trim();
            OneMagicItem.CL = temp;
        }

        private void ComputeWeightValue(string temp)
        {
            bool tons = false;
            if (temp == "varies")
            {
                OneMagicItem.WeightValue = 0;
                return;
            }
            temp = temp.Replace("lbs.", string.Empty).Replace("lbs", string.Empty).Replace("tons", string.Empty)
                .Replace("ton", string.Empty).Replace("lb.", string.Empty).Trim();
            if (temp.Contains("tons"))
            {
                tons = true;
                temp = temp.Replace("tons", string.Empty).Trim();
            }
            if (temp.IndexOf("/") > 0)
            {
                OneMagicItem.WeightValue = Convert.ToInt32(temp.Substring(0, temp.IndexOf("/")));
                OneMagicItem.WeightValue /= Convert.ToInt32(temp.Substring(temp.IndexOf("/") + 1));
            }
            else
            {
                if (temp == "-")
                {
                    OneMagicItem.WeightValue = 0;
                }
                else
                {
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos >= 0) temp = temp.Substring(0, Pos).Trim();

                    temp = temp.Replace("each", string.Empty);
                    try
                    {
                        OneMagicItem.WeightValue = Convert.ToDouble(temp);
                    }
                    catch
                    {
                        throw new Exception("ComputeWeightValue--convert weight error : " + temp);
                    }
                    if (tons) OneMagicItem.WeightValue *= 2000;
                }
            }
        }

        private void CreateFullText()
        {
            StatBlockFormating.MagicItemStatBlock_Format MI_SB_Form = new StatBlockFormating.MagicItemStatBlock_Format();
            MI_SB_Form.ItalicPhrases = ItalicPhrases;
            MI_SB_Form.BoldPhrases = BoldPhrases;
            OneMagicItem.full_text = MI_SB_Form.CreateFullText(OneMagicItem);
        }

        private void ParseUEStatistics(string Statistics)
        {
            //intelligent items only
            Statistics = Statistics.Replace("Statistics", string.Empty).Trim();

            //work your way back
            string temp;
            int Pos = Statistics.IndexOf("Languages");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Languages", string.Empty).Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Languages = temp;
            }

            Pos = Statistics.IndexOf("Language");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Language", string.Empty).Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Languages = temp;
            }


            Pos = Statistics.IndexOf("Dedicated Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Dedicated Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
                OneMagicItem.DedicatedPowers = temp.Trim();
            }

            Pos = Statistics.IndexOf("Dedicated Power");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Dedicated Power", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
                OneMagicItem.DedicatedPowers = temp.Trim();
            }

            Pos = Statistics.IndexOf("Special Purpose");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Special Purpose", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty).Trim();
                OneMagicItem.SpecialPurpose = temp;
            }

            Pos = Statistics.IndexOf("Purpose");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Purpose", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty).Trim();
                OneMagicItem.SpecialPurpose = temp;
            }

            Pos = Statistics.IndexOf("Lesser Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Lesser Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Powers = temp;
            }          

                

            Pos = Statistics.IndexOf("Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Powers = temp;
            }

            Pos = Statistics.IndexOf("Communication");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Communication", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Communication = temp;
            }

            Pos = Statistics.IndexOf(StatBlockInfo.CHA);
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.CHA, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Cha = temp;
            }

            Pos = Statistics.IndexOf("Wis ");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.WIS, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(",", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Wis = temp;
            }

            Pos = Statistics.IndexOf(StatBlockInfo.INT);
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.INT, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(",", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Int = temp;
            }

            Pos = Statistics.IndexOf("Senses");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Senses", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                if (temp.LastIndexOf(";") == temp.Length - 1) temp = temp.Substring(0, temp.Length - 1);
                OneMagicItem.Senses = temp.Trim();
            }    

            Pos = Statistics.IndexOf("Ego");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Ego", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Ego = temp;
                OneMagicItem.IsIntelligentItem = true;
            }
                    

            Pos = Statistics.IndexOf("AL");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                temp = temp.Replace("AL", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.AL = temp;
            }

            Pos = Statistics.IndexOf("Alignment");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                temp = temp.Replace("Alignment", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.AL = temp.Trim();
            }
        }

        private void ParseStatistics(string Statistics)
        {
            /*Aura moderate divination; CL 10th
            Slot none; Price 0 gp; Weight 0 lbs.
            STATISTICS
            AL N; Ego 11
            Int 13, Wis 14, Cha 12
            Senses 60 ft.
            Languages read languages, speech (Azlanti, Common)
            Special Purpose slay lawful outsiders
            Description */
            //intelligent items only
            Statistics = Statistics.Replace("Statistics", string.Empty).Trim();

            //work your way back
            string temp;           

            int Pos = Statistics.IndexOf("Dedicated Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Dedicated Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.DedicatedPowers = temp;
            }

            Pos = Statistics.IndexOf("Dedicated Power");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Dedicated Power", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.DedicatedPowers = temp;
            }

            Pos = Statistics.IndexOf("Special Purpose");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Special Purpose", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty).Trim();
                OneMagicItem.SpecialPurpose = temp;
            }

            Pos = Statistics.IndexOf("Purpose");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Purpose", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty).Trim();
                OneMagicItem.SpecialPurpose = temp;
            }

            Pos = Statistics.IndexOf("Lesser Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Lesser Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Powers = temp;
            }

            Pos = Statistics.IndexOf("Powers");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Powers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Powers = temp;
            }             

            Pos = Statistics.IndexOf("Languages");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Languages", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Languages = temp.Trim();
            }

            Pos = Statistics.IndexOf("Language");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Language", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Languages = temp;
            }

            Pos = Statistics.IndexOf("Communication");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Communication", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Communication = temp;
            }

            Pos = Statistics.IndexOf("Senses");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Senses", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                if (temp.LastIndexOf(";") == temp.Length - 1) temp = temp.Substring(0, temp.Length - 1);
                OneMagicItem.Senses = temp.Trim();
            }

            Pos = Statistics.IndexOf(StatBlockInfo.CHA);
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.CHA, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Cha = temp;
            }

            Pos = Statistics.IndexOf("Wis ");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.WIS, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(",", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Wis = temp;
            }

            Pos = Statistics.IndexOf(StatBlockInfo.INT);
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(StatBlockInfo.INT, string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(",", PathfinderConstants.SPACE).Trim();
                OneMagicItem.Int = temp;
            }

            
            Pos = Statistics.IndexOf("Ego");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Ego", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                OneMagicItem.Ego = temp;
                OneMagicItem.IsIntelligentItem = true;
            }            

            Pos = Statistics.IndexOf("AL");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                temp = temp.Replace("AL", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.AL = temp;
            }

            Pos = Statistics.IndexOf("Alignment");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                temp = temp.Replace("Alignment", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", PathfinderConstants.SPACE).Trim();
                OneMagicItem.AL = temp;
            }
        }       

        private string KeepCRs(string temp)
        {
            //mark the keeper CRs
            temp = temp.Replace(PathfinderConstants.PAREN_RIGHT + CR, ")<br>")
                .Replace("D" + CR, "D<br>");
            for (int a = 0; a <= 9; a++)
            {
                temp = temp.Replace(CR + a.ToString(), PathfinderConstants.BREAK + a.ToString());
            }
            temp = temp.Replace("DC<br>", "DC ")
                .Replace(CR + "At Will", "<br>At Will");

            //remove the unwanted CRs
            temp = temp.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            temp = temp.Replace(PathfinderConstants.BREAK, CR);
            return temp;
        }

        private static string KeepCRs(string CR, string temp)
        {
            //mark the keeper CRs
            temp = temp.Replace("." + CR, ".<br>");

            //remove the unwanted CRs
            temp = temp.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            temp = temp.Replace(PathfinderConstants.BREAK, CR);
            return temp;
        }
    }
}
