using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using PathfinderGlobals;

namespace StatBlockCommon.Spell_SB
{   
    [Serializable]
    [XmlRootAttribute(ElementName = "Spell", IsNullable = false)]
    public class SpellStatBlock : ISpellStatBlock
    {

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string school { get; set; }
        public string subschool { get; set; }
        public string descriptor { get; set; }
        public string spell_level { get; set; }
        public string casting_time { get; set; }
        public string components { get; set; }
        public bool costly_components { get; set; }
        public string range { get; set; }
        public string area { get; set; }
        public string effect { get; set; }
        public string targets { get; set; }
        public string duration { get; set; }
        public bool dismissible { get; set; }
        public bool shapeable { get; set; }
        public string saving_throw { get; set; }
        public string spell_resistence { get; set; }
        public string description { get; set; }
        public string description_formated { get; set; }
        public string source { get; set; }
        public string full_text { get; set; }
        public bool verbal { get; set; }
        public bool somatic { get; set; }
        public bool material { get; set; }
        public bool focus { get; set; }
        public bool divine_focus { get; set; }
        public System.Nullable<int> sor { get; set; }
        public System.Nullable<int> wiz { get; set; }
        public System.Nullable<int> cleric { get; set; }
        public System.Nullable<int> druid { get; set; }
        public System.Nullable<int> ranger { get; set; }
        public System.Nullable<int> bard { get; set; }
        public System.Nullable<int> paladin { get; set; }
        public System.Nullable<int> alchemist { get; set; }
        public System.Nullable<int> summoner { get; set; }
        public System.Nullable<int> witch { get; set; }
        public System.Nullable<int> inquisitor { get; set; }
        public System.Nullable<int> oracle { get; set; }
        public System.Nullable<int> magus { get; set; }
        public System.Nullable<int> antipaladin { get; set; }
        public System.Nullable<int> adept { get; set; }
        public System.Nullable<int> SLA_Level { get; set; }
        public string deity { get; set; }
        public string linktext { get; set; }
        public string domain { get; set; }
        public string short_description { get; set; }
        public System.Nullable<bool> acid { get; set; }
        public System.Nullable<bool> air { get; set; }
        public System.Nullable<bool> chaotic { get; set; }
        public System.Nullable<bool> cold { get; set; }
        public System.Nullable<bool> curse { get; set; }
        public System.Nullable<bool> darkness { get; set; }
        public System.Nullable<bool> death { get; set; }
        public System.Nullable<bool> disease { get; set; }
        public System.Nullable<bool> earth { get; set; }
        public System.Nullable<bool> electricity { get; set; }
        public System.Nullable<bool> emotion { get; set; }
        public System.Nullable<bool> evil { get; set; }
        public System.Nullable<bool> fear { get; set; }
        public System.Nullable<bool> fire { get; set; }
        public System.Nullable<bool> force { get; set; }
        public System.Nullable<bool> good { get; set; }
        public System.Nullable<bool> language_dependent { get; set; }
        public System.Nullable<bool> lawful { get; set; }
        public System.Nullable<bool> light { get; set; }
        public System.Nullable<bool> mind_affecting { get; set; }
        public System.Nullable<bool> pain { get; set; }
        public System.Nullable<bool> poison { get; set; }
        public System.Nullable<bool> shadow { get; set; }
        public System.Nullable<bool> sonic { get; set; }
        public System.Nullable<bool> water { get; set; }
        public System.Nullable<int> material_costs { get; set; }
        public string mythic_text { get; set; }
        public bool mythic { get; set; }
        public string augmented { get; set; }
        public System.Nullable<int> bloodrager { get; set; }
        public System.Nullable<int> shaman { get; set; }
        public System.Nullable<int> psychic { get; set; }
        public System.Nullable<int> medium { get; set; }
        public System.Nullable<int> mesmerist { get; set; }
        public System.Nullable<int> occultist { get; set; }
        public System.Nullable<int> spiritualist { get; set; }
        public System.Nullable<int> skald { get; set; }
        public System.Nullable<int> investigator { get; set; }
        public System.Nullable<int> hunter { get; set; }
        public string haunt_statistics { get; set; }
        public System.Nullable<bool> ruse { get; set; }
        public System.Nullable<bool> draconic { get; set; }
        public System.Nullable<bool> meditative { get; set; }

        #endregion Properties

        public SpellStatBlock()
        {           
            name = string.Empty;
            school = string.Empty;
            subschool = string.Empty;
            descriptor = string.Empty;
            spell_level = string.Empty;
            casting_time = string.Empty;
            components = string.Empty;
            costly_components = false;
            range = string.Empty;
            area = string.Empty;
            effect = string.Empty;
            targets = string.Empty;
            duration = string.Empty;
            dismissible = false;
            shapeable = false;
            saving_throw = string.Empty;
            spell_resistence = string.Empty;
            description = string.Empty;
            description_formated = string.Empty;
            source = string.Empty;
            full_text = string.Empty;
            verbal = false;
            somatic = false;
            material = false;
            focus = false;
            divine_focus = false;
            sor = null;
            cleric = null;
            druid = null;
            ranger = null;
            bard = null;
            paladin = null;
            wiz = null;
            alchemist = null;
            summoner = null;
            witch = null;
            oracle = null;
            inquisitor = null;
            antipaladin = null;
            magus = null;
            deity = string.Empty;
            SLA_Level = null;
            acid = false;
            air = false;
            chaotic = false;
            cold = false;
            curse = false;
            darkness = false;
            death = false;
            disease = false;
            earth = false;
            electricity = false;
            emotion = false;
            evil = false;
            fear = false;
            fire = false;
            force = false;
            good = false;
            language_dependent = false;
            lawful = false;
            light = false;
            mind_affecting = false;
            pain = false;
            poison = false;
            shadow = false;
            sonic = false;
            water = false;
            material_costs = null;
            mythic_text = string.Empty;
            mythic = false;
            augmented = string.Empty;
            short_description = string.Empty;
            adept = null;
            bloodrager = null;
            shaman = null;
            psychic = null;
            medium = null;
            mesmerist = null;
            occultist = null;
            spiritualist = null;
            skald = null;
            investigator = null;
            hunter = null;
            haunt_statistics = string.Empty;
            ruse = false;
            draconic = false;
            meditative = false;     
        }

        public int GetSpellLevel()
        {
            //not always correct
            List<string> levels = spell_level.Split(',').ToList();
            int Pos = 0;
            string temp = string.Empty;
            int spellLevel = 0;

            foreach (string level in levels)
            {
                temp = level;
                Pos = temp.LastIndexOf(PathfinderConstants.SPACE);
                temp = temp.Substring(Pos).Trim();
                spellLevel = Convert.ToInt32(temp);
            }

            return spellLevel;
        }

        public int GetSpellLevelByClass(string className, bool HasCurseHaunted, bool HasShadeOfTheUskwood, bool HasRazmiranPriest)
        {
            int level = -100;

            switch (className.ToLower())
            {
                case "alchemist":
                case "investigator":
                    level = alchemist ?? -100;
                    break;
                case "antipaladin":
                    level = antipaladin ?? -100;
                    break;
                case "adept":
                    level =  adept ?? -100; 
                    break;
                case "bard":
                case "skald":
                    level = bard ?? -100;
                    break;
                case "cleric":
                    level = cleric ?? -100;
                    break;
                case "druid":
                    level = druid ?? -100;
                    if (HasShadeOfTheUskwood && level == -100)
                    {
                        switch (this.name.ToLower())
                        {
                            case "disrupt undead":
                            case "ray of frost":
                                level = 0;
                                break;
                            case "ghost sound":
                            case "touch of fatigue":
                                level = 1;
                                break;
                            case "chill touch":
                            case "spectral hand":
                                level = 2;
                                break;
                            case "ghoul touch":
                            case "invisibility":
                                level = 3;
                                break;
                            case "displacement":
                            case "ray of exhaustion":
                                level = 4;
                                break;
                            case "animate dead":
                            case "phantasmal killer":
                                level = 5;
                                break;
                            case "nightmare":
                            case "waves of fatigue":
                                level = 6;
                                break;
                            case "circle of death":
                            case "shadow walk":
                                level = 7;
                                break;
                            case "mass invisibility":
                            case "waves of exhaustion":
                                level = 8;
                                break;
                            case "horrid wilting":
                            case "weird":
                                level = 9;
                                break;
                        }
                    }
                    break;
                case "inquisitor":
                    level = inquisitor ?? -100;
                    break;
                case "magus":
                    level = magus ?? -100;
                    break;
                case "medium":
                    level = medium ?? -100;
                    break;
                case "mesmerist":
                    level = mesmerist ?? -100;
                    break;
                case "occultist":
                    level = occultist ?? -100;
                    break;
                case "oracle":
                    level = oracle ?? -100;
                    if (HasCurseHaunted && level == -100)
                    {
                        switch (this.name.ToLower())
                        {
                            case "mage hand":
                            case "ghost sound":
                                level = 0;
                                break;
                            case "levitate":
                            case "minor image":
                                level = 2;
                                break;
                            case "telekinesis":
                                level = 5;
                                break;
                            case "reverse gravity":
                                level = 7;
                                break;
                        }
                    }
                    break;
                case "ranger":
                    level = ranger ?? -100;
                    break;
                case "red mantis assassin":
                    level = sor ?? -100;
                    break;
                case "paladin":
                    level = paladin ?? -100;
                    break;
                case "spiritualist":
                    level = spiritualist ?? -100;
                    break;
                case "sorcerer":
                case "arcanist":                
                    level = sor ?? -100;
                    if (HasRazmiranPriest && level == -100)
                    {
                        if (this.name.ToLower() == "remove disease") level = 3;
                        if (this.name.ToLower() == "aid") level = 1;
                    }
                    break;
                case "summoner":
                    level = summoner ?? -100;
                    break;
                case "witch":
                    level = witch ?? -100;
                    break;
                case "bloodrager":
                    level = bloodrager ?? -100;
                    break;
                case "shaman":
                    level = shaman ?? -100;
                    break;
                case "wizard":
                case "enchanter":
                case "diviner":
                case "transmuter":
                case "evoker":
                case "illusionist":
                case "necromancer":
                case "conjurer":
                case "abjurer":
                case "universalist":
                case "thassilonian conjurer":
                    level = wiz ?? -100;
                    break;
                case "warpriest":
                    level = cleric ?? -100;
                    break;
                case "psychic":
                    level = psychic ?? -100;
                    break;
                case "hunter":
                    level = hunter ?? -100;
                    break;
                default:
                    level = -2000;
                    break;
            }

            return level;
        }   
    }
}
