using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YtManagement.Model;

namespace YtManagement.Repository
{
    public interface IRulesRepository
    {
        ActionResult<int> Add(ManagementRule item);
        ActionResult Delete(int id);
        ActionResult Update(ManagementRule item);
        ActionResult<ManagementRule> Get(int id);
        ActionResult<List<ManagementRule>> GetAll();
    }
}
