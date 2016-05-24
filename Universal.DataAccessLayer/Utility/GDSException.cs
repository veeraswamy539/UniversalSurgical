using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace Universal.DataAccessLayer
{
    public class GDSException
    {

        const string  SPEXCEPTIONINSERTERRORLOG ="spExceptionInsertERRORLOG";       

        /// Author: Manish Narang 
        /// Dated : 23/01/09
        /// <summary>
        /// Logs exception into database
        /// </summary>
        /// <param name="exception">Logentry Object</param>
        private static void InsertDatabaseLog(Exception exception)
        {
            try
            {
                ArrayList paramColl = new ArrayList();

                SqlParameter exceptionMessage = new SqlParameter();
                exceptionMessage.ParameterName = "vchMessage";
                exceptionMessage.Value = exception.Message; 
                exceptionMessage.SqlDbType = SqlDbType.VarChar;
                paramColl.Add(exceptionMessage);
                
                SqlParameter stackTrace = new SqlParameter();
                stackTrace.ParameterName = "nvchStackTrace";
                stackTrace.Value = exception.StackTrace; 
                stackTrace.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(stackTrace);

                SqlParameter source = new SqlParameter();
                source.ParameterName = "vchSource";
                source.Value = exception.Source;
                source.SqlDbType = SqlDbType.VarChar;
                paramColl.Add(stackTrace);

                
                if (paramColl.Count > 0)
                {
                    GDSDBWrapper.ExecuteNonQueryInPutParam(SPEXCEPTIONINSERTERRORLOG, paramColl);
                }
                
            }
            catch (Exception ExpInsertDataBase)
            {
                EventLog objEvLog = new EventLog();
                string strAppName = string.Empty;
                strAppName = "GDSErrorLog";
                objEvLog.Source = strAppName;
                Logger.Write(ExpInsertDataBase);
            }
            finally
            {
                            }
        }
    }
}
