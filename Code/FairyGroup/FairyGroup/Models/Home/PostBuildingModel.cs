using FairyGroup.Models.DBFairyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FairyGroup.Models.Home
{
    public class PostBuildingModel
    {
        public PostBuilding mdPost { get; set; }
        //public List<spGetPostDetail_Result> mdPostDetail { get; set; }

        public List<PostType> mdPostType { get; set; }
        public List<BuildingType> mdBuildingType { get; set; }

        public List<ProvinceGroup> mdProvinceGroup { get; set; }
        public List<Province> mdProvince { get; set; }
        public List<District> mdDistrict { get; set; }
        public List<SubDistrict> mdSubDistrict { get; set; }
    }
}