using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "GoodsService", IsNullable = false)]
    public class goods_services {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string category { get; set; }
        public virtual string cost { get; set; }
        public virtual string weight { get; set; }
        public virtual string source { get; set; }
        public virtual short? craft_dc { get; set; }
    }
}
