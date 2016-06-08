using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;

namespace ProjectPool.Models
{
    public class ProjectViewModel
    {

        public HttpPostedFileBase uploadProjectImage { get; set; }
        public string projectTags { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateModified { get; set; }
        public string Description { get; set; }
        public string GUID { get; set; }
        public string ImageName { get; set; }
        public int ProjectId { get; set; }
        public string References { get; set; }
        public string Title { get; set; }
        public string VedioUrl { get; set; }
        public int Views { get; set; }

        //public List<ProjectStepViewModel> ProjectSteps { get; set; }
    }
}