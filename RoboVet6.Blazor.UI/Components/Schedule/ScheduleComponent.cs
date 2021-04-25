using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.State;
using Syncfusion.Blazor.Schedule;

namespace RoboVet6.Blazor.UI.Components.Schedule
{
    public partial class ScheduleComponent
    {
        [Inject]
        public AppState AppState { get; set; }

        [Inject]
        public IAppointmentDataService AppointmentDataService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public List<AppointmentData> AppointmentsToView { get; set; } = new List<AppointmentData>();

        
        private int _diaryId;
        [Parameter]
        public int DiaryId
        {
            get => _diaryId;
            set
            {
                _diaryId = value;
                UpdateAppointments();
            }
        }

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();

        //private bool _authenticated;


        //protected override async Task OnInitializedAsync()
        //{
        //    var authState = await AuthenticationStateTask;
        //    var user = authState.User;

        //    _authenticated = user.Identity?.IsAuthenticated ?? false;

        //    if (_authenticated)
        //    {
        //        //Do something in here
        //    }
        //}

        private async Task UpdateAppointments()
        {
            if (DiaryId != 0)
            {
                Appointments = await AppointmentDataService.GetAppointmentsByDiaryId(DiaryId);

                if (Appointments.Any())
                {
                    AppointmentsToView = MapAppointments(Appointments);
                }
                StateHasChanged();
            }
            
        }

        DateTime CurrentDate = DateTime.Now;

        private View CurrentView = View.Day;

        private List<AppointmentData> MapAppointments(IEnumerable<Appointment> appointments)
        {
            var appointmentsToReturn = new List<AppointmentData>();

            foreach (var appointment in appointments)
            {
                appointmentsToReturn.Add(new AppointmentData
                {
                    Id = appointment.Id,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.StartTime.AddMinutes(appointment.AppointmentLength),
                    Description = appointment.Notes
                });
            }


            return appointmentsToReturn;
        }
    }
   
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
    }
}
