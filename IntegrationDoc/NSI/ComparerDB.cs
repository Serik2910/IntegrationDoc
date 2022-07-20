using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationDoc.NSI
{
    public interface IHasGuid
    {
        int? guid { get; set; }
    }
    //public class ComparerDBOrg : IEqualityComparer<nsi_org>
    //{
    //    public bool Equals(nsi_org x, nsi_org y)
    //    {
    //        if(Object.ReferenceEquals(x,y)) return true;
    //        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
    //        return x.member == y.member;
    //    }

    //    public int GetHashCode(nsi_org obj)
    //    {
    //        if (Object.ReferenceEquals(obj, null)) return 0;
    //        int hashProductMember = obj.member.GetHashCode();
    //        return hashProductMember ;
    //    }
    //}
    public class ComparerDB<T> : IEqualityComparer<T> where T : IHasGuid
    {
        public bool Equals(T x, T y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
            return x.guid == y.guid;
        }

        public int GetHashCode(T obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashProductMember = obj.guid.GetHashCode();
            return hashProductMember;
        }
    }
    //public class ComparerDBPosTypes : IEqualityComparer<nsi_pos_type>
    //{
    //    public bool Equals(nsi_pos_type x, nsi_pos_type y)
    //    {
    //        if (Object.ReferenceEquals(x, y)) return true;
    //        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
    //        return x.guid == y.guid;
    //    }

    //    public int GetHashCode(nsi_pos_type obj)
    //    {
    //        if (Object.ReferenceEquals(obj, null)) return 0;
    //        int hashProductMember = obj.guid.GetHashCode();
    //        return hashProductMember;
    //    }
    //}

}