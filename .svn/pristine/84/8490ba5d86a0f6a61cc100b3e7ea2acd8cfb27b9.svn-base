using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities.Core
{
    public interface IEditableBusinessObject : IBusinessObject 
    {
        /// <summary>
        /// When Property value is changed and if the object is already exists,
        /// then IsDirty value will become true else false
        /// </summary>
        bool IsDirty { get; }
        /// <summary>
        /// If object is newly created, then IsNew value becomes true else false
        /// </summary>
        bool IsNew { get; }

        /// <summary>
        /// If any body deletes this object, then IsDeleted value becomes true else false.
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Every Business objects needs to implement this method 
        /// for implementing common save functionality.
        /// </summary>
        void Save();

        /// <summary>
        /// Every Business objects needs to implement this method 
        /// for implementing common delete functionality.
        /// </summary>
        void Delete();
    }
}
