using BlazorTechnicalExam.Server.Interfaces;
using BlazorTechnicalExam.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTechnicalExam.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageLibraryController : ControllerBase
    {
        private readonly IImageLibraryService _imageLibraryService;

        public ImageLibraryController(IImageLibraryService imageLibraryService)
        {
            _imageLibraryService = imageLibraryService;
        }

        [HttpGet]
        public IEnumerable<ImageLibrary> Get()
        {
            return _imageLibraryService.GetImageLibraryList()
                .ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<ImageLibrary> GetById(int id)
        {
            var imageLibrary = _imageLibraryService.GetImageLibraryById(id);

            if (imageLibrary == null)
            {
                return NotFound();
            }

            return imageLibrary;
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ImageLibrary imageLibrary)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            imageLibrary = _imageLibraryService.InsertImageLibrary(imageLibrary);

            return Ok(imageLibrary);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ImageLibrary imageLibrary)
        {
            imageLibrary = _imageLibraryService.UpdateImageLibrary(imageLibrary);

            return Ok(imageLibrary);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _imageLibraryService.DeleteImageLibrary(id);

            return Ok();
        }
    }
}
