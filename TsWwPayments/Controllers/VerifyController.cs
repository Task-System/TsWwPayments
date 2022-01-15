using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using TsWwPayments.Repositories;
using TsWwPaymentsModelApi.Models.Enums;
using ZarinSharp;

namespace TsWwPayments.Controllers
{
    [Route("[controller]")]
    public class VerifyController : Controller
    {
        private readonly IZarinClient _zarinClient;
        private readonly TransmissionRepository transmissions;
        private readonly PaymentsAccountRepository accounts;
        private readonly ITelegramBotClient botClient;
        private readonly ILogger<VerifyController> logger;

        public VerifyController(
            IZarinClient zarinClient,
            TransmissionRepository transmissions,
            PaymentsAccountRepository accounts,
            ITelegramBotClient botClient,
            ILogger<VerifyController> logger)
        {
            _zarinClient = zarinClient;
            this.transmissions = transmissions;
            this.accounts = accounts;
            this.botClient = botClient;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string authority, string status)
        {
            var transmission = await transmissions.GetTransmission(authority);
            if (transmission == null)
            {
                logger.LogInformation("Transmission not found. authority {authority}", authority);
                return Json(new { Error = "Invalid transmission!" });
            }

            if (transmission.Status != TransmissionStatus.Pending)
            {
                logger.LogInformation("Transmission duplicated. authority {authority}", authority);
                return Json(new { Error = "Transmission is duplicated!" });
            }

            try
            {
                var verify = await _zarinClient.VerifyPaymentAsync(transmission.EnsureIRT(), authority);

                var acc = await accounts.GetByIDAsync(transmission.PaymentsAccountId);
                if (acc == null)
                {
                    logger.LogWarning("Transmission done but no account found!. authority {authority}", authority);

                    return Json(new
                    {
                        Error = "Transmission done but no account found!",
                        verify.RefId
                    });
                }

                await transmissions.TransmissionDone(
                    transmission.TransmissionId,
                    TransmissionStatus.Succeeded);
                logger.LogInformation("Transmission done!. authority: {authority} refId: {refId}", authority, verify.RefId);

                _ = await botClient.SendTextMessageAsync(acc.TelegramId,
                    $"Transmission Done!\nReference Id: {verify.RefId}\nCard: {verify.CardPan}");

                return Json(new
                {
                    Error = "Transmission done!",
                    verify.RefId,
                    Link = "https://t.me/tspaybot"
                });
            }
            catch (ZarinpalException e)
            {
                await transmissions.TransmissionDone(
                    transmission.TransmissionId,
                    TransmissionStatus.Timedout);

                logger.LogInformation("Transmission failed!. authority: {authority} code: {code}", authority, e.Code);

                return Json(new {e.Code, e.Description});
            }
        }
    }
}
