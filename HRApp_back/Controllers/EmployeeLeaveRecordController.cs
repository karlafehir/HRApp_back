using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.DTOs;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeLeaveRecordsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeLeaveRecordsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Get all Employee Leave Records
    [HttpGet("GetAllLeaveRecords")]
    public async Task<ActionResult<IEnumerable<EmployeeLeaveRecord>>> GetAllLeaveRecords()
    {
        var leaveRecords = await _unitOfWork.EmployeeLeaveRecords.GetAllAsync();

        var leaveRecordDtos = leaveRecords.Select(record => new EmployeeLeaveRecord
        {
            EmployeeId = record.EmployeeId,
            AnnualLeaveDays = record.AnnualLeaveDays,
            SickLeaveDays = record.SickLeaveDays,
            RemainingAnnualLeave = record.RemainingAnnualLeave,
            RemainingSickLeave = record.RemainingSickLeave
        }).ToList();

        return Ok(leaveRecordDtos);
    }

    // Get Employee Leave Record by ID
    [HttpGet("GetLeaveRecordById/{id}")]
    public async Task<ActionResult<EmployeeLeaveRecord>> GetLeaveRecordById(int id)
    {
        var leaveRecord = await _unitOfWork.EmployeeLeaveRecords.GetByIdAsync(id);

        if (leaveRecord == null)
        {
            return NotFound("Leave record not found.");
        }

        var leaveRecordDto = new EmployeeLeaveRecord
        {
            EmployeeId = leaveRecord.EmployeeId,
            AnnualLeaveDays = leaveRecord.AnnualLeaveDays,
            SickLeaveDays = leaveRecord.SickLeaveDays,
            RemainingAnnualLeave = leaveRecord.RemainingAnnualLeave,
            RemainingSickLeave = leaveRecord.RemainingSickLeave
        };

        return Ok(leaveRecordDto);
    }

    // Add a new Employee Leave Record
    [HttpPost("AddLeaveRecord")]
    public async Task<ActionResult<EmployeeLeaveRecord>> AddLeaveRecord(EmployeeLeaveRecord leaveRecord)
    {
        await _unitOfWork.EmployeeLeaveRecords.AddAsync(leaveRecord);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetLeaveRecordById), new { id = leaveRecord.Id }, leaveRecord);
    }

    // Update an Employee Leave Record
    [HttpPut("UpdateLeaveRecord/{id}")]
    public async Task<IActionResult> UpdateLeaveRecord(int id, EmployeeLeaveRecord leaveRecord)
    {
        if (id != leaveRecord.Id)
        {
            return BadRequest("Leave record ID mismatch.");
        }

        _unitOfWork.EmployeeLeaveRecords.Update(leaveRecord);

        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await LeaveRecordExists(id)))
            {
                return NotFound("Leave record not found.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Delete an Employee Leave Record
    [HttpDelete("DeleteLeaveRecord/{id}")]
    public async Task<IActionResult> DeleteLeaveRecord(int id)
    {
        var leaveRecord = await _unitOfWork.EmployeeLeaveRecords.GetByIdAsync(id);
        if (leaveRecord == null)
        {
            return NotFound("Leave record not found.");
        }

        _unitOfWork.EmployeeLeaveRecords.Delete(leaveRecord);
        await _unitOfWork.SaveAsync();

        return Ok("Leave record deleted.");
    }

    // Helper method: Check if a leave record exists
    private async Task<bool> LeaveRecordExists(int id)
    {
        var leaveRecord = await _unitOfWork.EmployeeLeaveRecords.GetByIdAsync(id);
        return leaveRecord != null;
    }
}
