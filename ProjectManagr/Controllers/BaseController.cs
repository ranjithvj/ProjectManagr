using AutoMapper;
using DataTables.Mvc;
using Models;
using Models.DTO;
using ProjectManagr.ViewModels;
using System;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    public class BaseController : Controller
    {
        //AutoMapper to map VM to DO and DO to VM
        protected IMapper IMapper
        {
            get
            {
                var mapperConfig = new MapperConfiguration(config =>
                {
                    config.CreateMap<Screen, ScreenVM>();
                    config.CreateMap<ScreenVM, Screen>();
                    config.CreateMap<ProjectSiteVM, ProjectSite>();
                    config.CreateMap<ProjectSite, ProjectSiteVM>();
                });

                return mapperConfig.CreateMapper();
            }
        }

        protected FilterRequestDTO CreateServiceRequest(IDataTablesRequest requestModel)
        {
            FilterRequestDTO serviceRequest = new FilterRequestDTO();
            serviceRequest.SearchText = requestModel.Search.Value.Trim();
            serviceRequest.RecordCountStart = requestModel.Start;
            serviceRequest.RecordCountLength = requestModel.Length;

            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }
            serviceRequest.OrderByString = orderByString;
            return serviceRequest;
        }
    }
}