using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using PathfinderGlobals;
using StatBlockCommon.Monster_SB;

namespace StatBlockParsing
{
    public class MonsterStatBlock_Parse
    {
        private string CR = Environment.NewLine;
        private ISBCommonBaseInput _sbcommonBaseInput;

        public MonsterStatBlock MonSB { get { return _sbcommonBaseInput.MonsterSB; } set { _sbcommonBaseInput.MonsterSB = value; } }
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }
        public List<string> BoldPhrasesSpecialAbilities { get; set; }
        public bool GroupInName { get; set; }
        public string SourceSuperScript { get; set; }

        public MonsterStatBlock_Parse()
        {
            _sbcommonBaseInput = new SBCommonBaseInput();
        }

        public MonsterStatBlock Parse(string Monstr, string Source, bool isFrogGods, ref string ErrorMessage)
        {
            _sbcommonBaseInput.MonsterSB = new MonsterStatBlock();
            SourceSuperScript = string.Empty;

            CR = Utility.FindCR(Monstr);
            int CRPos = Monstr.IndexOf(CR);

            int CRPosTemp = Monstr.IndexOf(CR + CR);

            if (CRPosTemp == -1)
            {
                CRPosTemp = Monstr.IndexOf(CR + PathfinderConstants.SPACE + CR);
                if (CRPosTemp == -1)
                {
                    ErrorMessage = "No double Carrige Return in text. This needed to to determine where Description text starts.";
                    return MonSB;
                }
            }

            for (int a = 0; a < ItalicPhrases.Count; a++)
            {
                ItalicPhrases[a] = Utility.RemoveSuperScripts(ItalicPhrases[a]);
            }

            Monstr = Monstr.Trim();
            Monstr = Utility.CommonMonsterFindReplace(Monstr, CR);

            int Pos;
            string temp;
            #region ParseIntro
            try
            {
                Pos = Monstr.IndexOf("CR ");
                if (Pos <= 0) ErrorMessage = "Can't find text CR.";
                temp = Monstr.Substring(0, Pos);
                CRPos = temp.LastIndexOf(CR);
                if (CRPos <= 0) ErrorMessage = "Can't find newline.";
                temp = temp.Substring(0, CRPos);
                Monstr = Monstr.Replace(temp, string.Empty).Trim();
            }
            catch
            {
                ErrorMessage += " Problem with CR.";
                return MonSB;
            }
            ParseIntro(temp);
            #endregion ParseIntro

            #region ParseBasic
            Monstr = Monstr.Replace("Defenses" + CR, "Defense" + CR);
            int DefensePos = Monstr.IndexOf("Defense" + CR);
            if (DefensePos == -1 && isFrogGods) DefensePos = Monstr.IndexOf("AC ");

            if (DefensePos == -1)
            {
                ErrorMessage = "ParseBasic: Issue with DefensePos  CR=" + (int)CR[0];
                return MonSB;
            }
            temp = Monstr.Substring(0, DefensePos);
            Monstr = Monstr.Replace(temp, string.Empty).Trim();
            try
            {
                ParseBasic(temp, ref ErrorMessage);
                if (ErrorMessage.Length > 0) return MonSB;
            }
            catch (Exception ex)
            {
                ErrorMessage = "ParseBasic: " + ex.Message;
                return MonSB;
            }

            #endregion ParseBasic

            #region ParseDefense
            Monstr = Monstr.Replace("Ofense", "Offense");
            DefensePos = Monstr.IndexOf("Defense" + CR);
            if (DefensePos == -1 && isFrogGods) DefensePos = Monstr.IndexOf("AC ");
            int OffensePos = Monstr.IndexOf("Offense" + CR);
            if (OffensePos == -1 && isFrogGods)
            {
                OffensePos = Monstr.IndexOf("Speed ");
                if (OffensePos == -1) OffensePos = Monstr.IndexOf("Spd ");
            }


            try
            {
                temp = Monstr.Substring(DefensePos, OffensePos - DefensePos);
                Monstr = Monstr.Replace(temp, string.Empty).Trim();
                ParseDefense(temp);
            }
            catch (Exception ex)
            {
                ErrorMessage = "ParseDefense: " + ex.Message;
                return MonSB;
            }
            #endregion ParseDefense


            #region ParseOffense
            int TacticsPos = Monstr.IndexOf("Tactics" + CR);
            if (TacticsPos == -1) //no tactics section
            {
                if (Monstr.Contains("Tactics"))
                {
                    TacticsPos = Monstr.IndexOf("Statistics" + CR);
                    if (TacticsPos > Monstr.IndexOf("Tactics"))
                    {

                        Monstr = Monstr.Replace("Tactics", "Tactics" + CR);
                        TacticsPos = Monstr.IndexOf("Tactics" + CR);
                    }
                }
                else
                {
                    TacticsPos = Monstr.IndexOf("Statistics" + CR);
                    if (TacticsPos == -1 && isFrogGods) TacticsPos = Monstr.IndexOf("Str ");
                }
            }

            OffensePos = Monstr.IndexOf("Offense" + CR);
            if (OffensePos == -1 && isFrogGods)
            {
                OffensePos = Monstr.IndexOf("Speed ");
                if (OffensePos == -1) OffensePos = Monstr.IndexOf("Spd ");
            }

            if (TacticsPos == -1)
            {
                ErrorMessage = "Issue with Tactics or Statistics header";
                return MonSB;
            }
            temp = Monstr.Substring(OffensePos, TacticsPos - OffensePos);
            Monstr = Monstr.Replace(temp, string.Empty).Trim();
            try
            {
                ParseOffense(temp);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Issue with Offense " + ex.Message;
                return MonSB;
            }
            #endregion ParseOffense

            int StatisticsPos = Monstr.IndexOf("Statistics" + CR);
            if (StatisticsPos == -1)
            {
                ErrorMessage = "Issue with Statistics header";
                return MonSB;
            }

            #region ParseTactics
            TacticsPos = Monstr.IndexOf("Tactics" + CR);
            MonSB.BaseStatistics = string.Empty;
            MonSB.BeforeCombat = string.Empty;
            MonSB.DuringCombat = string.Empty;
            MonSB.Morale = string.Empty;
            if (TacticsPos >= 0)
            {
                temp = Monstr.Substring(TacticsPos, StatisticsPos - TacticsPos);
                Monstr = Monstr.Replace(temp, string.Empty).Trim();
                ParseTactics(temp);
            }
            #endregion ParseTactics

            #region ParseStatistics
            int EcologyPos = Monstr.IndexOf("Ecology" + CR);
            if (EcologyPos == -1 && isFrogGods) EcologyPos = Monstr.IndexOf("Environment ");
            StatisticsPos = Monstr.IndexOf("Statistics" + CR);
            if (StatisticsPos == -1 && isFrogGods) StatisticsPos = Monstr.IndexOf("Str ");
            if (EcologyPos == -1)
            {
                ErrorMessage = "Issue with Ecology header";
                return MonSB;
            }

            temp = Monstr.Substring(StatisticsPos, EcologyPos - StatisticsPos);
            Monstr = Monstr.Replace(temp, string.Empty).Trim();
            ParseStatistics(temp, out ErrorMessage);
            if (ErrorMessage.Length > 0) return MonSB;
            #endregion ParseStatistics

            #region ParseEcology
            int SpecialAbilitiesPos = Monstr.IndexOf("Special Abilities" + CR);
            CRPos = Monstr.IndexOf(CR + CR);   //double CR = start of Description block
            if (CRPos < SpecialAbilitiesPos) SpecialAbilitiesPos = -1;
            EcologyPos = Monstr.IndexOf("Ecology" + CR);
            if (EcologyPos == -1 && isFrogGods) EcologyPos = Monstr.IndexOf("Environment ");
            if (SpecialAbilitiesPos == -1)
            {
                CRPos = Monstr.IndexOf(CR + CR);   //double CR = start of Description block             
                if (CRPos == -1)
                {
                    ErrorMessage = "No double CR";
                    return MonSB;
                }
                temp = Monstr.Substring(EcologyPos, CRPos - EcologyPos);
            }
            else
            {
                temp = Monstr.Substring(EcologyPos, SpecialAbilitiesPos - EcologyPos);
            }

            Monstr = Monstr.Replace(temp, string.Empty).Trim();

            Pos = Monstr.IndexOf("*");
            if (Pos >= 0 && Monstr.IndexOf("(*)") == 0)
            {
                CRPos = Monstr.IndexOf(CR);
                string temp2 = Monstr.Substring(Pos, CRPos - Pos);
                Monstr = Monstr.Replace(temp2, string.Empty).Trim();
                MonSB.Note = temp2.Trim();
            }

            Pos = Monstr.IndexOf(" See ");
            if (Pos >= 0 && !Monstr.Contains(". See ") && !Monstr.Contains(": See ") 
                && !Monstr.Contains("(Ex) See ") && !Monstr.Contains("(Ex) See ") && !Monstr.Contains("(Su) See "))
            {
                FindSuperScriptNotes(ref Monstr, Pos);
            }

            ParseEcology(temp);
            #endregion ParseEcology

            MonSB.IsBestiary = MonSB.Environment.Length > 0;
            if (MonSB.Race.Length == 0 && MonSB.Environment.Length == 0) MonSB.Race = MonSB.name;


            #region ParseSpecialAbilities
            SpecialAbilitiesPos = Monstr.IndexOf("Special Abilities" + CR);
            CRPos = Monstr.IndexOf(CR + CR);   //double CR = start of Description block
            if (CRPos < SpecialAbilitiesPos) SpecialAbilitiesPos = -1;
            if (SpecialAbilitiesPos >= 0)
            {
                CRPos = Monstr.IndexOf(CR + CR, SpecialAbilitiesPos);
                if (CRPos == -1) CRPos = Monstr.Length;
                temp = Monstr.Substring(SpecialAbilitiesPos, CRPos - SpecialAbilitiesPos);
                Monstr = Monstr.Replace(temp, string.Empty).Trim();
                ParseSpecialAbilities(temp);
            }
            #endregion ParseSpecialAbilities

            //mark the keeper CRs
            Monstr = Monstr.Replace("." + CR, ".<br>");
            //remove the unwanted CRs
            Monstr = Monstr.Replace(CR, PathfinderConstants.SPACE);
            //put back the keeper CRs
            Monstr = Monstr.Replace(PathfinderConstants.BREAK, CR);

            MonSB.Description = Monstr.Trim();
            MonSB.CharacterFlag = Monstr.Contains(" Characters") || Monstr.Contains(" CHARACTERS");
            MonSB.CompanionFlag = Monstr.Contains(" Companions");

            if (Monstr.Contains("do not possess racial Hit Dice") || Monstr.Contains("do not possess racial HD")
                || Monstr.Contains("have no racial Hit Dice") || Monstr.Contains("they do not have racial Hit Dice")
                || Monstr.Contains("they don’t possess racial Hit Dice") || Monstr.Contains("don't have racial Hit Dice"))
            {
                MonSB.DontUseRacialHD = true;
            }

            #region CreateFullText
            StatBlockFormating.MonsterStatBlock_Format MonSB_Form = new StatBlockFormating.MonsterStatBlock_Format();
            MonSB_Form.ItalicPhrases = ItalicPhrases;
            MonSB_Form.BoldPhrases = BoldPhrases;
            MonSB_Form.BoldPhrasesSpecialAbilities = BoldPhrasesSpecialAbilities;
            MonSB_Form.SourceSuperScript = SourceSuperScript;
            try
            {
                MonSB.FullText = MonSB_Form.CreateFullText(MonSB);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Create Full Text " + ex.Message;
            }
            #endregion CreateFullText

            if (MonSB.IsBestiary && (MonSB.Gear.Length > 0 || MonSB.OtherGear.Length > 0))
            {
                ErrorMessage = "Bestiary Monster has Gear section populated. Should be in Treasure section.";
            }

            return MonSB;
        }

        public void UpdateCR(string newCR)
        {
            CR = newCR;
        }

        private void FindSuperScriptNotes(ref string Monstr, int Pos)
        {
            string temp3 = Monstr.Substring(0, Pos);

            int LastCRPos = temp3.LastIndexOf(CR);
            int CRPos = Monstr.IndexOf(CR, Pos);
            string temp2 = Monstr.Substring(LastCRPos, CRPos - LastCRPos);
            Monstr = Monstr.Replace(temp2, string.Empty).Trim();
            MonSB.Note = temp2.Trim();
            List<string> superScripts = MonSB.Note.Split('.').ToList();
            superScripts.RemoveAll(x => x == string.Empty);
            List<string> values = new List<string>();
            foreach (string super in superScripts)
            {
                string temp6 = super.Trim();
                CRPos = temp6.IndexOf(PathfinderConstants.SPACE);
                values.Add(temp6.Substring(0, CRPos).Trim());
                // SourceSuperScript = temp6.Substring(0, CRPos);
            }
            SourceSuperScript = string.Join(",", values.ToArray());
        }

        protected void ParseBasic(string Basic, ref string ErrorMessage)
        {
            BasicRegionParser basicRegionParser = new BasicRegionParser(_sbcommonBaseInput);
            basicRegionParser.ParseBasic(Basic, ref ErrorMessage, CR, PathfinderConstants.TAB);
        }

        private void ParseIntro(string basic)
        {
            IntroParser introParser = new IntroParser(_sbcommonBaseInput);
            introParser.ParseIntro(basic, GroupInName, CR);
        }

        protected void ParseDefense(string defense)
        {
            DefenseRegionParser defenseParser = new DefenseRegionParser(_sbcommonBaseInput);
            defenseParser.ParseDefense(defense, CR);
        }

        private void ParseTactics(string tactics)
        {
            TacticsRegionParser tacticsRegionParser = new TacticsRegionParser(_sbcommonBaseInput);
            tacticsRegionParser.ParseTactics(tactics, CR);
        }

        protected void ParseOffense(string offense)
        {
            OffenseRegionParser offenseRegionParser = new OffenseRegionParser(_sbcommonBaseInput);
            offenseRegionParser.ParseOffense(offense, CR);
        }

        protected void ParseStatistics(string Statistics, out string ErrorMessage)
        {
            StatisticsRegionParser statisticsRegionParser = new StatisticsRegionParser(_sbcommonBaseInput);
            SourceSuperScript = statisticsRegionParser.ParseStatistics(Statistics, CR, out ErrorMessage);
        }

        private void ParseEcology(string Ecology)
        {
            EcologyRegionParser ecologyRegionParser = new EcologyRegionParser(_sbcommonBaseInput);
            ecologyRegionParser.ParseEcology(Ecology, CR);
        }

        protected void ParseSpecialAbilities(string SA)
        {
            SpecialAbilitiesParser specialAbilitiesParser = new SpecialAbilitiesParser(_sbcommonBaseInput);
            specialAbilitiesParser.ParseSpecialAbilities(SA, CR);
        }
    }
}
