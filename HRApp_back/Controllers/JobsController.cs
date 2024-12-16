using HRApp_back.DataAccess.Data;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JobsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllJobs")]
    public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
    {
        var jobs = await _unitOfWork.Jobs.GetAllAsync();
        return Ok(jobs);
    }

    [HttpPost("AddJob")]
    public async Task<ActionResult<Job>> CreateJob(Job job)
    {
        if (job == null) return BadRequest("Job cannot be null");

        job.PostedDate = DateTime.UtcNow;
        job.Status = JobStatus.Open;
        
        await _unitOfWork.Jobs.AddAsync(job);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, job);
    }

    [HttpGet("GetJobById/{id}")]
    public async Task<ActionResult<Job>> GetJobById(int id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        if (job == null) return NotFound();

        return Ok(job);
    }
    
    [HttpGet("GetJobByIdWithCandidates/{id}")]
    public async Task<ActionResult<Job>> GetJobByIdWithCandidates(int id)
    {
        var job = await _unitOfWork.Jobs.GetJobWithCandidatesAsync(id);
        if (job == null) return NotFound();

        return Ok(job);
    }
    
    [HttpPut("UpdateJob/{id}")]
    public async Task<IActionResult> PutJob(int id, Job job)
    {
        if (id != job.Id)
        {
            return BadRequest("Job ID mismatch.");
        }

        _unitOfWork.Jobs.Update(job);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await JobExists(id)))
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

    [HttpDelete("DeleteJob/{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        if (job == null)
        {
            return NotFound();
        }

        _unitOfWork.Jobs.Delete(job);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    private async Task<bool> JobExists(int id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        return job != null;
    }
}
