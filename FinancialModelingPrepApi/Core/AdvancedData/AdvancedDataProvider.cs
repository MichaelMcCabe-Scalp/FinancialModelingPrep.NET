﻿using MatthiWare.FinancialModelingPrep.Abstractions.AdvancedData;
using MatthiWare.FinancialModelingPrep.Core.Http;
using MatthiWare.FinancialModelingPrep.Model;
using MatthiWare.FinancialModelingPrep.Model.AdvancedData;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MatthiWare.FinancialModelingPrep.Core.AdvancedData
{
    public class AdvancedDataProvider : IAdvancedDataProvider
    {
        private readonly FinancialModelingPrepHttpClient client;

        public AdvancedDataProvider(FinancialModelingPrepHttpClient client)
        {
            this.client = client ?? throw new System.ArgumentNullException(nameof(client));
        }

        public Task<ApiResponse<string>> GetAnnualReportsForm10KJsonAsync(string symbol, int year)
            => GetFinancialReportsJsonAsync(symbol, year, Period.Annual, null);

        public Task<ApiResponse<string>> GetQuarterlyReportsForm10QJsonAsync(string symbol, int year, Quarter quarter)
            => GetFinancialReportsJsonAsync(symbol, year, Period.Quarter, quarter);

        private Task<ApiResponse<string>> GetFinancialReportsJsonAsync(string symbol, int year, Period period, Quarter? quarter)
        {
            const string url = "[version]/financial-reports-json";

            var pathParams = new NameValueCollection()
            {
                { "version", ApiVersion.v4.ToString() }
            };

            var queryString = new QueryStringBuilder();
            queryString.Add("symbol", symbol);
            queryString.Add("year", year);

            if (period == Period.Quarter && quarter != null)
            {
                queryString.Add("period", quarter.Value.ToString());
            }
            else
            {
                queryString.Add("period", "FY");
            }

            return client.GetStringAsync(url, pathParams, queryString);
        }

        public Task<ApiResponse<StandardIndustrialClassificationResponse>> GetStandardIndustrialClassificationByCikAsync(string cik)
            => GetStandardIndustrialClassificationInternalAsync("cik", cik);

        public Task<ApiResponse<StandardIndustrialClassificationResponse>> GetStandardIndustrialClassificationBySicCodeAsync(string sic)
            => GetStandardIndustrialClassificationInternalAsync("sicCode", sic);

        public Task<ApiResponse<StandardIndustrialClassificationResponse>> GetStandardIndustrialClassificationBySymbolAsync(string symbol)
            => GetStandardIndustrialClassificationInternalAsync("symbol", symbol);

        private async Task<ApiResponse<StandardIndustrialClassificationResponse>> GetStandardIndustrialClassificationInternalAsync(string queryParamName, string queryParamValue)
        {
            const string url = "[version]/standard_industrial_classification";

            var pathParams = new NameValueCollection()
            {
                { "version", ApiVersion.v4.ToString() }
            };

            var queryString = new QueryStringBuilder();
            queryString.Add(queryParamName, queryParamValue);

            var result = await client.GetJsonAsync<List<StandardIndustrialClassificationResponse>>(url, pathParams, queryString);

            if (result.HasError)
            {
                return ApiResponse.FromError<StandardIndustrialClassificationResponse>(result.Error);
            }

            return ApiResponse.FromSucces(result.Data.First());
        }
    }
}
