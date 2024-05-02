using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public interface IDistanceService
    {
        Task<Element> GetDistance(string Origin, string Destination);
    }
}
