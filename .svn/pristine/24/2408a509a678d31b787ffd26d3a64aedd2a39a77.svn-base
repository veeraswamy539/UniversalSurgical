/*----------------------------------------------------------------------
Name        :   ObjectBuilder
Description :   This class is responsible for converting Dataset values into entities

History:
--------
30 Jun 2012 1.00  Harinath
---------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Universal.BusinessEntities.Core;

namespace Universal.BusinessEntities.Core
{
    public class ObjectBuilder
    {
        #region Public Methods

        /// Author  :   Harinath
        /// Dated   :   30 Jun 2012
        /// <summary>
        /// Fills Entities from Database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="target"></param>
        public static void Fill<T>(DataTable dt, T entity) where T : IBusinessObject
        {
            if (dt.Rows.Count > 0)
            {
                BuildEntity(entity, dt.Rows[0]);
                //after building the object mark it as OLD                
            }
        }

        /// Author  :   Harinath
        /// Dated   :   30 Jun 2012
        /// <summary>
        /// Fills EntityCollection from DataBase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="target"></param>
        public static void FillList<T>(DataTable dt, object entityCollection) where T : IBusinessObject
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    object newObject = null;
                    //Creating new instance entity of the respective collection
                    newObject = typeof(T).InvokeMember(null, BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, newObject, new object[] { });
                    //Fill the entity with datarow
                    BuildEntity(newObject, dr);
                    //Add the entity to its collection
                    entityCollection.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, entityCollection, new object[] { newObject });
                }
            }
        }

        #endregion

        #region Private Methods

        /// Author  :   Harinath
        /// Dated   :   30 Jun 2012
        /// <summary>
        /// private method, which will be used to build entity from Dataset
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dr"></param>
        private static void BuildEntity(object entity, DataRow dr)
        {
            //Get all the properties of the entity
            System.Reflection.PropertyInfo[] propInfoColl = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            //Loop through all the properties and assign the values from datatable.
            foreach (System.Reflection.PropertyInfo propInfo in propInfoColl)
            {
                //Get the DataBase Field name of the property
                string attribute = GetColumnName(propInfo);

                if (dr.Table.Columns.Contains(attribute))
                {
                    //Get the DataBase Field Value
                    //object datatype is required to hold the datarow's column value
                    object value = dr[attribute];

                    if (value != null)
                    {
                        if (value.GetType() != typeof(System.DBNull))
                        {
                            if (propInfo.PropertyType == typeof(DateTime?))
                            {
                                DateTime? dtNull = null;
                                if (value != null)
                                { dtNull = (DateTime?)value; }
                                value = dtNull;
                            }
                            else if (propInfo.PropertyType == typeof(int?))
                            {
                                int? intNull = null;
                                if (value != null)
                                { intNull = (int?)value; }
                                value = intNull;
                            }
                            else if (propInfo.PropertyType == typeof(Single?))
                            {
                                Single? singleNull = null;
                                if (value != null)
                                { singleNull = Convert.ToSingle(value); }
                                value = singleNull;
                            }
                            else if (propInfo.PropertyType == typeof(Guid?))
                            {
                                Guid? uidNull = null;
                                if (value != null)
                                { uidNull = (Guid?)value; }
                                value = uidNull;
                            }
                            else if (propInfo.PropertyType == typeof(bool?))
                            {
                                bool? boolNull = null;
                                if (value != null)
                                { boolNull = (bool?)value; }
                                value = boolNull;
                            }

                            else
                            {
                                value = Convert.ChangeType(value, propInfo.PropertyType);
                                //Set DataBase Field value to the entity's respective property
                            }
                            propInfo.SetValue(entity, value, null);
                        }
                    }
                }
            }
        }

        /// Author  :   Harinath
        /// Dated   :   30 Jun 2012
        /// <summary>
        /// Get Attribute(Database Field Mapper) name from property
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        private static string GetColumnName(PropertyInfo propInfo)
        {
            string columnName = string.Empty;

            //Get the Attribut(DataBaseFieldMapperAttribut) of the Property
            DataBaseFieldMapperAttribute objStateAttr = (DataBaseFieldMapperAttribute)Attribute.GetCustomAttribute(propInfo, typeof(DataBaseFieldMapperAttribute));

            //Check if Attribute is null or not
            if (objStateAttr != null)
            {
                //alias property of the attribute contains DataBase Field Name
                columnName = objStateAttr.alias;

                //If columnName(alias of attribute) value is null return property name itself
                if (string.IsNullOrEmpty(columnName))
                {
                    return propInfo.Name;
                }
            }

            return columnName;
        }

        #endregion

    }
}
