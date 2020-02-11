/* Options:
Date: 2020-02-06 15:35:24
Version: 5.70
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: https://local-management.logon-dev.com

//GlobalNamespace: 
//MakePartial: True
//MakeVirtual: True
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//ExportValueTypes: False
//IncludeTypes: 
//ExcludeTypes: 
//AddNamespaces: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using LogonLabs.Shared.Model;

namespace fribs.netcore.api.mstests.sample.Model
{
    [Route("/apps/{app_id}/users", "POST")]
    [DataContract]
    public partial class AddAppUser
        : IReturn<AddAppUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string role { get; set; }
    }

    public partial class AddAppUserResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
        [DataMember]
        public virtual string user_id { get; set; }
    }

    public partial class AddressInfo
    {
        [DataMember]
        public virtual string address_line_1 { get; set; }

        [DataMember]
        public virtual string address_line_2 { get; set; }

        [DataMember]
        public virtual string city { get; set; }

        [DataMember]
        public virtual string state { get; set; }

        [DataMember]
        public virtual string country { get; set; }

        [DataMember]
        public virtual string zip { get; set; }
    }

    [DataContract]
    public partial class App
    {
        public App()
        {
            cors_whitelist = new List<string>{};
            callback_url_whitelist = new List<string>{};
            destination_url_whitelist = new List<string>{};
        }

        [DataMember]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual string name { get; set; }

        [DataMember]
        public virtual string callback_url { get; set; }

        [DataMember]
        public virtual string payment_id { get; set; }

        [DataMember]
        public virtual string created_date { get; set; }

        [DataMember]
        public virtual List<string> cors_whitelist { get; set; }

        [DataMember]
        public virtual List<string> callback_url_whitelist { get; set; }

        [DataMember]
        public virtual List<string> destination_url_whitelist { get; set; }

        [DataMember]
        public virtual string plan { get; set; }

        [DataMember]
        public virtual bool event_logs_enabled { get; set; }

        [DataMember]
        public virtual bool? ip_restriction_enabled { get; set; }

        [DataMember]
        public virtual string ip_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? region_restriction_enabled { get; set; }

        [DataMember]
        public virtual string region_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? time_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_restriction_mode { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }
    }

    public partial class AppProvider
        : Provider
    {
        [DataMember]
        public virtual bool enabled { get; set; }
    }

    [DataContract]
    public partial class AppSecret
    {
        [DataMember]
        public virtual string secret_id { get; set; }

        [DataMember]
        public virtual string secret { get; set; }

        [DataMember]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class AppUser
    {
        [DataMember]
        public virtual string user_id { get; set; }

        [DataMember]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string role { get; set; }
    }

    [Route("/providers/{identity_provider_id}/apps/{app_id}", "POST")]
    [DataContract]
    public partial class AssignProvider
        : IReturn<AssignProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    public partial class AssignProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/{user_id}/secrets/{secret_id}/apps/{app_id}", "POST")]
    [DataContract]
    public partial class AssignUserSecret
        : IReturn<AssignUserSecretResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string secret_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class AssignUserSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/session", "POST")]
    [DataContract]
    public partial class AuthenticateUser
        : IReturn<AuthenticateUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string password { get; set; }
    }

    [DataContract]
    public partial class AuthenticateUserResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string session_token { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/session/token", "POST")]
    [DataContract]
    public partial class AuthenticateWithOneTimeToken
        : IReturn<AuthenticateWithOneTimeTokenResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string one_time_token { get; set; }
    }

    [DataContract]
    public partial class AuthenticateWithOneTimeTokenResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string session_token { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/pusher/{app_id}", "POST")]
    [DataContract]
    public partial class AuthenticateWithPusherChannel
    {
        [DataMember]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual string channel_name { get; set; }

        [DataMember]
        public virtual string socket_id { get; set; }
    }

    [Route("/authcallback", "GET")]
    [Route("/authcallback", "POST")]
    [DataContract]
    public partial class AuthorizationCallback
    {
        [DataMember]
        public virtual string token { get; set; }
    }

    [Route("/users/resetpassword", "PUT")]
    [DataContract]
    public partial class CompleteResetPassword
        : IReturn<CompleteResetPasswordResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string token { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string password { get; set; }
    }

    [DataContract]
    public partial class CompleteResetPasswordResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/confirm", "PUT")]
    [DataContract]
    public partial class ConfirmUser
        : IReturn<ConfirmUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string pin { get; set; }
    }

    [DataContract]
    public partial class ConfirmUserResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps", "POST")]
    [DataContract]
    public partial class CreateApp
        : IReturn<CreateAppResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string gateway_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string name { get; set; }
    }

    [DataContract]
    public partial class CreateAppResponse
        : IBaseResponse
    {
        [DataMember(IsRequired=true)]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/secrets", "POST")]
    [DataContract]
    public partial class CreateAppSecret
        : IReturn<CreateAppSecretResponse>
    {
        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class CreateAppSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string secret_id { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/session/delegate/gateway", "POST")]
    [DataContract]
    public partial class CreateDelegatedGatewaySession
        : IReturn<CreateDelegatedGatewaySessionResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class CreateDelegatedGatewaySessionResponse
        : IBaseResponse
    {
        public CreateDelegatedGatewaySessionResponse()
        {
            delegated_sessions = new List<DelegatedGatewaySession>{};
        }

        [DataMember]
        public virtual List<DelegatedGatewaySession> delegated_sessions { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/session/delegate", "POST")]
    [DataContract]
    public partial class CreateDelegatedSession
        : IReturn<CreateDelegatedSessionResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string asset_type { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string asset_id { get; set; }
    }

    [DataContract]
    public partial class CreateDelegatedSessionResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string one_time_token { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/domains", "POST")]
    [DataContract]
    public partial class CreateDomain
        : IReturn<CreateDomainResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string domain { get; set; }
    }

    public partial class CreateDomainResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string domain_id { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/payments", "POST")]
    [DataContract]
    public partial class CreatePaymentInfo
        : IReturn<CreatePaymentInfoResponse>
    {
        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string token { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string coupon { get; set; }
    }

    [DataContract]
    public partial class CreatePaymentInfoResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual string payment_id { get; set; }
    }

    [Route("/providers", "POST")]
    [DataContract]
    public partial class CreateProvider
        : IReturn<CreateProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string name { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string description { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string identity_provider { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string type { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string protocol { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string login_url { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string token_url { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string client_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string client_secret { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string service_provider_certificate { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string identity_provider_certificate { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_button_image_uri { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_background_hex_color { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_icon_image_uri { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_text_hex_color { get; set; }
    }

    public partial class CreateProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual string identity_provider_id { get; set; }
    }

    [Route("/apps/{app_id}/restrictions", "POST")]
    [Route("/apps/{app_id}/domains/{domain_id}/restrictions", "POST")]
    [DataContract]
    public partial class CreateRestriction
        : IReturn<CreateRestrictionResponse>
    {
        public CreateRestriction()
        {
            region_country_codes = new List<string>{};
            time_restriction_days = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="path")]
        public virtual string domain_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string type { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string ip_from { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string ip_to { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual List<string> region_country_codes { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual List<string> time_restriction_days { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string time_restriction_from { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string time_restriction_to { get; set; }
    }

    public partial class CreateRestrictionResponse
        : IBaseResponse
    {
        public CreateRestrictionResponse()
        {
            restriction_ids = new List<string>{};
        }

        [DataMember]
        public virtual List<string> restriction_ids { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/{user_id}/secrets", "POST")]
    [DataContract]
    public partial class CreateUserSecret
        : IReturn<CreateUserSecretResponse>
    {
        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string user_id { get; set; }
    }

    [DataContract]
    public partial class CreateUserSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string secret_id { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [DataContract]
    public partial class DelegatedGatewaySession
    {
        [DataMember]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual string session_token { get; set; }

        [DataMember]
        public virtual string gateway_base_url { get; set; }
    }

    [Route("/apps/{app_id}/events", "DELETE")]
    [DataContract]
    public partial class DeleteEvents
        : IReturn<DeleteEventsResponse>
    {
        [DataMember]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual string email_address { get; set; }
    }

    public partial class DeleteEventsResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/providers/{identity_provider_id}", "DELETE")]
    [Route("/apps/{app_id}/domains/{domain_id}/providers/{identity_provider_id}", "DELETE")]
    [DataContract]
    public partial class DisableProvider
        : IReturn<DisableProviderResponse>
    {
        [DataMember]
        [ApiMember(ParameterType="path")]
        public virtual string identity_provider_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(ParameterType="body")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string domain_id { get; set; }
    }

    [DataContract]
    public partial class DisableProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    public partial class Domain
    {
        [DataMember]
        public virtual string domain_id { get; set; }

        [DataMember]
        public virtual string domain { get; set; }

        [DataMember]
        public virtual bool? ip_restriction_enabled { get; set; }

        [DataMember]
        public virtual string ip_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? region_restriction_enabled { get; set; }

        [DataMember]
        public virtual string region_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? time_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_restriction_mode { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }
    }

    [Route("/apps/{app_id}/providers/{identity_provider_id}", "POST")]
    [Route("/apps/{app_id}/domains/{domain_id}/providers/{identity_provider_id}", "POST")]
    [DataContract]
    public partial class EnableProvider
        : IReturn<EnableProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="path")]
        public virtual string domain_id { get; set; }
    }

    [DataContract]
    public partial class EnableProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}", "GET")]
    [DataContract]
    public partial class GetApp
        : IReturn<GetAppResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    [Route("/apps/{app_id}/providers", "GET")]
    [Route("/apps/{app_id}/domains/{domain_id}/providers", "GET")]
    [DataContract]
    public partial class GetAppProviders
        : IReturn<GetAppProvidersResponse>, IPagedRequest
    {
        [DataMember]
        public virtual string app_id { get; set; }

        [DataMember]
        public virtual string domain_id { get; set; }

        [DataMember]
        public virtual int? page { get; set; }

        [DataMember]
        public virtual int? page_size { get; set; }
    }

    [DataContract]
    public partial class GetAppProvidersResponse
        : IBaseResponse
    {
        public GetAppProvidersResponse()
        {
            results = new List<AppProvider>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<AppProvider> results { get; set; }
    }

    [DataContract]
    public partial class GetAppResponse
        : App, IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps", "GET")]
    [DataContract]
    public partial class GetApps
        : IReturn<GetAppsResponse>, IPagedRequest
    {
        [DataMember]
        public virtual int? page { get; set; }

        [DataMember]
        public virtual int? page_size { get; set; }
    }

    [Route("/apps/{app_id}/secrets", "GET")]
    [DataContract]
    public partial class GetAppSecrets
        : IReturn<GetAppSecretsResponse>, IPagedRequest
    {
        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page_size { get; set; }
    }

    public partial class GetAppSecretsResponse
        : IBaseResponse
    {
        public GetAppSecretsResponse()
        {
            results = new List<AppSecret>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<AppSecret> results { get; set; }
    }

    [DataContract]
    public partial class GetAppsResponse
        : IBaseResponse
    {
        public GetAppsResponse()
        {
            results = new List<App>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<App> results { get; set; }
    }

    [Route("/apps/{app_id}/users", "GET")]
    [DataContract]
    public partial class GetAppUsers
        : IReturn<GetAppUsersResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        ///<summary>
        ///Defaults to 1
        ///</summary>
        [DataMember]
        [ApiMember(Description="Defaults to 1", ParameterType="query")]
        public virtual int? page { get; set; }

        ///<summary>
        ///Defaults to 25
        ///</summary>
        [DataMember]
        [ApiMember(Description="Defaults to 25", ParameterType="query")]
        public virtual int? page_size { get; set; }
    }

    [DataContract]
    public partial class GetAppUsersResponse
        : IBaseResponse
    {
        public GetAppUsersResponse()
        {
            results = new List<AppUser>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<AppUser> results { get; set; }
    }

    [Route("/apps/{app_id}/domains/{domain_id}", "GET")]
    [DataContract]
    public partial class GetDomain
        : IReturn<GetDomainResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string domain_id { get; set; }
    }

    [DataContract]
    public partial class GetDomainResponse
        : Domain, IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/domains", "GET")]
    [DataContract]
    public partial class GetDomains
        : IReturn<GetDomainsResponse>, IPagedRequest
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page_size { get; set; }
    }

    [DataContract]
    public partial class GetDomainsResponse
        : IBaseResponse
    {
        public GetDomainsResponse()
        {
            results = new List<Domain>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<Domain> results { get; set; }
    }

    [Route("/payments/{payment_id}", "GET")]
    [DataContract]
    public partial class GetPaymentInfo
        : IReturn<GetPaymentInfoResponse>
    {
        [DataMember]
        public virtual string payment_id { get; set; }
    }

    public partial class GetPaymentInfoResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual int expiry_month { get; set; }

        [DataMember]
        public virtual int expiry_year { get; set; }

        [DataMember]
        public virtual string payment_method { get; set; }

        [DataMember]
        public virtual string last_four_digits { get; set; }

        [DataMember]
        public virtual AddressInfo address_info { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/profile", "GET")]
    [DataContract]
    public partial class GetProfile
        : IReturn<GetProfileResponse>
    {
    }

    [DataContract]
    public partial class GetProfileResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string user_id { get; set; }

        [DataMember]
        public virtual string email_address { get; set; }

        [DataMember]
        public virtual string first_name { get; set; }

        [DataMember]
        public virtual string last_name { get; set; }

        [DataMember]
        public virtual string company_name { get; set; }

        [DataMember]
        public virtual string payment_id { get; set; }

        [DataMember]
        public virtual string referral_code { get; set; }

        [DataMember]
        public virtual string referred_by_code { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/providers/{identity_provider_id}/details", "GET")]
    [DataContract]
    public partial class GetProviderDetails
        : IReturn<GetProviderDetailsResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }
    }

    public partial class GetProviderDetailsResponse
        : Provider, IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual string login_url { get; set; }

        [DataMember]
        public virtual string token_url { get; set; }

        [DataMember]
        public virtual string scopes { get; set; }

        [DataMember]
        public virtual string client_id { get; set; }

        [DataMember]
        public virtual string client_secret { get; set; }

        [DataMember]
        public virtual string service_provider_certificate { get; set; }

        [DataMember]
        public virtual string identity_provider_certificate { get; set; }
    }

    [Route("/providers/", "GET")]
    [DataContract]
    public partial class GetProviders
        : IReturn<GetProvidersResponse>, IPagedRequest
    {
        [DataMember]
        public virtual int? page { get; set; }

        [DataMember]
        public virtual int? page_size { get; set; }
    }

    public partial class GetProvidersResponse
        : IBaseResponse
    {
        public GetProvidersResponse()
        {
            results = new List<Provider>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<Provider> results { get; set; }
    }

    [Route("/apps/{app_id}/restrictions", "GET")]
    [Route("/apps/{app_id}/domains/{domain_id}/restrictions", "GET")]
    [DataContract]
    public partial class GetRestrictions
        : IReturn<GetRestrictionsResponse>, IPagedRequest
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="path")]
        public virtual string domain_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page_size { get; set; }
    }

    [DataContract]
    public partial class GetRestrictionsResponse
        : IBaseResponse
    {
        public GetRestrictionsResponse()
        {
            results = new List<Restriction>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<Restriction> results { get; set; }
    }

    [Route("/users/{user_id}/secrets/{secret_id}/apps", "GET")]
    [DataContract]
    public partial class GetUserSecretApps
        : IReturn<GetUserSecretAppsResponse>, IPagedRequest
    {
        [DataMember]
        public virtual int? page { get; set; }

        [DataMember]
        public virtual int? page_size { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string secret_id { get; set; }
    }

    [DataContract]
    public partial class GetUserSecretAppsResponse
        : IBaseResponse
    {
        public GetUserSecretAppsResponse()
        {
            results = new List<App>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<App> results { get; set; }
    }

    [Route("/users/{user_id}/secrets", "GET")]
    [DataContract]
    public partial class GetUserSecrets
        : IReturn<GetUserSecretsResponse>, IPagedRequest
    {
        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual string user_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page { get; set; }

        [DataMember]
        [ApiMember(ParameterType="query")]
        public virtual int? page_size { get; set; }
    }

    public partial class GetUserSecretsResponse
        : IBaseResponse
    {
        public GetUserSecretsResponse()
        {
            results = new List<UserSecret>{};
        }

        [DataMember]
        public virtual IErrorResponse error { get; set; }

        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<UserSecret> results { get; set; }
    }

    [Route("/session", "DELETE")]
    [DataContract]
    public partial class Logout
        : IReturn<LogoutResponse>
    {
    }

    [DataContract]
    public partial class LogoutResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/ping", "GET")]
    [DataContract]
    public partial class Ping
        : IReturn<PingResponse>
    {
    }

    public partial class PingResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual string version { get; set; }

        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [DataContract]
    public partial class Provider
    {
        [DataMember]
        public virtual string name { get; set; }

        [DataMember]
        public virtual string description { get; set; }

        [DataMember]
        public virtual string provider_id { get; set; }

        [DataMember]
        public virtual string identity_provider { get; set; }

        [DataMember]
        public virtual string protocol { get; set; }

        [DataMember]
        public virtual string type { get; set; }

        [DataMember]
        public virtual string login_button_image_uri { get; set; }

        [DataMember]
        public virtual string login_background_hex_color { get; set; }

        [DataMember]
        public virtual string login_icon_image_uri { get; set; }

        [DataMember]
        public virtual string login_text_hex_color { get; set; }

        [DataMember]
        public virtual bool sandbox { get; set; }
    }

    [Route("/users/register", "POST")]
    [DataContract]
    public partial class RegisterUser
        : IReturn<RegisterUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string password { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string first_name { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string last_name { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string company_name { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string referred_by_code { get; set; }
    }

    [DataContract]
    public partial class RegisterUserResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}", "DELETE")]
    [DataContract]
    public partial class RemoveApp
        : IReturn<RemoveAppResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    public partial class RemoveAppResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/secrets/{secret_id}", "DELETE")]
    [DataContract]
    public partial class RemoveAppSecret
        : IReturn<RemoveAppSecretResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string secret_id { get; set; }
    }

    [DataContract]
    public partial class RemoveAppSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/users/{user_id}", "DELETE")]
    [DataContract]
    public partial class RemoveAppUser
        : IReturn<RemoveAppUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }
    }

    public partial class RemoveAppUserResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/domains/{domain_id}", "DELETE")]
    [DataContract]
    public partial class RemoveDomain
        : IReturn<RemoveDomainResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string domain_id { get; set; }
    }

    public partial class RemoveDomainResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/payments/{payment_id}", "DELETE")]
    [DataContract]
    public partial class RemovePaymentInfo
        : IReturn<RemovePaymentInfoResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string payment_id { get; set; }
    }

    [DataContract]
    public partial class RemovePaymentInfoResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/providers/{identity_provider_id}", "DELETE")]
    [DataContract]
    public partial class RemoveProvider
        : IReturn<RemoveProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }
    }

    public partial class RemoveProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/restrictions/{restriction_id}", "DELETE")]
    [DataContract]
    public partial class RemoveRestriction
        : IReturn<RemoveRestrictionResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string restriction_id { get; set; }
    }

    [DataContract]
    public partial class RemoveRestrictionResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/restrictions", "DELETE")]
    [DataContract]
    public partial class RemoveRestrictions
        : IReturn<RemoveRestrictionsResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class RemoveRestrictionsResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/{user_id}/secrets/{secret_id}", "DELETE")]
    [DataContract]
    public partial class RemoveUserSecret
        : IReturn<RemoveUserSecretResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string secret_id { get; set; }
    }

    [DataContract]
    public partial class RemoveUserSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/register", "PUT")]
    [DataContract]
    public partial class ResendConfirmationEmail
        : IReturn<ResendConfirmationEmailResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }
    }

    public partial class ResendConfirmationEmailResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/resetpassword", "POST")]
    [DataContract]
    public partial class ResetPassword
        : IReturn<ResetPasswordResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string email_address { get; set; }
    }

    [DataContract]
    public partial class ResetPasswordResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [DataContract]
    public partial class Restriction
    {
        public Restriction()
        {
            region_country_codes = new List<string>{};
        }

        [DataMember]
        public virtual string restriction_id { get; set; }

        [DataMember]
        public virtual string domain_id { get; set; }

        [DataMember]
        public virtual string type { get; set; }

        [DataMember]
        public virtual string ip_from { get; set; }

        [DataMember]
        public virtual string ip_to { get; set; }

        [DataMember]
        public virtual List<string> region_country_codes { get; set; }

        [DataMember]
        public virtual string time_restriction_days { get; set; }

        [DataMember]
        public virtual string time_restriction_from { get; set; }

        [DataMember]
        public virtual string time_restriction_to { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }
    }

    [Route("/providers/{identity_provider_id}/apps/{app_id}", "DELETE")]
    [DataContract]
    public partial class UnassignProvider
        : IReturn<UnassignProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    public partial class UnassignProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/users/{user_id}/secrets/{secret_id}/apps/{app_id}", "DELETE")]
    [DataContract]
    public partial class UnassignUserSecret
        : IReturn<UnassignUserSecretResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string secret_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    [DataContract]
    public partial class UnassignUserSecretResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}", "PATCH")]
    [DataContract]
    public partial class UpdateApp
        : IReturn<UpdateAppResponse>
    {
        public UpdateApp()
        {
            cors_whitelist = new List<string>{};
            callback_url_whitelist = new List<string>{};
            destination_url_whitelist = new List<string>{};
        }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string payment_id { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string plan { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string name { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string callback_url { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual List<string> cors_whitelist { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual List<string> callback_url_whitelist { get; set; }

        [DataMember]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual List<string> destination_url_whitelist { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual bool? event_logs_enabled { get; set; }

        [DataMember]
        public virtual bool? ip_restriction_enabled { get; set; }

        [DataMember]
        public virtual string ip_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? region_restriction_enabled { get; set; }

        [DataMember]
        public virtual string region_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? time_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_restriction_mode { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }
    }

    public partial class UpdateAppResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/users/{user_id}", "PATCH")]
    [DataContract]
    public partial class UpdateAppUser
        : IReturn<UpdateAppUserResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string user_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string role { get; set; }
    }

    public partial class UpdateAppUserResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/apps/{app_id}/domains/{domain_id}", "PATCH")]
    [DataContract]
    public partial class UpdateDomain
        : IReturn<UpdateDomainResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string domain_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string domain { get; set; }

        [DataMember]
        public virtual bool? ip_restriction_enabled { get; set; }

        [DataMember]
        public virtual string ip_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? region_restriction_enabled { get; set; }

        [DataMember]
        public virtual string region_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? time_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_restriction_mode { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }
    }

    public partial class UpdateDomainResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/payments/{payment_id}", "PATCH")]
    [DataContract]
    public partial class UpdatePaymentInfo
        : IReturn<UpdatePaymentInfoResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string payment_id { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string token { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string coupon { get; set; }
    }

    public partial class UpdatePaymentInfoResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/profile", "PATCH")]
    [DataContract]
    public partial class UpdateProfile
        : IReturn<UpdateProfileResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string first_name { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string last_name { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string company_name { get; set; }
    }

    [DataContract]
    public partial class UpdateProfileResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [Route("/providers/{identity_provider_id}", "PATCH")]
    [DataContract]
    public partial class UpdateProvider
        : IReturn<UpdateProviderResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string identity_provider_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string name { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string description { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string login_url { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string token_url { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string client_id { get; set; }

        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="body")]
        public virtual string client_secret { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string service_provider_certificate { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string identity_provider_certificate { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_button_image_uri { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_background_hex_color { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_icon_image_uri { get; set; }

        [DataMember]
        [ApiMember(ParameterType="body")]
        public virtual string login_text_hex_color { get; set; }
    }

    public partial class UpdateProviderResponse
        : IBaseResponse
    {
        [DataMember]
        public virtual IErrorResponse error { get; set; }
    }

    [DataContract]
    public partial class UserSecret
    {
        [DataMember]
        public virtual string secret_id { get; set; }

        [DataMember]
        public virtual string secret { get; set; }

        [DataMember]
        public virtual string user_id { get; set; }
    }
}

namespace LogonLabs.Mgmt.API.Internal.Model
{

    public partial class App
    {
        public App()
        {
            callback_url_whitelist = new List<string>{};
            destination_url_whitelist = new List<string>{};
            domains = new List<Domain>{};
            cors_whitelist = new List<string>{};
            hashed_secrets = new List<string>{};
        }

        [DataMember]
        public virtual Guid app_id { get; set; }

        [DataMember]
        public virtual string name { get; set; }

        [DataMember]
        public virtual string callback_url { get; set; }

        [DataMember]
        public virtual List<string> callback_url_whitelist { get; set; }

        [DataMember]
        public virtual List<string> destination_url_whitelist { get; set; }

        [DataMember]
        public virtual DateTime timestamp { get; set; }

        [DataMember]
        public virtual string plan { get; set; }

        [DataMember]
        public virtual Domain default_domain { get; set; }

        [DataMember]
        public virtual List<Domain> domains { get; set; }

        [DataMember]
        public virtual List<string> cors_whitelist { get; set; }

        [DataMember]
        public virtual List<string> hashed_secrets { get; set; }

        [DataMember]
        public virtual bool event_logs_enabled { get; set; }

        [DataMember]
        public virtual int default_provider_throttle_limit { get; set; }
    }

    public partial class Domain
    {
        public Domain()
        {
            restrictions = new List<Restriction>{};
            custom_providers = new List<Provider>{};
            enabled_sandbox_providers = new List<string>{};
        }

        [DataMember]
        public virtual Guid domain_id { get; set; }

        [DataMember]
        public virtual string domain_url { get; set; }

        [DataMember]
        public virtual string ip_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? ip_restriction_enabled { get; set; }

        [DataMember]
        public virtual string region_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? region_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_restriction_mode { get; set; }

        [DataMember]
        public virtual bool? time_restriction_enabled { get; set; }

        [DataMember]
        public virtual string time_zone { get; set; }

        [DataMember]
        public virtual List<Restriction> restrictions { get; set; }

        [DataMember]
        public virtual List<Provider> custom_providers { get; set; }

        [DataMember]
        public virtual List<string> enabled_sandbox_providers { get; set; }
    }

    [Route("/internal/gateways/{gateway_id}", "GET")]
    [DataContract]
    public partial class GetGateway
        : IReturn<GetGatewayResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string gateway_id { get; set; }
    }

    [Route("/internal/apps/{app_id}", "GET")]
    [DataContract]
    public partial class GetGatewayApp
        : IReturn<GetGatewayAppResponse>
    {
        [DataMember(IsRequired=true)]
        [ApiMember(IsRequired=true, ParameterType="path")]
        public virtual string app_id { get; set; }
    }

    public partial class GetGatewayAppResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
        [DataMember]
        public virtual App App { get; set; }
    }

    public partial class GetGatewayResponse
        : IBaseResponse
    {
        public GetGatewayResponse()
        {
            configuration = new Dictionary<string, string>{};
        }

        public virtual IErrorResponse error { get; set; }
        public virtual Dictionary<string, string> configuration { get; set; }
    }

    [Route("/internal/sandbox_providers", "GET")]
    [DataContract]
    public partial class GetGatewaySandboxProviders
        : IReturn<GetGatewaySandboxProvidersResponse>, IPagedRequest
    {
        [DataMember]
        public virtual int? page { get; set; }

        [DataMember]
        public virtual int? page_size { get; set; }
    }

    public partial class GetGatewaySandboxProvidersResponse
        : IBaseResponse
    {
        public GetGatewaySandboxProvidersResponse()
        {
            results = new List<Provider>{};
        }

        public virtual IErrorResponse error { get; set; }
        [DataMember]
        public virtual int page_size { get; set; }

        [DataMember]
        public virtual int total_pages { get; set; }

        [DataMember]
        public virtual int total_items { get; set; }

        [DataMember]
        public virtual int current_page { get; set; }

        [DataMember]
        public virtual List<Provider> results { get; set; }
    }

    [Route("/internal/ip/{ip}", "GET")]
    [DataContract]
    public partial class IPLookup
        : IReturn<IPLookupResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string ip { get; set; }
    }

    public partial class IPLookupResponse
        : IBaseResponse
    {
        [DataMember(IsRequired=true)]
        public virtual IErrorResponse error { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ip { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string state_prov_code { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string country_code { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string country_name { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string continent_code { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string continent_name { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string latitude { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string longitude { get; set; }
    }

    public partial class Provider
    {
        [DataMember]
        public virtual Guid provider_id { get; set; }

        [DataMember]
        public virtual string name { get; set; }

        [DataMember]
        public virtual string description { get; set; }

        [DataMember]
        public virtual string identity_provider { get; set; }

        [DataMember]
        public virtual string protocol { get; set; }

        [DataMember]
        public virtual bool sandbox { get; set; }

        [DataMember]
        public virtual string type { get; set; }

        [DataMember]
        public virtual string client_id { get; set; }

        [DataMember]
        public virtual string client_secret { get; set; }

        [DataMember]
        public virtual string service_provider_certificate { get; set; }

        [DataMember]
        public virtual string identity_provider_certificate { get; set; }

        [DataMember]
        public virtual string request_token_url { get; set; }

        [DataMember]
        public virtual string login_url { get; set; }

        [DataMember]
        public virtual string logout_url { get; set; }

        [DataMember]
        public virtual string token_url { get; set; }

        [DataMember]
        public virtual string data_url { get; set; }

        [DataMember]
        public virtual string email_url { get; set; }

        [DataMember]
        public virtual string scopes { get; set; }

        [DataMember]
        public virtual bool include_auth_data_in_response { get; set; }

        [DataMember]
        public virtual string parameters { get; set; }

        [DataMember]
        public virtual string login_button_image_uri { get; set; }

        [DataMember]
        public virtual string login_background_hex_color { get; set; }

        [DataMember]
        public virtual string login_icon_image_uri { get; set; }

        [DataMember]
        public virtual string login_text_hex_color { get; set; }
    }

    public partial class Restriction
    {
        public Restriction()
        {
            region_restriction_country_codes = new List<string>{};
        }

        [DataMember]
        public virtual Guid? restriction_id { get; set; }

        [DataMember]
        public virtual string restriction_type { get; set; }

        [DataMember]
        public virtual string ip_restriction_from { get; set; }

        [DataMember]
        public virtual string ip_restriction_to { get; set; }

        [DataMember]
        public virtual List<string> region_restriction_country_codes { get; set; }

        [DataMember]
        public virtual string region_restriction_continent_code { get; set; }

        [DataMember]
        public virtual string time_restriction_days { get; set; }

        [DataMember]
        public virtual TimeSpan? time_testriction_time_from { get; set; }

        [DataMember]
        public virtual TimeSpan? time_restriction_time_to { get; set; }
    }

    [Route("/internal/email", "POST")]
    [DataContract]
    public partial class SendEmail
        : IReturn<SendEmailResponse>
    {
        public SendEmail()
        {
            to = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual string app_id { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> to { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string subject { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string body { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool is_body_html { get; set; }
    }

    public partial class SendEmailResponse
        : IBaseResponse
    {
        public virtual IErrorResponse error { get; set; }
    }
}

namespace LogonLabs.Shared.Model
{

    public partial interface IBaseResponse
    {
        [DataMember]
        IErrorResponse error { get; set; }
    }

    public partial interface IErrorResponse
    {
        string code { get; set; }
        string message { get; set; }
    }

    public partial interface IPagedRequest
    {
        int? page { get; set; }
        int? page_size { get; set; }
    }
}
