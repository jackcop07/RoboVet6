﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.Client
{
    public class ClientToInsertDto
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
    }
}