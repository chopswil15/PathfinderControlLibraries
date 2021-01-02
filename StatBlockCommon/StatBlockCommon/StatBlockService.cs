using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Feat_SB;
using PathfinderDomains;
using AutoMapper;
using PathfinderContext.Services;

namespace StatBlockCommon
{
    public class StatBlockService
    {
       // private static FeatService _featService;

        static StatBlockService()
        {
            Mapper.CreateMap<FeatStatBlock, feats>();
            Mapper.CreateMap<feats, FeatStatBlock>();
        }

        #region Feat

        private static feats MapThisToFeatObject(FeatStatBlock SB)
        {
            return Mapper.Map<FeatStatBlock, feats>(SB);
        }

        private static FeatStatBlock MapThisToFeatStatBlockObject(feats Feats)
        {
            return Mapper.Map<feats, FeatStatBlock>(Feats);
        }

        //public static bool AddFeat(ref IEnumerable<string> Error, FeatStatBlock SB)
        //{
        //    Error = _featService.AddFeat(MapThisToFeatObject(SB));
        //    return Error.Count() == 0 ? true : false;
        //}

        //public static bool UpdateFeat(FeatStatBlock SB, ref IEnumerable<string> Error)
        //{
        //    SetFeatService();
        //    Error = _featService.UpdateFeat(MapThisToFeatObject(SB));
        //    return Error.Count() == 0 ? true : false;
        //}

        public static IFeatStatBlock GetFeatByName(string name)
        {
            FeatService _featService = new FeatService(StatBlockGlobals.ConnectionString);
            feats tempFeat = _featService.GetFeatByName(name);
            return MapThisToFeatStatBlockObject(tempFeat);
        }

        public static IFeatStatBlock GetMythicFeatByName(string name)
        {
            FeatService _featService = new FeatService(StatBlockGlobals.ConnectionString);
            feats tempFeat = _featService.GetMythicFeatByName(name);
            return MapThisToFeatStatBlockObject(tempFeat);
        }

        public static IFeatStatBlock GetFeatByNameSource(string name, string source)
        {
            FeatService _featService = new FeatService(StatBlockGlobals.ConnectionString);
            feats tempFeat = _featService.GetFeatByNameSource(name, source);
            return MapThisToFeatStatBlockObject(tempFeat);
        }       

        #endregion Feat
    }
}
