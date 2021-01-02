using PathfinderGlobals;
using StatBlockCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker.Parsers
{
    public class BeforeCombatParser : IBeforeCombatParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private ISpellsData _spellsData;

        public BeforeCombatParser(ISBCheckerBaseInput sbCheckerBaseInput, ISpellsData spellsData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _spellsData = spellsData;
        }

        // Stat Block values reflect whatever spells, potions etc were cast/used in Before Combat text
        // parse out italiced bloacks for later use
        public void ParseBeforeCombat()
        {
            string text = _sbCheckerBaseInput.MonsterSB.FullText;
            _spellsData.BeforeCombatMagic = new List<string>();

            int Pos = text.IndexOf("Before Combat");
            int Pos3 = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("Permanent");

            if (Pos == -1 && Pos3 == -1) return;

            if (Pos >= 0)
            {
                int Pos2 = text.IndexOf(PathfinderConstants.EH5, Pos); //end of block
                string hold = text.Substring(Pos, Pos2 - Pos);
                string temp = string.Empty;

                Pos = hold.IndexOf(PathfinderConstants.ITACLIC);

                while (Pos >= 0)
                {
                    Pos2 = hold.IndexOf(PathfinderConstants.EITACLIC, Pos);
                    temp = hold.Substring(Pos, Pos2 - Pos);
                    temp = temp.Replace(PathfinderConstants.ITACLIC, string.Empty);
                    temp = temp.Replace(PathfinderConstants.EITACLIC, string.Empty).Trim();
                    temp = temp.Replace("potions", "potion");
                    if (temp != "permanency")
                    {
                        _spellsData.BeforeCombatMagic.Add(temp);
                    }
                    Pos = hold.IndexOf(PathfinderConstants.ITACLIC, Pos2);
                }
            }

            if (Pos3 >= 0)
            {
                text = _sbCheckerBaseInput.MonsterSB.FullText;
                Pos3 = text.IndexOf("Permanent");
                int Pos4 = text.IndexOf(PathfinderConstants.EH5, Pos3); //end of block
                string hold = text.Substring(Pos3, Pos4 - Pos3);
                string temp = string.Empty;

                Pos3 = hold.IndexOf(PathfinderConstants.ITACLIC);

                while (Pos3 >= 0)
                {
                    Pos4 = hold.IndexOf(PathfinderConstants.EITACLIC, Pos3);
                    temp = hold.Substring(Pos3, Pos4 - Pos3);
                    temp = temp.Replace(PathfinderConstants.ITACLIC, string.Empty);
                    temp = temp.Replace(PathfinderConstants.EITACLIC, string.Empty).Trim();
                    temp = temp.Replace("potions", "potion");
                    if (temp != "permanency")
                    {
                        _spellsData.BeforeCombatMagic.Add(temp);
                    }
                    Pos3 = hold.IndexOf(PathfinderConstants.ITACLIC, Pos4);
                }
            }
        }
    }
}
