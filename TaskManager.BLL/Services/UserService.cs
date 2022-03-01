using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Commons;
using TaskManager.BLL.Services.Dto;
using TaskManager.Core.Repository;
using TaskManager.Core.Responses;
using TaskManager.DAL.Entities;
using TaskManager.Infastructure.Auth;
using TaskManager.Infastructure.Auth.Models;

namespace TaskManager.BLL.Services
{
    public interface IUserService
    {
        Task<ApiResponse<LoginResult>> Login(LoginRequest request);
        Task<ApiResponse<SignUpResult>> SignInAsAdmin(SignUpAsAdminRequest request);
        Task<ApiResponse<SignUpResult>> CreateUserAsStaff(SignUpRequest request);
    }
    public class UserService : BaseApiServices, IUserService
    {
        private readonly IAuthManager _authManager;
        private readonly IRepository<Organization> _organizationRepository;

        public UserService(IAuthManager authManager,
            IRepository<Organization> organizationRepository,
            IServiceProvider serviceProvider
            ) : base(serviceProvider)
        {
            _authManager = authManager;
            _organizationRepository = organizationRepository;
        }

        public async Task<ApiResponse<SignUpResult>> CreateUserAsStaff(SignUpRequest request)
        {
            ApiResponse<SignUpResult> response = null;
            try
            {
                var result = await _authManager.SignUp(request, false, GetCurrentTenantId());
                response = new ApiResponse<SignUpResult>(result);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<SignUpResult>(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<LoginResult>> Login(LoginRequest request)
        {
            ApiResponse<LoginResult> response = null;
            try
            {
                var result = await _authManager.Login(request.Email, request.Password);
                response = new ApiResponse<LoginResult>(result);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<LoginResult>(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<SignUpResult>> SignInAsAdmin(SignUpAsAdminRequest request)
        {
            ApiResponse<SignUpResult> response = null;
            try
            {
                await _organizationRepository.Add(new Organization
                {
                    Name = request.OrganizationName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber
                });

                var organizationResult = await _organizationRepository.Commit();
                if (organizationResult == 1)
                {
                    var addedOrganizatonId = _organizationRepository.GetAll().Max(i => i.Id);
                    SignUpRequest signUpRequest = new SignUpRequest
                    {
                        Email = request.SignUpRequest.Email,
                        Password = request.SignUpRequest.Password,
                        UserName = request.SignUpRequest.UserName
                    };
                    var signUpResult = await _authManager.SignUp(signUpRequest, true, addedOrganizatonId);
                    response = new ApiResponse<SignUpResult>(signUpResult);
                }
                else
                {
                    response = new ApiResponse<SignUpResult>("Error occured organisation saving time");
                }
            }
            catch (Exception ex)
            {
                response = new ApiResponse<SignUpResult>(ex.Message);
            }
            return response;
        }
    }
}
