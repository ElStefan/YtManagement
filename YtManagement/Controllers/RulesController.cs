using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YtManagement.Common.Model;
using YtManagement.Repository;

namespace YtManagement.Controllers
{
    [Route("api/[controller]")]
    public class RulesController : Controller
    {
        private IRulesRepository _rulesRepository;

        public RulesController(IRulesRepository rulesRepository)
        {
            this._rulesRepository = rulesRepository;
        }

        [HttpGet]
        public ActionResult<List<ManagementRule>> Get() => _rulesRepository.GetAll();
        
        [HttpGet("{id}")]
        public ActionResult<ManagementRule> Get(int id) => _rulesRepository.Get(id);

        [HttpPost]
        public Common.Model.ActionResult Post([FromBody]ManagementRule item) => _rulesRepository.Update(item);

        [HttpPut()]
        public Common.Model.ActionResult Put([FromBody]ManagementRule item) => _rulesRepository.Add(item);

        [HttpDelete("{id}")]
        public Common.Model.ActionResult Delete(int id) => _rulesRepository.Delete(id);
    }
}
