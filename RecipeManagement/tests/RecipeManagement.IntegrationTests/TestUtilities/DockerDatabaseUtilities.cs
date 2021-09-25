namespace RecipeManagement.IntegrationTests.TestUtilities
{
    using System.Linq;
    using System.Threading.Tasks;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Model.Builders;
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Services.Extensions;

    public static class DockerDatabaseUtilities
    {
        private const string DB_PASSWORD = "#testingDockerPassword#";
        private const string DB_USER = "SA";
        private const string DB_IMAGE = "mcr.microsoft.com/mssql/server";
        private const string DB_IMAGE_TAG = "2019-latest";
        private const string DB_CONTAINER_NAME = "IntegrationTesting_RecipeManagement";
        private const string DB_VOLUME_NAME = "IntegrationTesting_RecipeManagement";

        public static async Task<int> EnsureDockerStartedAndGetPortPortAsync()
        {
            await DockerUtilities.CleanupRunningContainers(DB_CONTAINER_NAME);
            await DockerUtilities.CleanupRunningVolumes(DB_CONTAINER_NAME);
            var freePort = DockerUtilities.GetFreePort();

            var hosts = new Hosts().Discover();
            var docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");     

            // create a volume, if one doesn't already exist
            var volume = docker?.GetVolumes().FirstOrDefault(v => v.Name == DB_VOLUME_NAME) ?? new Builder()
                .UseVolume()
                .WithName(DB_VOLUME_NAME)
                .Build();

            // create container, if one doesn't already exist
            var existingContainer = docker?.GetContainers().FirstOrDefault(c => c.Name == DB_CONTAINER_NAME);

            if (existingContainer == null)
            {
                var container = new Builder().UseContainer()
                    .WithName(DB_CONTAINER_NAME)
                    .UseImage($"{DB_IMAGE}:{DB_IMAGE_TAG}")
                    .ExposePort(freePort, 1433)
                    .WithEnvironment(
                        "ACCEPT_EULA=Y",
                        $"SA_PASSWORD={DB_PASSWORD}")
                    .WaitForPort("1433/tcp", 30000 /*30s*/)
                    .MountVolume(volume, "/var/lib/sqlserver/data", MountType.ReadWrite)
                    .Build();
        
                container.Start();

                await DockerUtilities.WaitUntilDatabaseAvailableAsync(GetSqlConnectionString(freePort.ToString()));
                return freePort;
            }

            return existingContainer.ToHostExposedEndpoint("1433/tcp").Port;
        }

        public static string GetSqlConnectionString(string port)
        {
            return DockerUtilities.GetSqlConnectionString(port, DB_PASSWORD, DB_USER);
        }
    }
}