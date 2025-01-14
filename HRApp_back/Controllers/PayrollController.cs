using HRApp_back.DataAccess.Repository.IRepository;
using HRApp_back.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRApp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Payrolls/GetAllPayrolls
        [HttpGet("GetAllPayrolls")]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetAllPayrolls()
        {
            var payrolls = await _unitOfWork.Payrolls.GetAllAsync(includeProperties:"Employee");
            return Ok(payrolls);
        }

        // GET: api/Payrolls/GetPayrollById/{id}
        [HttpGet("GetPayrollById/{id}")]
        public async Task<ActionResult<Payroll>> GetPayrollById(int id)
        {
            var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id, includeProperties:"Employee");
            if (payroll == null) return NotFound();

            return Ok(payroll);
        }

        [HttpPost("GenerateMonthlyPayroll")]
        public async Task<IActionResult> GenerateMonthlyPayroll()
        {
            var currentDate = DateTime.UtcNow;

            // if (currentDate.Day != 14)
            //     return BadRequest("Payroll generation is only allowed on the 14th of each month.");

            // Check if payroll has already been generated for this month
            var existingPayrolls = await _unitOfWork.Payrolls
                .GetAllAsync(p => p.Date.Year == currentDate.Year && p.Date.Month == currentDate.Month);

            if (existingPayrolls.Any())
                return BadRequest("Payrolls for this month have already been generated.");

            var employees = await _unitOfWork.Employees.GetAllAsync();
            if (!employees.Any())
                return NotFound("No employees found.");

            foreach (var employee in employees)
            {
                var payroll = new Payroll
                {
                    EmployeeId = employee.Id,
                    Date = currentDate,
                    Salary = employee.Salary,
                    Bonus = 0, // Default bonus
                    IsComplete = false
                };

                await _unitOfWork.Payrolls.AddAsync(payroll);
            }

            await _unitOfWork.SaveAsync();

            return Ok();
        }
        
        [HttpDelete("DeletePayrollsForCurrentMonth")]
        public async Task<IActionResult> DeletePayrollsForCurrentMonth()
        {
            var currentDate = DateTime.UtcNow;

            var payrollsToDelete = await _unitOfWork.Payrolls
                .GetAllAsync(p => p.Date.Year == currentDate.Year && p.Date.Month == currentDate.Month);

            if (!payrollsToDelete.Any())
            {
                return NotFound("No payrolls found for the current month.");
            }

            foreach (var payroll in payrollsToDelete)
            {
                _unitOfWork.Payrolls.Delete(payroll);
            }

            await _unitOfWork.SaveAsync();

            return Ok("All payrolls for the current month have been deleted.");
        }
        
        // PUT: api/Payrolls/UpdatePayroll/{id}
        [HttpPut("UpdatePayroll/{id}")]
        public async Task<IActionResult> UpdatePayroll(int id, Payroll payroll)
        {
            if (id != payroll.Id) return BadRequest("Payroll ID mismatch.");

            _unitOfWork.Payrolls.Update(payroll);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await PayrollExists(id)))
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

        // DELETE: api/Payrolls/DeletePayroll/{id}
        [HttpDelete("DeletePayroll/{id}")]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
            if (payroll == null) return NotFound();

            _unitOfWork.Payrolls.Delete(payroll);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // Helper method to check if a payroll entry exists
        private async Task<bool> PayrollExists(int id)
        {
            var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
            return payroll != null;
        }
    }
}
