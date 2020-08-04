using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuatToan_Learning.List_CollectionNew
{
    static class ListObject_DiffAndGetTwoCollection
    {

        public static void run()
        {
            List<UserGroupmapCollection> _lstUserGroupmapCollections = new List<UserGroupmapCollection>();

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

                new UserGroupMap { UserId = "4", GroupId = "3", FormGroupFlag = "1", GroupDescription = "desc1", GroupName = "g1"}
            };

            //var uniqueInList2 = new HashSet<UserGroupMap>(list2);
            //uniqueInList2.ExceptWith(list1);

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                {
                    var ugmCollectionsA = list1[i];
                    var ugmCollectionsB = list2[i];

                    _lstUserGroupmapCollections.Add(new UserGroupmapCollection(ugmCollectionsA, ugmCollectionsB));
                }
            }

            if (_lstUserGroupmapCollections.Count > 0)
            {
                foreach (var item in _lstUserGroupmapCollections)
                {
                    Console.WriteLine(item.ToString());
                }
            }

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

        public override string ToString() => $"({UserId},{GroupId},{FormGroupFlag},{GroupDescription},{GroupName})";
    }

    public class UserGroupmapCollection
    {
        public UserGroupMap UserGroupOld { get; set; }
        public UserGroupMap UserGroupNew { get; set; }

        public UserGroupmapCollection(UserGroupMap _userGroupOld, UserGroupMap _userGroupNew)
        {
            UserGroupOld = _userGroupOld;
            UserGroupNew = _userGroupNew;
        }

        public override string ToString() => $"{UserGroupOld.ToString()} \n {UserGroupNew.ToString()}";
    }
}
