﻿using System.Collections.Generic;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Mvc;

namespace SmartStore.Admin.Models.Plugins
{
	public class LicensePluginModel : ModelBase
	{
		public string SystemName { get; set; }

		public List<LicenseModel> Licenses { get; set; }

		public class LicenseModel
		{
			[SmartResourceDisplayName("Admin.Configuration.Plugins.LicenseKey")]
			public string LicenseKey { get; set; }

			public string OldLicenseKey { get; set; }
			public int StoreId { get; set; }
			public string StoreName { get; set; }
			public string StoreUrl { get; set; }
		}
	}
}