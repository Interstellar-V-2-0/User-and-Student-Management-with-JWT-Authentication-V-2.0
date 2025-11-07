using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStudentMgmt.Application.Interfaces;

namespace UserStudentMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentTypesController : ControllerBase
{
    private readonly IDocumentTypeService _docTypeService;

    public DocumentTypesController(IDocumentTypeService docTypeService)
    {
        _docTypeService = docTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var docs = await _docTypeService.GetAllAsync();
        return Ok(docs);
    }
}