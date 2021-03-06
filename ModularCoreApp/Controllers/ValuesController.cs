﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ModularCoreApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public ValuesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_actionDescriptorCollectionProvider);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
