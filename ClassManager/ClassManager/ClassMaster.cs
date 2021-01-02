using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonStrings;
using ClassFoundational;
using OnGoing;
using CommonStatBlockInfo;
using OracleMysteries;
using Skills;
using Utilities;
using PathfinderGlobals;

namespace ClassManager
{
    public class ClassMaster
    {
        public List<ClassWrapper> Classes { get; set; }
        private ClassWrapper CW;
        private Assembly Assemb;

        public ClassMaster(ClassMasterInput classMasterInput)
        {
            Classes = new List<ClassWrapper>();
            try
            {
                Assemb = Assembly.Load("ClassDetails");
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("ClassMaster -- Assembly not found ClassDetails", ex.InnerException);
            }

            ParseClassList(classMasterInput);
        }

        private void ParseClassList(ClassMasterInput classMasterInput)
        {
            List<string> tempClasses = classMasterInput.ClassList.Split('/').ToList();
            tempClasses.RemoveAll(x => x== string.Empty);
            string className, classLevel, archetype;
            string diety = string.Empty;
            int Pos, Pos2;

            foreach (string tempclass in tempClasses)
            {
                archetype = string.Empty;
                string holdClass = tempclass.ToLower();
                Pos = tempclass.IndexOf(" (");
                if (Pos == -1)
                {
                    Pos = tempclass.Length;
                }
                else
                {
                    archetype = tempclass.Substring(Pos);
                    archetype = Utility.RemoveSuperScripts(archetype);
                    Pos2 = archetype.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    archetype = archetype.Substring(0, Pos2 + 1);
                    holdClass = holdClass.Replace(archetype, string.Empty).Trim();
                    archetype = archetype.Replace("*", string.Empty);
                    archetype = Utility.RemoveParentheses(archetype);
                    archetype = Utility.RemoveSuperScripts(archetype);
                    archetype = archetype.ProperCase().Trim();
                    Pos = holdClass.IndexOf(" (");
                    if (Pos == -1) Pos = holdClass.Length;
                }
                Pos = holdClass.LastIndexOf(PathfinderConstants.SPACE, Pos);
                className = holdClass.Substring(0, Pos).Trim().ToLower();
                classLevel = holdClass.Replace(className, string.Empty).Trim();

                Pos = className.IndexOf(" of ");
                if (Pos >= 0)
                {
                    var IgnoreOfs = new List<string> { "arclord of", "knight of", "prophet of", "brother of", "zealot of", "disciple of" };
                    bool foundIgnoreOf = false;

                    foreach (var oneIgnore in IgnoreOfs)
                    {
                        if (className.Contains(oneIgnore))
                        {
                            foundIgnoreOf = true;
                            break;
                        }
                    }
                    if (!foundIgnoreOf)
                    {
                        diety = className.Substring(Pos);
                        diety = diety.Replace("of ", string.Empty).Trim();
                        className = className.Substring(0, Pos);
                    }
                }


                if (className.IndexOf("Ex-") >= 0 || className.IndexOf("ex-") >= 0)
                {
                    className = className.Replace("Ex-", string.Empty);
                    className = className.Replace("ex-", string.Empty);
                    className = className.ProperCase();
                    className = "Ex" + className;
                }
                else
                {
                    className = className.ProperCase();
                }

                className = className.Replace(PathfinderConstants.SPACE, string.Empty);

                Type ClassInst = Assemb.GetType("ClassDetails." + className);
                if (ClassInst == null) throw new Exception("Class " + className + " not defined");

                try
                {
                    object obj = Activator.CreateInstance(ClassInst);
                    CW = new ClassWrapper();
                    ClassFoundation Class = (ClassFoundation)obj;
                    CW.ClassInstance = Class;
                    CW.ClassInstance.ClassDiety = diety;
                    int classlevel;
                    if (!int.TryParse(classLevel, out classlevel))
                    {
                        throw new Exception("classLevel not int -" + classLevel + " for class " + Class);
                    }
                    CW.Level = classlevel;
                    Class.ProcessPowers(CW.Level);
                    CW.Archetype = archetype;
                    List<string> archetypes = archetype.Split(',').ToList();
                    archetypes.RemoveAll(x => x == string.Empty); ;
                    foreach (string oneArchetype in archetypes)
                    {
                        if (oneArchetype.Length > 0 && !CW.ClassInstance.ClassArchetypes.Contains(oneArchetype.Trim())) throw new Exception(className + " does not have archetype " + oneArchetype + " defined.");
                    }

                    if (Class.DomainSpellUse)
                    {
                        if (classMasterInput.Domians.Contains("subdomain") || classMasterInput.Domians.Contains(PathfinderConstants.PAREN_LEFT) || classMasterInput.Domians.Contains(","))
                        {
                            List<string> tempDomains = classMasterInput.Domians.Split(',').ToList();
                            for (int a = 0; a < tempDomains.Count; a++)
                            {
                                Pos = tempDomains[a].IndexOf(PathfinderConstants.PAREN_LEFT);
                                if (Pos >= 0)
                                {
                                    tempDomains[a] = tempDomains[a].Substring(Pos);
                                    tempDomains[a] = tempDomains[a].Replace("subdomain", string.Empty);
                                }
                                tempDomains[a] = Utility.RemoveParentheses(tempDomains[a]);
                                tempDomains[a] = Utility.RemoveSuperScripts(tempDomains[a]);
                            }
                            classMasterInput.Domians = string.Join(",", tempDomains.ToArray());
                        }
                        classMasterInput.Domians = Utility.RemoveSuperScripts(classMasterInput.Domians);
                        classMasterInput.Domians = Utility.RemoveParentheses(classMasterInput.Domians);
                        Class.ProcessDomains(classMasterInput.Domians);
                    }
                    if (Class.MysteryUse)
                    {
                        Class.ProcessMysteries(classMasterInput.Mysteries);
                    }
                    if (Class.BloodlineUse)
                    {
                        Class.ProcessBloodline(classMasterInput.Bloodline);
                    }
                    if (Class.PatronUse)
                    {
                        Class.ProcessPatron(classMasterInput.Patron.ProperCase());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Initalize Class Issue with " + className + PathfinderConstants.SPACE + ex.Message, ex);
                }
                Classes.Add(CW);

            }
        }

        public int ClassCount()
        {
            return Classes.Count;
        }

        public string GetBloodlineName()
        {
            ClassWrapper temp = GetClass("sorcerer");
            if (temp != null)
            {
                return temp.ClassInstance.Bloodline.Name;
            }
            temp = GetClass("bloodrager");
            if (temp != null)
            {
                return temp.ClassInstance.Bloodline.Name;
            }

            return string.Empty;
        }

        public SkillData.SkillNames GetBloodlineClassSkill()
        {
            ClassWrapper temp = GetClass("sorcerer");
            if (temp != null)
            {
               return temp.ClassInstance.Bloodline.ClassSkillName;
            }
            temp = GetClass("bloodrager");
            if (temp != null)
            {
                return temp.ClassInstance.Bloodline.ClassSkillName;
            }

            return SkillData.SkillNames.Unknown;
        }

        public List<PreReqSkill> GetPrestigePreReqSkills(string ClassName)
        {
            ClassWrapper temp = GetClass(ClassName);
            if (temp == null) return new List<PreReqSkill>();

            return temp.ClassInstance.PrestigePreReqSkills();
        }

        public List<string> GetPrestigePreReqFeats(string ClassName)
        {
            ClassWrapper temp = GetClass(ClassName);
            if (temp == null) return new List<string>();

            return temp.ClassInstance.PrestigePreReqFeats();
        }

        public List<string> GetClassAlignments(string ClassName)
        {
            ClassWrapper temp = GetClass(ClassName);
            if (temp == null) return new List<string>();

            return temp.ClassInstance.ClassAlignments;
        }

        public ClassWrapper GetClass(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class;
            }
            return null;
        }

        public string FindClassDiety(string ClassName)
        {
            ClassWrapper one_class = FindClass(ClassName);
            if(one_class != null) return one_class.ClassInstance.ClassDiety;
            return string.Empty;
        }

        public string FindDiety()
        {
            foreach (ClassWrapper one_class in Classes)
            {
                if (one_class.ClassInstance.ClassDiety.Length > 0) return one_class.ClassInstance.ClassDiety;
            }

            return string.Empty;
        }

        public bool HasClass(string ClassName)
        {            
            return Classes.Any(x => x.ClassInstance.Name.ToLower() == ClassName.ToLower());            
        }

        public List<string> GetClassNames()
        {
            List<string> Names = new List<string>();
            foreach (ClassWrapper one_class in Classes)
            {
                Names.Add(one_class.Name);
            }

            return Names;
        }

        public int CanCastSpellsCount()
        {
            int count = 0;
            foreach (var oneClass in Classes)
            {
                if (oneClass.ClassInstance.CanCastSpells) count++;
            }

            return count;
        }

        public bool CanClassCastSpells(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.CanCastSpells;
            }
            return false;
        }

        public bool CanClassCastDomainSpells(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.DomainSpellUse;
            }
            return false;
        }

        public string GetSpellBonusAbility(string ClassName)
        {
            if (!Classes.Any())
            {
                Type ClassInst = Assemb.GetType("ClassDetails." + ClassName);
                if (ClassInst != null)
                {
                    object obj = Activator.CreateInstance(ClassInst);
                    CW = new ClassWrapper();
                    ClassFoundation Class = (ClassFoundation)obj;
                    return Class.GetSpellBonusAbility();
                }
            }
            else
            {
                ClassWrapper one_class = FindClassWrapper(ClassName);
                if (one_class != null)
                {
                    return one_class.ClassInstance.GetSpellBonusAbility();
                }
            }
            return string.Empty;
        }

        public List<int> GetClassSpellLevels(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.GetSpellsPerLevel(one_class.Level);
            }
            return null;
        }

        public List<int> GetClassSpellsPerDay(string ClassName, int overloadLevel)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.GetSpellsPerDay(one_class.Level + overloadLevel);
            }
            return null;
        }

        public List<int> GetClassSpellLevels(string ClassName, int overloadLevel)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.GetSpellsPerLevel(one_class.Level + overloadLevel);
            }
            return null;
        }

        public int GetClassBloodlineSpellLevels(string ClassName, int overloadLevel)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.GetBloodlineSpellsPerLevel(one_class.Level + overloadLevel);
            }
           
            return 0;
        }

        public int GetClassMysterySpellLevels(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.GetMysteriesPerLevel(one_class.Level);
            }
            return 0;
        }

        public int GetDomainSpellCount(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null && one_class.ClassInstance.DomainSpellUse)
            {
                return one_class.ClassInstance.GetDomainSpellsPerLevel(one_class.Level);
            }
            return 0;
        }

        public int GetSkillRanksPerLevel(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.SkillRanksPerLevel;
            }
            return 0;
        }

        public List<string> GetClassSkills(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.ClassSkills();
            }

            return new List<string>();
        }

        public int GetSkillsRanksForClasses()
        {
            int Count = 0;
            foreach (ClassWrapper one_class in Classes)
            {
                Count += one_class.ClassInstance.SkillRanksPerLevel;
            }
            return Count;
        }

        public StatBlockInfo.HitDiceCategories GetClassHDType(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.HitDiceType;
            }
            return StatBlockInfo.HitDiceCategories.None;
        }

        private ClassWrapper FindClass(string ClassName)
        {
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class;
            }
            return null;
        }        

        public int FindClassLevel(string ClassName)
        {           
            if (!Classes.Any()) return 0;

            ClassWrapper wrapper = FindClassInstance(ClassName);
            return wrapper == null ? 0 : wrapper.Level;         
        }

        public int FindAllClassLevels()
        {
            int sum = 0;
            foreach (ClassWrapper one_class in Classes)
            {               
                sum += one_class.Level;               
            }
            return sum;
        }

        public int FindAllClassLevelsExcludingClass(string ClassName)
        {
            ClassName = ClassName.ToLower();
            if (!Classes.Any()) return 0;
            int sum = 0;
            foreach (ClassWrapper one_class in Classes)
            {
                if (one_class.ClassInstance.Name.ToLower() != ClassName)
                {
                    sum += one_class.Level;
                }
            }
            return sum;
        }
        public int GetFortSaveValue()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {                
                Count += StatBlockInfo.ParseSaveBonues(wrapper.Level, wrapper.ClassInstance.FortSaveType);
            }
            return Count;
        }

        public int GetRefSaveValue()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {
                Count += StatBlockInfo.ParseSaveBonues(wrapper.Level, wrapper.ClassInstance.RefSaveType);
            }
            return Count;
        }

        public int GetWillSaveValue()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {
                Count += StatBlockInfo.ParseSaveBonues(wrapper.Level, wrapper.ClassInstance.WillSaveType);
            }
            return Count;
        }

        public Dictionary<string, int> GetBloodlineBonusSpells()
        {
            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.ClassInstance.BloodlineUse)
                {
                    return wrapper.ClassInstance.Bloodline.BonusSpells;
                }
            }

            return null;
        }

        public Dictionary<string, int> GetMysteryBonusSpells()
        {
            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.ClassInstance.MysteryUse)
                {
                    List<IMystery> temp = wrapper.ClassInstance.OracleMysteries;
                    return temp[0].BonusSpells(wrapper.Level);
                }
            }

            return null;
        }

        public List<string> GetPatronBonusSpells()
        {
            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.ClassInstance.PatronUse)
                {
                    List<string> temp = wrapper.ClassInstance.GetPatronSpells(wrapper.Level);
                    return temp;
                }
            }

            return new List<string>();
        }
       
        public int GetBABValue()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {
                Count += StatBlockInfo.ComputeBAB(wrapper.Level, wrapper.ClassInstance.BABType);
            }
            return Count;
        }

        public int GetBABValue(out string formula)
        {
            int Count = 0;
            formula = string.Empty;
            foreach (ClassWrapper wrapper in Classes)
            {
                int value = StatBlockInfo.ComputeBAB(wrapper.Level, wrapper.ClassInstance.BABType);
                Count += value;
                formula += wrapper.Name + ": " + wrapper.Level.ToString() + PathfinderConstants.SPACE +wrapper.ClassInstance.BABType.ToString()
                     + " computed BAB--" + value.ToString();
            }
            return Count;
        }

        public int GetNonMonkBABValue()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.Name != "Monk")
                {
                    Count += StatBlockInfo.ComputeBAB(wrapper.Level, wrapper.ClassInstance.BABType);
                }
            }
            return Count;
        }

        public int FindClassFeatCount()
        {
            if (Classes.Count() == 0) return 0;

            int Count = 0;
           
            foreach (ClassWrapper wrapper in Classes)
            {

                Count += wrapper.ClassInstance.ClassFeatCount(wrapper.Level, wrapper.Archetype);
            }
           
            return Count;
        }

        public int FindTotalClassLevels()
        {
            if (Classes.Count() == 0) return 0;
            int Levels = 0;

            foreach (ClassWrapper wrapper in Classes)
            {               
                Levels += wrapper.Level;
            }
            return Levels;
        }

        public int FindAlld8HDLevels()
        {
            int Count = 0;
            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.ClassInstance.HitDiceType == StatBlockInfo.HitDiceCategories.d8)
                {
                    Count += wrapper.Level;
                }
            }
            return Count;
        }

        public List<OnGoingPower> GetAllClassPowers()
        {
            List<OnGoingPower>  temp = new List<OnGoingPower>();
            foreach (ClassWrapper class_wrapper in Classes)
            {
                temp.AddRange(class_wrapper.ClassInstance.ClassPowers);
            }
            return temp;
        }

        public bool IsClassArchetype(string ClassName, string Archetype)
        {            
            if (!Classes.Any()) return false;
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                List<string> Archetypes = one_class.ClassInstance.ClassArchetypes;
                if (Archetypes.Exists(p => p == Archetype.ProperCase())) return true;
            }

            return false;
        }

        public List<string> GetClassArchetypeSkills(string ClassName, string Archetype)
        {            
            ClassWrapper one_class = FindClassWrapper(ClassName);
            if (one_class != null)
            {
                return one_class.ClassInstance.ClassArchetypeSkills(Archetype.ProperCase());             
            }

            return new List<string>();
        }

        public StatBlockInfo.WeaponProficiencies GetAllKnownWeaponsProficiencies()
        {
            StatBlockInfo.WeaponProficiencies temp = StatBlockInfo.WeaponProficiencies.None;

            foreach (ClassWrapper wrapper in Classes)
            {
                temp |= wrapper.ClassInstance.WeaponProficiencies;
            }

            return temp;
        }

        public StatBlockInfo.ArmorProficiencies GetAllKnownArmorProficiencies()
        {
            StatBlockInfo.ArmorProficiencies temp = StatBlockInfo.ArmorProficiencies.None;

            foreach (ClassWrapper wrapper in Classes)
            {
                temp |= wrapper.ClassInstance.ArmorProficiencies;
            }

            return temp;
        }

        public StatBlockInfo.ShieldProficiencies GetAllKnownShieldProficiencies()
        {
            StatBlockInfo.ShieldProficiencies temp = StatBlockInfo.ShieldProficiencies.None;

            foreach (ClassWrapper wrapper in Classes)
            {
                temp |= wrapper.ClassInstance.ShieldProficiencies;
            }

            return temp;
        }

        public List<string> GetAllWeaponProficienciesExtra()
        {
            List<string> temp = new List<string>();

            foreach (ClassWrapper wrapper in Classes)
            {
                temp.AddRange(wrapper.ClassInstance.GetWeaponProficienciesExtra());
            }

            return temp;
        }

        public List<string> GetAllShieldProficienciesExtra()
        {
            List<string> temp = new List<string>();

            foreach (ClassWrapper wrapper in Classes)
            {
                temp.AddRange(wrapper.ClassInstance.GetShieldProficienciesExtra());
            }

            return temp;
        }


        public void GetSpellOverLoadsForPrestigeClasses(out int overloadLevel, out int overloadClassLevel)
        {
            overloadLevel = 0;
            overloadClassLevel=0;

            foreach (ClassWrapper wrapper in Classes)
            {
                if (wrapper.ClassInstance.IsPrestigeClass)
                {
                    wrapper.ClassInstance.GetSpellOverLoads(ref overloadLevel, ref overloadClassLevel, wrapper.Level, wrapper.Level);
                }
            }
        }

        public List<CheckClassError> CheckClass(string ClassName)
        {           
            if (!Classes.Any()) return new List<CheckClassError>();

            ClassWrapper wrapper = FindClassInstance(ClassName);
            return wrapper.ClassInstance.CheckClass(wrapper.Level);
        }

        private ClassWrapper FindClassWrapper(string ClassName)
        {
           return Classes.Find(p => p.Name == ClassName.ProperCase());
        }

        private ClassWrapper FindClassInstance(string ClassName)
        {
            return Classes.Where(x => x.Name.ToLower() == ClassName.ToLower()).FirstOrDefault();
        }
    }

    public class ClassMasterInput
    {
        public string ClassList { get; set; }
        public string Domians { get; set; }
        public string Mysteries { get; set; }
        public string Bloodline { get; set; }
        public string Patron { get; set; }

        public ClassMasterInput (string classList, string domians, string mysteries, string bloodline, string patron)
        {
            ClassList = classList;
            Domians = domians;
            Mysteries = mysteries;
            Bloodline = bloodline;
            Patron = patron;

            FormatInput();
        }

        private void FormatInput()
        {
            int Pos = Patron.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos > 0)
            {
                Patron = Patron.Substring(0, Pos - 1).Trim();
            }

            Bloodline = Bloodline.Replace("*", string.Empty);
            Bloodline = Bloodline.Replace("-", PathfinderConstants.SPACE);


            ClassList = Utility.RemoveSuperScripts(ClassList);
            Domians = Utility.RemoveSuperScripts(Domians);
            Mysteries = Utility.RemoveSuperScripts(Mysteries);
            Bloodline = Utility.RemoveSuperScripts(Bloodline);
            Patron = Utility.RemoveSuperScripts(Patron);

            if (Bloodline.Length > 0)
            {
                Bloodline = Bloodline.ProperCase();
                Bloodline = Bloodline.Replace(PathfinderConstants.SPACE, string.Empty);
            }

            if (Mysteries.Length > 0)
            {
                Mysteries = Mysteries.ProperCase();
                Mysteries = Mysteries.Replace(PathfinderConstants.SPACE, string.Empty);
                Mysteries = Mysteries.Replace("*", string.Empty);
                Pos = Mysteries.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos > 0)
                {
                    Mysteries = Mysteries.Substring(0, Pos).Trim();
                }
            }
        }
    }
}
