﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YtManagement.Core;

namespace YtManagement.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ISystemCore _core;

        public ValuesController(ISystemCore core)
        {
            this._core = core;
        }
        // GET api/values
        [HttpGet]
        public int Get()
        {
            return _core.Value;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _core.Value = id;
            return "OK";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
