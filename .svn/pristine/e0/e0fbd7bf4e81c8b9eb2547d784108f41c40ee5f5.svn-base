using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities.Core
{
    [Serializable]
    public abstract class BusinessBase<T> : IEditableBusinessObject where T : IBusinessObject
    {
        private bool _IsNew = false;
        private bool _IsDirty = false;
        private bool _IsDeleted = false;


        #region IEditableBusinessObject Members

        public bool IsDirty
        {
            get { return _IsDirty; }
            set { _IsDirty =value; }
        }
    

        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //public void Save()
        //{

        //}

        //public void Delete()
        //{

        //}

        #endregion

        #region "Row state information"

        //protected virtual void MarkNew()
        //{
        //    _IsNew = true;
        //    _IsDeleted = false;
        //    _IsDirty = false;
        //}

        //protected virtual void MarkOld()
        //{
        //    _IsDeleted = false;
        //    _IsNew = false;
        //    _IsDirty = false;
        //}

        //protected virtual void MarkDirty()
        //{
        //    _IsNew = false;
        //    _IsDirty = true;
        //    _IsDeleted = false;
        //}

        #endregion

        #region IEditableBusinessObject Members


        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
