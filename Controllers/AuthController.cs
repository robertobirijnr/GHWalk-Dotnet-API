
using System.Threading.Tasks;
using GHWalk.Models.DTO;
using GHWalk.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GHWalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository){
                _tokenRepository = tokenRepository;
                _userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequest){
            var identityuser = new IdentityUser{
                UserName = registerRequest.Username,
                Email = registerRequest.Email
            };
             var user =   await _userManager.CreateAsync(identityuser,registerRequest.Password);
             if(user.Succeeded){
                if(registerRequest.Roles != null && registerRequest.Roles.Any()){
                 user =   await _userManager.AddToRolesAsync(identityuser,registerRequest.Roles);
                 if(user.Succeeded){
                    return Ok("user was registerd! please login");
                 }

                }
             }
             return BadRequest("Something went wront");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequest){
           var user = await _userManager.FindByEmailAsync(loginRequest.Email);
           if(user != null){
              var checkPassword =  await _userManager.CheckPasswordAsync(user,loginRequest.Password);
              if(checkPassword){
                //Get Roles for user
                var roles = await _userManager.GetRolesAsync(user);
                //create token 
                if(roles != null){
                  var jwtToken =   _tokenRepository.CreateJWTToken(user,roles.ToList());
                   return Ok(jwtToken);
                }
              
               
              }
           }

            return BadRequest("Username or password incorrect");
        }
        
    }
}