using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Security.Models
{
    public sealed class UserListing : IModel
    {
        public int UserId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        public string ProfileImageUrl { get; set; }

        [Display(Name = "Joined")]
        public DateTime Created { get; set; }

        [Display(Name = "Last Active")]
        public DateTime LastActive { get; set; }

        [Display(Name = "Post Count")]
        public int PostCount { get; set; }

        [Display(Name = "Topic Count")]
        public int TopicCount { get; set; }
    }

    public sealed class UserListingTable : ICollection<UserListing>
    {
        private readonly List<UserListing> list;

        public UserListingTable()
            => list = new List<UserListing>();

        public UserListingTable(IEnumerable<UserListing> collection)
            => list = new List<UserListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(UserListing item)
            => list.Add(item);

        public void Clear()
            => list.Clear();

        public bool Contains(UserListing item)
            => list.Contains(item);

        public void CopyTo(UserListing[] array, int arrayIndex)
            => list.CopyTo(array, arrayIndex);

        public IEnumerator<UserListing> GetEnumerator()
            => list.GetEnumerator();

        public bool Remove(UserListing item)
            => list.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => list.GetEnumerator();
    }
}