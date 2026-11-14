using Dapper;
using MediProApi.BOL;
using System.Data;

namespace MediProApi.DAL
{
    public class PatientDAL
    {
        public bool AddUser(PatientBOL obj = null)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@Username", obj.Username);
                param.Add("@Email", obj.Email);
                param.Add("@Phone", obj.Phone);
                param.Add("@Password", obj.Password);
                return GeneralActions.ExecuteWithReturn("INSERT INTO Patients(Username,Email,Phone,Password) VALUES(@Username,@Email,@Phone,@Password)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }
        public bool AddPatient(PatientDetailsBOL obj = null)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PatientId", obj.PatientId);
                param.Add("@Name", obj.Name);
                param.Add("@Place", obj.Place);
                param.Add("@Address", obj.Address);
                param.Add("@Contact", obj.Contact);
                param.Add("@Age", obj.Age);
                param.Add("@Gender", obj.Gender);
                return GeneralActions.ExecuteWithReturn("INSERT INTO PatientDetails(PatientId,Name,Place,Address,Contact,Age,Gender) VALUES(@PatientId,@Name,@Place,@Address,@Contact,@Age,@Gender)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }

        public PatientBOL GetUserById(long id)
        {
            PatientBOL result = null;
            try
            {
                string sql = $@"select * from Patients where Id = {id}";
                var usrList = GeneralActions.Get<PatientBOL>(sql, CommandType.Text);
                if (usrList != null)
                {
                    return usrList;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public PatientBOL GetPatientId(String email)
        {
            PatientBOL result = null;
            try
            {
                string sql = $@"select Id from Patients where Email = {email}";
                var usrList = GeneralActions.Get<PatientBOL>(sql, CommandType.Text);
                if (usrList != null)
                {
                    return usrList;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public PatientDetailsBOL GetPatientById(long id)
        {
            PatientDetailsBOL result = null;
            try
            {
                string sql = $@"select * from PatientDetails where PatientId = {id}";
                var patient = GeneralActions.Get<PatientDetailsBOL>(sql, CommandType.Text);
                if (patient != null)
                {
                    return patient;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public List<PatientBOL> GetAllUsers()
        {
            List<PatientBOL> result = null;
            try
            {

                string sql = $@"select * from Patients";

                var pList = GeneralActions.GetList<PatientBOL>(sql, CommandType.Text);

                if (pList != null)
                {
                    return pList.ToList();
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
