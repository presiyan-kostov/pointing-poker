﻿using System;
using Microsoft.AspNetCore.Mvc;
using Poker.Model.User;
using Poker.Service.Interfaces;

namespace Poker.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        #region -- private readonly fields --

        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        #endregion

        #region -- constructor --

        public AuthenticationController(IAuthenticationService authenticationService, 
                                        IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        #endregion

        [HttpGet("login")]
        public ActionResult Login(string username, string password)
        {
            try
            {
                int? userId = _authenticationService.Authenticate(username.Trim(), password.Trim());

                if (!userId.HasValue)
                {
                    return BadRequest();
                }

                UserModel model = _userService.Get(userId.Value);

                return Ok(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}