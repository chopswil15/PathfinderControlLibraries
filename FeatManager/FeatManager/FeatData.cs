using FeatFoundational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatManager
{
    public class FeatData : FeatFoundation
    {
        public FeatData(string Name, string PreReqs, string PreReqFeats, bool BonusFeat, string SelectedItem,
                string Type, string PreReqSkillRanks)
        {
            base.Name = Name;
            base.Type = ConvertType(Type);
            base.BonusFeat = BonusFeat;
            base.SelectedItem = SelectedItem;
            base.PreRequistFeats = PreReqFeats.Split(',').ToList();
            base.PreRequistSkillRanks = PreReqSkillRanks.Split(',').ToList();
            base.PreRequistSkillRanks.RemoveAll(x => x== string.Empty);
            PreRequistFeats.RemoveAll(x => x == string.Empty);
            PreReqs = PreReqs.Replace(".", string.Empty);
            PreReqs = PreReqs.Replace(";", ",");
            List<string> PreReqList = PreReqs.Split(',').ToList();
            PreReqList.RemoveAll(x => x == string.Empty);
            List<string> PreReqFields = new List<string> {"BAB" ,"base attack bonus ", "Int " ,"Str " ,"Dex " ,"Wis " ,"Con " ,"Cha " };


            for (int x = 0; x < PreRequistSkillRanks.Count; x++)
            {
                PreRequistSkillRanks[x] = PreRequistSkillRanks[x].Trim();
            }

            //List<string> replaceList = new List<string>();
            //foreach (string temp in PreReqList)
            //{
            //    if (temp.Contains(" or "))
            //    {
            //        replaceList.Add(temp);
            //    }
            //}

            //foreach (string replace in replaceList)
            //{
            //    PreReqList.Remove(replace);
            //    List<string> newReplace = replace.Split(new string[] { " or " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //    foreach(string temp in newReplace)
            //    {
            //        PreReqList.Add(temp);
            //    }
            //}

            foreach (string temp in PreReqList)
            {                
                foreach (string field in PreReqFields)
                {
                    if(temp.Contains(field))
                    {
                        try
                        {
                            string hold = temp.Replace("+", string.Empty);
                            if(hold.Contains(" or ")) continue;
                            switch (field)
                            {
                                case "base attack bonus ":
                                case "BAB":
                                    base.BAB = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Int ":
                                    base.Int = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Str ":
                                    base.Str = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Dex ":
                                    base.Dex = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Wis ":
                                    base.Wis = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Con ":
                                    base.Con = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                                case "Cha ":
                                    base.Cha = int.Parse(hold.Replace(field, string.Empty));
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("FeatData: " + ex.Message);
                        }
                    }
                }
            }
        }

        private FeatTypes ConvertType(string Type)
        {
            switch (Type)
            {
                case "General":
                    return FeatTypes.General;
                case "Combat":
                    return FeatTypes.Combat;
                case "Racial":
                    return FeatTypes.Racial;
                case "ItemCreation":
                    return FeatTypes.ItemCreation;
                case "Matemagic":
                    return FeatTypes.Metamagic;
                case "Monster":
                    return FeatTypes.Monster;
                case "Local":
                    return FeatTypes.Local;
                case "Achievent":
                    return FeatTypes.Achievent;
                case "Story":
                    return FeatTypes.Story;
                case "Mythic":
                    return FeatTypes.Mythic;
                default: 
                    return FeatTypes.None;
            }
        }
    }
}
