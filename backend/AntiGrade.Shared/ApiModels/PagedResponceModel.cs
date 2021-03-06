 
using System.Collections.Generic;

namespace AntiGrade.Shared.ApiModels
{
    public class PagedResponseModel<TPayload, TSummary> : ResponseModel<List<TPayload>>
        where TSummary : PayloadSummary
        {
            public PagedResponseModel(List<TPayload> items, TSummary summary) : this(items, summary, new List<Error>())
            { }

            public PagedResponseModel(List<TPayload> items, TSummary summary, List<Error> errors) : base(items, errors)
            {
                Payload = items;
                Summary = summary;
            }

            public PagedResponseModel()
            { }

            public TSummary Summary
            {
                get;
                set;
            }
        }
}