using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities.Core
{
    public abstract class BusinessCollectionBase<T>: List<T>, Core.ICollectionObject where T:IEditableBusinessObject 
    {
        
    }
}
