namespace PlatformService.Data.SeedData
{
    public static class PlatformSeedData
    {
        public static Platform[] GetPlatformSeedData()
        {
            return new Platform[]
            {
                new Platform
                {
                    Id = 1,
                    Name = "DotNet",
                    Publisher="Microsoft",
                    Cost="Free"
                },
                new Platform
                {
                    Id = 2,
                    Name = "SQL Server Express",
                    Publisher="Microsoft",
                    Cost="Free"
                },
                new Platform
                {
                    Id = 3,
                    Name = "Kubernetes",
                    Publisher="Cloud Native Computing Foundation",
                    Cost="Free"
                },
            };
        }
    }
}