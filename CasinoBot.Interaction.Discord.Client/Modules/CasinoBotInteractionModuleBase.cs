using CasinoBot.Domain.Interfaces;
using Discord.Interactions;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public abstract class CasinoBotInteractionModuleBase : InteractionModuleBase
    {
        protected readonly ILoggingService _loggingService;

        public CasinoBotInteractionModuleBase(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public override void BeforeExecute(ICommandInfo command)
        {
            // TODO: still analyze any alternate approaches, since we cannot access this scope on the client itself for any error handling
            var userId = Context.User.Id;
            var guildId = Context.Guild?.Id;
            var traceId = Guid.NewGuid();

            _loggingService.SetLoggingInformation(traceId, userId, guildId);
        }

    }
}
