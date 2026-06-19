using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Shared.Emails;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Interfaces.AppUsers;
using StudyBuddy.Shared.DTOs.AuthDTOs;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Helpers;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Security.Claims;
using System.Text.Json;


namespace StudyBuddy.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<Skill> skillRepo;
        private readonly IEmailService emailService;

        public AuthService(
            IAppUserRepository appUserRepository,
            IRepo<ClientUser> clientUserRepo,
            IRepo<Major> majorRepo,
            IRepo<Skill> skillRepo,
            IEmailService emailService)
        {
            this.appUserRepository = appUserRepository;
            this.clientUserRepo = clientUserRepo;
            this.majorRepo = majorRepo;
            this.skillRepo = skillRepo;
            this.emailService = emailService;
        }

        public async Task<Result> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(token))
                return Result.Failure(Error.TokenIsEmpty);
            var user = await appUserRepository.FindByEmailAsync(email);
            if (user == null)
                return Result.Failure(Error.UserNotFound);

            var result = await appUserRepository.ConfirmEmail(user, token);

            if (!result.Succeeded)
                return Result.Failure(Error.InvalidOrExpiredToken);

            return Result.Success();
        }

        public UserInfoDTO GetUserInfo(ClaimsPrincipal user)
        {
            var claims = user.Claims
                .Where(c => c.Type != ClaimTypes.Role && !c.Type.StartsWith("Permission"))
                .ToDictionary(c => c.Type, c => c.Value);

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToArray();

            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var claim = user.Claims.FirstOrDefault(c => c.Type == AuthHelper.CleintId);
            int clientId = int.Parse(claim != null ? claim.Value : "0");

            var json = JsonSerializer.Serialize(roles);

            return new UserInfoDTO
            {
                IsAuthenticated = user.Identity.IsAuthenticated,
                UserName = user.Identity.Name,
                UserId = userId,
                ClientId = clientId,
                ExposedClaims = claims,  // now a Dictionary<string,string>
                Json = json
            };
        }

        public async Task<Result<string>> Login(LoginDTO loginDTO)
        {
            var user = await appUserRepository.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return Result<string>.Failure(AuthErrorMessage.UserCannotFound);

            if (!user.EmailConfirmed)
                return Result<string>.Failure(AuthErrorMessage.EmailNotVerify);
            var check = await appUserRepository.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!check)
                return Result<string>.Failure(AuthErrorMessage.PasswordNotCorrect);

            try
            {
                await appUserRepository.SignInAsync(user, loginDTO.RememberMe);
            }
            catch
            {
                return Result<string>.Failure(AuthErrorMessage.LoginFailed);
            }

            return Result<string>.Success(user.Id.ToString());
        }

        public async Task<Result> Logout()
        {
            try
            {
                await appUserRepository.SignOutAsync();
            }
            catch
            {
                return Result.Failure(AuthErrorMessage.LogoutFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Register(RegisterDTO registerDTO , string rootPath)
        {
            var major = await majorRepo.GetByIdAsync(registerDTO.MajorId);
            if (major == null)
                return Result.Failure(Error.MajorNotFound);

            var user = await appUserRepository.FindByEmailAsync(registerDTO.Email);
            if (user != null)
                return Result.Failure(Error.UserAlReadyRegistered);

            var newUser = new AppUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await appUserRepository.CreateAsync(newUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Any()
                    ? string.Join(", ", result.Errors.Select(e => e.Description))
                    : AuthErrorMessage.RegisterFailed;

                return Result.Failure(errorMessages);
            }
            var newClientUser = new CreateClientUserDTO();
            newClientUser.UserId = newUser.Id;
            newClientUser.UserName = registerDTO.UserName;
            newClientUser.MajorId = registerDTO.MajorId;
            var resultCreateClientUser = ClientUser.Create(newClientUser);
            if (!resultCreateClientUser.IsSuccess)
                return Result.Failure(resultCreateClientUser.Error!);

            var clientUser = resultCreateClientUser.Value;
            if (clientUser == null)
                return Result.Failure(Error.ClientUserNotFound);

            //make skills from major
            clientUser.UpdateFlagIsSkillMajor(true);
            var path = Path.Combine(rootPath, "data", "major_tags.json");
            if (!File.Exists(path))
                return Result.Failure(Error.JsonNotFound);

            var jsonString = await File.ReadAllTextAsync(path);

            var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);
            if (data == null)
                return Result.Failure(Error.JsonFormatWrong);

            data.TryGetValue(major.Name, out var tags);
            if (tags == null)
                return Result.Failure(Error.TagsNotFound);

            foreach (var skill in tags.Distinct())
            {
                var skillIn = await skillRepo.GetQuery()
                    .FirstOrDefaultAsync(s => s.Name.ToLower() == skill.ToLower());
                if (skillIn == null)
                {
                    skillIn = Skill.Create(skill.ToLower());
                    await skillRepo.AddAsync(skillIn);
                }

                clientUser.AddSkill(skillIn);
            }

            await clientUserRepo.AddAsync(clientUser);
            try
            {
                await clientUserRepo.SaveAsync();
                var sendTokenResult = await SendToken(newUser.Email);
                if (!sendTokenResult.IsSuccess)
                    return Result.Failure(sendTokenResult.Error!);

            }
            catch
            {
                return Result.Failure(Error.CreateUserFailed);
            }

           




            return Result.Success();

        }

        public async Task<Result> SendToken(string email)
        {
            var user = await appUserRepository.FindByEmailAsync(email);
            if (user == null)
                return Result.Failure(Error.UserNotFound);
            var token = await appUserRepository.GenerateToken(user);
            var result = await emailService.SendConfirmToken(email, token);
            return result;
        }
    }
}
