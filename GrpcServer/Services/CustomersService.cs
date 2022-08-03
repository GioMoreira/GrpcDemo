using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {

            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel()
                {
                    FirstName = "Giovanna",
                    LastName = "Moreira",
                    EmailAddress = "sgiomoreira@gmail.com",
                    Age = 23,
                    IsAlive = true
                },

                new CustomerModel()
                {
                    FirstName = "John",
                    LastName = "Snow",
                    EmailAddress = "js@gmail.com",
                    Age = 19,
                    IsAlive = false
                },

                new CustomerModel()
                {
                    FirstName = "Lana",
                    LastName = "Velaryon",
                    EmailAddress = "lv@gmail.com",
                    Age = 27,
                    IsAlive = true
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }

        }
    }
}
