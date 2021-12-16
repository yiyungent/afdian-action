using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afdian.Action.ViewModels
{
    public class AfdianViewModel
    {
        public Afdian.Sdk.ResponseModels.QueryOrderResponseModel Order { get; set; }

        public Afdian.Sdk.ResponseModels.QuerySponsorResponseModel Sponsor { get; set; }
    }
}
