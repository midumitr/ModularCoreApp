using Microsoft.AspNetCore.Mvc;
using System;

namespace Module.B
{
    [Route("api/[Controller]")]
    public class ModuleBController :  Controller
    {
        private readonly ITestClassB _testClassA;
        public ModuleBController(ITestClassB testClassA)
        {
            _testClassA = testClassA;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_testClassA.GetModule());
        }
    }
}
