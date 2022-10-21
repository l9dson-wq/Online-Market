
using StoackApp.Core.Application.Interfaces.Repositories;
using StoackApp.Core.Application.Interfaces.Services;
using StoackApp.Core.Application.ViewModels.Products;
using StoackApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.password = vm.Password; 

            user = await _repository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Username = user.Username;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;
            userVm.Password = user.password;

            return userVm;
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _repository.LoginAsync(loginVm);

            if(user == null)
            {
                return null;
            }

            UserViewModel userVm = new();
            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Username = user.Username;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;
            userVm.Password = user.password;

            return userVm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = new();
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.password = vm.Password;

            await _repository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.Username = user.Username;
            vm.Email = user.Email;
            vm.Phone = user.Phone;
            vm.Password = user.password;
            
            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var Userlist = await _repository.GetAllWithInclude(new List<string> { "Products" });

            return Userlist.Select(user => new UserViewModel { 
                Name = user.Name,
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.password
            }).ToList();
        }
    }
}
