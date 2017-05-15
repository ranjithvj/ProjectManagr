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

    }
}