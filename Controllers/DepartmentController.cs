using MediProApi.BOL;
using MediProApi.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediProApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public class DepartmentController : ControllerBase
    {
        [HttpPost]
        [ActionName("AddDep")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddDep(DepartmentBOL dep)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<DepartmentBOL>();
            try
            {
                res = new DepartmentDAL().AddDep(dep);
                Tres = new ResultBOL
                {
                    RecordStatus = (res) ? "Success" : "Failed",
                    Details = res.ToString()
                };


                if (res)
                {
                    IsExist = true;
                    return StatusCode(200, Tres);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CatelogueController - InsertCategory() : " + ex.ToString());
                return StatusCode(500);
            }

            return StatusCode(400, Tres);
        }

        [HttpGet]
        [ActionName("GetDepartment")]
        [ProducesResponseType(200, Type = typeof(List<DepartmentBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDepartment(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<DepartmentBOL>();
            try
            {
                res = new DepartmentDAL().GetDepartmentById(id);
                if (res != null)
                {
                    IsExist = true;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("MasterDataController - GetDepartment() : " + ex.ToString());
                return StatusCode(500);
            }
            if (IsExist)
            {
                return Ok(res);
            }
            return StatusCode(204);
        }


         [HttpGet]
         [ActionName("GetAllDepartment")]
         [ProducesResponseType(200, Type = typeof(List<DepartmentBOL>))]
         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
         public IActionResult GetAllDepartment()
         {
             if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
             if (!ModelState.IsValid)
             {
                 return BadRequest();
             }
             bool IsExist = false;
             var res = new List<DepartmentBOL>();
             try
             {
                 res = new DepartmentDAL().GetAllDepartment();
                 if (res.Count > 0)
                 {
                     IsExist = true;
                 }
             }
             catch (Exception ex)
             {
                 Utility.WriteErrorLog("MasterDataController - GetDepartment() : " + ex.ToString());
                 return StatusCode(500);
             }
             if (IsExist)
             {
                 return Ok(res);
             }
             return StatusCode(204);
         }

    }
}
