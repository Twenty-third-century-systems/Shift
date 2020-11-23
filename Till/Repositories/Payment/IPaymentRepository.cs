using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Till.Repositories.Base;

namespace Till.Repositories.Payment {
    public interface IPaymentRepository:IRepository<Models.Payment> {
        Task<IEnumerable<Models.Payment>> GetUnreconsiledPaymentsByUser(Guid user);
    }
}