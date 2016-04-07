using FairyGroup.Models.DBFairyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FairyGroup.Models.Home
{
    public class PostBuildingDetailModel
    {
        public List<spGetPostGroupDetail_Result> mdPostGroupDetail { get; set; }
        public List<spGetPostDetail_Result> mdPostDetail { get; set; }
    }
}