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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GenericRepository<IPD> _IPDRepository;
        private readonly GenericRepository<IPDInitialAssessmentForNewborns> _IPDInitialAssessmentForNewborns;
        private readonly GenericRepository<FormDatas> _FormDatasRepository;
        private readonly GenericRepository<Customer> _CustomerRepository;
        private readonly GenericRepository<TableDataDto> _TableDataDtoRepository;
        private readonly GenericRepository<OrderDto> _OrderDtoRepository;
        private readonly GenericRepository<ChargeItemDto> _ChargeItemDtoRepository;
        private readonly GenericRepository<CustomerDto> _CustomerDtoRepository;
        private readonly GenericRepository<UserDto> _UserDtoRepository;
        private readonly GenericRepository<ChargeVisitDto> _ChargeVisitDtoRepository;
        private readonly GenericRepository<ChargeDto> _ChargeDtoRepository;
        private readonly GenericRepository<ChargeItemMicrobiologyDto> _ChargeItemMicrobiologyDtoRepository;
        private readonly GenericRepository<ChargeItemPathologyDto> _ChargeItemPathologyDtoRepository;

        public UnitOfWork()
        {
            _IPDRepository = new GenericRepository<IPD>("IPDs");
            _IPDInitialAssessmentForNewborns = new GenericRepository<IPDInitialAssessmentForNewborns>("IPDInitialAssessmentForNewborns");
            _FormDatasRepository = new GenericRepository<FormDatas>("FormDatas");
            _CustomerRepository = new GenericRepository<Customer>("Customers");
            _TableDataDtoRepository = new GenericRepository<TableDataDto>("TableDatas");
            _OrderDtoRepository = new GenericRepository<OrderDto>("Orders");
            _ChargeItemDtoRepository = new GenericRepository<ChargeItemDto>("ChargeItems");
            _CustomerDtoRepository = new GenericRepository<CustomerDto>("Customers");
            _UserDtoRepository = new GenericRepository<UserDto>("Users");
            _ChargeVisitDtoRepository = new GenericRepository<ChargeVisitDto>("ChargeVisits");
            _ChargeDtoRepository = new GenericRepository<ChargeDto>("Charges");
            _ChargeItemMicrobiologyDtoRepository = new GenericRepository<ChargeItemMicrobiologyDto>("ChargeItemMicrobiologies");
            _ChargeItemPathologyDtoRepository = new GenericRepository<ChargeItemPathologyDto>("ChargeItemPathologies");
        }

        public GenericRepository<IPD> IPDRepository => _IPDRepository;
        public GenericRepository<IPDInitialAssessmentForNewborns> IPDInitialAssessmentForNewborns => _IPDInitialAssessmentForNewborns;
        public GenericRepository<FormDatas> FormDatasRepository => _FormDatasRepository;
        public GenericRepository<Customer> CustomerRepository => _CustomerRepository;
        public GenericRepository<TableDataDto> TableDataDtoRepository => _TableDataDtoRepository;
        public GenericRepository<OrderDto> OrderDtoRepository => _OrderDtoRepository;
        public GenericRepository<ChargeItemDto> ChargeItemDtoRepository => _ChargeItemDtoRepository;
        public GenericRepository<CustomerDto> CustomerDtoRepository => _CustomerDtoRepository;
        public GenericRepository<UserDto> UserDtoRepository => _UserDtoRepository;
        public GenericRepository<ChargeVisitDto> ChargeVisitDtoRepository => _ChargeVisitDtoRepository;
        public GenericRepository<ChargeDto> ChargeDtoRepository => _ChargeDtoRepository;
        public GenericRepository<ChargeItemMicrobiologyDto> ChargeItemMicrobiologyDtoRepository => _ChargeItemMicrobiologyDtoRepository;
        public GenericRepository<ChargeItemPathologyDto> ChargeItemPathologyDtoRepository => _ChargeItemPathologyDtoRepository;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
