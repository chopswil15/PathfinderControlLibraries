using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;

namespace EquipmentBasic
{
    public class EquipmentGoods : IEquipment
    {
        public bool Masterwork { get; set; }
        public bool Broken { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Goods; }
        }

         #region Properties
        public int id { get; set; }
        public virtual string name { get; set; }
        public virtual string category { get; set; }
        public virtual string cost { get; set; }
        public virtual string weight { get; set; }
        public virtual string source { get; set; }
        public virtual short? craft_dc { get; set; }
        #endregion Properties      

        public EquipmentGoods()
        {
            name = string.Empty;
            category = string.Empty;
            cost = string.Empty;
            weight = string.Empty;
            source = string.Empty;
        }
    }
}
