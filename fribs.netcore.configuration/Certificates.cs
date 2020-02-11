using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace fribs.netcore.configuration
{
    public class Certificates
    {
		// USAGE
		//private string _siteApiWcfCertThumbprint;
		//public string SiteApiWcfCertThumbprint { get { return _siteApiWcfCertThumbprint ?? (_siteApiWcfCertThumbprint = Get<string>("SiteApiWcfCertThumbprint", isRequired: true)); } }

		//private string _siteApiWcfCertStoreLocation;
		//public string SiteApiWcfCertStoreLocation { get { return _siteApiWcfCertStoreLocation ?? (_siteApiWcfCertStoreLocation = Get<string>("SiteApiWcfCertStoreLocation", isRequired: true)); } }

		//private X509Certificate2 _siteApiWcfCert;
		//public X509Certificate2 SiteApiWcfCert { get { return _siteApiWcfCert ?? (_siteApiWcfCert = GetCert(SiteApiWcfCertStoreLocation, SiteApiWcfCertThumbprint)); } }

		private static Dictionary<string, X509Certificate2> _certificates = new Dictionary<string, X509Certificate2>();
		public static X509Certificate2 GetCert(string location, string thumbprint, bool confirmPrivateKey = false)
		{
			if (!_certificates.ContainsKey(thumbprint))
			{
				_certificates.Add(thumbprint, new X509StoreDefn(location).GetCertificate(thumbprint, confirmPrivateKey));
			}

			return _certificates[thumbprint];
		}

		private static List<string> RunX509StoreDiagnostic()
		{
			var result = new List<string>();

			foreach (StoreLocation storeLocation in (StoreLocation[])Enum.GetValues(typeof(StoreLocation)))
			{
				foreach (StoreName storeName in (StoreName[])Enum.GetValues(typeof(StoreName)))
				{
					using (var store = new X509Store(storeName, storeLocation))
					{

						try
						{
							store.Open(OpenFlags.OpenExistingOnly);

							result.Add($"Succeess: {store.Certificates.Count}, {store.Name}, {store.Location}");
						}
						catch (Exception ex)
						{
							result.Add($"Failed: {store.Certificates.Count}, {store.Name}, {store.Location}, {ex.Message}");
						}
					}
				}
			}

			return result;
		}
	}

	public class X509StoreDefn
	{
		public StoreName Name;
		public StoreLocation Location;

		public X509StoreDefn(string defn)
		{
			Name = StoreName.My;
			Location = StoreLocation.CurrentUser;

			var certStoreData = defn.Split(',');

			if (certStoreData.Length == 2)
			{
				if (!Enum.TryParse(certStoreData[0], out Name))
				{
					throw new Exception($"Bad store name {certStoreData[0]} in {defn}");
				}

				if (!Enum.TryParse(certStoreData[1], out Location))
				{
					throw new Exception($"Bad store location {certStoreData[1]} in {defn}");
				}
			}
			else
			{
				throw new Exception($"Bad store definition {defn}");
			}

#if DebugLocal
			Location = StoreLocation.LocalMachine;
#endif
		}

		private X509Certificate2Collection _certificates;
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (_certificates == null)
				{
					_certificates = new X509Certificate2Collection();

					using (var store = new X509Store(Name, Location))
					{
						store.Open(OpenFlags.ReadOnly);

						_certificates = store.Certificates;

						store.Close();
					}
				}

				return _certificates;
			}
		}

		public X509Certificate2 GetCertificate(string thumbprint, bool confirmPrivateKey = false)
		{
			var certificates = Certificates.Find(X509FindType.FindByThumbprint, thumbprint, true);

			if (certificates.Count > 0)
			{
				var cert = certificates[0];

				if (!confirmPrivateKey || (cert.HasPrivateKey /* this just means it exists, not that the current user has access to it*/ && cert.PrivateKey != null /*// this will toss an error if the AppDomains identity does not have read access to the private key*/))
				{
					return cert;
				}
			}

			throw new Exception($"Unable to find certificate or access its private key. Check that the AppDomains current Identity has access");
		}
	}
}
