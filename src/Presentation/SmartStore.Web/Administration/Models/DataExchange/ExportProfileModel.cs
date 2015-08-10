﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using SmartStore.Admin.Validators.DataExchange;
using SmartStore.Core.Domain.DataExchange;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Mvc;

namespace SmartStore.Admin.Models.DataExchange
{
	[Validator(typeof(ExportProfileValidator))]
	public partial class ExportProfileModel : EntityModelBase
	{
		public int StoreCount { get; set; }
		public string AllString { get; set; }
		public string UnspecifiedString { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.Name")]
		public string Name { get; set; }

		[SmartResourceDisplayName("Common.Enabled")]
		public bool Enabled { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.SchedulingHours")]
		public int SchedulingHours { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.LastExecution")]
		[AllowHtml]
		public string LastExecution { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.Offset")]
		public int Offset { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.Limit")]
		public int Limit { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.BatchSize")]
		public int BatchSize { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.PerStore")]
		public bool PerStore { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.CompletedEmailAddresses")]
		public string CompletedEmailAddresses { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.CreateZipArchive")]
		public bool CreateZipArchive { get; set; }

		[SmartResourceDisplayName("Admin.Configuration.Export.Cleanup")]
		public bool Cleanup { get; set; }

		public ProviderModel Provider { get; set; }

		public ExportProductFilterModel ProductFilter { get; set; }
		public ExportOrderFilterModel OrderFilter { get; set; }

		public ExportProjectionModel Projection { get; set; }

		public List<ExportDeploymentModel> Deployments { get; set; }

		public class ProviderModel
		{
			public string ConfigPartialViewName { get; set; }
			public Type ConfigDataType { get; set; }
			public object ConfigData { get; set; }

			[SmartResourceDisplayName("Common.Image")]
			public string ThumbnailUrl { get; set; }

			[SmartResourceDisplayName("Common.Website")]
			public string Url { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Plugins.Fields.Configure")]
			public string ConfigurationUrl { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Export.ProviderSystemName")]
			public string SystemName { get; set; }
			public List<SelectListItem> AvailableExportProviders { get; set; }

			[SmartResourceDisplayName("Common.Provider")]
			public string FriendlyName { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Plugins.Fields.Author")]
			public string Author { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Plugins.Fields.Version")]
			public string Version { get; set; }

			[SmartResourceDisplayName("Common.Description")]
			[AllowHtml]
			public string Description { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Export.EntityType")]
			public ExportEntityType EntityType { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Export.EntityType")]
			public string EntityTypeName { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Export.FileType")]
			public string FileType { get; set; }

			[SmartResourceDisplayName("Admin.Configuration.Export.SupportedFileTypes")]
			public string SupportedFileTypes { get; set; }
		}
	}
}