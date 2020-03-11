﻿using System.Collections.Generic;
using System.Linq;
using SmartStore.Web.Framework.Modelling;
using SmartStore.Services.Catalog;

namespace SmartStore.Admin.Models.Orders
{
    public class OrdersDashboardReportModel : ModelBase
    {
        // public OrdersDashboardReportLineModel[] Reports { get; set; } = new OrdersDashboardReportLineModel[4];

        public OrdersDashboardReportLineModel Day { get; set; } = new OrdersDashboardReportLineModel(24);
        public OrdersDashboardReportLineModel Week { get; set; } = new OrdersDashboardReportLineModel(7);
        public OrdersDashboardReportLineModel Month { get; set; } = new OrdersDashboardReportLineModel(4);
        public OrdersDashboardReportLineModel Year { get; set; } = new OrdersDashboardReportLineModel(12);

        //public OrdersDashboardReportModel()
        //{
        //    Reports[0] = new OrdersDashboardReportLineModel(24);
        //    Reports[1] = new OrdersDashboardReportLineModel(7);
        //    Reports[2] = new OrdersDashboardReportLineModel(4);
        //    Reports[3] = new OrdersDashboardReportLineModel(12);
        //}        
    }
    public class OrdersDashboardReportLineModel : ModelBase
    {
        public int PercentageDelta { get; set; }
        public string TotalAmount { get; set; }
        public string[] Labels { get; set; }

        public ChartDataPoint[] Data { get; set; }

        public OrdersDashboardReportLineModel(int amountDatasets)
        {
            Data = new ChartDataPoint[4];
            Data[0] = new ChartDataPoint(amountDatasets);
            Data[1] = new ChartDataPoint(amountDatasets);
            Data[2] = new ChartDataPoint(amountDatasets);
            Data[3] = new ChartDataPoint(amountDatasets);
            Labels = new string[amountDatasets];
        }
    }
    public class ChartDataPoint
    {
        public string TotalAmount { get; set; }
        public int[] Quantity { get; set; }
        public decimal[] Amount { get; set; }
        public string[] FormattedAmount { get; set; }

        public ChartDataPoint(int amountDatasets)
        {
            Quantity = new int[amountDatasets];
            Amount = new decimal[amountDatasets];
            FormattedAmount = new string[amountDatasets];
        }
    }
}