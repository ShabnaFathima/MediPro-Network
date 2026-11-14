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
    public class DoctorController : ControllerBase
        {
            [HttpGet]
            [ActionName("GetAllDoctors")]
            [ProducesResponseType(200, Type = typeof(List<DoctorBOL>))]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public IActionResult GetAllDoctors()
            {
                if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                bool IsExist = false;
                var res = new List<DoctorBOL>();
                try
                {
                    res = new DoctorDAL().GetAllDoctors();
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
            [ActionName("GetDoctorsByClinId")]
            [ProducesResponseType(200, Type = typeof(List<DoctorBOL>))]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public IActionResult GetDoctorsByClinId(long Cid)
            {
                if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                bool IsExist = false;
                var res = new List<DoctorBOL>();
                try
                {
                    res = new DoctorDAL().GetDoctorsByClinId(Cid);
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
        [ActionName("GetClinicsByDocId")]
        [ProducesResponseType(200, Type = typeof(List<DoctorClinicsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetClinicsByDocId(long Did)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<DoctorClinicsBOL>();
            try
            {
                res = new DoctorDAL().GetClinicsByDocId(Did);
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
        [ActionName("GetDoctorById")]
        [ProducesResponseType(200, Type = typeof(List<DoctorBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDoctorById(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new DoctorBOL();
            try
            {
                res = new DoctorDAL().GetDoctorById(id);
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
        [ActionName("AddDoc")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddDoc(DoctorClinicsBOL doc)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<DoctorClinicsBOL>();
            try
            {
                res = new DoctorDAL().AddDoc(doc);
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
        [ActionName("GetSlotDetails")]
        [ProducesResponseType(200, Type = typeof(List<DoctorClinicsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSlotDetails(long Did, long Cid)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new DoctorClinicsBOL();
            try
            {
                res = new DoctorDAL().GetSlotDetails(Did,Cid);
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
        [ActionName("GetDoctorByDep")]
        [ProducesResponseType(200, Type = typeof(List<DoctorBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDoctorByDep(String dep)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<DoctorBOL>();
            try
            {
                res = new DoctorDAL().GetDoctorByDep(dep);
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
        [ActionName("GetClinicsofDoc")]
        [ProducesResponseType(200, Type = typeof(List<ClinicDetailsBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetClinicsofDoc(long doc)
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
                res = new DoctorDAL().GetClinicsofDoc(doc);
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
