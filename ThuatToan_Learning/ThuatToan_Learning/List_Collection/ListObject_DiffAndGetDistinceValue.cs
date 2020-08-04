using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuatToan_Learning.List_Collection
{
    static class ListObject_DiffAndGetDistinceValue
    {
        public static void run()
        {
            var list1 = new List<UserGroupMap>
            {
                new UserGroupMap { UserId = "1", GroupId = "1", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "1", GroupId = "2", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "1", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "2", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},

                new UserGroupMap { UserId = "4", GroupId = "4", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"}
            };

            var list2 = new List<UserGroupMap>
            {
                new UserGroupMap { UserId = "1", GroupId = "1", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "1", GroupId = "2", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "1", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "2", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},

                new UserGroupMap { UserId = "4", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"},
                new UserGroupMap { UserId = "3", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"}
            };

            var uniqueInList2 = new HashSet<UserGroupMap>(list2);
            uniqueInList2.ExceptWith(list1);
        }
    }

    public class UserGroupMap : IEquatable<UserGroupMap>
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public string FormGroupFlag { get; set; }
        public string GroupDescription { get; set; }
        public string GroupName { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + (UserId ?? "").GetHashCode();
                hash = hash * 23 + (GroupId ?? "").GetHashCode();
                hash = hash * 23 + (FormGroupFlag ?? "").GetHashCode();
                hash = hash * 23 + (GroupDescription ?? "").GetHashCode();
                hash = hash * 23 + (GroupName ?? "").GetHashCode();

                return hash;
            }
        }

        public bool Equals(UserGroupMap other)
        {
            if (other == null) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            return this.UserId == other.UserId
                && this.GroupId == other.GroupId
                && this.FormGroupFlag == other.FormGroupFlag
                && this.GroupDescription == other.GroupDescription
                && this.GroupName == other.GroupName;
        }
    }
}
