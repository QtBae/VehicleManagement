using APICleanArchi.Entities;
using APICleanArchi.Repositories;
using ApiCleanArchiDTO;
using AutoMapper;

namespace APICleanArchi.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,IMapper mapper,ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null)
            {
                _logger.LogError("No users found");
                return null;
            }

            _logger.LogInformation("Users found");
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid user id");
                return null;
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found");
                return null;
            }

            _logger.LogInformation("User found");
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO user)
        {
            if (user == null)
            {
                _logger.LogError("Invalid user");
                return null;
            }

            var userEntity = _mapper.Map<UserEntity>(user);
            var createdUser = await _userRepository.CreateUserAsync(userEntity);
            if (createdUser == null)
            {
                _logger.LogError("User not created");
                return null;
            }

            _logger.LogInformation("User created");
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            if (user == null)
            {
                _logger.LogError("Invalid user");
                return null;
            }

            var userEntity = _mapper.Map<UserEntity>(user);
            var updatedUser = await _userRepository.UpdateUserAsync(userEntity);
            if (updatedUser == null)
            {
                _logger.LogError("User not updated");
                return null;
            }

            _logger.LogInformation("User updated");
            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid user id");
                return false;
            }

            var deleted = await _userRepository.DeleteUserAsync(id);
            if (!deleted)
            {
                _logger.LogError("User not deleted");
                return false;
            }

            _logger.LogInformation("User deleted");
            return true;
        }
    }

    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        // get user by id
        Task<UserDTO> GetUserByIdAsync(int id);

        // create user
        Task<UserDTO> CreateUserAsync(UserDTO user);

        // update user
        Task<UserDTO> UpdateUserAsync(UserDTO user);

        // delete user
        Task<bool> DeleteUserAsync(int id);


    }
}
