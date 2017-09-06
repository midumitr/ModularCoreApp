using Microsoft.AspNetCore.Mvc;
using System;

namespace Module.A
{
    [Route("api/[Controller]")]
    public class ModuleAController : Controller
    {
        private readonly ITestClassA _testClassA;
        public ModuleAController(ITestClassA testClassA)
        {
            _testClassA = testClassA;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_testClassA.GetTime());
        }
    }
}
