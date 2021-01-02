using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EquipmentBasic;
using CommonInterFacesDD;
using CommonStatBlockInfo;
using StatBlockCommon.MagicItem_SB;
using Utilities;
using EquipmentBusiness;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon;

namespace StatBlockParsing
{
    public static class Equipment_Parse
    {
        private static Dictionary<IEquipment, int> EquipementRoster;
        private static StatBlockGlobals.AC_Values Indvid_AC_Values;
        private static IMagicItemStatBlockBusiness _magicItemStatBlockBusiness;
        private static IWeaponBusiness _weaponBusiness;
        private static IArmorBusiness _armorBusiness;
        private static IEquipmentGoodsBusiness _equipmentGoodsBusiness;

        public struct ArmorProperties
        {
            public bool Masterwork;
            public bool Broken;
            public bool Darkwood;
            public bool Ironwood;
            public bool Mithral;
            public bool Adamantine;
            public bool Spiked;
            public bool EelHide;
            public bool Bone;
        }

        public static Dictionary<IEquipment, int> ParseEquipment(string Gear, string OtherGear, Dictionary<IEquipment, int> EquipementRoster_In,
                          ref StatBlockGlobals.AC_Values Indvid_AC_Values_In)
        {
            EquipementRoster = EquipementRoster_In;
            if (Gear == null || Gear.Length == 0) return EquipementRoster_In;
            if (Gear.IndexOf("CL ") >= 0)
            {
                HandleCLCommas(ref Gear);
            }
            List<string> gearHold = Gear.Split(',').ToList();
            if (OtherGear != null)
            {
                gearHold.AddRange(OtherGear.Split(',').ToList());
            }
            gearHold.RemoveAll(x => x == string.Empty);
            for (int a = 0; a < gearHold.Count - 1; a++)
            {
                gearHold[a] = gearHold[a].Trim();
            }
            ParseEquipmentBasic(gearHold);
            List<string> temp = FindMagicItems(gearHold);
            //ApplyMagicItem(string.Empty);
            ParseArmor(gearHold);

            Indvid_AC_Values_In = Indvid_AC_Values;
            return EquipementRoster;
        }

        public static Dictionary<IEquipment, int> ParseMagicItems(List<string> Gear, Dictionary<IEquipment, int> EquipementRoster_In,
            IMagicItemStatBlockBusiness magicItemStatBlockBusiness, IWeaponBusiness weaponBusiness, IArmorBusiness armorBusiness,
            IEquipmentGoodsBusiness equipmentGoodsBusiness)
        {
            EquipementRoster = EquipementRoster_In;
            _magicItemStatBlockBusiness = magicItemStatBlockBusiness;
            _armorBusiness = armorBusiness;
            _weaponBusiness = weaponBusiness;
            _equipmentGoodsBusiness = equipmentGoodsBusiness;
            _ = FindMagicItems(Gear);
            return EquipementRoster;
        }

        private static void HandleCLCommas(ref string Gear)
        {
            int Pos = Gear.IndexOf("CL ");
            int CommaPos;

            while (Pos >= 0)
            {
                CommaPos = Gear.IndexOf(", ", Pos);
                Gear = Gear.Substring(0, CommaPos) + "|" + Gear.Substring(CommaPos + 1);
                Pos = Gear.IndexOf("CL ", CommaPos);
            }
        }

        public static Dictionary<IEquipment, int> ParseWeaponsSimple(List<string> gearHold, IWeaponBusiness weaponBusiness, IMagicItemStatBlockBusiness magicItemStatBlockBusiness)
        {
            _weaponBusiness = weaponBusiness;
            _magicItemStatBlockBusiness = magicItemStatBlockBusiness;
            List<string> hold = new List<string>(gearHold);
            Dictionary<IEquipment, int> EquipementRosterSimple = new Dictionary<IEquipment, int>();
            if (!gearHold.Any()) return EquipementRosterSimple;

            foreach (string item in hold)
            {
                try
                {
                    ParseOneSimpleWeapon(gearHold, EquipementRosterSimple, item);
                }
                catch (Exception ex)
                {
                    throw new Exception("ParseOneSimpleWeapon --" + ex.Message);
                }
            }
            return EquipementRosterSimple;
        }

        private static void ParseOneSimpleWeapon(List<string> gearHold, Dictionary<IEquipment, int> EquipementRosterSimple, string item)
        {
            int Pos;
            MagicItemStatBlock MI = null;
            bool spiked = false;
            string doulbeWeaponHold = string.Empty;
            string holdItem;

            Weapon weapon = null;
            int count = 1;
            bool doubleWeapon = item.Contains("/");
            holdItem = item;
            if (doubleWeapon)
            {
                if (item.Contains("cloak of quick reflexes")) return;
                //do one end
                doulbeWeaponHold = item;
                holdItem = item.Substring(item.IndexOf("/") + 1).Trim();
            }

            if (holdItem.Contains("(treat "))
            {
                Pos = holdItem.IndexOf("(treat ");
                holdItem = holdItem.Substring(0, Pos).Trim();
            }

            if (FindOneWeapon(holdItem, ref weapon, ref count, false, new ShieldSpecialAbilities(), ref spiked))
            {
                EquipementRosterSimple.Add(weapon, count);
                if (weapon.search_name.ToLower() == "halfling sling staff")
                {
                    if (FindOneWeapon("club", ref weapon, ref count, false, new ShieldSpecialAbilities(), ref spiked))
                    {
                        EquipementRosterSimple.Add(weapon, count);
                    }
                }

                if (doubleWeapon) //do other end
                {
                    doulbeWeaponHold = doulbeWeaponHold.Replace(holdItem, string.Empty);
                    doulbeWeaponHold = doulbeWeaponHold.Replace("/", string.Empty).Trim();
                    doulbeWeaponHold += PathfinderConstants.SPACE + weapon.search_name.ToLower();
                    weapon = null;
                    count = 1;
                    spiked = false;
                    if (FindOneWeapon(doulbeWeaponHold, ref weapon, ref count, false, new ShieldSpecialAbilities(), ref spiked))
                    {
                        EquipementRosterSimple.Add(weapon, count);
                    }

                }
                if (!spiked && !holdItem.Contains("shield")) gearHold.Remove(item);
            }
            else
            {
                //named weapon
                string temp = item;
                string baseASWeapon = string.Empty;
                bool AsWeapon = false;
                if (item.IndexOf("(as") >= 0)
                {
                    temp = item.Substring(item.IndexOf("(as"));
                    temp = temp.Replace("(as", string.Empty)
                        .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
                    AsWeapon = true;
                    baseASWeapon = item.Substring(0, item.IndexOf("(as")).Trim();

                }
                else
                {
                    Pos = item.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos >= 0) temp = temp.Substring(0, Pos).Trim();
                }
                MI = _magicItemStatBlockBusiness.GetMagicItemByName(temp);
                if (MI != null)
                {
                    if (MI.BaseItem.Length > 0)
                    {
                        bool fake = false;
                        if (FindOneWeapon(MI.BaseItem, ref weapon, ref count, false, new ShieldSpecialAbilities(), ref fake))
                        {
                            // weapon.BaseWeaponName = weapon.name;
                            weapon.BaseWeaponName = weapon.search_name;
                            weapon.Masterwork = true;

                            if (AsWeapon)
                            {
                                weapon.name = baseASWeapon;
                                weapon.search_name = baseASWeapon;
                            }
                            else
                            {
                                weapon.name = temp;
                                weapon.search_name = temp;
                            }
                            weapon.NamedWeapon = true;
                            EquipementRosterSimple.Add(weapon, count);
                            gearHold.Remove(item);
                        }
                    }
                }
            }
        }

        public static bool FindOneWeapon(string item, ref Weapon weapon, ref int count, bool Shield, ShieldSpecialAbilities ShieldSA,
                                       ref bool spikedFound)
        {
            bool Found = false;
            string temp = string.Empty;
            string tempEnchantBonus = string.Empty;
            string tempSize = string.Empty;
            int CompositeBonus = 0;
            bool Masterwork = false;
            bool Broken = false;
            bool Composite = false;
            WeaponSpecialMaterials weaponSpecialMaterials = WeaponSpecialMaterials.None;

            item = item.Replace("rusty", string.Empty).Trim();
            if (item.Contains("(as"))
            {
                item = item.Substring(item.IndexOf("(as"));
                item = item.Replace("(as", string.Empty)
                    .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            }

            bool SpikedArmor = false;
            if (item.Contains("armor spikes"))
            {
                int Pos2 = item.IndexOf("with ");
                if (Pos2 == -1) Pos2 = 0;
                string temp3 = item.Substring(Pos2);
                temp = "armor spikes";
                item = item.Replace(temp3, string.Empty);
                SpikedArmor = true;
            }
            else if (item.Contains("with "))
            {
                item = item.Substring(0, item.IndexOf("with "));
            }


            if (item.Contains(" and "))
            {
                item = item.Substring(0, item.IndexOf(" and "));
            }

            WeaponSpecialAbilities SA = ParseWeaponString(ref Masterwork, ref Broken, ref Composite, ref weaponSpecialMaterials,
                  ref CompositeBonus, ref temp, ref tempEnchantBonus, ref tempSize, ref count, item);

            temp = temp.Replace("swords", "sword").Replace("javelins", "javelin").Replace("sabres", "sabre")
                .Replace("starknives", "starknife").Replace("starknives", "starknife").Replace("*", string.Empty);
            int Pos = temp.IndexOf(PathfinderConstants.SPACE);
            if (Pos >= 1)
            {
                string temp2 = temp.Substring(0, Pos);
                if (int.TryParse(temp2, out count))
                {
                    temp = temp.Replace(temp2, string.Empty).Trim();
                }
            }
            weapon = _weaponBusiness.GetWeaponByName(temp);
            if (weapon != null)
            {
                Found = true;
                if (SpikedArmor)
                {
                    spikedFound = true;
                    weapon.name = "armor spikes";
                    weapon.search_name = "armor spikes";
                }
                weapon.Masterwork = Masterwork;
                weapon.EnhancementBonus = 0;
                if (Shield)
                {
                    weapon.ShieldSpecialAbilities = ShieldSA;
                }
                else
                {
                    weapon.WeaponSpecialAbilities = SA;
                }
                weapon.WeaponSpecialMaterial = weaponSpecialMaterials;
                if (tempEnchantBonus.Length > 0)
                {
                    try
                    {
                        tempEnchantBonus = tempEnchantBonus.Replace("+", string.Empty);
                        int bonus;
                        int.TryParse(tempEnchantBonus, out bonus);
                        weapon.EnhancementBonus = bonus;
                    }
                    catch
                    {
                        throw new Exception("FindOneWeapon: tempEnchantBonus is not numeric --" + tempEnchantBonus);
                    }
                    weapon.Masterwork = true;
                }
                weapon.Broken = Broken;
                if (tempSize.Length > 0) weapon.WeaponSize = StatBlockInfo.GetSizeEnum(tempSize);
                if (Composite) weapon.CompositeBonus = CompositeBonus;
            }

            return Found;
        }

        private static WeaponSpecialAbilities ParseWeaponString(ref bool Masterwork, ref bool Broken, ref bool Composite, ref WeaponSpecialMaterials weaponSpecialMaterials,
                 ref int CompositeBonus, ref string temp, ref string tempEnchantBonus, ref string tempSize, ref int count, string item)
        {
            int Pos;
            string temp2;
            Masterwork = false;
            Broken = false;
            tempEnchantBonus = string.Empty;
            temp = item;

            temp = temp.Replace("black flame", string.Empty);//Demon, Vrolikai only

            if (temp.Contains("masterwork"))
            {
                Masterwork = true;
                temp = temp.Replace("masterwork", string.Empty).Trim();
            }
            if (temp.Contains("masterwork"))
            {
                Masterwork = true;
                temp = temp.Replace("masterwork", string.Empty).Trim();
            }
            if (temp.Contains("mwk "))
            {
                Masterwork = true;
                temp = temp.Replace("mwk ", string.Empty).Trim();
            }
            if (temp.Contains("broken"))
            {
                Broken = true;
                temp = temp.Replace("broken", string.Empty).Trim();
            }
            if (temp.Contains("mithral"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Mithral;
                temp = temp.Replace("mithral", string.Empty).Trim();
            }
            if (temp.Contains("alchemical silver"))
            {
                weaponSpecialMaterials = WeaponSpecialMaterials.AlchemicalSilver;
                temp = temp.Replace("alchemical silver", string.Empty).Trim();
            }
            if (temp.Contains("silver"))
            {
                weaponSpecialMaterials = WeaponSpecialMaterials.AlchemicalSilver;
                temp = temp.Replace("silver", string.Empty).Trim();
            }
            if (temp.Contains("cold iron"))
            {
                weaponSpecialMaterials = WeaponSpecialMaterials.ColdIron;
                temp = temp.Replace("cold iron", string.Empty).Trim();
            }
            if (temp.Contains("adamantine"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Adamantine;
                temp = temp.Replace("adamantine", string.Empty).Trim();
            }
            if (temp.Contains("darkwood"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Darkwood;
                temp = temp.Replace("darkwood", string.Empty).Trim();
            }
            if (temp.Contains("greenwood"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Greenwood;
                temp = temp.Replace("greenwood", string.Empty).Trim();
            }
            if (temp.Contains("bronze"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Bronze;
                temp = temp.Replace("bronze", string.Empty).Trim();
            }
            if (temp.Contains("paint "))
            {
                temp = temp.Replace("paint ", string.Empty).Trim();
            }
            if (temp.Contains("siccatite"))
            {
                Masterwork = true;
                weaponSpecialMaterials = WeaponSpecialMaterials.Siccatite;
                temp = temp.Replace("siccatite", string.Empty).Trim();
            }
            if (temp.Contains("integrated"))
            {
                temp = temp.Replace("integrated", string.Empty).Trim();
            }

            temp = temp.Replace("glamered", string.Empty).Trim();

            if (temp.Contains("(invisible)"))
            {
                temp = temp.Replace("(invisible)", string.Empty).Trim();
            }


            if (temp.Contains("composite"))
            {
                Composite = true;
                Pos = temp.IndexOf("(+");
                if (Pos == -1)
                {
                    Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos == -1)
                    {
                        Pos = temp.IndexOf("[+");
                    }
                    else if (Pos != -1) Pos = temp.IndexOf(" +", Pos);
                }
                if (Pos >= 0)
                {
                    temp2 = temp.Substring(Pos);
                    temp = temp.Replace(temp2, string.Empty);
                    Pos = temp2.IndexOf(StatBlockInfo.STR);
                    if (Pos == -1)
                    {
                        Pos = temp2.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    }
                    temp2 = temp2.Substring(0, Pos);
                    temp2 = temp2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                        .Replace("[", string.Empty);
                    CompositeBonus = Convert.ToInt32(temp2);
                }
            }

            List<string> Sizes = CommonMethods.GetSizes();
            foreach (string size in Sizes)
            {
                if (temp.IndexOf(size) >= 0)
                {
                    tempSize = size;
                    temp = temp.Replace(size, string.Empty).Trim();
                    break;
                }
            }

            if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
            {
                temp2 = temp.Substring(temp.IndexOf(PathfinderConstants.PAREN_LEFT));
                temp = temp.Replace(temp2, string.Empty).Trim();
                if (temp.Substring(temp.Length - 1, 1) == "s")
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                temp2 = Utility.RemoveParentheses(temp2);

                try
                {
                    count = Convert.ToInt32(temp2);
                }
                catch
                {
                    count = 1;
                }
            }
            else
            {
                count = 1;
            }

            if (temp.Contains("+"))
            {
                temp2 = temp.Substring(0, temp.IndexOf(PathfinderConstants.SPACE));
                if (temp2.Length > 0 && temp2 != "ghost")
                {
                    temp = temp.Replace(temp2, string.Empty).Trim();
                    tempEnchantBonus = temp2;
                    Pos = tempEnchantBonus.IndexOf("/");
                    if (Pos >= 0) tempEnchantBonus = tempEnchantBonus.Substring(0, Pos).Trim();
                }
            }

            if (temp.Contains("with ")) temp = temp.Substring(0, temp.IndexOf("with ")).Trim();

            WeaponSpecialAbilities SA = ParseWeaponSpecialAbilities(ref temp);
            temp = temp.Replace("*", string.Empty).Trim();
            return SA;
        }

        public static Weapon GetBarbarianBite()
        {
            Weapon Bite = new Weapon();
            Bite.name = "bite";
            Bite.search_name = "bite";
            Bite.damage_medium = "1d4";
            Bite.damage_small = "1d3";

            return Bite;
        }

        public static Weapon GetBomb()
        {
            Weapon Bomb = new Weapon();
            Bomb.name = "bomb";
            Bomb.search_name = "bomb";
            Bomb.damage_medium = "1d6";
            Bomb.damage_small = "1d6";

            return Bomb;
        }

        public static Weapon AddRockAsWeapon()
        {
            Weapon Rock = new Weapon();
            Rock.name = "rock";
            Rock.search_name = "rock";
            Rock.damage_medium = "1d8";
            Rock.damage_small = "1d8";

            return Rock;
        }

        public static Weapon GetUnarmedStrikeWeapon(IWeaponBusiness weaponBusiness)
        {
            return weaponBusiness.GetWeaponByName("unarmed strike");
        }

        public static IncorporealTouch GetIncorporealTouchWeapon()
        {
            return new IncorporealTouch
            {
                name = "incorporeal touch",
                search_name = "incorporeal touch",
                damage_medium = "—",
                damage_small = "—"
            };
        }

        private static WeaponSpecialAbilities ParseWeaponSpecialAbilities(ref string weapon)
        {
            WeaponSpecialAbilities SA = new WeaponSpecialAbilities();
            List<string> SAList = new List<string>{"Anarchic","Axiomatic","Bane","Brilliant Energy","Dancing","Defending","Disruption",
                                                    "Distance","Evil", "Flaming Burst","Flaming","Frost","Ghost touch","good","Unholy", "Holy","Icy burst",
                                                    "Keen","Ki Focus","Merciful","Mighty cleaving","Outsider", "Returning","Seeking",
                                                    "Shocking burst","Shocking","Shock","Speed","Spell storing","Throwing","Thundering",
                                                    "Vicious","Vorpal","Wounding", "Grayflame","Furious","Thawing","Corrosive","Conductive", "Cruel","Berserking",
                                                     "Menacing","Heartseeker","Valiant","Dispelling","Furyborn","Impervious","Impact","Reliable","Planar","Jurist"};

            weapon = weapon.ToLower();
            if (weapon == "throwing axe" || weapon == "throwing shield") return SA;

            foreach (string ability in SAList)
            {
                var abilityLower = ability.ToLower();
                if (weapon.Contains(abilityLower))
                {
                    switch (abilityLower)
                    {
                        case "anarchic":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Anarchic);
                            break;
                        case "axiomatic":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Axiomatic);
                            break;
                        case "bane":
                            int Pos = weapon.IndexOf(" bane");
                            if (Pos == -1) Pos = weapon.IndexOf("-bane");
                            if (Pos == -1) break;
                            string temp = weapon.Substring(0, Pos).Trim();
                            weapon = weapon.Replace(temp, string.Empty)
                                .Replace("-bane", string.Empty)
                                .Replace(abilityLower, string.Empty).Trim();
                            SA.BaneFoe = temp;
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Bane);
                            break;
                        case "berserking":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Berserking);
                            break;
                        case "brilliant energy":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.BrilliantEnergy);
                            break;
                        case "dancing":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Dancing);
                            break;
                        case "defending":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Defending);
                            break;
                        case "disruption":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Disruption);
                            break;
                        case "distance":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Distance);
                            break;
                        case "evil":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Evil);
                            break;
                        case "flaming":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Flaming);
                            break;
                        case "flaming burst":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.FlamingBurst);
                            break;
                        case "frost":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Frost);
                            break;
                        case "furious":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Furious);
                            break;
                        case "ghost touch":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.GhostTouch);
                            break;
                        case "good":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Good);
                            break;
                        case "holy":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Holy);
                            break;
                        case "icy burst":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.IcyBurst);
                            break;
                        case "keen":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Keen);
                            break;
                        case "ki focus":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.KiFocus);
                            break;
                        case "merciful":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Merciful);
                            break;
                        case "mighty cleaving":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.MightyCleaving);
                            break;
                        case "outsider":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Outsider);
                            break;
                        case "returning":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Returning);
                            break;
                        case "seeking":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Seeking);
                            break;
                        case "shocking burst":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.ShockingBurst);
                            break;
                        case "shock":
                        case "shocking":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Shock);
                            break;
                        case "speed":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Speed);
                            break;
                        case "spell storing":
                        case "spell-storing":
                            weapon = weapon.Replace(abilityLower, string.Empty)
                                .Replace("spell-storing", string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.SpellStoring);
                            break;
                        case "throwing":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Throwing);
                            break;
                        case "thundering":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Thundering);
                            break;
                        case "unholy":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Unholy);
                            break;
                        case "vicious":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Vicious);
                            break;
                        case "vorpal":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Vorpal);
                            break;
                        case "wounding":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Wounding);
                            break;
                        case "grayflame":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Grayflame);
                            break;
                        case "thawing":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Thawing);
                            break;
                        case "corrosive":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Corrosive);
                            break;
                        case "conductive":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Conductive);
                            break;
                        case "cruel":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Cruel);
                            break;
                        case "menacing":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Menacing);
                            break;
                        case "heartseeker":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Heartseeker);
                            break;
                        case "valiant":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Valiant);
                            break;
                        case "dispelling":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Dispelling);
                            break;
                        case "furyborn":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Furyborn);
                            break;
                        case "impervious":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Impervious);
                            break;
                        case "impact":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Impact);
                            break;
                        case "reliable":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Reliable);
                            break;
                        case "planar":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Planar);
                            break;
                        case "jurist":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Jurist);
                            break;
                        case "called":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Called);
                            break;
                        case "vampiric":
                            weapon = weapon.Replace(abilityLower, string.Empty);
                            SA.AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum.Vampiric);
                            break;
                    }
                }
            }
            weapon = weapon.Trim();
            return SA;
        }

        private static ShieldSpecialAbilities ParseShieldSpecialAbilities(ref string shield)
        {
            ShieldSpecialAbilities SA = new ShieldSpecialAbilities();
            List<string> SAList = new List<string>{"Arrow Catching","Bashing","Blinding","Light Fortification",  "Moderate Fortification","Heavy Fortification",
                                       "Arrow Deflection","Animated","Spell Resistance","Energy Resistance", "Energy Resistance, Improved",
                                       "Energy Resistance, Greater", "Energy Resistance","Ghost Touch", "Wild","Undead Controlling","Reflecting", "Ramming"};


            foreach (string ability in SAList)
            {
                var abilityLower = ability.ToLower();
                if (shield.Contains(abilityLower))
                {
                    switch (abilityLower)
                    {
                        case "animated":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Animated);
                            break;
                        case "arrow catching":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.ArrowCatching);
                            break;
                        case "arrow deflection":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.ArrowDeflection);
                            break;
                        case "bashing":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Bashing);
                            break;
                        case "blinding":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Blinding);
                            break;
                        case "energy resistance":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.EnergyResistance);
                            break;
                        case "improved energy resistance":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.EnergyResistanceImproved);
                            break;
                        case "greater energy resistance":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.EnergyResistanceGreater);
                            break;
                        case "light fortification":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.LightFortification);
                            break;
                        case "moderate fortification":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.ModerateFortification);
                            break;
                        case "heavy fortification":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.HeavyFortification);
                            break;
                        case "ghost touch":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.GhostTouch);
                            break;
                        case "reflecting":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Reflecting);
                            break;
                        case "spell resistance":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.SpellResistance);
                            break;
                        case "undead controlling":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.UndeadControlling);
                            break;
                        case "wild":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Wild);
                            break;
                        case "ramming":
                            shield = shield.Replace(abilityLower, string.Empty);
                            SA.AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum.Ramming);
                            break;
                    }
                }
            }
            shield = shield.Trim();
            return SA;
        }

        private static ArmorSpecialAbilities ParseArmorSpecialAbilities(ref string armor)
        {
            ArmorSpecialAbilities SA = new ArmorSpecialAbilities();
            List<string> SAList = new List<string>{"Animated","Arrow Catching","Arrow Deflection","Bashing","Blinding","Energy Resistance",
                   "Energy Resistance, Improved","Improved Cold Resistance","Improved Fire Resistance","Energy Resistance, Greater","Etherealness","Light Fortification",
                    "Moderate Fortification","Heavy Fortification","Ghost Touch","Glamered","Unrighteous",
                    "Invulnerability","Reflecting","Shadow","Shadow, Improved","Shadow, Greater","Slick","Slick, Improved","Slick, Greater",
                     "Spell Resistance","Undead Controlling","Undead-Controlling","Wild","Acid Resistance", "Cold Resistance", "Electricity Resistance",
                     "Fire Resistance", "Sonic Resistance", "Defiant","Spell Storing","Brawling","Warding","Expeditious","Hosteling","Deathless" };

            foreach (string ability in SAList)
            {
                var abilityLower = ability.ToLower();
                if (armor.Contains(abilityLower))
                {
                    switch (abilityLower)
                    {
                        case "animated":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Animated);
                            break;
                        case "arrow catching":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.ArrowCatching);
                            break;
                        case "arrow deflection":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.ArrowDeflection);
                            break;
                        case "bashing":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Bashing);
                            break;
                        case "energy resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance);
                            break;
                        case "acid resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance_Acid);
                            break;
                        case "cold resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance_Cold);
                            break;
                        case "electricity resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance_Electricity);
                            break;
                        case "fire resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance_Fire);
                            break;
                        case "sonic resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistance_Sonic);
                            break;
                        case "improved energy resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistanceImproved);
                            break;
                        case "improved cold resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Imporoved_EnergyResistance_Cold);
                            break;
                        case "improved fire resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Imporoved_EnergyResistance_Fire);
                            break;
                        case "greater energy resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.EnergyResistanceGreater);
                            break;
                        case "etherealness":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Etherealness);
                            break;
                        case "light fortification":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.LightFortification);
                            break;
                        case "moderate fortification":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.ModerateFortification);
                            break;
                        case "heavy fortification":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.HeavyFortification);
                            break;
                        case "ghost touch":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.GhostTouch);
                            break;
                        case "glamered":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Glamered);
                            break;
                        case "invulnerability":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Invulnerability);
                            break;
                        case "reflecting":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Reflecting);
                            break;
                        case "shadow":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Shadow);
                            break;
                        case "greater shadow":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.ShadowGreater);
                            break;
                        case "improved shadow":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.ShadowImproved);
                            break;
                        case "slick":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Slick);
                            break;
                        case "greater slick":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.SlickGreater);
                            break;
                        case "improved slick":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.SlickImproved);
                            break;
                        case "spell resistance":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.SpellResistance);
                            break;
                        case "undead controlling":
                        case "undead-controlling":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.UndeadControlling);
                            break;
                        case "wild":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Wild);
                            break;
                        case "unrighteous":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Unrighteous);
                            break;
                        case "defiant":
                            int Pos2 = armor.IndexOf("defiant");
                            if (Pos2 == -1) Pos2 = armor.IndexOf("-defiant");
                            if (Pos2 == -1) break;
                            string temp2 = armor.Substring(0, Pos2).Trim();
                            if (temp2.Length > 0)//find foe
                            {
                                armor = armor.Replace(temp2, string.Empty);
                            }
                            else
                            {
                                Pos2 = armor.IndexOf(PathfinderConstants.PAREN_LEFT, Pos2);
                                if (Pos2 > 0)
                                {
                                    int Pos4 = armor.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos2);
                                    temp2 = armor.Substring(Pos2, Pos4 - Pos2 + 1).Trim();
                                    armor = armor.Replace(temp2, string.Empty);
                                    temp2 = Utility.RemoveParentheses(temp2);
                                }
                            }
                            armor = armor.Replace("-defiant", string.Empty)
                                .Replace(abilityLower, string.Empty).Trim();
                            SA.DefiantFoe = temp2;
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Defiant);
                            break;
                        case "spell storing":
                            int Pos3 = armor.IndexOf(" (");
                            if (Pos3 != -1) //get the spell stored
                            {
                                string temp3 = armor.Substring(Pos3).Trim();
                                armor = armor.Replace(temp3, string.Empty).Trim();
                                SA.SpellStored = temp3;
                            }
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.SpellStoring);
                            break;
                        case "brawling":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Brawling);
                            break;
                        case "warding":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Warding);
                            break;
                        case "expeditious":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Expeditious);
                            break;
                        case "hosteling":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Hosteling);
                            break;
                        case "deathless":
                            armor = armor.Replace(abilityLower, string.Empty);
                            SA.AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum.Deathless);
                            break;
                    }
                }
            }
            armor = armor.Trim();

            return SA;
        }

        private static void ParseEquipmentBasic(List<string> gearHold)
        {
            List<string> hold = new List<string>(gearHold);
            EquipmentGoods equipmentGoods;
            string temp, temp2;
            int count = 1;

            foreach (string item in hold)
            {
                temp = item;
                if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
                {
                    if (!temp.Contains("CL "))
                    {
                        temp2 = temp.Substring(temp.IndexOf(PathfinderConstants.PAREN_LEFT));
                        temp = temp.Replace(temp2, string.Empty).Trim();
                        temp2 = Utility.RemoveParentheses(temp2);
                        try
                        {
                            count = Convert.ToInt32(temp2);
                        }
                        catch
                        {
                            count = 1;
                        }
                    }
                }
                else
                {
                    count = 1;
                }
                equipmentGoods = _equipmentGoodsBusiness.GetGoodsServicesByName(temp.Trim());
                if (equipmentGoods != null)
                {
                    gearHold.Remove(item);
                    EquipementRoster.Add(equipmentGoods, count);
                }
            }
        }

        private static void ParseArmor(List<string> gearHold)
        {
            List<string> hold = new List<string>(gearHold);
            Armor armor;
            string temp;

            foreach (string item in hold)
            {
                temp = item.Replace("armor", string.Empty).Trim();
                armor = _armorBusiness.GetArmorByName(temp);
                if (armor != null)
                {
                    if (armor.category == "shield")
                    {
                        Indvid_AC_Values.Shield = armor;
                        gearHold.Remove(item);
                        EquipementRoster.Add(armor, 1);
                    }
                    else
                    {
                        Indvid_AC_Values.Armor = armor;
                        gearHold.Remove(item);
                        EquipementRoster.Add(armor, 1);
                    }
                }
            }
        }

        public static Dictionary<IEquipment, int> ParseArmorSimple(List<string> gearHold, IArmorBusiness armorBusiness)
        {
            _armorBusiness = armorBusiness;
            List<string> hold = new List<string>(gearHold);
            Dictionary<IEquipment, int> EquipementRosterSimple = new Dictionary<IEquipment, int>();
            if (!gearHold.Any()) return EquipementRosterSimple;
            Armor armor = null;
            ArmorProperties armorProperties = new ArmorProperties();

            string temp = string.Empty;
            string temp2 = string.Empty;
            string hold2;
            string tempEnchantBonus = string.Empty;
            string tempSize = string.Empty;
            MagicItemStatBlock MI = null;

            foreach (string item in hold)
            {
                armorProperties = new ArmorProperties();
                tempEnchantBonus = string.Empty;
                temp = item.Trim();

                ArmorSpecialAbilities SA = new ArmorSpecialAbilities();
                ShieldSpecialAbilities SSA = new ShieldSpecialAbilities();

                if (temp.Contains("shield"))
                {
                    SSA = GetShieldInfo(ref armorProperties, ref temp, ref tempEnchantBonus);
                }
                else
                {
                    SA = GetArmorInfo(ref armorProperties, ref temp, ref temp2, ref tempEnchantBonus, ref tempSize);
                }
                temp = temp.Replace("*", string.Empty).Replace("rusty", string.Empty)
                    .Replace("barding", string.Empty).Replace("armor spikes", string.Empty)
                    .Replace("with ", string.Empty).Replace("hand of the apprentice", string.Empty)
                    .Replace("  ", PathfinderConstants.SPACE);
                if (!(temp.Contains("armored coat") || (temp.Contains("armored kilt") || temp.Contains("bracers of armor") || temp.Contains("ceremonial armor"))))
                {
                    temp = temp.Replace("armor", string.Empty).Trim();
                }

                if (temp.Contains("red ") && !temp.Contains("armored coat") && !temp.Contains("armored kilt"))
                {
                    temp = temp.Replace("red ", string.Empty);
                }
                if (temp.Contains("black "))  temp = temp.Replace("black ", string.Empty);

                int Pos2 = temp.IndexOf(" with ");
                if (Pos2 >= 0) temp = temp.Substring(0, Pos2);

                temp = temp.Replace("  ", PathfinderConstants.SPACE);
                if (armorProperties.Mithral && temp == "shirt") temp = "mithral shirt";
                armor = _armorBusiness.GetArmorByName(temp);
                if (armor != null)
                {
                    SA = UpdateArmorRoster(gearHold, EquipementRosterSimple, armor, armorProperties, tempEnchantBonus, tempSize, item, SA);
                }
                else
                {
                    //named armor
                    int Pos = item.IndexOf(PathfinderConstants.PAREN_LEFT);
                    string temp5 = item;
                    if (Pos >= 0) temp5 = temp5.Substring(0, Pos).Trim();
                    if (temp5.Contains("rhino hide armor")) temp5 = temp5.Replace("armor", string.Empty).Trim();
                    if (temp5.Contains("scorpion-hide armor")) temp5 = temp5.Replace("armor", string.Empty).Trim();

                    MI = _magicItemStatBlockBusiness.GetMagicItemByName(temp5);
                    if (MI != null)
                    {
                        AddMagicArmor(gearHold, EquipementRosterSimple, ref armor, ref armorProperties, ref temp, ref temp2, ref tempEnchantBonus, ref tempSize, MI, item, ref SA, temp5);
                    }
                    else
                    {
                        MI = _magicItemStatBlockBusiness.GetMagicItemByName(temp);
                        if (MI != null)
                        {
                            AddMagicArmor(gearHold, EquipementRosterSimple, ref armor, ref armorProperties, ref temp, ref temp2, ref tempEnchantBonus, ref tempSize, MI, item, ref SA, temp5);
                        }
                    }
                }
            }
            return EquipementRosterSimple;
        }

        private static void AddMagicArmor(List<string> gearHold, Dictionary<IEquipment, int> EquipementRosterSimple, ref Armor armor, ref ArmorProperties armorProperties, ref string temp, ref string temp2, ref string tempEnchantBonus, ref string tempSize, MagicItemStatBlock MI, string item, ref ArmorSpecialAbilities SA, string temp5)
        {
            if (MI.BaseItem.Length > 0)
            {
                armorProperties.Masterwork = true;
                armorProperties.Broken = false;
                armorProperties.Darkwood = false;
                armorProperties.Spiked = false;
                armorProperties.Ironwood = false;
                tempEnchantBonus = string.Empty;
                temp = MI.BaseItem;

                ArmorSpecialAbilities SA2 = GetArmorInfo(ref armorProperties, ref temp, ref temp2, ref tempEnchantBonus, ref tempSize);

                if (!temp.Contains("armored coat")) temp = temp.Replace("armor", string.Empty).Trim();
                armor = _armorBusiness.GetArmorByName(temp);
                if (armor != null)
                {
                    armor.BaseArmorName = armor.name;
                    armor.name = temp5;
                    armor.NamedArmor = true;
                    SA = UpdateArmorRoster(gearHold, EquipementRosterSimple, armor, armorProperties, tempEnchantBonus, tempSize, item, SA);
                    gearHold.Remove(item);
                }
            }
        }

        private static ShieldSpecialAbilities GetShieldInfo(ref ArmorProperties shieldProperties, ref string shield, ref string tempEnchantBonus)
        {
            if (shield.Contains("masterwork"))
            {
                shieldProperties.Masterwork = true;
                shield = shield.Replace("masterwork", string.Empty).Trim();
            }
            if (shield.Contains("mwk "))
            {
                shieldProperties.Masterwork = true;
                shield = shield.Replace("mwk ", string.Empty).Trim();
            }
            if (shield.Contains("darkwood"))
            {
                shieldProperties.Darkwood = true;
                shieldProperties.Masterwork = true;
                shield = shield.Replace("darkwood", string.Empty).Trim();
            }

            if (shield.Contains("+"))
            {
                string temp2 = shield.Substring(0, shield.IndexOf(PathfinderConstants.SPACE));
                if (temp2.Length > 0 && temp2 != "ghost")
                {
                    shield = shield.Replace(temp2, string.Empty).Trim();
                    tempEnchantBonus = temp2;
                }
            }

            return ParseShieldSpecialAbilities(ref shield);
        }

        private static ArmorSpecialAbilities GetArmorInfo(ref ArmorProperties armorProperties, ref string armor, ref string temp2, ref string tempEnchantBonus, ref string tempSize)
        {
            if (armor.Contains("masterwork"))
            {
                armorProperties.Masterwork = true;
                armor = armor.Replace("masterwork", string.Empty).Trim();
            }
            if (armor.Contains("mwk "))
            {
                armorProperties.Masterwork = true;
                armor = armor.Replace("mwk ", string.Empty).Trim();
            }
            if (armor.Contains("spiked"))
            {
                armorProperties.Spiked = true;
                armor = armor.Replace("spiked", string.Empty).Trim();
            }
            if (armor.Contains("broken"))
            {
                armorProperties.Broken = true;
                armor = armor.Replace("broken", string.Empty).Trim();
            }
            if (armor.Contains("darkwood"))
            {
                armorProperties.Darkwood = true;
                armorProperties.Masterwork = true;
                armor = armor.Replace("darkwood", string.Empty).Trim();
            }
            if (armor.Contains("ironwood"))
            {
                armorProperties.Ironwood = true;
                armorProperties.Masterwork = true;
                armor = armor.Replace("ironwood", string.Empty).Trim();
            }
            if (armor.Contains("eel hide"))
            {
                armorProperties.EelHide = true;
                armor = armor.Replace("eel hide", string.Empty).Trim();
            }
            if (armor.Contains("bone"))
            {
                armorProperties.Bone = true;
                armor = armor.Replace("bone", string.Empty).Trim();
            }
            if (armor.Contains("(invisible)"))
            {
                armor = armor.Replace("(invisible)", string.Empty).Trim();
            }

            if (armor.Contains("adamantine"))
            {
                armorProperties.Masterwork = true;
                armor = armor.Replace("adamantine", string.Empty).Trim();
            }

            if (armor.Contains("mithral"))
            {
                armorProperties.Mithral = true;
                armorProperties.Masterwork = true;
                armor = armor.Replace("mithral", string.Empty).Trim();
            }
            if (armor.Contains("dragonhide"))
            {
                armor = armor.Replace("dragonhide", string.Empty).Trim();
                int Pos = armor.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos > 0) armor = armor.Substring(0, Pos);
            }

            if (armor.Contains("as "))
            {
                int Pos = armor.IndexOf("as ");
                armor = armor.Substring(Pos + ("as ").Length);
                Pos = armor.IndexOf("|");
                if (Pos != -1) armor = armor.Substring(0, Pos);
                Pos = armor.IndexOf(PathfinderConstants.PAREN_RIGHT);
                if (Pos != -1) armor = armor.Substring(0, Pos);
            }

            foreach (string size in CommonMethods.GetSizes())
            {
                if (armor.IndexOf(size) >= 0)
                {
                    tempSize = size;
                    armor = armor.Replace(size, string.Empty).Trim();
                    break;
                }
            }

            if (armor.Contains("+"))
            {
                temp2 = armor.Substring(0, armor.IndexOf(PathfinderConstants.SPACE));
                if (temp2.Length > 0 && temp2 != "ghost")
                {
                    armor = armor.Replace(temp2, string.Empty).Trim();
                    tempEnchantBonus = temp2;
                }
            }

            return ParseArmorSpecialAbilities(ref armor);
        }

        private static ArmorSpecialAbilities UpdateArmorRoster(List<string> gearHold, Dictionary<IEquipment, int> EquipementRosterSimple,
                      Armor armor, ArmorProperties armorProperties, string tempEnchantBonus, string tempSize, string item, ArmorSpecialAbilities SA)
        {
            armor.Masterwork = armorProperties.Masterwork;
            if (armor.category != "shield")  armor.ArmorSpecialAbilities = SA;
            if (tempEnchantBonus.Length > 0)
            {
                armor.EnhancementBonus = Convert.ToInt32(tempEnchantBonus);
                armor.Masterwork = true;
            }
            if (tempSize.Length > 0) armor.ArmorSize = StatBlockInfo.GetSizeEnum(tempSize);

            armor.Broken = armorProperties.Broken;
            armor.Spiked = armorProperties.Spiked;

            if (armorProperties.Darkwood && armor.category == "shield")  armor.armor_check_penalty = 0;

            if (armorProperties.Mithral)
            {
                armor.Mithral = true;
                armor.armor_check_penalty += 3;
                if (armor.armor_check_penalty > 0) armor.armor_check_penalty = 0;
                armor.max_dex_bonus += 2;
            }

            if (armorProperties.Bone) armor.bonus--;

            if (armorProperties.Adamantine)
            {
                armor.armor_check_penalty++;
                if (armor.armor_check_penalty > 0) armor.armor_check_penalty = 0;
            }

            if (armor.category == "shield")
            {
                string temp = item;
                ShieldSpecialAbilities ShieldSA = ParseShieldSpecialAbilities(ref temp);

                armor.ShieldSpecialAbilities = ShieldSA;
                Indvid_AC_Values.Shield = armor;
                gearHold.Remove(item);
                EquipementRosterSimple.Add(armor, 1);
                if (armorProperties.Spiked) //it's armor and aweapon
                {
                    Weapon weapon = null;
                    int count = 1;
                    if (item.Contains("light")) temp = "light spiked shield";
                    if (item.Contains("heavy")) temp = "heavy spiked shield";
                    if (armor.EnhancementBonus != 0) temp = "+" + armor.EnhancementBonus.ToString() + PathfinderConstants.SPACE + temp;

                    bool fake = false;
                    if (FindOneWeapon(temp, ref weapon, ref count, true, ShieldSA, ref fake))
                    {
                        EquipementRosterSimple.Add(weapon, count);
                    }
                }
            }
            else
            {
                Indvid_AC_Values.Armor = armor;
                gearHold.Remove(item);
                EquipementRosterSimple.Add(armor, 1);
            }
            return SA;
        }

        public static List<string> FindMagicItems(List<string> gearHold)
        {
            List<string> temp = new List<string>();
            List<string> hold = new List<string>(gearHold);
            List<string> MI_Nouns = CommonMethods.GetMagicItemNouns();
            MagicItemStatBlock MI = null;
            string find;
            string holdItem;
            string extraAbilities;
            int count = 1;

            foreach (string item in hold)
            {
                holdItem = item;
                extraAbilities = string.Empty;
                if (holdItem.Contains("as "))
                {
                    int Pos = holdItem.IndexOf("as ");
                    holdItem = holdItem.Substring(Pos + ("as ").Length);
                    Pos = holdItem.IndexOf("|");
                    if (Pos != -1) holdItem = holdItem.Substring(0, Pos);
                    Pos = holdItem.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    if (Pos != -1) holdItem = holdItem.Substring(0, Pos);
                }

                if (holdItem.Contains(PathfinderConstants.PAREN_LEFT))
                {
                    string tempBonus = string.Empty;
                    int Pos = holdItem.IndexOf(PathfinderConstants.PAREN_LEFT);
                    int Pos2 = holdItem.IndexOf("+", Pos);
                    if (Pos2 != -1) tempBonus = holdItem.Substring(Pos2).Trim();
                    extraAbilities = holdItem.Substring(Pos).Trim();
                    if (!extraAbilities.Contains("CL"))
                    {
                        extraAbilities = extraAbilities.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                            .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                        int.TryParse(extraAbilities, out count);
                        extraAbilities = string.Empty;
                    }
                    else
                    {
                        int countParen = extraAbilities.Length - (extraAbilities.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Length);
                        if (countParen > 1)
                        {
                            Pos = extraAbilities.LastIndexOf(PathfinderConstants.PAREN_LEFT);
                            string holdItem2 = extraAbilities.Substring(Pos);
                            extraAbilities = extraAbilities.Replace(holdItem2, string.Empty);
                            holdItem2 = holdItem2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                                .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                            count = int.Parse(holdItem2);
                        }

                        if (extraAbilities.Contains("|"))
                        {
                            List<string> split = extraAbilities.Split('|').ToList();
                            foreach (string a in split)
                            {
                                if (a.Contains("CL"))
                                {
                                    extraAbilities = a;
                                    break;
                                }
                            }
                        }
                    }
                    holdItem = holdItem.Substring(0, Pos).Trim();
                    if (tempBonus.Length > 0) holdItem += PathfinderConstants.SPACE + tempBonus;
                }

                if (holdItem.Contains("amulet of mighty fists"))
                {
                    foreach (string ability in CommonMethods.GetAmuletOfMightFistsSpecialAbilities())
                    {
                        holdItem = holdItem.Replace(ability, string.Empty);
                    }
                    holdItem = holdItem.Trim();
                }

                find = CommonMethods.FindListContains(holdItem, MI_Nouns);
                if (find.Length > 0)
                {
                    holdItem = holdItem.Replace("ghost touch", string.Empty).Trim();
                    MI = _magicItemStatBlockBusiness.GetMagicItemByName(holdItem);
                    if (MI != null)
                    {
                        temp.Add(holdItem);
                        gearHold.Remove(item);
                        EquipementRoster.Add(MI, 1);
                    }
                    else
                    {
                        IEquipment Equip = EquipmentCommon.FindEquipment(find, holdItem, extraAbilities);
                        if (Equip != null)
                        {
                            EquipementRoster.Add(Equip, count);
                            gearHold.Remove(item);
                        }
                        else
                        {
                            if (item.Contains("+"))
                            {
                                MI = _magicItemStatBlockBusiness.GetMagicItemByName(holdItem);
                                if (MI != null)
                                {
                                    temp.Add(holdItem);
                                    gearHold.Remove(item);
                                    EquipementRoster.Add(MI, 1);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MI = _magicItemStatBlockBusiness.GetMagicItemByName(holdItem);
                    if (MI != null)
                    {
                        temp.Add(holdItem);
                        gearHold.Remove(item);
                        MI.ExtraAbilities = extraAbilities;
                        EquipementRoster.Add(MI, 1);
                    }
                }
            }
            return temp;
        }
    }
}
