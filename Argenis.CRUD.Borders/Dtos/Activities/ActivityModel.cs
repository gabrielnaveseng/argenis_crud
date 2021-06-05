using System;

namespace Argenis.CRUD.Borders.Dtos.Activities
{
    public class ActivityModel
    {
        public ActivityModel(string ActivityName, Guid Id, string GroupName)
        {
            this.ActivityName = ActivityName;
            this.Id = Id;
            this.GroupName = GroupName;
        }

        public string ActivityName { get; private set; }
        public Guid Id { get; private set; }
        public string GroupName { get; private set; }
    }
}
