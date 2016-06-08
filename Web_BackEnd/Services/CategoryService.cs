using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;
using ProjectPool.Library;
using ProjectPool.Repository;

namespace ProjectPool.Services
{
    public class CategoryService : GenericRepository<Category>
    {
        string ConnectionString;

        public CategoryService(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public int GetCateGoryId(string Category)
        {
            return Load(x => x.Name == Category).Select(x => x.CategoryId).FirstOrDefault();
        }


    }
}