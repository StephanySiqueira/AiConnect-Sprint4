using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly FirebaseAuth _auth;

    public AuthController()
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(@"C:\Users\tesiq\Downloads\AiConnect-Sprint4\AiConnect-Sprint4\AiConnect\Firebase\firebase_credentials.json"),
                ProjectId = "aiconnect-5b3fa"
            });
        }
        _auth = FirebaseAuth.DefaultInstance;

    }

    [HttpPost("generate-custom-token")]
    public async Task<IActionResult> GenerateCustomToken([FromBody] string uid)
    {
        try
        {
            var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
            return Ok(new { token = customToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Erro ao gerar o token", detalhes = ex.Message });
        }
    }
    
}
