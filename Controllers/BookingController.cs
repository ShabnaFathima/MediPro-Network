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
    public class BookingController : ControllerBase
    {
        [HttpGet]
        [ActionName("GetBookingsById")]
        [ProducesResponseType(200, Type = typeof(List<BookingBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBookingsById(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<BookingBOL>();
            try
            {
                res = new BookingDAL().GetBookingsById(id);
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
        [ActionName("GetBookingsofClinic")]
        [ProducesResponseType(200, Type = typeof(List<BookingBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBookingsofClinic(long id)
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<BookingBOL>();
            try
            {
                res = new BookingDAL().GetBookingsofClinic(id);
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
        [ActionName("AddBooking")]
        [ProducesResponseType(200, Type = typeof(ResultBOL))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddBooking(BookingBOL bkg)
        {
            ResultBOL Tres = new ResultBOL();
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = false;
            var cnt = new List<BookingBOL>();
            try
            {
                res = new BookingDAL().AddBooking(bkg);
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
        [ActionName("GetSlotNum")]
        [ProducesResponseType(200, Type = typeof(List<BookingBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSlotNum()
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<BookingBOL>();
            try
            {
                res = new BookingDAL().GetSlotNum();
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
        [ActionName("GetBookings")]
        [ProducesResponseType(200, Type = typeof(List<BookingBOL>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBookings()
        {
            if (!Utility.IsLicenseValidated()) { return StatusCode(402, new { Message = Utility.LicenceError }); }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool IsExist = false;
            var res = new List<BookingBOL>();
            try
            {
                res = new BookingDAL().GetBookings();
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
