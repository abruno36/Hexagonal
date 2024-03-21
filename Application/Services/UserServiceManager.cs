using Domain.Adapters;
using Domain.Entities;
using Domain.Ports;

namespace Application.Services;

public class UserServiceManager : IUserService
{
    private readonly IEmailService _emailAdapter;
    private readonly IUserRepository _userRepository;

    public UserServiceManager(IEmailService emailAdapter,
                              IUserRepository userRepository)
    {
        _emailAdapter = emailAdapter;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();
        return users;
    }

    public async Task<User> AddNewUserAsync(User user)
    {
        await _userRepository.Insert(user);
        _emailAdapter.SendEmail("abruno36@gmail.com", user.Email, "Usuário incluído com sucesso!!", "Added user" );
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var userUpdated = await _userRepository.Update(user);
        _emailAdapter.SendEmail("abruno36@gmail.com", user.Email, "Usuário alterado com sucesso!!", "Updated user");
        return userUpdated;
    }

    public async Task<User> DeleteUserAsync(Guid id)
    {
        var userDeleted = await _userRepository.Delete(id);
        _emailAdapter.SendEmail("abruno36@gmail.com", userDeleted.Email, "Usuário excluído com sucesso!!", "Deleted user");
        return userDeleted;
    }
}
