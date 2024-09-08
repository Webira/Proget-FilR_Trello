using Microsoft.AspNetCore.Mvc;
using owi_back.Context;
using owi_back.DAO;
using owi_back.DTO;
using owi_back.Mapping;
using owi_back.Models;

namespace owi_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly CommentDAO _DAO;
    private readonly Mapper _mapper;

    public CommentController(CommentDAO dao, Mapper mapper)
    {
        _DAO = dao;
        _mapper = mapper;
    }

    // GET: api/Comment
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        var response = await _DAO.GetComments();
        return Ok(response);
    }

    // GET: api/Comment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDTO>> GetComment(int id)
    {
        var comment = await _DAO.GetComment(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(_mapper.CommentToDTO(comment));
    }

    // GET: api/Comment/Task/5
    [HttpGet("Task/{taskId}")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentByTaskIdAndListingId(int taskId)
    {
        var comments = await _DAO.GetCommentsByTaskId(taskId);
        if (comments == null)
        {
            return NotFound();
        }
        return Ok(comments);
    }


    // GET: api/Comment/Task/5
    [HttpGet("Task/{taskId}/{listingId}")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentByTaskIdAndListingId(int taskId, int listingId)
    {
        var comments = await _DAO.GetCommentsByTaskAndListingId(taskId, listingId);
        if (comments == null)
        {
            return NotFound();
        }
        return Ok(comments);
    }

    // PUT: api/Comment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(int id, Comment Comment)
    {
        if (id != Comment.Id)
        {
            return BadRequest();
        }

        var result = await _DAO.UpdateComment(Comment);
        return Ok(result);
    }

    // POST: api/Comment
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment(Comment Comment)
    {
        var response = await _DAO.AddComment(Comment);
        return Ok(response);
    }

    // DELETE: api/Comment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var response = await _DAO.DeleteComment(id);
        return Ok(response);
    }
}
