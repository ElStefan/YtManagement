using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YtManagement.Model;
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
        public ActionResult<List<ManagementRule>> Get()
        {
            return _rulesRepository.GetAll();
        }

        
        [HttpGet("{id}")]
        public ActionResult<ManagementRule> Get(int id)
        {
            return _rulesRepository.Get(id);
        }

        [HttpPost]
        public Model.ActionResult Post([FromBody]ManagementRule item)
        {
            return _rulesRepository.Update(item);
        }
        
        [HttpPut()]
        public Model.ActionResult Put([FromBody]ManagementRule item)
        {
            return _rulesRepository.Add(item);
        }
        
        [HttpDelete("{id}")]
        public Model.ActionResult Delete(int id)
        {
            return _rulesRepository.Delete(id);
        }
    }
}
