using Argenis.CRUD.Shared.Configurations;
using System;

namespace Argenis.CRUD.Borders.Entities
{
    public class Activity
    {
        public Activity(string activityName, Guid id, string groupName)
        {
            ActivityName = activityName;
            Id = id;
            Version = Constants.ProductVersion;
            GroupName = groupName;
        }

        public string ActivityName { get; private set; }
        public Guid Id { get; private set; }
        public Guid Version { get; private set; }
        public string GroupName { get; private set; }
    }
}
