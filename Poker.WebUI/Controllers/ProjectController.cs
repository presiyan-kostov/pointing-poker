using System;
using Microsoft.AspNetCore.Mvc;
using Poker.Model.Project;
using Poker.Service.Interfaces;

namespace Poker.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Project")]
    public class ProjectController : Controller
    {
        #region -- private readonly fields --

        private readonly IProjectService _projectService;

        #endregion

        #region -- constructor --

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #endregion

        #region -- public methods --

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                ProjectModel result = _projectService.Get(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}