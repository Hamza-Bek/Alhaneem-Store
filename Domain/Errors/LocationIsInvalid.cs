using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public class LocationIsInvalid : Error
    {
        public LocationIsInvalid() : base("Location is missed or invalid.")
        {
        }
    }
}
