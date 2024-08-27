using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CandidatesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetCandidates")]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
    {
        var candidates = await _unitOfWork.Candidates.GetAllAsync(includeProperties: "Job");
        return Ok(candidates);
    }
    
    [HttpGet("GetCandidateById/{id}")]
    public async Task<ActionResult<Candidate>> GetCandidateById(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id, includeProperties: "Job");

        if (candidate == null)
        {
            return NotFound();
        }

        return Ok(candidate);
    }

    [HttpPost("AddCandidate")]
    public async Task<ActionResult<Candidate>> AddCandidate(Candidate candidate)
    {
        await _unitOfWork.Candidates.AddAsync(candidate);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.Id }, candidate);
    }

    [HttpPut("UpdateCandidate/{id}")]
    public async Task<IActionResult> UpdateCandidate(int id, Candidate candidate)
    {
        if (id != candidate.Id)
        {
            return BadRequest();
        }

        _unitOfWork.Candidates.Update(candidate);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await CandidateExists(id)))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("DeleteCandidate/{id}")]
    public async Task<IActionResult> DeleteCandidate(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }

        _unitOfWork.Candidates.Delete(candidate);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    private async Task<bool> CandidateExists(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);
        return candidate != null;
    }
}
