using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.State;

namespace RoboVet6.Blazor.UI.Components.Diary
{
    public partial class DiaryComponent
    {
        [Inject]
        public IDiaryDataService DiaryDataService { get; set; }

        [Inject]
        public AppState AppState { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private bool _authenticated;

        public List<Models.Diary> Diaries { get; set; }
        public int PreSelectedDiaryId { get; set; }

        private Models.Diary _selectedDiary;
        public Models.Diary SelectedDiary
        {
            get => _selectedDiary;
            set
            {
                _selectedDiary = value;
                AppState.SetDiaryId(SelectedDiary.Id);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (_authenticated)
            {
                Diaries = (await DiaryDataService.GetAllDiaries()).ToList();
                PreSelectedDiaryId = AppState.SelectedDiaryId;
                if(PreSelectedDiaryId != 0)
                {
                    SelectedDiary = Diaries.FirstOrDefault(x => x.Id == PreSelectedDiaryId);
                }
            }
        }
        
    }
}
