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
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    [HttpGet]
    public IActionResult Get()
    {   
        return Ok(_service.GetRecipes());   
    }

    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {
        var result = _service.GetRecipe(name);
        if(result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("/recipe")]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        _service.AddRecipe(recipe);
        return Created("201",recipe);
    }

    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        try
        {
            _service.UpdateRecipe(recipe);
            return NoContent();
        }
        catch
        {   
            return BadRequest();
        }
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var result = this.Get(name);
        if(result == null) return NotFound();
        _service.DeleteRecipe(name);
        return NoContent();
    }    
}
