using Argenis.CRUD.Borders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argenis.CRUD.Borders.Dtos.Activities
{
    public class OutputActivityModel
    {
        public OutputActivityModel(IEnumerable<Activity> activities)
        {
            Version = activities.First().Version;
            Activities = activities.Select(activity => new ActivityModel(activity.ActivityName, activity.Id, activity.GroupName));
        }

        public Guid Version { get; private set; }
        public IEnumerable<ActivityModel> Activities { get; private set; }
    }
}
