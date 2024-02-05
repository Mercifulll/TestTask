using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Domain;
using MyBooks.Service.Interfaces;

namespace MyBooks.Controllers;

public class BookController : Controller
{
    private readonly IAuthorService _authorService;

    public BookController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> TaskHandler()
    {
        var response = await _authorService.GetAuthors();
        return Json(new {data = response.Data});
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(SaveViewModel model)
    {
        var response = await _authorService.AddAuthor(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBook(int authorId, SaveViewModel model)
    {
        var response = await _authorService.AddBook(authorId, model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
    
    [HttpPost("GetExistingAuthorIdByLastName")]
    public async Task<ActionResult<int?>> GetExistingAuthorIdByLastName(string lastName)
    {
        try
        {
            var authorId = await _authorService.GetExistingAuthorIdByLastName(lastName);
            return Ok(new { data = authorId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { description = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateViewModel model)
    {
        var response = await _authorService.UpdateAuthor(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteAuthor(int authorId)
    {
        var response = await _authorService.DeleteAuthor(authorId);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description});
        }
        return BadRequest(new { description = response.Description });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAuthorById(int authorId)
    {
        var author = await _authorService.GetAuthorById(authorId);
        return Json(author);
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthorDetails(int authorId)
    {
        try
        {
            var authorDetails = await _authorService.GetAuthorDetails(authorId);
            return Json(authorDetails);
        }
        catch (Exception ex)
        {
            // Log the error and return an error response
            return BadRequest(new { description = "Error fetching author details" });
        }
    }

}