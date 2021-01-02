using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.Spell_SB;
using CommonStrings;
using Utilities;
using PathfinderGlobals;

namespace StatBlockCommon.ReflectionWrappers
{
    public static class SpellReflectionWrapper
    {

        public static string CastSpell(string SpellName, int CasterLevel, List<IndividualStatBlock_Combat> Targets)
        {
            SpellName = SpellName.Replace("/", PathfinderConstants.SPACE);
            SpellName = SpellName.ProperCase();
            SpellName = SpellName.Replace(PathfinderConstants.SPACE, "_");
            SpellName = SpellName.Replace("'", string.Empty); 
            SpellName = SpellName.Replace(",", string.Empty);
            //IndividualStatBlock_Combat oTargetStatBlock = null;

            Assembly a = null;
            try
            {
                a = Assembly.Load("SpellEffects");
            }
            catch (FileNotFoundException ex)
            {

            }

            Type spell = a.GetType("SpellEffects.CastSpell"); //null if not found
            object obj = Activator.CreateInstance(spell);

            MethodInfo mi = spell.GetMethod(SpellName);

            if (mi == null)
            {
                throw new Exception("Missing Spell Effect: " + SpellName);
            }
            
            ParameterInfo[] ParamInfos = mi.GetParameters();
            object[] args = new object[ParamInfos.Count()];
            int cnt = 0;
            foreach (ParameterInfo pi in ParamInfos)
            {
                switch (pi.Name)
                {
                    case "CasterLevel":
                        args[cnt] = CasterLevel;
                        break;
                    case "target":
                        args[cnt] = Targets[0];
                        break;
                    case "targets":
                        args[cnt] = Targets;
                        break;
                }
                cnt++;
            }
            if (ParamInfos.Any())
            {
                return (string)mi.Invoke(obj, args);
            }
            else
            {
                return (string)mi.Invoke(obj, null);
            }
            
        }
        
        public static string CastSpell(string SpellName, SpellStatBlock_Combat SpellData, List<IndividualStatBlock_Combat> Targets)
        {
            SpellName = SpellName.Replace(PathfinderConstants.SPACE, "_");            
            //IndividualStatBlock_Combat oTargetStatBlock = null;

            Assembly a = null;
            try
            {
                a = Assembly.Load("SpellEffects");
            }
            catch (FileNotFoundException ex)
            {

            }

            Type spell = a.GetType("SpellEffects.CastSpell"); //null if not found
            object obj = Activator.CreateInstance(spell);

            MethodInfo mi = spell.GetMethod(SpellName);

            if (mi == null)
            {
                throw new Exception("Missing Spell Effect: " + SpellName);
            }
            
            ParameterInfo[] ParamInfos = mi.GetParameters();
            object[] args = new object[ParamInfos.Count()];
            int cnt = 0;
            foreach (ParameterInfo pi in ParamInfos)
            {
                switch (pi.Name)
                {
                    case "CasterLevel":
                        args[cnt] = SpellData.CasterLevel;
                        break;
                    case "target":
                        args[cnt] = Targets[0];
                        break;
                    case "targets":
                        args[cnt] = Targets;
                        break;
                }
                cnt++;
            }
            if (ParamInfos.Any())
            {
                return (string)mi.Invoke(obj, args);
            }
            else
            {
                return (string)mi.Invoke(obj, null);
            }
           
        }
    }
}
