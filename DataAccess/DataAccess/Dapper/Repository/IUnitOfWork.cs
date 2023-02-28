using DataAccess.Models;
using DataAccess.Models.DTOs;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<IPD> IPDRepository { get; }
        GenericRepository<IPDInitialAssessmentForNewborns> IPDInitialAssessmentForNewborns { get; }
        GenericRepository<FormDatas> FormDatasRepository { get; }
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<TableDataDto> TableDataDtoRepository { get; }
        GenericRepository<OrderDto> OrderDtoRepository { get; }
        GenericRepository<ChargeItemDto> ChargeItemDtoRepository { get; }
        GenericRepository<CustomerDto> CustomerDtoRepository { get; }
        GenericRepository<UserDto> UserDtoRepository { get; }
        GenericRepository<ChargeVisitDto> ChargeVisitDtoRepository { get; }
        GenericRepository<ChargeDto> ChargeDtoRepository { get; }
        GenericRepository<ChargeItemMicrobiologyDto> ChargeItemMicrobiologyDtoRepository { get; }
        GenericRepository<ChargeItemPathologyDto> ChargeItemPathologyDtoRepository { get; }
    }
}
