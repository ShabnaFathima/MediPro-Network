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
    public class PatientController : ControllerBase
    {
        [HttpPost]
        [ActionName("AddUser")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddUser(PatientBOL usr)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<PatientBOL>();
            try
            {
                res = new PatientDAL().AddUser(usr);
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
        [HttpPost]
        [ActionName("AddPatient")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddPatient(PatientDetailsBOL patient)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<PatientDetailsBOL>();
            try
            {
                res = new PatientDAL().AddPatient(patient);
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
        [ActionName("GetPatientById")]
        [ProducesResponseType(200, Type = typeof(List<PatientDetailsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPatientById(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new PatientDetailsBOL();
            try
            {
                res = new PatientDAL().GetPatientById(id);
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
        [ActionName("GetUserById")]
        [ProducesResponseType(200, Type = typeof(List<PatientBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetUserById(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new PatientBOL();
            try
            {
                res = new PatientDAL().GetUserById(id);
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
        [ActionName("GetPatientId")]
        [ProducesResponseType(200, Type = typeof(List<PatientBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetPatientId(String email)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new PatientBOL();
            try
            {
                res = new PatientDAL().GetPatientId(email);
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
        [ActionName("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(List<PatientBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllUsers()
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<PatientBOL>();
            try
            {
                res = new PatientDAL().GetAllUsers();
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
