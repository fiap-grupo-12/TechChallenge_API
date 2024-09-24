using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Amazon;

namespace FIAP.TechChallenge.ByteMeBurguer.API.Extensions
{
    public class SecretsService()
    {
        public static string GetSecret(string secretName)
        {
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
                throw e;
            }

            return response.SecretString;
        }
    }

}
