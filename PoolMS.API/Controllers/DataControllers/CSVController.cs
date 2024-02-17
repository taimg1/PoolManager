using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using PoolMS.Repository.Repository;
using PoolMS.Service.Interface;

namespace PoolMS.API.Controllers.DataControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController(IRepository<Payment> paymentRepository, IRepository<Role> roleRepository, IRepository<Subscription> subscriptionRepository,
            UserRepository userRepository, IRepository<Visit> visitRepository, IRepository<Pool> poolRepository, 
            IRepository<Reservation> reservationRepository, IRepository<PoolSize> poolSizeRepository, IRepository<SubType> subtypeRepository,
            CSVController _csvController) : ControllerBase
    { 
        private readonly CSVController _csvController;


    }
}
