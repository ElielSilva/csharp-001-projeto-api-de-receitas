using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{    
    public readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        this._service = service;        
    }

    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {
        User user = _service.GetUser(email);
        return (user != null) ? Ok(user) : NotFound();
    }

    [HttpPost("/user")]
    public IActionResult Create([FromBody]User user)
    {
        try
        {
            _service.AddUser(user);
            return Created("201", user);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody]User user)
    {
        User result = _service.GetUser(email);
        if(result == null) return NotFound();
        if(result.Email != user.Email) return BadRequest();
        _service.UpdateUser(user);
        return Ok(user);
    }

    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        var user = _service.GetUser(email);
        if (user == null) return NotFound();
        _service.DeleteUser(email);
        return NoContent();
    } 
}