global using Common.DomainModels;
global using Common.DomainModels.Events;
global using Common.EventBus;
global using Common.EventBus.Abstractions;
global using Dapr;
global using Dapr.Client;
global using HealthChecks.UI.Client;
global using MerchantPayment.API;
global using MerchantPayment.API.Data;
global using MerchantPayment.API.Models.DTO;
global using MerchantPayment.API.Models.Persistance;
global using MerchantPayment.API.Services;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using System.Text.RegularExpressions;
