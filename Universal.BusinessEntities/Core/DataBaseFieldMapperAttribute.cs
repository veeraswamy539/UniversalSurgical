using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities.Core
{
    /// <summary>
    /// Maps this property to its corresponding field in DataBase
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataBaseFieldMapperAttribute:System.Attribute
    {
        #region Local Memebers

        private string _column = string.Empty;
        private System.Type _type = null;

        #endregion

        #region Properties

        public string alias
        {
            get { return _column; }            
        }
        
        public System.Type DataType
        {
            get { return _type; }
        }

        #endregion

        #region Constructors

        public DataBaseFieldMapperAttribute()
        {
        }

        public DataBaseFieldMapperAttribute(string _alias)
        {
            _column = _alias;
        }

        public DataBaseFieldMapperAttribute(string _alias, Type type)
        {
            _column = _alias;
            _type = type;
        }

        #endregion
    }
}
