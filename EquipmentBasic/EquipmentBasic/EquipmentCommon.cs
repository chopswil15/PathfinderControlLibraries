using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using PathfinderGlobals;
using Utilities;

namespace EquipmentBasic
{
    public static class EquipmentCommon
    {
        public static IEquipment FindEquipment(string find, string item)
        {
            return FindEquipment(find, item, string.Empty);
        }

        public static IEquipment FindEquipment(string find, string item, string extraAbilities)
        {
            switch (find.Trim())
            {
                case "potion":
                case "potions":
                    Potion potion = new Potion(item, extraAbilities);
                    return potion;
                case "wand":
                case "wands":
                    Wand wand = new Wand(item);
                    return wand;
                case "scroll":
                case "scroll of":
                case "scrolls":
                    Scroll scroll = new Scroll(item);
                    return scroll;
                case "oil of":
                case "oils of":
                    Oil oil = new Oil(item, extraAbilities);
                    return oil;
            }
            return null;
        }
    }

    public enum WeaponSpecialMaterials
    {
        None = 0,
        Adamantine,
        AlchemicalSilver,
        Bone,
        Bronze,
        ColdIron,
        Darkwood,
        Dragonhide,
        Gold,
        Mithral,
        NexavaranSteel,
        Obsidian,
        Silversheen,
        Stone,
        Greenwood,
        Siccatite 
    }

    [Flags]
    public enum WeaponSpecialAbilitiesEnum : long
    {
        None = 0,
        Anarchic = 1 << 0,
        Axiomatic = 1 << 1,
        Bane = 1 << 2,
        BrilliantEnergy = 1 << 3,
        Dancing = 1 << 4,
        Defending = 1 << 5,
        Disruption = 1 << 6,
        Distance = 1 << 7,
        Evil = 1 << 8,
        Flaming = 1 << 9,
        FlamingBurst = 1 << 10,
        Frost = 1 << 11,
        GhostTouch = 1 << 12,
        Good = 1 << 13,
        Holy = 1 << 14,
        IcyBurst = 1 << 15,
        Keen = 1 << 16,
        KiFocus = 1 << 17,
        Merciful = 1 << 18,
        MightyCleaving = 1 << 19,
        Outsider = 1 << 20,
        Returning = 1 << 21,
        Seeking = 1 << 22,
        Shock = 1 << 23,
        ShockingBurst = 1 << 24,
        Speed = 1 << 25,
        SpellStoring = 1 << 26,
        Throwing = 1 << 27,
        Thundering = 1 << 28,
        Unholy = 1 << 29,
        Vicious = 1 << 30,
        Vorpal = 1L << 31,
        Wounding = 1L << 32,
        Grayflame = 1L << 33,
        Furious = 1L << 34,
        Thawing = 1L << 35,
        Corrosive = 1L << 36,
        Conductive = 1L << 37,
        Cruel = 1L << 38,
        Berserking = 1L << 39,
        Menacing = 1L << 40,
        Heartseeker = 1L << 41,
        Valiant = 1L << 42,
        Dispelling = 1L << 43,
        Furyborn = 1L << 44,
        Impervious = 1L << 45,
        Impact = 1L << 46,
        Reliable = 1L << 47,
        Planar = 1L << 48 ,
        Jurist = 1L << 49,
        Called = 1L << 50,
        Vampiric = 1L << 51
    }


    public struct WeaponSpecialAbilities
    {       
        public string BaneFoe;        

        public WeaponSpecialAbilitiesEnum WeaponSpecialAbilitiesValue;

        public void AddWeaponSpecialAbility(WeaponSpecialAbilitiesEnum value)
        {
            WeaponSpecialAbilitiesValue |= value;
        }

        private bool FlagSet(WeaponSpecialAbilitiesEnum value)
        {
            return (WeaponSpecialAbilitiesValue & value) == value;
        }

        public override string ToString()
        {
            List<string> temp = new List<string>();
            if (FlagSet(WeaponSpecialAbilitiesEnum.Anarchic)) { temp.Add("anarchic"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Axiomatic)) { temp.Add("axiomatic"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Bane)) { temp.Add(BaneFoe + "bane"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.BrilliantEnergy)) { temp.Add("brilliant energy"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Dancing)) { temp.Add("dancing"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Defending)) { temp.Add("defending"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Disruption)) { temp.Add("disruption"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Distance)) { temp.Add("distance"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Evil)) { temp.Add("evil"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Flaming)) { temp.Add("flaming"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.FlamingBurst)) { temp.Add("flaming burst"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Frost)) { temp.Add("frost"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.GhostTouch)) { temp.Add("ghost touch"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Good)) { temp.Add("good"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Holy)) { temp.Add("holy"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.IcyBurst)) { temp.Add("icy burst"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Keen)) { temp.Add("keen"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.KiFocus)) { temp.Add("ki focus"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Merciful)) { temp.Add("merciful"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.MightyCleaving)) { temp.Add("mighty cleaving"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Outsider)) { temp.Add("outsider"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Returning)) { temp.Add("returning"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Seeking)) { temp.Add("seeking"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Shock)) { temp.Add("shock"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.ShockingBurst)) { temp.Add("shocking burst"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Speed)) { temp.Add("speed"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.SpellStoring)) { temp.Add("spell storing"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Throwing)) { temp.Add("throwing"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Thundering)) { temp.Add("thundering"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Unholy)) { temp.Add("unholy"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Vicious)) { temp.Add("vicious"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Vorpal)) { temp.Add("vorpal"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Wounding)) { temp.Add("wounding"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Grayflame)) { temp.Add("grayflame"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Furious)) { temp.Add("furious"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Corrosive)) { temp.Add("corrosive"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Conductive)) { temp.Add("conductive"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Cruel)) { temp.Add("cruel"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Berserking)) { temp.Add("berserking"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Menacing)) { temp.Add("menacing"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Heartseeker)) { temp.Add("heartseeker"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Valiant)) { temp.Add("valiant"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Dispelling)) { temp.Add("dispelling"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Furyborn)) { temp.Add("furyborn"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Impervious)) { temp.Add("impervious"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Impact)) { temp.Add("impact"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Reliable)) { temp.Add("reliable"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Planar)) { temp.Add("planar"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Jurist)) { temp.Add("jurist"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Called)) { temp.Add("called"); }
            if (FlagSet(WeaponSpecialAbilitiesEnum.Vampiric)) { temp.Add("vampiric"); }


            return string.Join(PathfinderConstants.SPACE, temp.ToArray());
        }
    }

    [Flags]
    public enum ArmorSpecialAbilitiesEnum : long
    {
        None = 0,
        Animated = 1 << 0,
        ArrowCatching = 1 << 1,
        ArrowDeflection = 1 << 2,
        Bashing = 1 << 3,
        EnergyResistance = 1 << 4,
        EnergyResistanceImproved = 1 << 5,
        EnergyResistanceGreater = 1 << 6,
        Etherealness = 1 << 7,
        LightFortification = 1 << 8,
        ModerateFortification = 1 << 9,
        HeavyFortification = 1 << 10,
        GhostTouch = 1 << 11,
        Glamered = 1 << 12,
        Invulnerability = 1 << 13,
        Reflecting = 1 << 14,
        Shadow = 1 << 15,
        ShadowImproved = 1 << 16,
        ShadowGreater = 1 << 17,
        Slick = 1 << 18,
        SlickImproved = 1 << 19,
        SlickGreater = 1 << 20,
        SpellResistance = 1 << 21,
        UndeadControlling = 1 << 22,
        Wild = 1 << 23,
        EnergyResistance_Acid = 1 << 24,
        EnergyResistance_Cold = 1 << 25,
        EnergyResistance_Electricity = 1 << 26,
        EnergyResistance_Fire = 1 << 27,
        EnergyResistance_Sonic = 1 << 28,
        Imporoved_EnergyResistance_Cold = 1 << 29,
        Unrighteous = 1 << 30,
        Defiant = 1L << 31,
        SpellStoring = 1L <<32,
        Brawling = 1L << 33,
        Warding = 1L << 34 ,
        Expeditious = 1L << 36 ,
        Imporoved_EnergyResistance_Fire = 1L << 37,
        Hosteling = 1L << 38 ,
        Deathless = 1L << 39
    }

    public struct ArmorSpecialAbilities
    {
        public string DefiantFoe;
        public string SpellStored;

        public ArmorSpecialAbilitiesEnum ArmorSpecialAbilityValues;
      

        public void AddArmorSpecialAbility(ArmorSpecialAbilitiesEnum value)
        {
            ArmorSpecialAbilityValues |= value;
        }

        private bool FlagSet(ArmorSpecialAbilitiesEnum value)
        {
            return (ArmorSpecialAbilityValues & value) == value;
        }

        public override string ToString()
        {
            List<string> temp = new List<string>();
            if (FlagSet(ArmorSpecialAbilitiesEnum.Animated)) { temp.Add("animated"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.ArrowCatching)) { temp.Add("arrow catching"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Bashing)) { temp.Add("bashing"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.EnergyResistance)) { temp.Add("energy resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.EnergyResistanceImproved)) { temp.Add("improved energy resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.EnergyResistanceGreater)) { temp.Add("greater energy resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Etherealness)) { temp.Add("etherealness"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.LightFortification)) { temp.Add("light fortification"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.ModerateFortification)) { temp.Add("moderate fortification"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.HeavyFortification)) { temp.Add("heavy fortification"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.GhostTouch)) { temp.Add("ghost touch"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Glamered)) { temp.Add("glamered"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Invulnerability)) { temp.Add("invulnerability"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Reflecting)) { temp.Add("reflecting"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Shadow)) { temp.Add("shadow"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.ShadowImproved)) { temp.Add("improved shadow"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.ShadowGreater)) { temp.Add("greater shadow"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Slick)) { temp.Add("slick"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.ShadowImproved)) { temp.Add("improved slick"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.SlickGreater)) { temp.Add("greater slick"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.SpellResistance)) { temp.Add("spell resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.UndeadControlling)) { temp.Add("undead controlling"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Wild)) { temp.Add("wild"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Imporoved_EnergyResistance_Cold)) { temp.Add("improved cold resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Unrighteous)) { temp.Add("unrighteous"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Defiant)) { temp.Add(DefiantFoe + "-defiant"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.SpellStoring)) { temp.Add(DefiantFoe + "spell storing"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Brawling)) { temp.Add(DefiantFoe + "brawling"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Warding)) { temp.Add(DefiantFoe + "warding"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Expeditious)) { temp.Add(DefiantFoe + "expeditious"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Imporoved_EnergyResistance_Fire)) { temp.Add("improved fire resistance"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Hosteling)) { temp.Add("hosteling"); }
            if (FlagSet(ArmorSpecialAbilitiesEnum.Deathless)) { temp.Add("deathless"); }

            return string.Join(PathfinderConstants.SPACE, temp.ToArray());
        }
    }

    [Flags]
    public enum ShieldSpecialAbilitiesEnum
    {
        None = 0,
        ArrowCatching = 1 << 0,
        Bashing = 1 << 1,
        Blinding = 1 << 2,
        LightFortification = 1 << 3,
        ModerateFortification = 1 << 4,
        HeavyFortification = 1 << 5,
        ArrowDeflection = 1 << 6,
        Animated = 1 << 7,
        SpellResistance = 1 << 8,
        EnergyResistance = 1 << 9,
        EnergyResistanceImproved = 1 << 10,
        EnergyResistanceGreater = 1 << 11,
        GhostTouch = 1 << 12,
        Wild = 1 << 13,
        UndeadControlling = 1 << 14,
        Reflecting = 1 << 15,
        Ramming = 1 << 16
    }

    public struct ShieldSpecialAbilities
    {
        public ShieldSpecialAbilitiesEnum ShieldSpecialAbilityValues;


        public void AddShieldSpecialAbility(ShieldSpecialAbilitiesEnum value)
        {
            ShieldSpecialAbilityValues |= value;
        }

        private bool FlagSet(ShieldSpecialAbilitiesEnum value)
        {
            return (ShieldSpecialAbilityValues & value) == value;
        }
         
        public override string ToString()
        {
            List<string> temp = new List<string>();
            if (FlagSet(ShieldSpecialAbilitiesEnum.Animated)) { temp.Add("animated"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.ArrowCatching)) { temp.Add("arrow catching"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.ArrowDeflection)) { temp.Add("arrow deflection"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.Bashing)) { temp.Add("bashing"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.Blinding)) { temp.Add("blinding"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.EnergyResistance)) { temp.Add("energy resistance"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.EnergyResistanceImproved)) { temp.Add("improved energy resistance"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.EnergyResistanceGreater)) { temp.Add("greater energy resistance"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.LightFortification)) { temp.Add("light fortification"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.ModerateFortification)) { temp.Add("moderate fortification"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.HeavyFortification)) { temp.Add("heavy fortification"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.GhostTouch)) { temp.Add("ghost touch"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.Reflecting)) { temp.Add("reflecting"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.GhostTouch)) { temp.Add("ghost touch"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.SpellResistance)) { temp.Add("spell resistance"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.UndeadControlling)) { temp.Add("undead controlling"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.Wild)) { temp.Add("wild"); }
            if (FlagSet(ShieldSpecialAbilitiesEnum.Ramming)) { temp.Add("ramming"); }

            return string.Join(PathfinderConstants.SPACE, temp.ToArray());
        }
    }
}
