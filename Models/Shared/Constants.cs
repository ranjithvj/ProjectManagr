namespace Models.Shared
{
    public static class Constants
    {
        public static class ConnectionStrings
        {
            public const string LocalSQLExpressConnectionString = "ProjectManagr";
        }

        public enum TaskType
        {
            RequirementAnalysis = 1,
            Development = 2,
            Bug = 3
        };

        public enum Role
        {
            Engineer = 1,
            SeniorEngineer = 2,
            TechLead = 3,
            Manager = 4
        }

        public enum Status
        {
            Proposed = 1,
            Active = 2,
            Resolved = 3,
            Closed = 4
        }

        public class DateFormat
        {
            public const string DefaultFormat = "M/dd/yyyy";
        }

        public class Site
        {
            public const string Global = "Global Sites";
        }

        public class EntityStatus
        {
            public const string Pipeline = "Pipeline";
            public const string SandI = "S&I";
            public const string SOM = "SOM";
            public const string Turnaround = "Turnaround";
            public const string Closed = " Closed";
            public const string InProgress = " In Progress";
            public const string InProgressConfirmed = "In Progress - Confirmed";
            public const string InProgressTentative = "In Progress - Tentative";
        }
    }
}