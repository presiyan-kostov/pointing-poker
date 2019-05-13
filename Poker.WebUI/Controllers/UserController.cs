using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Poker.Model.Project;
using Poker.Model.User;
using Poker.Service.Interfaces;

namespace Poker.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        #region -- private readonly fields --

        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        #endregion

        #region -- constructor --

        public UserController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        #endregion

        #region -- public methods --

        [HttpGet("{username}")]
        public ActionResult Get(string username)
        {
            try
            {
                UserModel result = _userService.Get(username.Trim());

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] SaveModel model)
        {
            try
            {
                IList<ValidationError> validationErrors = _userService.Register(model);

                if (validationErrors.Count > 0)
                {
                    return BadRequest(validationErrors);
                }

                UserModel user = _userService.Get(model.Username.Trim());

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{username}/projects")]
        public ActionResult GetProjects(string username)
        {
            try
            {
                IList<ProjectModel> result = _projectService.GetForUser(username);

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