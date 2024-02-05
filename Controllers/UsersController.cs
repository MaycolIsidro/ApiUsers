using API_Users.Models;
using API_Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMailService mailService;
        public UsersController(ApiContext context, IMailService mailService)
        {
            _context = context;
            this.mailService = mailService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _context.Users;
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            var userFound = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userFound != null) return BadRequest("El usuario ya se encuentra registrado");
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] User user)
        {
            var userFound = _context.Users.FirstOrDefault(p => p.IdUser == user.IdUser);
            if (userFound == null) return NoContent();
            userFound.Name = user.Name;
            userFound.LastName = user.LastName;
            userFound.Email = user.Email;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("SendEmail/{email}")]
        public async Task<ActionResult> SendEmailRecuperation(string email)
        {
            var user = _context.Users.FirstOrDefault(p => p.Email == email);
            if (user == null) return NoContent();
            GenerateToken generateToken = new GenerateToken();
            var token = generateToken.GenerateTokens();
            MailRequest mailRequest = new ()
            {
                ToEmail = email,
                Subject = "Restaurar contraseña",
                Body = $"Hola {user.Name} {user.LastName}, para restablecer tu contraseña has click en el siguiente enlace\n <a href='#/${token}' style='padding: 15px; background-color: aquamarine; border-radius: 5px; text-decoration: none; color: #fff; font-size: 19px;'>Restablecer contraseña</a>"
            };
            await mailService.SendMailAsync(mailRequest);
            user.TokenRecuperation = token;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("RestorePassword/{tokenRecuperation}")]
        public ActionResult RecuperatePassword([FromBody] User user, string tokenRecuperation)
        {
            var userFound = _context.Users.FirstOrDefault(p => p.IdUser == user.IdUser);
            if (userFound == null) return NoContent();
            if (userFound.TokenRecuperation != tokenRecuperation) return BadRequest();
            userFound.Password = user.Password;
            userFound.TokenRecuperation = null;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idUser}")]
        public ActionResult Delete(string idUser)
        {
            if (!Guid.TryParse(idUser, out var id)) return BadRequest();
            var userFound = _context.Users.FirstOrDefault(p => p.IdUser == id);
            if (userFound == null) return NoContent();
            _context.Users.Remove(userFound);
            _context.SaveChanges();
            return Ok();
        }
    }
}
