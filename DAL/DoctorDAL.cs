using Dapper;
using MediProApi.BOL;
using System.Data;
using System.Security.Cryptography;

namespace MediProApi.DAL
{
    public class DoctorDAL
    {
        public List<DoctorBOL> GetAllDoctors()
        {
            List<DoctorBOL> result = null;
            try
            {
                string sql = $@"select * from Doctors";

                var doctorList = GeneralActions.GetList<DoctorBOL>(sql, CommandType.Text);

                if (doctorList != null)
                {
                    return doctorList.ToList(); 
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public List<DoctorBOL> GetDoctorsByClinId(long Cid)
        {
            List<DoctorBOL> result = null;
            try
            {
                string sql = $@"select* from Doctors where Id in (select DocId from DoctorClinics where ClinId={Cid})";

                var dList = GeneralActions.GetList<DoctorBOL>(sql, CommandType.Text);

                if (dList != null)
                {
                    return dList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        
        public List<DoctorClinicsBOL> GetClinicsByDocId(long Did)
        {
            List<DoctorClinicsBOL> result = null;
            try
            {

                string sql = $@"select * from DoctorCLinics where DocId = {Did}";

                var dList = GeneralActions.GetList<DoctorClinicsBOL>(sql, CommandType.Text);

                if (dList != null)
                {
                    return dList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public DoctorBOL GetDoctorById(long id)
        {
            DoctorBOL result = null;
            try
            {

                string sql = $@"select * from Doctors where Id = {id}";

                var dList = GeneralActions.Get<DoctorBOL>(sql, CommandType.Text);

                if (dList != null)
                {
                    return dList;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public bool AddDoc(DoctorClinicsBOL obj = null)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DocId", obj.DocId);
                param.Add("@ClinId", obj.ClinId);
                param.Add("@NumSlots", obj.NumSlots);
                param.Add("@AvgConTime", obj.AvgConTime);
                param.Add("@StartTimeHr", obj.StartTimeHr);
                param.Add("@StartTimeMin", obj.StartTimeMin);
                param.Add("@AmPm", obj.AmPm);

                return GeneralActions.ExecuteWithReturn("INSERT INTO DoctorClinics(DocId,ClinId,NumSlots,AvgConTime,StartTimeHr,StartTimeMin) VALUES(@DocId,@ClinId,@NumSlots,@AvgConTime,@StartTimeHr,@StartTimeMin)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        } 

        public DoctorClinicsBOL GetSlotDetails(long Did,long Cid)
        {
            DoctorClinicsBOL result = null;
            try
            {

                string sql = $@"select NumSlots,AvgConTime,StartTimeHr,StartTimeMin from DoctorClinics where DocId = {Did} and ClinId = {Cid}";

                var dList = GeneralActions.Get<DoctorClinicsBOL>(sql, CommandType.Text);

                if (dList != null)
                {
                    return dList;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public List<DoctorBOL> GetDoctorByDep(String dep)
        {
            List<DoctorBOL> result = null;
            try
            {

                string sql = $@"select Name from Doctors where Department = {dep}";

                var List = GeneralActions.GetList<DoctorBOL>(sql, CommandType.Text);

                if (List != null)
                {
                    return List.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }
        public List<ClinicDetailsBOL> GetClinicsofDoc(long doc)
        {
            List<ClinicDetailsBOL> result = null;
            try
            {

                string sql = $@"select Name from ClinicDetails where ClinicId in (select ClinId from DoctorCLinics where DocId = {doc})";

                var List = GeneralActions.GetList<ClinicDetailsBOL>(sql, CommandType.Text);

                if (List != null)
                {
                    return List.ToList();
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
