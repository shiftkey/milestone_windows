using System;

namespace MilestoneForWindows.Models
{
    [Flags]
    public enum RepoType : byte
    {
        None = 0,
        Owned = 1,
        Watched = 2,
        Both = 3
    }
}