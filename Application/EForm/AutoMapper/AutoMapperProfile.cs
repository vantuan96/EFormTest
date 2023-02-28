
using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.DTOs;
using DataAccess.Models.GeneralModel;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TableData, TableDataDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<ChargeItem, ChargeItemDto>();
            CreateMap<CustomerManualUpdateParameterModel, Customer>()
                .ForMember(destination => destination.DateOfBirth, options => options.MapFrom(source => source.ConvertedDateOfBirth))
                .ForMember(destination => destination.Gender, options => options.MapFrom(source => source.ConvertedGender))
                .ForMember(destination => destination.IssueDate, options => options.MapFrom(source => source.ConvertedIssueDate))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Customer, CustomerDto>();
            CreateMap<ChargeItem, ChargeItemDto>().ReverseMap();
            CreateMap<Charge, ChargeDto>().ReverseMap();
            CreateMap<ChargeItemMicrobiology, ChargeItemMicrobiologyDto>().ReverseMap();
            CreateMap<ChargeItemPathology, ChargeItemPathologyDto>().ReverseMap();
            CreateMap<ChargeVisit, ChargeVisitDto>().ReverseMap();
            CreateMap<OrdersResponse.MedicationOrder, OrdersOutput>()
                 .ForMember(destination => destination.PID, options => options.MapFrom(source => source.Patient.Value))
                .ForMember(destination => destination.Medication, options => options.MapFrom(source => source.BrandLocalName))
                .ForMember(destination => destination.Status, options => options.MapFrom(source => source.Status.LocalDescription))
                .ForMember(destination => destination.Dose, options => options.MapFrom(source =>Double.Parse(source.StandardDose.DosageStrengthLow.ToString()) + " " + source.StandardDose.DosageUnitOfMeasure.LocalDescription + " ("+ Double.Parse(source.StandardDose.AdministrationQuantityLow.ToString())+" "+ source.StandardDose.AdministrationUnitOfMeasure.LocalDescription + ")"))
                .ForMember(destination => destination.Frequency, options => options.MapFrom(source => source.Frequency.Frequency.LocalDescription))
                .ForMember(destination => destination.RxQuantity, options => options.MapFrom(source => !String.IsNullOrEmpty(source.RxQuantity.Quantity.ToString()) ? Double.Parse(source.RxQuantity.Quantity.ToString()) + " " + source.RxQuantity.UnitOfMeasurement.LocalDescription : ""))
                .ForMember(destination => destination.Route, options => options.MapFrom(source => source.Route.LocalDescription))
                .ForMember(destination => destination.PRN, options => options.MapFrom(source => source.Prn))
                .ForMember(destination => destination.StartDateTime, options => options.MapFrom(source => source.StartDateTime))
                .ForMember(destination => destination.StopDateTime, options => options.MapFrom(source => source.StopDateTime))
                .ForMember(destination => destination.Duration, options => options.MapFrom(source => (source.Duration.Duration != null ) ? Double.Parse(source.Duration.Duration.ToString()) + " " + source.Duration.DurationUnit.LocalDescription : source.Duration.DurationUnit.LocalDescription))
                .ForMember(destination => destination.Supply, options => options.MapFrom(source => source.SupplyStatus.LocalDescription))
                //.ForMember(destination => destination.Facility, options => options.MapFrom(source => source.StopDateTime))
                //.ForMember(destination => destination.WardOrClinic, options => options.MapFrom(source => source.StopDateTime))
                .ForMember(destination => destination.VisitTypeGroup, options => options.MapFrom(source => source.MedicationProfileType.Value))
                .ForMember(destination => destination.VisitCode, options => options.MapFrom(source => source.Visit.DisplayId))
                //.ForMember(destination => destination.VisitStart, options => options.MapFrom(source => source.StopDateTime))
                //.ForMember(destination => destination.VisitClosure, options => options.MapFrom(source => source.StopDateTime))
                .ForMember(destination => destination.PrescriberName, options => options.MapFrom(source => source.Prescriber.Name.FirstName + " " + source.Prescriber.Name.MiddleName + " "+ source.Prescriber.Name.LastName))
                .ForMember(destination => destination.Rx, options => options.MapFrom(source => source.RxNumber))
                //.ForMember(destination => destination.RxDate, options => options.MapFrom(source => source.Prescriber.Name.FirstName))
                .ForMember(destination => destination.RxItem, options => options.MapFrom(source => source.RxItemNumber));

            //.ForMember(destination => destination.ListMedicationOrdersResponse., options => options.MapFrom(source => source.Body.ListMedicationOrdersResponse.ListMedicationOrdersResult))
            //    CreateMap<ICDJsonModel, ICD10Visit>()
            //        .ForMember(des => des.Code, act => act.MapFrom(src => src.code))
            //        .ForMember(des => des.EnName, act => act.MapFrom(src => src.EnName))
            //        .ForMember(des => des.Name, act => act.MapFrom(src => src.ViName));
        }
    }
}