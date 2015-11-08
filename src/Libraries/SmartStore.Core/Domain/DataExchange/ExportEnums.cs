﻿
using System;
namespace SmartStore.Core.Domain.DataExchange
{
	/// <summary>
	/// Supported entity types
	/// </summary>
	public enum ExportEntityType
	{
		Product = 0,
		Category,
		Manufacturer,
		Customer,
		Order,
		NewsLetterSubscription
	}

	/// <summary>
	/// Supported deployment types
	/// </summary>
	public enum ExportDeploymentType
	{
		FileSystem = 0,
		Email,
		Http,
		Ftp
	}

	/// <summary>
	/// Supported HTTP transmission types
	/// </summary>
	public enum ExportHttpTransmissionType
	{
		SimplePost = 0,
		MultipartFormDataPost
	}

	/// <summary>
	/// Controls the merging of various data as product description
	/// </summary>
	public enum ExportDescriptionMerging
	{
		None = 0,
		ShortDescriptionOrNameIfEmpty,
		ShortDescription,
		Description,
		NameAndShortDescription,
		NameAndDescription,
		ManufacturerAndNameAndShortDescription,
		ManufacturerAndNameAndDescription
	}

	/// <summary>
	/// Controls the merging of various data while exporting attribute combinations as products
	/// </summary>
	public enum ExportAttributeValueMerging
	{
		None = 0,
		AppendAllValuesToName
	}

	/// <summary>
	/// Controls data processing and projection items supported by an export provider
	/// </summary>
	[Flags]
	public enum ExportFeature
	{
		None = 0,

		/// <summary>
		/// Whether to automatically create a file based public deployment when an export profile is created
		/// </summary>
		CreatesInitialPublicDeployment = 1,

		/// <summary>
		/// Whether to offer option to include\exclude grouped products
		/// </summary>
		CanOmitGroupedProducts = 1 << 2,

		/// <summary>
		/// Whether to offer option to export attribute combinations as products
		/// </summary>
		CanProjectAttributeCombinations = 1 << 3,

		/// <summary>
		/// Whether to offer further options to manipulate the product description
		/// </summary>
		CanProjectDescription = 1 << 4,

		/// <summary>
		/// Whether to offer option to enter a brand fallback
		/// </summary>
		OffersBrandFallback = 1 << 5,

		/// <summary>
		/// Whether to offer option to set a picture size and to get the URL of the main image
		/// </summary>
		CanIncludeMainPicture = 1 << 6,

		/// <summary>
		/// Whether to use SKU as manufacturer part number if MPN is empty
		/// </summary>
		UsesSkuAsMpnFallback = 1 << 7,

		/// <summary>
		/// Whether to offer option to enter a shipping time fallback
		/// </summary>
		OffersShippingTimeFallback = 1 << 8,

		/// <summary>
		/// Whether to offer option to enter a shipping costs fallback and a free shipping threshold
		/// </summary>
		OffersShippingCostsFallback = 1 << 9,

		/// <summary>
		/// Whether to get the calculated old product price
		/// </summary>
		UsesOldPrice = 1 << 10,

		/// <summary>
		/// Whether to get the calculated special and regular price (ignoring special offers)
		/// </summary>
		UsesSpecialPrice = 1 << 11
	}

	/// <summary>
	/// Possible order status change after order exporting
	/// </summary>
	public enum ExportOrderStatusChange
	{
		None = 0,
		Processing,
		Complete
	}

	/// <summary>
	/// Export abortion types
	/// </summary>
	public enum ExportAbortion
	{
		/// <summary>
		/// No abortion. Go on with processing.
		/// </summary>
		None = 0,

		/// <summary>
		/// Break item processing but not the rest of the execution. Typically used for demo limitations.
		/// </summary>
		Soft,

		/// <summary>
		/// Break processing immediately
		/// </summary>
		Hard
	}
}
