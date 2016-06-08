using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;

namespace Web_BackEnd.Interfaces
{
    public interface IProjectService : IService<Project>
    {
        List<Project> GetAll();
    }
}