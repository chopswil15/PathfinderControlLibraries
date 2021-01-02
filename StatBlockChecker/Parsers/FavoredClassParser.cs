using ClassManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker.Parsers
{
    public class FavoredClassParser
    {
        private SBCheckerBaseInput _sbCheckerBaseInput;
        private FavoredClassData _favoredClassData;
        private int _hdModifier;
        private int _maxHPMod;

        public FavoredClassParser(SBCheckerBaseInput sbCheckerBaseInput, int hdModifier, int maxHPMod)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _hdModifier = hdModifier;
            _maxHPMod = maxHPMod;
            _favoredClassData = new FavoredClassData();
        }

        public FavoredClassData FindFavoredClass()
        {            
            try
            {
                string CheckName = "FindFavoredClass";
                _favoredClassData.FavoredClassHP = 0;
                _favoredClassData.FavoredClassLevels = 0;
                _favoredClassData.FavoredClass = string.Empty;
                _favoredClassData.FavoredClass2nd = string.Empty;
                bool HalfElf = _sbCheckerBaseInput.MonsterSB.Race == "half-elf" ? true : false;

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("animal companion")) return _favoredClassData;

                if (!_sbCheckerBaseInput.CharacterClasses.Classes.Any()) return _favoredClassData;

                ClassWrapper wrapperHold = new ClassWrapper();
                if (_sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass).Count() == 1)
                {
                    wrapperHold = _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass).First();
                    _favoredClassData.FavoredClass = wrapperHold.Name;
                    _favoredClassData.FavoredClassLevels = wrapperHold.Level;
                    return _favoredClassData;
                }

                int ModDiff = _hdModifier - _maxHPMod;

                //more than one class but no FavoredCLass points assigned to HP
                //so we just take the 1st nonPrestigeClass
                if (ModDiff == 0 && _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass).Any())
                {
                    wrapperHold = _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass).First();
                    _favoredClassData.FavoredClass = wrapperHold.Name;
                    _favoredClassData.FavoredClassLevels = wrapperHold.Level;
                    if (HalfElf) FindNextFavoredClass();
                    return _favoredClassData;
                }

                List<ClassWrapper> tempWrappers = _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass
                                                          && x.Level == ModDiff).ToList<ClassWrapper>();


                //only one class can justify HP mod diff
                //so it is FC
                if (tempWrappers.Count == 1)
                {
                    wrapperHold = tempWrappers.First();
                    _favoredClassData.FavoredClass = wrapperHold.Name;
                    _favoredClassData.FavoredClassLevels = wrapperHold.Level;
                    if (HalfElf) FindNextFavoredClass();
                    return _favoredClassData;
                }

                if (!tempWrappers.Any() && HalfElf && _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels() == ModDiff)
                {
                    tempWrappers = _sbCheckerBaseInput.CharacterClasses.Classes;
                    wrapperHold = tempWrappers.First();
                    _favoredClassData.FavoredClass = wrapperHold.Name;
                    _favoredClassData.FavoredClassLevels = wrapperHold.Level;
                    FindNextFavoredClass();
                    return _favoredClassData;
                }

                //no classes support HP mod diff
                //can't tell add fail
                if (!tempWrappers.Any())
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Can't determin Favored Class, No Class has level above ModDiff of " + ModDiff.ToString());
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Can't determin Favored Class, more than one Class has level above ModDiff of " + ModDiff.ToString());
                }

                tempWrappers = _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass
                                                        && x.Level >= ModDiff).ToList<ClassWrapper>();

                //can't tell
                //so we just take the 1st nonPrestigeClass  
                if (tempWrappers.Any())
                {
                    wrapperHold = tempWrappers.First();
                    _favoredClassData.FavoredClass = wrapperHold.Name;
                    _favoredClassData.FavoredClassLevels = wrapperHold.Level;
                    if (HalfElf) FindNextFavoredClass();
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("FindFavoredClass", ex.Message);
            }

            return _favoredClassData;
        }

        private void FindNextFavoredClass()
        {
            string CheckName = "FindNextFavoredClass";
            List<ClassWrapper> tempWrappers = _sbCheckerBaseInput.CharacterClasses.Classes.Where(x => !x.ClassInstance.IsPrestigeClass
                                                       && x.Name != _favoredClassData.FavoredClass).ToList<ClassWrapper>();

            //hasn't picked another class yet as FC
            if (!tempWrappers.Any()) return;

            if (tempWrappers.Count == 1)
            {
                _favoredClassData.FavoredClass2nd = tempWrappers[0].Name;
                _favoredClassData.FavoredClassLevels += tempWrappers[0].Level;
                return;
            }
        }
    }

    public class FavoredClassData : IFavoredClassData
    {
        public string FavoredClass { get; set; }
        public int FavoredClassLevels { get; set; }
        public string FavoredClass2nd { get; set; } //half-evles only
        public int FavoredClassHP { get; set; }
    }
}
