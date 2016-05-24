/*----------------------------------------------------------------------

Name:       GDSDBWrapper 

Description: This class is responsible for Core DataBase Operations

History:
--------
Created Date    :   29-June-2012  
Author          :   Harinath
---------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
//using GDS.CrossLayer;
using System.Collections;
using System.Configuration;

namespace Universal.DataAccessLayer
{
    public class GDSDBWrapper
    {
        public GDSDBWrapper()
        {
        }
        private DbTransaction transaction = null;
        private DbConnection connection = null;

        ////private DbTransaction transaction1 = null;
        //private DbConnection connection1 = null;

        
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Create Database Parameter
        /// </summary>
        ///<returns>DbParameter</returns>        
        public static DbParameter CreateParam()
        {
            try
            {
               string providerName= ConfigurationManager.ConnectionStrings[""].ProviderName;
               DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbParameter param = factory.CreateParameter();
                return param;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute DataReader
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="nullableParam">Input Parameter,pass null if there is no Input Param</param>
        /// <returns>IDataReader</returns>
        public static IDataReader ExecuteDataReader(string spName,DbParameter nullableParam)
        {
            DbCommand dbCommand = null;
            IDataReader dataReader = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand(spName);
                if (nullableParam != null)
                    db.AddInParameter(dbCommand, nullableParam.ParameterName, nullableParam.DbType, nullableParam.Value);
                dataReader = db.ExecuteReader(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataReader;        }

        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute DataReader
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="nullableParam">Multiple Input Parameters(array of Parameters)</param>
        /// <returns>IDataReader</returns>
        public static IDataReader ExecuteDataReaderMultiParam(string spName, SqlParameter[] paramsCollection)
        {
            DbCommand dbCommand = null;
            IDataReader dataReader = null;
            try
            {
                Database dbExecuteReaderMultiParam = DatabaseFactory.CreateDatabase();
                dbCommand = dbExecuteReaderMultiParam.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = 3000;
                if (paramsCollection.Length > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        dbExecuteReaderMultiParam.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                    }
                }
                dataReader = dbExecuteReaderMultiParam.ExecuteReader(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataReader;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute DataReader with parameter collection
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="nullableParam">Multiple Input Parameters(Collection of Parameters</param>
        /// <returns>IDataReader</returns>
        public static DataTable ExecuteDataReaderMultiParam(string spName, ArrayList paramsCollection)
        {
            DbCommand dbCommand = null;
            IDataReader dataReader = null;
            DataTable dataTable = null;
            try
            {
                Database dbExecuteReaderMultiParam = DatabaseFactory.CreateDatabase("ApplicationServices");
                dbCommand = dbExecuteReaderMultiParam.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = 3000;
                if (paramsCollection.Count> 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        dbExecuteReaderMultiParam.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                    }
                }
                dataReader = dbExecuteReaderMultiParam.ExecuteReader(dbCommand);
                dataTable = new DataTable();
                dataTable.Load(dataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dataReader != null && dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
            return dataTable;
            //return dataReader;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute NonQuery with InPutParameters only
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection">InPut Parameters Collection</param>
        ///<returns>returns number of rows affected</returns>
        public static int ExecuteNonQueryInPutParam(string spName,SqlParameter[] paramsCollection)
        {
            DbCommand dbCommand = null;
            int result = 0;
            try
            {
                Database dbNonQuery = DatabaseFactory.CreateDatabase();                
                dbCommand = dbNonQuery.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = 3000;
                if (paramsCollection.Length > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        dbNonQuery.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                    }
                }

                result = dbNonQuery.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute NonQuery with InPutParameters only
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection">InPut Parameters Array Collection</param>
        ///<returns>returns number of rows affected</returns>
        public static int ExecuteNonQueryInPutParam(string spName, ArrayList paramsCollection)
        {
            DbCommand dbCommand = null;
            int result = 0;
            try
            {
                Database dbNonQuery = DatabaseFactory.CreateDatabase();

                dbCommand = dbNonQuery.GetStoredProcCommand(spName);
                if (paramsCollection!=null && paramsCollection.Count > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        dbNonQuery.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                    }
                }

                result = dbNonQuery.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute Dataset
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        ///<param name="nullableParam">Input Parameter,pass null if there is no Input Param</param>
        ///<returns>dataset</returns>
        public static DataSet ExecuteCustomDataset(string spName,DbParameter nullableParam)
        {
            DbCommand dbCommand = null;
            DataSet resultantData = null;
            try
            {
                Database dbForExecuteDataSet = DatabaseFactory.CreateDatabase("ApplicationServices");
                dbCommand = dbForExecuteDataSet.GetStoredProcCommand(spName);
                if (nullableParam != null)
                    dbForExecuteDataSet.AddInParameter(dbCommand, nullableParam.ParameterName, nullableParam.DbType, nullableParam.Value);
                resultantData = dbForExecuteDataSet.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultantData;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute Dataset with MultiParams
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection"> Parameter Collection</param>
        ///<returns>dataset</returns>
        public static DataSet ExecuteCustomDatasetMultiParam(string spName, SqlParameter[] paramsCollection)
        {
            DbCommand dbCommand = null;
            DataSet resultantData = null;
            try
            {
                Database dbForExecuteDataSetMultipleParam = DatabaseFactory.CreateDatabase();
                dbCommand = dbForExecuteDataSetMultipleParam.GetStoredProcCommand(spName);
                if (paramsCollection.Length>0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        if (param.Direction == ParameterDirection.Input)
                            dbForExecuteDataSetMultipleParam.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                        if (param.Direction == ParameterDirection.Output)
                            dbForExecuteDataSetMultipleParam.AddOutParameter(dbCommand, param.ParameterName, param.DbType, param.Size);
                    }
                }
                resultantData = dbForExecuteDataSetMultipleParam.ExecuteDataSet(dbCommand);
                foreach (DbParameter param in paramsCollection)
                {
                    if (param.Direction == ParameterDirection.Output)
                        param.Value = dbForExecuteDataSetMultipleParam.GetParameterValue(dbCommand, param.ParameterName);
                }
            }
            catch (Exception expDBWrapper)
            {
                throw expDBWrapper;
            }
            return resultantData;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute Dataset with MultiParams
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection"> Parameter Collection</param>
        ///<returns>dataset</returns>
        public static DataSet ExecuteCustomDatasetMultiParam(string spName, ArrayList paramsCollection)
        {
            DbCommand dbCommand = null;
            DataSet resultantData = null;
            try
            {
                Database dbForExecuteDataSetMultipleParam = DatabaseFactory.CreateDatabase("ApplicationServices");
                dbCommand = dbForExecuteDataSetMultipleParam.GetStoredProcCommand(spName);
                if (paramsCollection.Count > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        if (param.Direction == ParameterDirection.Input)
                            dbForExecuteDataSetMultipleParam.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                    }
                }
                resultantData = dbForExecuteDataSetMultipleParam.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultantData;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute NonQuery with OutParams
        /// </summary>
        ///<param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection">InPut/Output Parameters Collection</param>
        ///<returns>returns number of rows affected in the DataBase</returns>
        public static int ExecuteNonQueryOutParam(string spName, SqlParameter[] paramsCollection)
        {
            DbCommand dbCommand = null;
            int result = 0;
            try
            {
                Database dbNonQueryOut = DatabaseFactory.CreateDatabase();
                dbCommand = dbNonQueryOut.GetStoredProcCommand(spName);
                if (paramsCollection.Length > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        if (param.Direction == ParameterDirection.Input)
                            dbNonQueryOut.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                        if (param.Direction == ParameterDirection.Output)
                            dbNonQueryOut.AddOutParameter(dbCommand, param.ParameterName, param.DbType, param.Size);
                    }
                }
                result = dbNonQueryOut.ExecuteNonQuery(dbCommand);
                foreach (DbParameter param in paramsCollection)
                {
                    if (param.Direction == ParameterDirection.Output)
                        param.Value = dbNonQueryOut.GetParameterValue(dbCommand, param.ParameterName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return result;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute NonQuery with OutParams
        /// </summary>
        ///<param name="spName">Stored Procedure Name</param>
        ///<param name="paramsCollection">InPut/Output Parameters Collection</param>
        ///<returns>returns number of rows affected in the DataBase</returns>
        public static int ExecuteNonQueryOutParam(string spName, ArrayList paramsCollection)
        {
            DbCommand dbCommand = null;
            int result = 0;
            try
            {
                Database dbNonQueryOut = DatabaseFactory.CreateDatabase("ApplicationServices");
                dbCommand = dbNonQueryOut.GetStoredProcCommand(spName);
                if (paramsCollection.Count > 0)
                {
                    foreach (DbParameter param in paramsCollection)
                    {
                        if (param.Direction == ParameterDirection.Input)
                            dbNonQueryOut.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                        if (param.Direction == ParameterDirection.Output)
                            dbNonQueryOut.AddOutParameter(dbCommand, param.ParameterName, param.DbType, param.Size);
                    }
                }
                result = dbNonQueryOut.ExecuteNonQuery(dbCommand);
                foreach (DbParameter param in paramsCollection)
                {
                    if (param.Direction == ParameterDirection.Output)
                        param.Value = dbNonQueryOut.GetParameterValue(dbCommand, param.ParameterName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute Scalar
        /// </summary>
        ///<param name="spName">Stored Procedure Name</param>
        ///<param name="param">Input Parameter</param>
        ///<returns>Object</returns>
        public static object ExecuteScalar(string spName, ArrayList nullableParamColl)
        {
            DbCommand dbCommand = null;
            object objResult = null;
            try
            {
                Database dbScalar = DatabaseFactory.CreateDatabase("ApplicationServices");
                dbCommand = dbScalar.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = 3000;
                if (nullableParamColl != null && nullableParamColl.Count > 0)
                {
                    foreach (SqlParameter param in nullableParamColl)
                        dbScalar.AddInParameter(dbCommand, param.ParameterName, param.DbType, param.Value);
                }
                objResult = dbScalar.ExecuteScalar(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objResult;
        }

        public SqlConnection Getconnection()
        {
            string connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            return conn;
        }

        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Create a Transaction
        /// </summary>
        public DbTransaction BeginTansaction(DbConnection connection)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                
                if (transaction == null)
                {
                    if (connection.State ==ConnectionState.Closed)
                        connection.Open();
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                }
            }
            catch (Exception ex)
            {
                transaction = null;
                throw ex;
            }
            return transaction;
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Commit Transaction
        /// </summary>
        public void CommitTransaction(DbTransaction commitTrans)
        {
            try
            {
                if (commitTrans != null)
                {
                    commitTrans.Commit();
                    commitTrans = null;
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Rollback Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction = null;
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute DataSet with Transaction object
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="spName"></param>
        /// <param name="paramsCollection">parameterValues:
        /// An array of paramters to pass to the stored procedure. The parameter values
        /// must be in call order as they appear in the stored procedure.
        ///</param>
        /// <returns></returns>
        public static DataSet DataSetwithTransaction(string spName, ArrayList paramsCollection,DbTransaction trans)
        {
            DataSet resultantData = null;
            try
            {
                Database dbExecuteDataSetwithTrans = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = null;
                dbCommand = dbExecuteDataSetwithTrans.GetStoredProcCommand(spName);
                resultantData = dbExecuteDataSetwithTrans.ExecuteDataSet(trans, spName, paramsCollection);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultantData;
        }

        /// Author: Harinath Cheekati
        /// Dated : 30/06/12
        /// <summary>
        /// Method to Execute DataSet with Transaction object
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="paramsCollection">parameterValues:
        //     An array of parameters to pass to the stored procedure. The parameter values
        //     must be in call order as they appear in the stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        public static int ExecutNonQuerywithTransaction(string spName, SqlParameter[] paramsCollection, SqlTransaction trans, SqlConnection connection1)
        {
            int result = 0;
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = connection1;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = spName; 
                dbCommand.Transaction = trans;

                foreach (SqlParameter param in paramsCollection)
                {
                    dbCommand.Parameters.Add(param);
                }
                result = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// Author: Harinath Cheekati
        /// Dated : 01/03/09
        /// <summary>
        /// Method to Execute DataSet with Transaction object(Parameter Collection
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="paramsCollection">parameterValues:
        //     An array of parameters to pass to the stored procedure. The parameter values
        //     must be in call order as they appear in the stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        public static int ExecutNonQuerywithTransaction(string spName, ArrayList paramsCollection, SqlTransaction trans, SqlConnection connection1)
        {
            int result = 0;
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = connection1;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = spName;
                dbCommand.Transaction = trans;
                if (paramsCollection.Count > 0)
                {
                    foreach (SqlParameter param in paramsCollection)
                    {
                        dbCommand.Parameters.Add(param);
                    }
                }
                result = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                paramsCollection = null;
            }
            return result;
        }
    }
}
