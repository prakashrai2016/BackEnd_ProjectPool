using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;

namespace ProjectPool.Models
{
    public class ProjectStepViewModel
    {
        public HttpPostedFileBase uploadStepImage { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateModified { get; set; }
        public string Description { get; set; }
        public string GUID { get; set; }
        public string ImageName { get; set; }
        public int ProjectId { get; set; }
        public int ProjectStepId { get; set; }
        public int Step { get; set; }
        public string Title { get; set; }
    }

    public class ProjectStepList
    {
        public List<ProjectStepViewModel> projectStepList { get; set; }

        public int projectId { get; set; }
    }
}