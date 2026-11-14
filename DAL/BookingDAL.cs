using Dapper;
using MediProApi.BOL;
using System.Data;
namespace MediProApi.DAL
{
    public class BookingDAL
    {
        public List<BookingBOL> GetBookingsById(long id)
        {
            List<BookingBOL> result = null;
            try
            {
                string sql = $@"select Clinic,Department,Doctor,SlotNum,Date,Time from Bookings where PatientId = {id}";
                var patient = GeneralActions.GetList<BookingBOL>(sql, CommandType.Text);
                if (patient != null)
                {
                    return patient.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public List<BookingBOL> GetBookingsofClinic(long id)
        {
            List<BookingBOL> result = null;
            try
            {
                string sql = $@"select * from Bookings where Clinic = (select Name from ClinicDetails where ClinicId = {id})";
                var patient = GeneralActions.GetList<BookingBOL>(sql, CommandType.Text);
                if (patient != null)
                {
                    return patient.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public List<BookingBOL> GetSlotNum()
        {
            List<BookingBOL> result = null;
            try
            {
                string sql = $@"select Clinic,Doctor,SlotNum,Date from Bookings";

                var slotList = GeneralActions.GetList<BookingBOL>(sql, CommandType.Text);

                if (slotList != null)
                {
                    return slotList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public bool AddBooking(BookingBOL obj = null)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PatientId", obj.PatientId);
                param.Add("@PatientName", obj.PatientName);
                param.Add("@Department", obj.Department);
                param.Add("@Doctor", obj.Doctor);
                param.Add("@Date", obj.Date);
                param.Add("@Time", obj.Time);
                param.Add("@Clinic", obj.Clinic);
                param.Add("@P_Place", obj.P_Place);
                param.Add("@P_Address", obj.P_Address);
                param.Add("@P_Contact", obj.P_Contact);
                param.Add("@P_Age", obj.P_Age);
                param.Add("@P_Gender", obj.P_Gender);
                param.Add("@SlotNum", obj.SlotNum);
                return GeneralActions.ExecuteWithReturn("INSERT INTO Bookings(PatientId,PatientName,P_Age,P_Gender,P_Place,P_Address,P_Contact,Clinic,Department,Doctor,SlotNum,Date,Time) VALUES(@PatientId,@PatientName,@P_Age,@P_Gender,@P_Place,@P_Address,@P_Contact,@Clinic,@Department,@Doctor,@SlotNum,@Date,@Time)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }
        public List<BookingBOL> GetBookings()
        {
            List<BookingBOL> result = null;
            try
            {
                string sql = $@"select * from Bookings";

                var bookings = GeneralActions.GetList<BookingBOL>(sql, CommandType.Text);

                if (bookings != null)
                {
                    return bookings.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

    }
}
