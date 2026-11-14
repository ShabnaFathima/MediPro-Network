using Dapper;
using MediProApi.BOL;
using System.Data;

namespace MediProApi.DAL
{
    public class ClinicDAL
    {
        public List<ClinicBOL> GetClinic()
        {
            List<ClinicBOL> result = null;
            try
            {

                string sql = $@"select * from Clinics";

                var clinicList = GeneralActions.GetList<ClinicBOL>(sql, CommandType.Text);

                if (clinicList != null)
                {
                    return clinicList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public List<ClinicDetailsBOL> GetClinicDetails()
        {
            List<ClinicDetailsBOL> result = null;
            try
            {

                string sql = $@"select * from ClinicDetails";

                var cList = GeneralActions.GetList<ClinicDetailsBOL>(sql, CommandType.Text);

                if (cList != null)
                {
                    return cList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public ClinicDetailsBOL GetClinicDetailsById(long id)
        {
            ClinicDetailsBOL result = null;
            try
            {

                string sql = $@"select * from ClinicDetails where ClinicId = {id}";

                var cList = GeneralActions.Get<ClinicDetailsBOL>(sql, CommandType.Text);

                if (cList != null)
                {
                    return cList;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public bool AddClinic(ClinicBOL obj = null)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@Username", obj.Username);
                param.Add("@Password", obj.Password);

                return GeneralActions.ExecuteWithReturn("INSERT INTO Clinics(Username,Password) VALUES(@Username,@Password)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }
        public bool AddClinicDetails(ClinicDetailsBOL obj = null)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@ClinicId", obj.ClinicId);
                param.Add("@Name", obj.Name);
                param.Add("@Description", obj.Description);
                param.Add("@Place", obj.Place);
                param.Add("@Address", obj.Address);
                param.Add("@Contact", obj.Contact);
                param.Add("@location", obj.location);

                return GeneralActions.ExecuteWithReturn("INSERT INTO ClinicDetails(ClinicId,Name,Description,Place,Address,Contact,location) VALUES(@ClinicId,@Name,@Description,@Place,@Address,@Contact,@location)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }

    }
}
