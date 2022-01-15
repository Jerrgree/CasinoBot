using Discord;

namespace CasinoBot.Interaction.Discord.Client.Extensions
{
    internal static class EmbedBuilderExtensions
    {
        /// <summary>
        /// Adds an embeded field with an invisible title
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value">The value of the field.</param>
        /// <param name="inline">Indicates whether the field is in-line or not.</param>
        /// <returns>The current builder.</returns>
        internal static EmbedBuilder AddFieldWithoutTitle(this EmbedBuilder builder, object value, bool inline = false)
        {
            return builder.AddField("\u200b", value, inline);
        }
    }
}
