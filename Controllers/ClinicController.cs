using MediProApi.BOL;
using MediProApi.DAL;
using Microsoft.AspNetCore.Mvc;

namespace MediProApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public class ClinicController : ControllerBase
    {
        [HttpGet]
        [ActionName("GetAllClinics")]
        [ProducesResponseType(200, Type = typeof(List<ClinicBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllClinics()
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<ClinicBOL>();
            try
            {
                res = new ClinicDAL().GetClinic();
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

        [HttpGet]
        [ActionName("GetClinicDetails")]
        [ProducesResponseType(200, Type = typeof(List<ClinicDetailsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetClinicDetails()
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<ClinicDetailsBOL>();
            try
            {
                res = new ClinicDAL().GetClinicDetails();
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

        [HttpGet]
        [ActionName("GetClinicDetailsById")]
        [ProducesResponseType(200, Type = typeof(List<ClinicDetailsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetClinicDetailsById(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new ClinicDetailsBOL();
            try
            {
                res = new ClinicDAL().GetClinicDetailsById(id);
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
        [HttpPost]
        [ActionName("AddClinic")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddClinic(ClinicBOL clinic)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<ClinicBOL>();
            try
            {
                res = new ClinicDAL().AddClinic(clinic);
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
        [ActionName("AddClinicDetails")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddClinicDetails(ClinicDetailsBOL clinicdetails)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<ClinicDetailsBOL>();
            try
            {
                res = new ClinicDAL().AddClinicDetails(clinicdetails);
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
    }
}
