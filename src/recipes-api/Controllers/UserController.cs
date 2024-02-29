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

    // 6 - Sua aplicação deve ter o endpoint GET /user/:email
    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {
        User user = _service.GetUser(email);
        return (user != null) ? Ok(user) : NotFound();
    }

    // 7 - Sua aplicação deve ter o endpoint POST /user
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

    // "8 - Sua aplicação deve ter o endpoint PUT /user
    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody]User user)
    {
        User result = _service.GetUser(email);
        if(result == null) return NotFound();
        if(result.Email != user.Email) return BadRequest();
        _service.UpdateUser(user);
        return Ok(user);
    }

    // 9 - Sua aplicação deve ter o endpoint DEL /user
    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        var user = _service.GetUser(email);
        if (user == null) return NotFound();
        _service.DeleteUser(email);
        return NoContent();
    } 
}