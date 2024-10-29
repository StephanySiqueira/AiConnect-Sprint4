using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AiConnect.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public FirebaseAuthService()
        {
            // Inicializa o Firebase apenas uma vez
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(@"C:\Users\tesiq\Downloads\AiConnect-Sprint4\AiConnect-Sprint4\AiConnect\Firebase\firebase_credentials.json")
                });
            }
        }

            public async Task<string> CreateCustomTokenAsync(string uid)
            {
                try
                {
                    // Cria um token personalizado com o UID do usuário
                    string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                    return customToken;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao criar o token personalizado: " + ex.Message);
                }
            }

        public async Task<string> VerifyIdTokenAsync(string idToken)
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                return decodedToken.Uid; // Retorna o UID do usuário autenticado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar o token: " + ex.Message);
            }
        }
    }

    public interface IFirebaseAuthService
    {
        Task<string> VerifyIdTokenAsync(string idToken);
        Task<string> CreateCustomTokenAsync(string uid);  
    }

}
