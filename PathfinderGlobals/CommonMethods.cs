using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderGlobals
{
    public static class CommonMethods
    {
        public static string GetStringValue(int value)
        {
            return PathfinderConstants.SPACE + GetPlusSign(value) + value.ToString();
        }

        public static string GetPlusSign(int value)
        {
            return value >= 0 ? "+" : string.Empty;
        }

        public static List<string> GetNPCClasses()
        {
            return new List<string> { "Adept", "Aristocrat", "Commoner", "Expert", "Warrior" };
        }

        public static string Get_CSS_Ref()
        {
            return "<link rel=" + PathfinderConstants.QUOTE + "stylesheet" + PathfinderConstants.QUOTE + "href=" +
                PathfinderConstants.QUOTE + "PF.css" + PathfinderConstants.QUOTE + ">";
        }

        public static void GoodLineBreaks(ref string list)
        {
            List<string> ordinal = GetOrdinalLevelList();
            int RightParen, Pos;

            foreach (string ord in ordinal)
            {
                Pos = list.IndexOf(ord);
                RightParen = list.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos + 3);
                if (Pos >= 0 && Pos > RightParen)
                {
                    if (!list.Contains(Environment.NewLine + ord) && list.Contains(ord))
                    {
                        list = list.Replace(ord, Environment.NewLine + ord);
                    }
                }
            }
        }

        public static bool FindNumber(string numberString)
        {
            List<string> tempSource = numberString.Split(' ').ToList();
            bool findNumber = false;
            foreach (string TS in tempSource)
            {
                try
                {
                    string temp5 = TS.Replace(",", string.Empty);
                    int dummy = Convert.ToInt32(temp5);
                    findNumber = true;
                    break;
                }
                catch { }
            }
            return findNumber;
        }

        public static string KeepCRs(string temp, string CR)
        {
            //monster SB
            //mark the keeper CRs
            temp = temp.Replace(PathfinderConstants.PAREN_RIGHT + CR, ")<br>")
                .Replace("D" + CR, "D<br>");
            for (int a = 0; a <= 9; a++)
            {
                temp = temp.Replace(CR + a.ToString(), PathfinderConstants.BREAK + a.ToString());
            }
            temp = temp.Replace("DC<br>", "DC ")
                .Replace(CR + "At Will", "<br>At Will")
                .Replace(CR + "At will", "<br>At Will");

            //remove the unwanted CRs
            temp = temp.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            temp = temp.Replace(PathfinderConstants.BREAK, CR);
            return temp;
        }

        public static string KeepCRsIndividual(string temp, string CR)
        {
            //mark the keeper CRs
            temp = temp.Replace(PathfinderConstants.PAREN_RIGHT + CR, ")<br>");

            //remove the unwanted CRs
            temp = temp.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            temp = temp.Replace(PathfinderConstants.BREAK, CR);
            return temp;
        }

        public static int MinutesToRounds(int minutes)
        {
            if (minutes < 0) minutes = 0;
            return PathfinderConstants.ROUNDS_PER_MINUTE * minutes;
        }

        public static string FindListContains(string find, List<string> temp)
        {
            if (temp.Contains(find)) return find;
            //foreach (string item in temp)
            //{
            //    if (find.IndexOf(item) >= 0) return item;
            //}

            return string.Empty;
        }

        public static List<string> GetSizes()
        {
            return new List<string> { "Fine", "Diminutive", "Tiny", "Small", "Medium", "Large", "Huge", "Gargantuan", "Colossal" };
        }

        public static List<string> GetAlignmentsPlusSpace()
        {
            var alignments = GetAlignments();
            alignments.ForEach(x => x += PathfinderConstants.SPACE);
            return alignments;
            //return new List<string> {PathfinderConstants.LE + PathfinderConstants.SPACE,PathfinderConstants.CE + PathfinderConstants.SPACE,
            //                         PathfinderConstants.NE + PathfinderConstants.SPACE, PathfinderConstants.NG + PathfinderConstants.SPACE,
            //                         PathfinderConstants.LG + PathfinderConstants.SPACE, PathfinderConstants.CG + PathfinderConstants.SPACE,
            //                         PathfinderConstants.LN + PathfinderConstants.SPACE, PathfinderConstants.CN + PathfinderConstants.SPACE,
            //                         PathfinderConstants.NE + PathfinderConstants.SPACE };
        }

        public static List<string> GetAlignments()
        {
            return new List<string> { PathfinderConstants.LE, PathfinderConstants.CE, PathfinderConstants.NE, 
                PathfinderConstants.NG, PathfinderConstants.LG, PathfinderConstants.CG, PathfinderConstants.LN, 
                PathfinderConstants.CN, PathfinderConstants.NE, PathfinderConstants.N };
        }

        public static List<string> GetGoodAlignments()
        {
            return new List<string> { PathfinderConstants.LG, PathfinderConstants.NG, PathfinderConstants.CG };
        }

        public static List<string> GetEvilAlignments()
        {
            return new List<string> { PathfinderConstants.LE, PathfinderConstants.NE, PathfinderConstants.CE };
        }

        public static List<string> GetNonGoodAlignments()
        {
            return new List<string> { PathfinderConstants.LN, PathfinderConstants.NE, PathfinderConstants.CN, 
                PathfinderConstants.LE, PathfinderConstants.NE, PathfinderConstants.CE };
        }

        public static List<string> GetNonEvilAlignments()
        {
            return new List<string> { PathfinderConstants.LG, PathfinderConstants.NG, PathfinderConstants.CG, 
                PathfinderConstants.LN, PathfinderConstants.NE, PathfinderConstants.CN };
        }

        public static List<string> GetNonLawfulAlignments()
        {
            return new List<string> { PathfinderConstants.NG, PathfinderConstants.CG, PathfinderConstants.NE,
                PathfinderConstants.CN, PathfinderConstants.NE, PathfinderConstants.CE };
        }

        public static List<string> GetLawfulAlignments()
        {
            return new List<string> { PathfinderConstants.LN, PathfinderConstants.LE, PathfinderConstants.LG };
        }

        public static List<string> GetNeutralAlignments()
        {
            return new List<string> { PathfinderConstants.NG, PathfinderConstants.NE, PathfinderConstants.CN, 
                PathfinderConstants.NE, PathfinderConstants.LN };
        }

        public static List<string> GetAllButTrueNeutralAlignments()
        {
            return new List<string> { PathfinderConstants.LE, PathfinderConstants.CE, PathfinderConstants.NE, 
                PathfinderConstants.NG, PathfinderConstants.LG, PathfinderConstants.CG, PathfinderConstants.LN, PathfinderConstants.CN };
        }

        public static List<string> GetSpellSchools()
        {
            return new List<string> { "Abjuration", "Conjuration", "Divination", "Enchantment", "Illusion", "Evocation", "Necromancy", "Transmutation", "Universal" };
        }

        public static List<string> GetClasses()
        {
            return new List<string> { "Ex-Paladin", "Ex-Druid", "Ex-Cleric", "Ex-Monk", "Ex-Barbarian","Ex-Warpriest",
                                       "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin",
                                       "Ranger", "Rogue", "Sorcerer","Investigator","Swashbuckler","Arcanist","Brawler","Hunter","Shaman","Skald",
                                       "Slayer","Warpriest","Vigilante",
                                       "Air Elementalist Wizard", "Earth Elementalist Wizard","Fire Elementalist Wizard","Water Elementalist Wizard",
                                       "Air Elementalist", "Earth Elementalist","Fire Elementalist","Water Elementalist",
                                       "Metal Elementalist Wizard","Wood Elementalist Wizard",
                                       "Thassilonian Conjurer",
                                       "Wizard", "Necromancer", "Diviner", "Evoker", "Abjurer",
                                       "Transmuter", "Enchanter",  "Conjurer",  "Illusionist", "Magus", "Samurai", "Ninja", "Gunslinger",
                                       "Alchemist", "Cavalier", "Inquisitor", "Oracle","Unchained Summoner" ,"Summoner", "Witch", "Antipaladin","Animal Companion","Bloodrager",
                                       "Hydrokineticist", "Kineticist","Occultist","Psychic","Mesmerist","Universalist","Eidolon", "Pyrokineticist","Medium","Spiritualist"};
        }

        public static List<string> GetPrestigeClasses()
        {
            return new List<string> { "Arcane Archer", "Arcane Trickster", "Assassin", "Dragon Disciple",
                      "Duelist", "Eldritch Knight", "Loremaster", "Mystic Theurge", "Pathfinder Chronicler",
                      "Shadowdancer", "Harrower" ,"Low Templar","Red Mantis Assassin","Shackles Pirate","Lion Blade",
                      "Spherewalker","Ashvawg Tamer", "Hellknight Signifer","Hellknight", "Student of War", "Pathfinder Savant",
                       "Pathfinder Delver", "Steel Falcon", "Bloatmage", "Demoniac", "Master Spy","Exalted","Umbral Court Agent",
                        "Holy Vindicator","Ex-Brother Of The Seal","Brother Of The Seal","Zealot Of Orcus","Archwizard" };
        }

        public static List<string> GetGreenRoninClasses()
        {
            return new List<string> { "Thaumaturge", "Unholy Warrior" };
        }

        public static List<string> GetSimpleClassTemplates()
        {
            //moster codex
            return new List<string> {"Barbarian","Bard","Cleric","Druid","Fighter","Monk",
                "Paladin" , "Ranger" , "Rogue" , "Sorcerer" , "Wizard"  };
        }

        public static List<string> GetCommonTemplates()
        {
            //these are templates where the words appear often as part of race name, like gaint
            //so we have a sperarete list
            return new List<string> { "Giant", "Nightmare" };
        }

        public static List<string> GetSkeletonTemplates()
        {
            return new List<string> { "Bloody", "Burning", "Acid", "Frost", "Electric", "Exploding", "Host", "Dread" };
        }

        public static List<string> GetZombieTemplates()
        {
            return new List<string> { "Fast", "Plague", "Void", "Yellow Musk", "JuJu", "Relentless", "Brain-Eating", "Hungry", "Pyre" };
        }

        public static List<string> GetAllClasses()
        {
            List<string> temp = GetPrestigeClasses();
            temp.AddRange(GetClasses());
            temp.AddRange(CommonMethods.GetNPCClasses());
            temp.AddRange(GetGreenRoninClasses());
            return temp;
        }

        public static List<string> GetMagicItemNouns()
        {
            return new List<string> {"rod ","wand ", "ring of ", "staff ", "potion ", "cloak of ", "dust of ", "bag of ", "circlet of ",
                 "goggles of ", "potions ", "vest of ", "sandals of ", "pearl of ", "hat of ", "oil of ","oils of ", "boots of ",
                 "scroll of ", "gem of ", "decanter of ", "scrolls of ", "elixir of ", "rope of " ,"phylactery of",
                 "quiver of", "salve of", "gauntlets of", "gloves of", "cape of", "ioun stone", "periapt of",
                 "mask of", "helm of", "horn of", "bracers of", "slippers of", "necklace of", "robe of", "brooch of ",
                 "rapier of", "amulet of ", "bracelet of " };
        }

        public static List<string> GetAgeCategories()
        {
            //elder for giants
            return new List<string> { "Young", "Middle Age", "Old", "Venerable", "Elder", "Ancient" };
        }

        public static List<string> GetDragonAgeCategories()
        {
            return new List<string> { "Wyrmling", "Very young", "Young", "Juvenile", "Young adult", "Mature adult", "Adult", "Very old", "Old ", "Ancient", "Great wyrm", "Wyrm" };
        }

        public static List<string> GetWizardFamiliarList()
        {
            return new List<string> { "bat", "cat", "centipede", "compsognathus", "donkey rat", "fox", "goat", "hedgehog", "crab", "lizard", "monkey", "octopus",
                  "owl", "pig", "rat", "raven", "parrot", "scorpion","greensting scorpion", "spider", "thrush", "toad", "turtle", "turtle, snapping",
                  "viper", "weasel","cacodaemon","quasit" };
        }

        public static List<string> GetWitchFamiliarList()
        {
            return new List<string> { "bat", "cat", "centipede", "compsognathus", "donkey rat", "fox", "goat", "hedgehog", "crab", "lizard", "monkey", "octopus",
                  "owl", "pig", "rat", "raven", "parrot", "scorpion","greensting scorpion", "spider", "thrush", "toad", "turtle",
                  "turtle, snapping", "viper", "weasel","cacodaemon","wren","snake","quasit","beheaded","compsognathus" };
        }

        public static List<string> GetOrdinalLevelList()
        {
            return new List<string> { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th" };
        }

        public static List<string> GetPerformValues(string PerformanceGroup)
        {
            switch (PerformanceGroup)
            {
                case "wind":
                case "wind instruments":
                    return new List<string> { "flute", "panpipes", "horn" };
                case "string":
                    return new List<string> { "harp", "kithara" };
                case "percussion":
                    return new List<string> { "drums" };
            }
            return new List<string>();
        }

        public static List<string> GetAmuletOfMightFistsSpecialAbilities()
        {
            return new List<string> { "shock", "corrosive" };
        }

        public static List<string> GetSpellSubSchools()
        {
            return new List<string> { "calling", "creation", "healing", "summoning", "teleportation", "scrying", "charm", "compulsion", "figment" ,
                 "glamer","pattern","phantasm","shadow","polymorph","haunted"};
        }

        //be sure to add new spell field and parse in spell
        public static List<string> GetSpellDescriptors()
        {
            return new List<string> { "acid", "air", "chaotic", "cold", "curse", "darkness", "death", "disease", "earth", "electricity", "emotion",
                "evil","fear","fire","force","good","language-dependent","lawful","light","mind-affecting","pain","poison","shadow","sonic",
                 "water","ruse","draconic","meditative"};
        }


        public static List<string> GetWizardSpecializations()
        {
            return new List<string> { "Necromancer", "Diviner", "Evoker", "Abjurer", "Transmuter", "Enchanter", "Conjurer", "Illusionist" };
        }


        public static Dictionary<string, string> GetDeityProficiencies()
        {
            return new Dictionary<string, string>()
            {
                {"abraxas", "flail"},
                {"abadar", "light crossbow"},
                {"ahriman", "whip"},
                {"angazhan", "spear"},
                {"areshkagal", "sickle"},
                {"aroden", "longsword"},
                {"asmodeus", "mace"},
                {"baphomet", "glaive"},
                {"besmara", "rapier"},
                {"brigh", "light hammer"},
                {"calistria", "whip"},
                {"cayden cailean", "rapier"},
                {"deskari", "scythe"},
                {"desna", "starknife"},
                {"droskar", "light hammer"},
                {"erastil", "longbow"},
                {"kabriri", "flail"},
                {"kelizandri","trident"},
                {"gozreh", "trident"},
                {"gorum", "greatsword"},
                {"groetus", "heavy flail"},
                {"hadregash", "flail"},
                {"haggakal", "greatclub"},
                {"hastur", "rapier"},
                {"hshurha","longbow"},
                {"iomedae", "longsword"},
                {"shyka","light mace" },
                {"lamashtu", "falchion"},
                {"lissala", "whip"},
                {"mestama", "punching dagger"},
                {"milani", "morningstar"},
                {"moloch", "whip"},
                {"minderhal", "hammer"},
                {"mahathallah", "net"},
                {"nethys", "quaterstaff"},
                {"nocticula", "hand crossbow"},
                {"norgorber", "short sword"},
                {"the old cults", ""},
                {"orcus", "heavy mace"},
                {"pazuzu", "longsword"},
                {"pharasma", "dagger"},
                {"rovagug", "greataxe"},
                {"sekhmet","battleaxe"},
                {"shax", "dagger"},
                {"shelyn", "glaive"},
                {"sifkesh", "war razor"},
                {"sarenrae", "scimitar"},
                {"torag", "warhammer"},
                {"the peacock spirit","lucerne hammer" },
                {"urgathoa", "scythe"},
                {"vapula", "quaterstaff"},
                // {"wadjet", ""}, ap 80, not here yet
                {"xhamen-dor","spear" },
                {"yamasoth","halberd" },
                {"zarongel", "dogslicer"},
                {"zursvaater", "greatsword"},
                {"zon-kuthon", "spiked chain"},
                {"zura","rapier"},
                {"zyphus","heavy pick"}
            };
        }


        public static List<string> GetInquisitions()
        {
            return new List<string> {"Anger","Banishment","Black Powder","Chivalry","Conversion","Damnation","Excommunication","Fate","Fervor",
                               "Final Rest","Heresy","Illumination","Imprisonment","Justice","Oblivion","Order","Persistence","Possession",
                               "Recovery","Redemption","Reformation","Restoration","Revelation","Sin","Spellkiller","Tactics",
                               "Torture","True Death","Truth","Valor","Vengeance","Zeal"};
        }

        public static List<string> GetCreatureSubTypes()
        {
            return new List<string>   {"air","angel","aquatic","archon","augmented","azata","chaotic","cold","demon","devil","dwarf","earth",
                "elemental","elf","evil","extraplanar","fire","giant","gnome","goblinoid","good","halfling","human","incorporeal","lawful",
                "native","orc","reptilian","shapechanger","swarm","water","fey","mythic" ,"humanoid","ratfolk","clockwork",
                "colossus" ,"daemon","dark folk","dhampir","tengu","grippli","boggard","inevitable","gnoll","agathion","vishkanya","dragon","hive","kyton",
                "changeling","great old one","protean","animal","kuru","kami","oni","psychopomp","derro","outsider","sahkil","oread","div"
                ,"leshy","manasaputra","wayang","blight","kaiju","munavri","qlippoth","troop","wild hunt","plant","asura","nightshade","rakshasa"
            ,"magical beast","aberration"};
        }


        public static List<string> GetFrogGodBooks()
        {

            return new List<string> { "Tome of Horrors Complete", "Tome of Horrors 4", "Rappan Athuk", "Sword of Air", "The Lost City of Barakus", "Slumering Tsar" };
        }

        public static List<string> GetBaseRaceNames()
        {
            //ParseRace()
            return new List<string> { " div", " angel", " inevitable", " agathion" };
        }

        public static Dictionary<string, int> GetDeedsByLevel()
        {
            return new Dictionary<string, int>
            {
                {"deadeye",1},
                {"gunslinger's dodge",1},
                {"quick clear",1},
                {"gunslinger initiative",3},
                {"pistol-whip",3},
                {"utility shot",3},
                {"dead shot",7},
                {"startling shot",7},
                {"targeting",7},
                {"bleeding wound",11},
                {"expert loading",11},
                {"lightning reload",11},
                {"evasive ",15},
                {"menacing shot",15},
                {"slinger's luck ",15},
                {"cheat death ",19},
                {"death's shot ",19},
                {"stunning shot ",19}
            };
        }
    }
}
