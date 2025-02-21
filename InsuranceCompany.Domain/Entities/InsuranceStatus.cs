﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InsuranceCompany.Domain.Entities
{
    public enum InsuranceStatus
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Active,
        Canceled
    }

}
