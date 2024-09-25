using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Amazon;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Extensions
{
    public class SecretsService()
    {
        public static string GetSecret(string secretName)
        {
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            ILogger<SecretsService> logger = loggerFactory.CreateLogger<SecretsService>();

            logger.LogInformation("Consumindo SecretsManager");

            string region = "us-east-1";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };

            GetSecretValueResponse response;

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (Exception e)
            {
                logger.LogError("Erro ao buscar secret " + e.Message);
                return string.Empty;
            }

            logger.LogInformation($@"SecretsManager recuperado com sucesso: {response.SecretString}");
            return response.SecretString;
        }
    }

}
