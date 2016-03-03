using FairyGroup.Models.DBFairyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FairyGroup.Models.Home
{
    public class ThaiAreaModel
    {
        public List<ProvinceGroup> mdProvinceGroup { get; set; }
        public List<Province> mdProvince { get; set; }
        public List<District> mdDistrict { get; set; }
        public List<SubDistrict> mdSubDistrict { get; set; }
    }
}