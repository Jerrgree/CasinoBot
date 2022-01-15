using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public class DrawCardsModule : InteractionModuleBase
    {
        [SlashCommand("echo", "Echo an input")]
        public async Task Echo([Summary(description: "Echo this input")] string input)
        {
            await RespondAsync(input);
        }

        [SlashCommand("choose", "Choose an animal")]
        public async Task ChoiceCommand([Choice("Dog", "dog"), Choice("Cat", "cat"), Choice("Penguin", "penguin")] string animal)
        {
            await RespondAsync(animal);
        }

        [AutocompleteCommand("parameter_name", "autoInput")]
        public async Task Autocomplete()
        {
            IEnumerable<AutocompleteResult> results = new List<AutocompleteResult>()
            {
                new AutocompleteResult("test", "1"),
                new AutocompleteResult("test2", "2"),
                new AutocompleteResult("test3", "3")
            };


            await (Context.Interaction as SocketAutocompleteInteraction).RespondAsync("test");
        }

    }
}
