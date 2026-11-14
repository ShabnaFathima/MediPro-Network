using Dapper;
using MediProApi.BOL;
using System.Data;
using System.Data.SqlClient;

namespace MediProApi.DAL
{ 

    public class GeneralActions
    {
        public static readonly string Constr = ConnectionBOL.ConStr;
        static readonly IDbConnection db = new SqlConnection(ConnectionBOL.ConStr);
        public static IEnumerable<T> GetList<T>(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null)
        {

            try
            {
                if (db.State == ConnectionState.Closed) { db.Open(); }
                return (List<T>)db.Query<T>(sql: commandText, param: param, commandTimeout: 0, commandType: type);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-GetList ");
            }
            return null;
        }
        public static T Get<T>(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "") where T : class
        {

            try
            {
                if (db.State == ConnectionState.Closed) { db.Open(); }
                var item = db.Query<T>(sql: commandText, param: param, commandTimeout: 0, commandType: type).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get " + ActionDescription);
            }

            return null;
        }
        public static bool ExecuteWithReturn(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "")
        {
            try
            {

                var count = db.Execute(sql: commandText, param: param, commandTimeout: 0, commandType: type);
                return count > 0;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-ExecuteWithoutreturn " + ActionDescription);
            }
            return false;
        }

        public static bool ExecuteWithReturn(IDbTransaction dbTransaction, string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "")
        {
            try
            {

                var count = db.Execute(sql: commandText, param: param, commandTimeout: 0, commandType: type, transaction: dbTransaction);
                return count > 0;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-ExecuteWithoutreturn " + ActionDescription);
            }
            return false;
        }

        public static IDbTransaction BeginTransaction()
        {
            IDbTransaction result = null;
            try
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }
                result = db.BeginTransaction(IsolationLevel.ReadCommitted);
                // db.Close();
                return result;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-BeginTransaction ");
            }
            return result;
        }

        public static bool CommitTransaction(IDbTransaction dbTransaction)
        {
            try
            {
                // db.Open();
                dbTransaction.Commit();


                // db.Close();
                return true;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-CommitTransaction ");
            }
            return false;
        }

        public static bool RollbackTransaction(IDbTransaction dbTransaction)
        {
            try
            {
                //  db.Open();
                dbTransaction.Rollback();
                // db.Close();
                return true;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-RollbackTransaction ");
            }
            return false;
        }

        public static string GetNextPk(int code_ = 0, IDbTransaction dbTransaction = null)
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Action", "NextPK");
                param.Add("@Code", code_);

                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure, transaction: dbTransaction).First();
                Pk = count;
                Pk--;
                return Pk.ToString();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Next Pk ");
            }
            return Pk.ToString();
        }

        public static string GetBranchCode(string code_ = "", IDbTransaction transaction_ = null)
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserCode", code_);
                param.Add("@Action", "BranchCode");
                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure, transaction: transaction_).First();
                Pk = count;

                return Pk.ToString();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get BranchCode ");
            }
            return Pk.ToString();
        }

        public static string GetCustomerBarcode()
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();

                param.Add("@Action", "CustomerBarcode");

                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();
                Pk = count;
                Pk--;
                return Pk.ToString();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get BranchCode ");
            }
            return Pk.ToString();
        }

        public static long GetServiceBarcode()
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();

                param.Add("@Action", "ServiceBarcode");

                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();
                Pk = count;
                Pk--;
                return Pk;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get BranchCode ");
            }
            return Pk;
        }

        public static long GetInvoiceNo()
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();

                param.Add("@Action", "InvoiceNo");

                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();
                Pk = count;
                Pk--;
                return Pk;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get BranchCode ");
            }
            return Pk;
        }

        public static long GetInvoiceNo(IDbTransaction dbTransaction)
        {
            long Pk = 0;
            try
            {
                string commandText = "uspGetCodes";
                DynamicParameters param = new DynamicParameters();

                param.Add("@Action", "InvoiceNo");

                var count = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure, transaction: dbTransaction).First();
                Pk = count;
                Pk--;
                return Pk;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-Get BranchCode ");
            }
            return Pk;
        }


        public static string GetTodayDate()
        {
            return DateTime.Today.ToString("dd-MMM-yyyy");
        }

        public static long ExecuteWithRowCount(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "")
        {
            try
            {
                return db.Execute(sql: commandText, param: param, commandTimeout: 0, commandType: type);

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-ExecuteWithoutreturn " + ActionDescription);
            }
            return 0;
        }

        public static bool ExecuteNoReturn(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "")
        {
            bool res = false;
            try
            {
                var count = db.Execute(sql: commandText, param: param, commandTimeout: 0, commandType: type);
                res = true;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-ExecuteWithoutreturn " + ActionDescription);
            }
            return res;
        }
        public static DataTable GetTable(string commandText, CommandType type = CommandType.Text, SqlParameter[] param = null)
        {
            DataTable dtreslt = new DataTable();
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(Constr))
                {
                    SqlCon.Open();
                    using (SqlCommand SqlCmd = new SqlCommand(commandText, SqlCon))
                    {
                        SqlCmd.CommandType = type;
                        SqlCmd.CommandTimeout = 0;
                        if (param != null)
                        {
                            foreach (SqlParameter p in param)
                            {
                                if (p.SqlDbType == SqlDbType.Structured)
                                {
                                    int Count = 0;
                                    try
                                    {
                                        var x = p.Value;
                                        Count = (int)x.GetType().GetProperty("Count").GetValue(x);
                                    }
                                    catch (Exception) { }
                                    if (Count == 0) { continue; }

                                }
                                SqlCmd.Parameters.Add(p);
                            }
                        }

                        using (SqlDataAdapter SqlDa = new SqlDataAdapter(SqlCmd))
                        {
                            SqlDa.Fill(dtreslt);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions_GetTable");
            }
            return dtreslt;
        }

        public static long GetScelarLong(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "", IDbTransaction dbTransaction = null)
        {
            long Result = 0;
            try
            {
                Result = db.Query<long>(sql: commandText, param: param, commandTimeout: 0, commandType: type, transaction: dbTransaction).First();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions_GetScelarLong" + ActionDescription);
            }
            return Result;
        }

        public static T GetScalar<T>(string commandText, CommandType type = CommandType.Text, DynamicParameters param = null, string ActionDescription = "")
        {

            try
            {
                return db.Query<T>(sql: commandText, param: param, commandTimeout: 0, commandType: type).First();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions_GetScelarLong" + ActionDescription);
            }
            return default(T);
        }




        public static bool IsApprovalRequired(string key)
        {
            try
            {
                var Result = db.Query<int>(sql: $"SELECT COUNT(*) FROM ApprovalCriteria WHERE ApprovalCriteriaCode='{key}' AND ISNULL(isactive,0)=1", commandTimeout: 0, commandType: CommandType.Text).First();
                return Result > 0;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-IsApprovalRequired");
                throw;
            }

        }

        public static string GetSerialNumber(string TableName)
        {
            string Result = "";
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@table_name", TableName);
                Result = db.Query<string>(sql: "uspGenerateNo", param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-GetSerialNumber");
            }

            return Result;
        }

        public static int GetPendingRecordCount(string CriteriaCode)
        {
            int Result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ApprovalCriteriaCode", CriteriaCode);
                param.Add("@Action", "GetPendingRecordCount");
                Result = db.Query<int>(sql: "uspMisc", param: param, commandTimeout: 0, commandType: CommandType.StoredProcedure).First();

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex + " - GeneralActions-GetPendingRecordCount");
            }

            return Result;
        }
    }
}
