using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;

public interface IOrderService
{
    Task<bool> SubmitOrderAsync(string sessionId);
}