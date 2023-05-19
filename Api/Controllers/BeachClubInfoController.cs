using Microsoft.AspNetCore.Mvc;
using SeaBreeze.Domain.Constants;

namespace Api.Controllers
{
    public class BeachClubInfoController : BaseController
    {

        public BeachClubInfoController()
        {
        }

        [HttpGet]
        public ActionResult<BeachClubInfo> GetBeachClubInfo()
        {

            BeachClubInfo beachClubInfo = new BeachClubInfo()
            {
                TicketPrice = Constants.TicketPrice,
                TicketEnsure = Constants.InsurancePrice,
                RoseBar = Constants.RoseBarPrice,
                PremiumTicketPrice = Constants.PremiumTicketPrice,
                PremiumTicketEnsure = Constants.PremiumInsurancePrice,
                DiscountForNextDay = Constants.DiscountPercentage
            };

            return beachClubInfo;
        }

    }


    public class BeachClubInfo
    {
        public decimal TicketPrice { get; set; }
        public decimal TicketEnsure { get; set; }
        public decimal RoseBar { get; set; }
        public decimal PremiumTicketPrice { get; set; }
        public decimal PremiumTicketEnsure { get; set; }
        public decimal DiscountForNextDay { get; set; }
    }
}
