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

        // POST: api/Payrolls/AddPayroll
        [HttpPost("AddPayroll")]
        public async Task<ActionResult<object>> AddPayroll(Payroll payroll)
        {
            if (payroll == null) 
                return BadRequest("Payroll cannot be null.");

            var employee = await _unitOfWork.Employees.GetByIdAsync(payroll.EmployeeId);
            if (employee == null) 
                return NotFound($"Employee with ID {payroll.EmployeeId} not found.");

            payroll.Salary = employee.Salary; // No casting needed
            await _unitOfWork.Payrolls.AddAsync(payroll);
            await _unitOfWork.SaveAsync();

            var response = new
            {
                Payroll = new
                {
                    payroll.Id,
                    payroll.EmployeeId,
                    payroll.Date,
                    payroll.Salary,
                    payroll.Bonus,
                    payroll.IsComplete
                },
                Employee = new
                {
                    employee.FirstName,
                    employee.LastName,
                    employee.Salary
                }
            };

            return CreatedAtAction(nameof(GetPayrollById), new { id = payroll.Id }, response);
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
