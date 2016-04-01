using FairyGroup.Models.DBFairyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FairyGroup.Models
{
    public class PostBuildingModel
    {
        public PostBuilding mdPost { get; set; }
        public List<spGetPostDetail_Result> mdPostDetail { get; set; }

        public PostType msPostType { get; set; }
        public BuildingType msBuildingType { get; set; }
    }
}