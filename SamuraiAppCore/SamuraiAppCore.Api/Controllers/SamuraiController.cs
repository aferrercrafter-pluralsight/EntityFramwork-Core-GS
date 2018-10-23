using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiAppCore.Data;
using SamuraiAppCore.Domain;

namespace SamuraiAppCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraiController : ControllerBase
    {
        private DisconnectedData _repo;

        public SamuraiController(DisconnectedData repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<int, string>> Get()
        {
            return _repo.GetSamuraiReferenceList();
        }

        [HttpGet("{id}")]
        public Samurai Get(int id)
        {
            return _repo.LoadSamuraiGraph(id);
        }

        [HttpPost]
        public void Post([FromBody] Samurai value)
        {
            _repo.SaveSamuraiGraph(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Samurai value)
        {
            _repo.SaveSamuraiGraph(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteSamuraiGraph(id);
        }
    }
}