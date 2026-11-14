using Dapper;
using MediProApi.BOL;
using System.Data;

namespace MediProApi.DAL
{
    public class DepartmentDAL
    {
        public bool AddDep(DepartmentBOL obj = null)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@DepName", obj.DepName);
                param.Add("@ClinicId", obj.ClinicId);

                return GeneralActions.ExecuteWithReturn("INSERT INTO Departments(DepName,ClinicId) VALUES(@DepName,@ClinicId)", CommandType.Text, param);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-CustomerSave: " + ex.ToString());
            }
            return false;
        }

        public List<DepartmentBOL> GetAllDepartment()
        {
            List<DepartmentBOL> result = null;
            try
            {

                string sql = $@"select distinct DepName from Departments";
                 
                var departmentList = GeneralActions.GetList<DepartmentBOL>(sql, CommandType.Text);

                if (departmentList != null)
                {
                    return departmentList.ToList();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("CategoryDAL-GetCategory: " + ex.ToString());
            }
            return result;
        }

        public List<DepartmentBOL> GetDepartmentById(long id)
        {
            List<DepartmentBOL> result = null;
            try
            {

                string sql = $@"select * from Departments where ClinicId = {id}";

                var departmentList = GeneralActions.GetList<DepartmentBOL>(sql, CommandType.Text);

                if (departmentList != null)
                {
                    return departmentList.ToList();
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
