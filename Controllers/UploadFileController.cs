using Microsoft.AspNetCore.Mvc;

namespace UploadFile.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadFileController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
    {
        var file = request.File;

        if (file.Length <= 0) return BadRequest("File Not found");

        if (!string.Equals(Path.GetExtension(file.FileName), "*.xls", StringComparison.OrdinalIgnoreCase))
            return BadRequest("File Not supported");

        var filePath = "assets/" + file.FileName;

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { message = "Archivo subido con éxito", filePath });
    }
}
public record FileUploadRequest(IFormFile File);