using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;

namespace RoboVet6.Blazor.UI.Pages.Client
{
    public partial class ClientEdit
    {
        [Parameter]
        public int ClientId { get; set; }

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Models.Client Client { get; set; } = new Models.Client();

        private bool _authenticated;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (!_authenticated)
            {
                return;
            }

            Saved = false;

            if (ClientId != 0)
            {
                Client = await ClientDataService.GetClientById(ClientId);
            }
        }

        protected async Task HandleValidSubmit()
        {

            if (Client.Id == 0) //new
            {
                var addedClient = await ClientDataService.AddClient(Client);

                if (addedClient != null)
                {
                    StatusClass = "alert-success";
                    Message = "New client added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new client. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await ClientDataService.UpdateClient(Client);
                StatusClass = "alert-success";
                Message = "Client updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateToClientSearch()
        {
            NavigationManager.NavigateTo("/client");
        }
    }
}
