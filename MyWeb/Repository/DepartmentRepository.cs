using MyWeb.Models;
using MyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyWeb.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public DepartmentRepository(IHttpClientFactory clientFactory, JsonSerializerOptions jsonSerializerOptions) : base(clientFactory, jsonSerializerOptions)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
        }
    }
}
